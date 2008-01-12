/*4:*/
//#line 69 "../gb_io.w"

/*7:*/
//#line 93 "../gb_io.w"

#include <stdio.h>
#ifdef SYSV
#include <string.h>
#else
#include <strings.h>
#endif

/*:7*/
//#line 70 "../gb_io.w"

#define cant_open_file 0x1
#define cant_close_file 0x2
#define bad_first_line 0x4
#define bad_second_line 0x8
#define bad_third_line 0x10
#define bad_fourth_line 0x20
#define file_ended_prematurely 0x40
#define missing_newline 0x80
#define wrong_number_of_lines 0x100
#define wrong_checksum 0x200
#define no_file_open 0x400
#define bad_last_line 0x800 \

#define unexpected_char 127 \

#define STR_BUF_LENGTH 160 \

#define gb_raw_open gb_r_open \

#define gb_raw_close gb_r_close \


//#line 71 "../gb_io.w"

/*5:*/
//#line 82 "../gb_io.w"

long io_errors;

/*:5*/
//#line 72 "../gb_io.w"

/*8:*/
//#line 107 "../gb_io.w"

static char buffer[81];
static char *cur_pos = buffer;
static FILE *cur_file;

      /*:8*//*10: */
//#line 154 "../gb_io.w"

static char icode[256];
static long checksum_prime = (1L << 30) - 83;

static long magic;
static long line_no;
static long final_magic;
static long tot_lines;
static char more_data;

       /*:10*//*11: */
//#line 184 "../gb_io.w"

//#line 17 "../PROTOTYPES/gb_io.ch"
static char *imap =
    "0123456789"
    "ABCDEFGHIJKLMNOPQRSTUVWXYZ"
    "abcdefghijklmnopqrstuvwxyz" "_^~&@,;.:?!%#$+-*/|\\<=>()[]{}`'\" \n";
//#line 187 "../gb_io.w"

       /*:11*//*33: */
//#line 480 "../gb_io.w"

static char file_name[20];

/*:33*/
//#line 73 "../gb_io.w"

/*9:*/
//#line 122 "../gb_io.w"

//#line 10 "../PROTOTYPES/gb_io.ch"
static void fill_buf(void)
//#line 124 "../gb_io.w"
{
    register char *p;

    if (!fgets(buffer, sizeof(buffer), cur_file)) {
        io_errors |= file_ended_prematurely;
        buffer[0] = more_data = 0;
    }
    for (p = buffer; *p; p++);
    if (p-- == buffer || *p != '\n') {
        io_errors |= missing_newline;
        p++;
    }
    while (--p >= buffer && *p == ' ');
    *++p = '\n';
    *++p = 0;
    cur_pos = buffer;
}

      /*:9*//*15: */
//#line 212 "../gb_io.w"

//#line 55 "../PROTOTYPES/gb_io.ch"
static void icode_setup(void)
//#line 214 "../gb_io.w"
{
    register long k;
    register char *p;

    for (k = 0; k < 256; k++)
        icode[k] = unexpected_char;
    for (p = imap, k = 0; *p; p++, k++)
        icode[*p] = k;
}

/*:15*/
//#line 74 "../gb_io.w"

/*12:*/
//#line 190 "../gb_io.w"

//#line 31 "../PROTOTYPES/gb_io.ch"
char imap_chr(long d)
{
    return ((char) (d < 0 || d > strlen(imap) ? '\0' : imap[d]));
}

//#line 196 "../gb_io.w"

//#line 41 "../PROTOTYPES/gb_io.ch"
long imap_ord(char c)
//#line 199 "../gb_io.w"
{
/*14:*/
//#line 209 "../gb_io.w"

    if (!icode['1'])
        icode_setup();

/*:14*/
//#line 200 "../gb_io.w"
    ;
    return (c < 0 || c > 255) ? unexpected_char : icode[c];
}

       /*:12*//*17: */
//#line 231 "../gb_io.w"

//#line 71 "../PROTOTYPES/gb_io.ch"
long new_checksum(char *s, long old_checksum)
//#line 235 "../gb_io.w"
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

       /*:18*//*20: */
//#line 261 "../gb_io.w"

//#line 91 "../PROTOTYPES/gb_io.ch"
long gb_eof(void)
{
    return !more_data;
}

//#line 263 "../gb_io.w"

       /*:20*//*22: */
//#line 279 "../gb_io.w"

//#line 105 "../PROTOTYPES/gb_io.ch"
char gb_char(void)
//#line 281 "../gb_io.w"
{
    if (*cur_pos)
        return (*cur_pos++);
    return '\n';
}

//#line 111 "../PROTOTYPES/gb_io.ch"
void gb_backup(void)
//#line 287 "../gb_io.w"
{
    if (cur_pos > buffer)
        cur_pos--;
}

       /*:22*//*24: */
