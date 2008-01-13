/*4:*/
//#line 69 "../gb_io.w"

/*7:*/
//#line 93 "../gb_io.w"

#include <stdio.h>
#include <string.h>

/*:7*/
//#line 70 "../gb_io.w"

#define cant_open_file 0x1 /* bit set in |io_errors| if |fopen| fails */
#define cant_close_file 0x2 /* bit set if |fclose| fails */
#define bad_first_line 0x4 /* bit set if the data file's first line isn't legit */
#define bad_second_line 0x8 /* bit set if the second line doesn't pass muster */
#define bad_third_line 0x10 /* bit set if the third line is awry */
#define bad_fourth_line 0x20 /* guess when this bit is set */
#define file_ended_prematurely 0x40 /* bit set if |fgets| fails */
#define missing_newline 0x80 /* bit set if line is too long or |'\n'| is missing */
#define wrong_number_of_lines 0x100 /* bit set if the line count is wrong */
#define wrong_checksum 0x200 /* bit set if the checksum is wrong */
#define no_file_open 0x400 /* bit set if user tries to close an unopened file */
#define bad_last_line 0x800 /* bit set if final line has incorrect form */

#define unexpected_char 127 \

#define STR_BUF_LENGTH 160 \

#define gb_raw_open gb_r_open \

#define gb_raw_close gb_r_close \


long io_errors; /* record of anomalies noted by {\sc GB\_\,IO} routines */

static char buffer[81]; /* the current line of input */
static char *cur_pos=buffer; /* the current character of interest */
static FILE *cur_file; /* current file, or |NULL| if none is open */

static char icode[256]; /* mapping of characters to internal codes */
static long checksum_prime = (1L << 30) - 83; /* large prime such that $2p+|unexpected_char|$ won't overflow */

static long magic; /* current checksum value */
static long line_no; /* current line number in file */
static long final_magic; /* desired final magic number */
static long tot_lines; /* total number of data lines */
static char more_data; /* is there data still waiting to be read? */

static char *imap =
    "0123456789"
    "ABCDEFGHIJKLMNOPQRSTUVWXYZ"
    "abcdefghijklmnopqrstuvwxyz" "_^~&@,;.:?!%#$+-*/|\\<=>()[]{}`'\" \n";

static char file_name[20]; /* name of the data file, without a prefix */

static void fill_buf(void)
{
    register char *p;

    if (!fgets(buffer, sizeof(buffer), cur_file)) {
        io_errors |= file_ended_prematurely;
        buffer[0] = more_data = 0;
    }
    for (p = buffer; *p; p++); /* advance to first null character */
    if (p-- == buffer || *p != '\n') {
        io_errors |= missing_newline;
        p++;
    }
    while (--p >= buffer && *p == ' '); /* move back over trailing blanks */
    *++p = '\n'; /* newline and null are always present at end of line */
    *++p = 0;
    cur_pos = buffer; /* get ready to read |buffer[0]| */
}

static void icode_setup(void)
{
    register long k;
    register char *p;

    for (k = 0; k < 256; k++)
        icode[k] = unexpected_char;
    for (p = imap, k = 0; *p; p++, k++)
        icode[*p] = k;
}

char imap_chr(long d)
{
    return ((char) (d < 0 || d > strlen(imap) ? '\0' : imap[d]));
}

long imap_ord(char c)
{
/* Make sure that |icode| has been initialized */
    if (!icode['1'])
        icode_setup();
    return (c < 0 || c > 255) ? unexpected_char : icode[c];
}

long new_checksum(char *s, long old_checksum)
{
    register long a = old_checksum;
    register char *p;

    for (p = s; *p; p++)
        a = (a + a + imap_ord(*p)) % checksum_prime;
    return a;
}

       /*:17*//*18: */
//#line 244 "../gb_io.w"

//#line 79 "../PROTOTYPES/gb_io.ch"
void gb_newline(void)
//#line 246 "../gb_io.w"
{
    if (++line_no > tot_lines)
        more_data = 0;
    if (more_data) {
        fill_buf();
        if (buffer[0] != '*')
            magic = new_checksum(buffer, magic);
    }
}

