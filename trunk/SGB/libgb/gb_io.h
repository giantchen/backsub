#include <stdio.h>
#include <string.h>

/* record of anomalies noted by {\sc GB\_\,IO} routines */
extern long io_errors;

#define unexpected_char  127

extern char imap_chr(long); /* the character that maps to a given character */
extern long imap_ord(char); /* the ordinal number of a given character */

extern void gb_newline(void); /* advance to next line of the data file */
extern long new_checksum(char *, long); /* compute change in magic number */

extern long gb_eof(void); /* has the data all been read? */

extern char gb_char(void); /* get next character of current line, or |'\n'| */
extern void gb_backup(void); /* move back ready to scan a character again */

extern long gb_digit(char); /* |gb_digit(d)| reads a digit between 0 and |d-1| */
extern unsigned long gb_number(char); /* |gb_number(d)| reads a radix-|d| number */

#define STR_BUF_LENGTH 160
extern char str_buf[]; /* safe place to receive output of |gb_string| */

extern char *gb_string(char *, char); /* |gb_string(p,c)| reads a string delimited by |c|
  into bytes starting at |p| */


#define gb_raw_open gb_r_open

extern void gb_raw_open(char *); /* open a file for GraphBase input */
extern long gb_open(char *); /* open a GraphBase data file; return 0 if OK */

#define gb_raw_close gb_r_close
extern long gb_close(void); /* close a GraphBase data file; return 0 if OK */
extern long gb_raw_close(void); /* close file and return the checksum */
