/*2:*/
//#line 57 "../gb_dijk.w"

#include "gb_graph.h"
#define dist z.I \

#define backlink y.V \

#define hh_val x.I \

#define print_dijkstra_result p_dijkstra_result \

#define llink v.V
#define rlink w.V \


//#line 59 "../gb_dijk.w"

/*16:*/
//#line 308 "../gb_dijk.w"

static Vertex head[128];

//#line 76 "../PROTOTYPES/gb_dijk.ch"
void init_dlist(long d)
//#line 313 "../gb_dijk.w"
{
    head->llink = head->rlink = head;
    head->dist = d - 1;
}

       /*:16*//*17: */
//#line 327 "../gb_dijk.w"

//#line 84 "../PROTOTYPES/gb_dijk.ch"
void enlist(Vertex * v, long d)
//#line 331 "../gb_dijk.w"
{
    register Vertex *t = head->llink;

    v->dist = d;
    while (d < t->dist)
        t = t->llink;
    v->llink = t;
    (v->rlink = t->rlink)->llink = v;
    t->rlink = v;
}

       /*:17*//*18: */
//#line 339 "../gb_dijk.w"

//#line 92 "../PROTOTYPES/gb_dijk.ch"
void reenlist(Vertex * v, long d)
//#line 343 "../gb_dijk.w"
{
    register Vertex *t = v->llink;

    (t->rlink = v->rlink)->llink = v->llink;
    v->dist = d;
    while (d < t->dist)
        t = t->llink;
    v->llink = t;
    (v->rlink = t->rlink)->llink = v;
    t->rlink = v;
}

       /*:18*//*19: */
//#line 352 "../gb_dijk.w"

//#line 98 "../PROTOTYPES/gb_dijk.ch"
Vertex *del_first(void)
//#line 354 "../gb_dijk.w"
{
    Vertex *t;

    t = head->rlink;
    if (t == head)
        return NULL;
    (head->rlink = t->rlink)->llink = head;
    return t;
}

       /*:19*//*21: */
//#line 371 "../gb_dijk.w"

static long master_key;

//#line 105 "../PROTOTYPES/gb_dijk.ch"
void init_128(long d)
//#line 376 "../gb_dijk.w"
{
    register Vertex *u;

    master_key = d;
    for (u = head; u < head + 128; u++)
        u->llink = u->rlink = u;
}

       /*:21*//*22: */
//#line 385 "../gb_dijk.w"

//#line 111 "../PROTOTYPES/gb_dijk.ch"
Vertex *del_128(void)
//#line 387 "../gb_dijk.w"
{
    long d;
    register Vertex *u, *t;

    for (d = master_key; d < master_key + 128; d++) {
        u = head + (d & 0x7f);
        t = u->rlink;
        if (t != u) {
            master_key = d;
            (u->rlink = t->rlink)->llink = u;
            return t;
        }
    }
    return NULL;
}

       /*:22*//*23: */
//#line 401 "../gb_dijk.w"

//#line 119 "../PROTOTYPES/gb_dijk.ch"
void enq_128(Vertex * v, long d)
//#line 405 "../gb_dijk.w"
{
    register Vertex *u = head + (d & 0x7f);

    v->dist = d;
    (v->llink = u->llink)->rlink = v;
    v->rlink = u;
    u->llink = v;
}

       /*:23*//*24: */
//#line 424 "../gb_dijk.w"

//#line 129 "../PROTOTYPES/gb_dijk.ch"
void req_128(Vertex * v, long d)
//#line 428 "../gb_dijk.w"
{
    register Vertex *u = head + (d & 0x7f);

    (v->llink->rlink = v->rlink)->llink = v->llink;
    v->dist = d;
    (v->llink = u->llink)->rlink = v;
    v->rlink = u;
    u->llink = v;
    if (d < master_key)
        master_key = d;
}

/*:24*/
//#line 60 "../gb_dijk.w"

/*8:*/
//#line 161 "../gb_dijk.w"

