/*6:*/
//#line 103 "../gb_flip.w"

#define gb_next_rand() (*gb_fptr>=0?*gb_fptr--:gb_flip_cycle())
extern long *gb_fptr;

//#line 10 "../PROTOTYPES/gb_flip.ch"
extern long gb_flip_cycle(void);

//#line 107 "../gb_flip.w"

      /*:6*//*11: */
//#line 230 "../gb_flip.w"

//#line 29 "../PROTOTYPES/gb_flip.ch"
extern void gb_init_rand(long);

//#line 232 "../gb_flip.w"

       /*:11*//*13: */
//#line 262 "../gb_flip.w"

//#line 42 "../PROTOTYPES/gb_flip.ch"
extern long gb_unif_rand(long);

//#line 264 "../gb_flip.w"

/*:13*/
