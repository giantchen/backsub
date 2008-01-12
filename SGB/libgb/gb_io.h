/*6:*/
//#line 85 "../gb_io.w"

/*7:*/
//#line 93 "../gb_io.w"

#include <stdio.h>
#ifdef SYSV
#include <string.h>
#else
#include <strings.h>
#endif

/*:7*/
//#line 86 "../gb_io.w"

extern long io_errors;


      /*:6*//*13: */
//#line 204 "../gb_io.w"

#define unexpected_char  127
//#line 48 "../PROTOTYPES/gb_io.ch"
extern char imap_chr(long);
extern long imap_ord(char);

//#line 208 "../gb_io.w"

       /*:13*//*16: */
//#line 224 "../gb_io.w"

//#line 62 "../PROTOTYPES/gb_io.ch"
extern void gb_newline(void);
extern long new_checksum(char *, long);

//#line 227 "../gb_io.w"

       /*:16*//*19: */
//#line 258 "../gb_io.w"

//#line 85 "../PROTOTYPES/gb_io.ch"
extern long gb_eof(void);

//#line 260 "../gb_io.w"

       /*:19*//*21: */
//#line 275 "../gb_io.w"

//#line 98 "../PROTOTYPES/gb_io.ch"
extern char gb_char(void);
extern void gb_backup(void);

//#line 278 "../gb_io.w"

       /*:21*//*23: */
//#line 306 "../gb_io.w"

//#line 118 "../PROTOTYPES/gb_io.ch"
extern long gb_digit(char);
extern unsigned long gb_number(char);

//#line 309 "../gb_io.w"

       /*:23*//*25: */
//#line 351 "../gb_io.w"

#define STR_BUF_LENGTH 160
extern char str_buf[];

//#line 139 "../PROTOTYPES/gb_io.ch"
extern char *gb_string(char *, char);


//#line 356 "../gb_io.w"

       /*:25*//*29: */
//#line 426 "../gb_io.w"

#define gb_raw_open gb_r_open
//#line 157 "../PROTOTYPES/gb_io.ch"
extern void gb_raw_open(char *);
extern long gb_open(char *);

//#line 430 "../gb_io.w"

       /*:29*//*41: */
//#line 567 "../gb_io.w"

#define gb_raw_close gb_r_close
//#line 185 "../PROTOTYPES/gb_io.ch"
extern long gb_close(void);
extern long gb_raw_close(void);

//#line 571 "../gb_io.w"

/*:41*/