//#line 34 "../PROTOTYPES/gb_dijk.ch"
static long dummy(Vertex * v)
//#line 164 "../gb_dijk.w"
{
    return 0;
}

      /*:8*//*15: */
//#line 294 "../gb_dijk.w"

//#line 65 "../PROTOTYPES/gb_dijk.ch"
void (*init_queue) (long) = init_dlist;
void (*enqueue) (Vertex *, long) = enlist;
void (*requeue) (Vertex *, long) = reenlist;

Vertex *(*del_min) (void) = del_first;

//#line 300 "../gb_dijk.w"

/*:15*/
//#line 61 "../gb_dijk.w"

/*9:*/
//#line 168 "../gb_dijk.w"

//#line 44 "../PROTOTYPES/gb_dijk.ch"
long dijkstra(Vertex * uu, Vertex * vv, Graph * gg, long (*hh) (Vertex *))
//#line 174 "../gb_dijk.w"
{
    register Vertex *t;

    if (!hh)
        hh = dummy;
/*10:*/
//#line 194 "../gb_dijk.w"

    for (t = gg->vertices + gg->n - 1; t >= gg->vertices; t--)
        t->backlink = NULL;
    uu->backlink = uu;
    uu->dist = 0;
    uu->hh_val = (*hh) (uu);
    (*init_queue) (0L);

/*:10*/
//#line 176 "../gb_dijk.w"
    ;
    t = uu;
    if (verbose)                /*12: */
        //#line 231 "../gb_dijk.w"

    {
        printf("Distances from %s", uu->name);
        if (hh != dummy)
            printf(" [%ld]", uu->hh_val);
        printf(":\n");
    }

/*:12*/
//#line 178 "../gb_dijk.w"
    ;
    while (t != vv) {
/*11:*/
//#line 203 "../gb_dijk.w"

        {
            register Arc *a;
            register long d = t->dist - t->hh_val;

            for (a = t->arcs; a; a = a->next) {
                register Vertex *v = a->tip;

                if (v->backlink) {
                    register long dd = d + a->len + v->hh_val;

                    if (dd < v->dist) {
                        v->backlink = t;
                        (*requeue) (v, dd);
                    }
                } else {
                    v->hh_val = (*hh) (v);
                    v->backlink = t;
                    (*enqueue) (v, d + a->len + v->hh_val);
                }
            }
        }

/*:11*/
//#line 181 "../gb_dijk.w"
        ;
        t = (*del_min) ();
        if (t == NULL)
            return -1;

        if (verbose)            /*13: */
            //#line 237 "../gb_dijk.w"

        {
            printf(" %ld to %s", t->dist - t->hh_val + uu->hh_val, t->name);
            if (hh != dummy)
                printf(" [%ld]", t->hh_val);
            printf(" via %s\n", t->backlink->name);
        }

/*:13*/
//#line 186 "../gb_dijk.w"
        ;
    }
    return vv->dist - vv->hh_val + uu->hh_val;
}

/*:9*/
//#line 62 "../gb_dijk.w"

/*14:*/
//#line 256 "../gb_dijk.w"

//#line 55 "../PROTOTYPES/gb_dijk.ch"
void print_dijkstra_result(Vertex * vv)
//#line 259 "../gb_dijk.w"
{
    register Vertex *t, *p, *q;

    t = NULL, p = vv;
    if (!p->backlink) {
        printf("Sorry, %s is unreachable.\n", p->name);
        return;
    }
    do {
        q = p->backlink;
        p->backlink = t;
        t = p;
        p = q;
    } while (t != p);
    do {
        printf("%10ld %s\n", t->dist - t->hh_val + p->hh_val, t->name);
        t = t->backlink;
    } while (t);
    t = p;
    do {
        q = t->backlink;
        t->backlink = p;
        p = t;
        t = q;
    } while (p != vv);
}

/*:14*/
//#line 63 "../gb_dijk.w"


/*:2*/
