/*3:*/
//#line 67 "../gb_dijk.w"

//#line 6 "../PROTOTYPES/gb_dijk.ch"
extern long dijkstra(Vertex *, Vertex *, Graph *, long (*)(Vertex *));

#define print_dijkstra_result p_dijkstra_result
extern void print_dijkstra_result(Vertex *);

//#line 71 "../gb_dijk.w"

      /*:3*//*5: */
//#line 118 "../gb_dijk.w"

#define dist z.I
#define backlink y.V

      /*:5*//*6: */
//#line 142 "../gb_dijk.w"

//#line 20 "../PROTOTYPES/gb_dijk.ch"
extern void (*init_queue) (long);

extern void (*enqueue) (Vertex *, long);

extern void (*requeue) (Vertex *, long);

extern Vertex *(*del_min) (void);

//#line 148 "../gb_dijk.w"

      /*:6*//*7: */
//#line 155 "../gb_dijk.w"

#define hh_val x.I

      /*:7*//*25: */
//#line 441 "../gb_dijk.w"

//#line 144 "../PROTOTYPES/gb_dijk.ch"
extern void init_dlist(long);
extern void enlist(Vertex *, long);
extern void reenlist(Vertex *, long);
extern Vertex *del_first(void);
extern void init_128(long);
extern Vertex *del_128(void);
extern void enq_128(Vertex *, long);
extern void req_128(Vertex *, long);

//#line 450 "../gb_dijk.w"

/*:25*/