//#line 314 "../gb_io.w"

//#line 126 "../PROTOTYPES/gb_io.ch"
long gb_digit(char d)
//#line 317 "../gb_io.w"
{
    icode[0] = d;
    if (imap_ord(*cur_pos) < d)
        return icode[*cur_pos++];
    return -1;
}

//#line 133 "../PROTOTYPES/gb_io.ch"
unsigned long gb_number(char d)
//#line 325 "../gb_io.w"
{
    register unsigned long a = 0;

    icode[0] = d;
    while (imap_ord(*cur_pos) < d)
        a = a * d + icode[*cur_pos++];
    return a;
}

       /*:24*//*26: */
//#line 359 "../gb_io.w"

char str_buf[STR_BUF_LENGTH];

//#line 148 "../PROTOTYPES/gb_io.ch"
char *gb_string(char *p, char c)
//#line 364 "../gb_io.w"
{
    while (*cur_pos && *cur_pos != c)
        *p++ = *cur_pos++;
    *p++ = 0;
    return p;
}

       /*:26*//*30: */
//#line 431 "../gb_io.w"

//#line 165 "../PROTOTYPES/gb_io.ch"
void gb_raw_open(char *f)
//#line 434 "../gb_io.w"
{
/*14:*/
//#line 209 "../gb_io.w"

    if (!icode['1'])
        icode_setup();

/*:14*/
//#line 435 "../gb_io.w"
    ;
/*31:*/
//#line 453 "../gb_io.w"

    cur_file = fopen(f, "r");

#ifdef DATA_DIRECTORY
    if (!cur_file && (strlen(DATA_DIRECTORY) + strlen(f) < STR_BUF_LENGTH)) {
        sprintf(str_buf, "%s%s", DATA_DIRECTORY, f);
        cur_file = fopen(str_buf, "r");
    }
#endif

/*:31*/
//#line 436 "../gb_io.w"
    ;
    if (cur_file) {
        io_errors = 0;
        more_data = 1;
        line_no = magic = 0;
        tot_lines = 0x7fffffff;
        fill_buf();
    } else
        io_errors = cant_open_file;
}

       /*:30*//*32: */
//#line 463 "../gb_io.w"

//#line 172 "../PROTOTYPES/gb_io.ch"
long gb_open(char *f)
//#line 466 "../gb_io.w"
{
    strncpy(file_name, f, sizeof(file_name) - 1);

    gb_raw_open(f);
    if (cur_file) {
/*34:*/
//#line 500 "../gb_io.w"

        sprintf(str_buf, "* File \"%s\"", f);
        if (strncmp(buffer, str_buf, strlen(str_buf)))
            return (io_errors |= bad_first_line);

/*:34*/
//#line 471 "../gb_io.w"
        ;
/*35:*/
//#line 505 "../gb_io.w"

        fill_buf();
        if (*buffer != '*')
            return (io_errors |= bad_second_line);

/*:35*/
//#line 472 "../gb_io.w"
        ;
/*36:*/
//#line 509 "../gb_io.w"

        fill_buf();
        if (*buffer != '*')
            return (io_errors |= bad_third_line);

/*:36*/
//#line 473 "../gb_io.w"
        ;
/*37:*/
//#line 513 "../gb_io.w"

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

/*:37*/
//#line 474 "../gb_io.w"
        ;
        gb_newline();
    }
    return io_errors;
}

       /*:32*//*39: */
//#line 535 "../gb_io.w"

//#line 178 "../PROTOTYPES/gb_io.ch"
long gb_close(void)
//#line 537 "../gb_io.w"
{
    if (!cur_file)
        return (io_errors |= no_file_open);
    fill_buf();
    sprintf(str_buf, "* End of file \"%s\"", file_name);
    if (strncmp(buffer, str_buf, strlen(str_buf)))
        io_errors |= bad_last_line;
    more_data = buffer[0] = 0;


    if (fclose(cur_file) != 0)
        return (io_errors |= cant_close_file);
    cur_file = NULL;
    if (line_no != tot_lines + 1)
        return (io_errors |= wrong_number_of_lines);
    if (magic != final_magic)
        return (io_errors |= wrong_checksum);
    return io_errors;
}

       /*:39*//*42: */
//#line 572 "../gb_io.w"

//#line 192 "../PROTOTYPES/gb_io.ch"
long gb_raw_close(void)
//#line 574 "../gb_io.w"
{
    if (cur_file) {
        fclose(cur_file);
        more_data = buffer[0] = 0;
        cur_pos = buffer;
        cur_file = NULL;
    }
    return magic;
}

/*:42*/
//#line 75 "../gb_io.w"


/*:4*/