/* has the data all been read? */
long gb_eof(void)
{
    return !more_data;
}

char gb_char(void)
{
    if (*cur_pos)
        return (*cur_pos++);
    return '\n';
}

void gb_backup(void)
{
    if (cur_pos > buffer)
        cur_pos--;
}

long gb_digit(char d)
{
    icode[0] = d; /* make sure |'\0'| is a nondigit */
    if (imap_ord(*cur_pos) < d)
        return icode[*cur_pos++];
    return -1;
}

unsigned long gb_number(char d)
{
    register unsigned long a = 0;

    icode[0] = d;/* make sure |'\0'| is a nondigit */
    while (imap_ord(*cur_pos) < d)
        a = a * d + icode[*cur_pos++];
    return a;
}

char str_buf[STR_BUF_LENGTH];  /* users can put strings here if they wish */

char *gb_string(char *p, char c)
//    char *p; /* where to put the result */
//    char c; /* character following the string */
{
    while (*cur_pos && *cur_pos != c)
        *p++ = *cur_pos++;
    *p++ = 0;
    return p;
}

void gb_raw_open(char *f)
{
    if (!icode['1'])
        icode_setup();

    cur_file = fopen(f, "r");

#ifdef DATA_DIRECTORY
    if (!cur_file && (strlen(DATA_DIRECTORY) + strlen(f) < STR_BUF_LENGTH)) {
        sprintf(str_buf, "%s%s", DATA_DIRECTORY, f);
        cur_file = fopen(str_buf, "r");
    }
#endif

    if (cur_file) {
        io_errors = 0;
        more_data = 1;
        line_no = magic = 0;
        tot_lines = 0x7fffffff; /* allow ``infinitely many'' lines */
        fill_buf();
    } else
        io_errors = cant_open_file;
}

long gb_open(char *f)
{
    strncpy(file_name, f, sizeof(file_name) - 1); /* save the name for use by |gb_close| */

    gb_raw_open(f);
    if (cur_file) {
        // Check the first line; return if unsuccessful
        sprintf(str_buf, "* File \"%s\"", f);
        if (strncmp(buffer, str_buf, strlen(str_buf)))
            return (io_errors |= bad_first_line);
        
        // Check the second line; return if unsuccessful
        fill_buf();
        if (*buffer != '*')
            return (io_errors |= bad_second_line);

        // Check the third line; return if unsuccessful
        fill_buf();
        if (*buffer != '*')
            return (io_errors |= bad_third_line);
        
        // Check the fourth line; return if unsuccessful
        fill_buf();
        if (strncmp(buffer, "* (Checksum parameters ", 23))
            return (io_errors |= bad_fourth_line);
        cur_pos += 23;
        tot_lines = gb_number(10);
        if (gb_char() != ',')
            return (io_errors |= bad_fourth_line);
        final_magic = gb_number(10);
        if (gb_char() != ')')
            return (io_errors |= bad_fourth_line);

        gb_newline(); /* the first line of real data is now in the buffer */
    }
    return io_errors;
}

long gb_close(void)
{
    if (!cur_file)
        return (io_errors |= no_file_open);
    fill_buf();
    sprintf(str_buf, "* End of file \"%s\"", file_name);
    if (strncmp(buffer, str_buf, strlen(str_buf)))
        io_errors |= bad_last_line;
    more_data = buffer[0] = 0;
   /* now the {\sc GB\_\,IO} routines are effectively shut down */
   /* we have |cur_pos=buffer| */
    if (fclose(cur_file) != 0)
        return (io_errors |= cant_close_file);
    cur_file = NULL;
    if (line_no != tot_lines + 1)
        return (io_errors |= wrong_number_of_lines);
    if (magic != final_magic)
        return (io_errors |= wrong_checksum);
    return io_errors;
}

long gb_raw_close(void)
{
    if (cur_file) {
        fclose(cur_file);
        more_data = buffer[0] = 0;
        cur_pos = buffer;
        cur_file = NULL;
    }
    return magic;
}
