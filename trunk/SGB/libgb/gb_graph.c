#include <string.h>
#include <stdio.h>
#include <stdlib.h>
#define gb_typed_alloc(n,t,s) (t*) gb_alloc((long) ((n) *sizeof(t) ) ,s)  \

#define n_1 uu.I /* utility field |uu| may denote size of bipartite first part */

#define arcs_per_block 102

#define gb_new_graph gb_nugraph /* abbreviations for external linkage */
#define gb_new_arc gb_nuarc
#define gb_new_edge gb_nuedge

#define string_block_size 1016  /* $1024-8$ is usually efficient */

#define hash_link u.V
#define hash_head v.V

#define HASH_MULT 314159  /* random multiplier */
#define HASH_PRIME 516595003  /* the 27182818th prime; it's less than $2^{29}$ */

/*
 * Type declarations
 */

typedef union {
    struct vertex_struct *V;  /* pointer to \&{Vertex} */
    struct arc_struct *A;  /* pointer to \&{Arc} */
    struct graph_struct *G; /* pointer to \&{Graph} */
    char *S; /* pointer to string */
    long I;  /* integer */
} util;

typedef struct vertex_struct {
    struct arc_struct *arcs;  /* linked list of arcs coming out of this vertex */
    char *name; /* string identifying this vertex symbolically */
    util u, v, w, x, y, z; /* multipurpose fields */
} Vertex;

typedef struct arc_struct {
    struct vertex_struct *tip;  /* the arc points to this vertex */
    struct arc_struct *next;  /* another arc pointing from the same vertex */
    long len;  /* length of this arc */
    util a, b;  /* multipurpose fields */
} Arc;

#define init_area(s)  *s= NULL

struct area_pointers {
    char *first; /* address of the beginning of this block */
    struct area_pointers *next; /* address of area pointers in the previously allocated block */
};

typedef struct area_pointers *Area[1];

#define ID_FIELD_SIZE 161
typedef struct graph_struct {
    Vertex *vertices;  /* beginning of the vertex array */
    long n;  /* total number of vertices */
    long m;  /* total number of arcs */
    char id[ID_FIELD_SIZE];  /* GraphBase identification */
    char util_types[15];  /* usage of utility fields */
    Area data;  /* the main data blocks */
    Area aux_data;  /* subsidiary data blocks */
    util uu, vv, ww, xx, yy, zz;  /* multipurpose fields */
} Graph;

typedef unsigned long siz_t; /* basic machine address, as signless integer */

/* 
 * Private declarations
 */
static Arc *next_arc; /* the next |Arc| available for allocation */
static Arc *bad_arc; /* but if |next_arc=bad_arc|, that |Arc| isn't there */
static char *next_string; /* the next byte available for storing a string */
static char *bad_string; /* but if |next_string=bad_string|, don't byte */
static Arc dummy_arc[2]; /* an |Arc| record to point to in an emergency */
static Graph dummy_graph; /* a |Graph| record that's normally unused */
static Graph *cur_graph=&dummy_graph; /* the |Graph| most recently created */


/*
 * External declarations
 */
long verbose=0; /* nonzero if ``verbose'' output is desired */
long panic_code=0; /* set nonzero if graph generator returns null pointer */
long gb_trouble_code=0; /* did |gb_alloc| return |NULL|? */
long extra_n=4; /* the number of shadow vertices allocated by |gb_new_graph| */
char null_string[1]; /* a null string constant */

siz_t edge_trick = sizeof(Arc) - (sizeof(Arc) & (sizeof(Arc) - 1));

char *gb_alloc(long n, Area s)
//  long n; /* number of consecutive bytes desired */
//  Area s; /* storage area that will contain the new block */
{
    long m = sizeof(char *);  /* |m| is the size of a pointer variable */
    Area t;  /* a temporary pointer */
    char *loc; /* the block address */

    if (n <= 0 || n > 0xffff00 - 2 * m) {
        gb_trouble_code |= 2; /* illegal request */
        return NULL;
    }
    n = ((n + m - 1) / m) * m; /* round up to multiple of |m| */
    loc = (char *) calloc((unsigned) ((n + 2 * m + 255) / 256), 256);
    if (loc) {
        *t = (struct area_pointers *) (loc + n);
        (*t)->first = loc;
        (*t)->next = *s;
        *s = *t;
    } else
        gb_trouble_code |= 1;
    return loc;
}

void gb_free(Area s)
{
    Area t;

    while (*s) {
        *t = (*s)->next;
        free((*s)->first);
        *s = *t;
    }
}

Graph *gb_new_graph(long n)
// long n;/* desired number of vertices */
{
    cur_graph = (Graph *) calloc(1, sizeof(Graph));
    if (cur_graph) {
        cur_graph->vertices = gb_typed_alloc(n + extra_n, Vertex, cur_graph->data);
        if (cur_graph->vertices) {
            Vertex *p;

            cur_graph->n = n;
            for (p = cur_graph->vertices + n + extra_n - 1; p >= cur_graph->vertices; p--)
                p->name = null_string;
            sprintf(cur_graph->id, "gb_new_graph(%ld)", n);
            strcpy(cur_graph->util_types, "ZZZZZZZZZZZZZZ");
        } else {
            free((char *) cur_graph);
            cur_graph = NULL;
        }
    }
    next_arc = bad_arc = NULL;
    next_string = bad_string = NULL;
    gb_trouble_code = 0;
    return cur_graph;
}

 /* |sprintf(g->id,"%s%s%s",s1,gg->id,s2)| */
void make_compound_id(Graph * g, char *s1, Graph * gg, char *s2)
//  Graph *g; /* graph whose |id| is to be set */
//  char *s1; /* string for the beginning of the new |id| */
//  Graph *gg; /* graph whose |id| is to be copied */
//  char *s2; /* string for the end of the new |id| */
{
    int avail = ID_FIELD_SIZE - strlen(s1) - strlen(s2);
    char tmp[ID_FIELD_SIZE];

    strcpy(tmp, gg->id);
    if (strlen(tmp) < avail)
        sprintf(g->id, "%s%s%s", s1, tmp, s2);
    else
        sprintf(g->id, "%s%.*s...)%s", s1, avail - 5, tmp, s2);
}

/* |sprintf(g->id,"%s%s%s%s%s",s1,gg->id,s2,ggg->id,s3)| */
void make_double_compound_id(Graph * g, char *s1, Graph * gg, char *s2, Graph * ggg, char *s3)
//  Graph *g; /* graph whose |id| is to be set */
//  char *s1; /* string for the beginning of the new |id| */
//  Graph *gg; /* first graph whose |id| is to be copied */
//  char *s2; /* string for the middle of the new |id| */
//  Graph *ggg; /* second graph whose |id| is to be copied */
//  char *s3; /* string for the end of the new |id| */
{
    int avail = ID_FIELD_SIZE - strlen(s1) - strlen(s2) - strlen(s3);

    if (strlen(gg->id) + strlen(ggg->id) < avail)
        sprintf(g->id, "%s%s%s%s%s", s1, gg->id, s2, ggg->id, s3);
    else
        sprintf(g->id, "%s%.*s...)%s%.*s...)%s", s1, avail / 2 - 5, gg->id,
                s2, (avail - 9) / 2, ggg->id, s3);
}

Arc *gb_virgin_arc(void)
{
    register Arc *cur_arc = next_arc;

    if (cur_arc == bad_arc) {
        cur_arc = gb_typed_alloc(arcs_per_block, Arc, cur_graph->data);
        if (cur_arc == NULL)
            cur_arc = dummy_arc;
        else {
            next_arc = cur_arc + 1;
            bad_arc = cur_arc + arcs_per_block;
        }
    } else
        next_arc++;
    return cur_arc;
}

void gb_new_arc(Vertex * u, Vertex * v, long len)
//   Vertex *u, *v; /* a newly created arc will go from |u| to |v| */
//  long len; /* its length */
{
    register Arc *cur_arc = gb_virgin_arc();

    cur_arc->tip = v;
    cur_arc->next = u->arcs;
    cur_arc->len = len;
    u->arcs = cur_arc;
    cur_graph->m++;
}

void gb_new_edge(Vertex * u, Vertex * v, long len)
//  Vertex *u, *v; /* new arcs will go from |u| to |v| and from |v| to |u| */
//  long len; /* their length */
{
    register Arc *cur_arc = gb_virgin_arc();

    if (cur_arc != dummy_arc)
        next_arc++;
    if (u < v) {
        cur_arc->tip = v;
        cur_arc->next = u->arcs;
        (cur_arc + 1)->tip = u;
        (cur_arc + 1)->next = v->arcs;
        u->arcs = cur_arc;
        v->arcs = cur_arc + 1;
    } else {
        (cur_arc + 1)->tip = v;
        (cur_arc + 1)->next = u->arcs;
        u->arcs = cur_arc + 1;
        cur_arc->tip = u;
        cur_arc->next = v->arcs;
        v->arcs = cur_arc;
    }
    cur_arc->len = (cur_arc + 1)->len = len;
    cur_graph->m += 2;
}

char *gb_save_string(register char *s /* the string to be copied */)
{
    register char *p = s;
    register long len; /* length of the string and the following null character */

    while (*p++);  /* advance to the end of the string */
    len = p - s;
    p = next_string;
    if (p + len > bad_string) { /* not enough room in the current block */
        long size = string_block_size;

        if (len > size)
            size = len;
        p = gb_alloc(size, cur_graph->data);
        if (p == NULL)
            return null_string; /* return a pointer to |""| if memory ran out */
        bad_string = p + size;
    }
    while (*s) /* copy the non-null bytes of the string */
        *p++ = *s++;
    *p++ = '\0'; /* and append a null character */
    next_string = p;
    return p - len;
}

void switch_to_graph(Graph * g)
{
    cur_graph->ww.A = next_arc;
    cur_graph->xx.A = bad_arc;
    cur_graph->yy.S = next_string;
    cur_graph->zz.S = bad_string;
    cur_graph = (g ? g : &dummy_graph);
    next_arc = cur_graph->ww.A;
    bad_arc = cur_graph->xx.A;
    next_string = cur_graph->yy.S;
    bad_string = cur_graph->zz.S;
    cur_graph->ww.A = NULL;
    cur_graph->xx.A = NULL;
    cur_graph->yy.S = NULL;
    cur_graph->zz.S = NULL;
}

void gb_recycle(Graph * g)
{
    if (g) {
        gb_free(g->data);
        gb_free(g->aux_data);
        free((char *) g); /* the user must not refer to |g| again */
    }
}

void hash_in(Vertex * v)
{
    register char *t = v->name;
    register Vertex *u;

		// Find vertex |u|, whose location is the hash code for string |t|
    {
        register long h;

        for (h = 0; *t; t++) {
            h += (h ^ (h >> 1)) + HASH_MULT * (unsigned char) *t;
            while (h >= HASH_PRIME)
                h -= HASH_PRIME;
        }
        u = cur_graph->vertices + (h % cur_graph->n);
    }

    v->hash_link = u->hash_head;
    u->hash_head = v;
}

Vertex *hash_out(char *s)
{
    register char *t = s;
    register Vertex *u;

		// Find vertex |u|, whose location is the hash code for string |t|
    {
        register long h;

        for (h = 0; *t; t++) {
            h += (h ^ (h >> 1)) + HASH_MULT * (unsigned char) *t;
            while (h >= HASH_PRIME)
                h -= HASH_PRIME;
        }
        u = cur_graph->vertices + (h % cur_graph->n);
    }

    for (u = u->hash_head; u; u = u->hash_link)
        if (strcmp(s, u->name) == 0)
            return u;
    return NULL;
}

void hash_setup(Graph * g)
{
    Graph *save_cur_graph;

    if (g && g->n > 0) {
        register Vertex *v;

        save_cur_graph = cur_graph;
        cur_graph = g;
        for (v = g->vertices; v < g->vertices + g->n; v++)
            v->hash_head = NULL;
        for (v = g->vertices; v < g->vertices + g->n; v++)
            hash_in(v);
        g->util_types[0] = g->util_types[1] = 'V'; /* indicate usage of |hash_head| and |hash_link| */
        cur_graph = save_cur_graph;
    }
}

Vertex *hash_lookup(char *s, Graph * g)
{
    Graph *save_cur_graph;

    if (g && g->n > 0) {
        register Vertex *v;

        save_cur_graph = cur_graph;
        cur_graph = g;
        v = hash_out(s);
        cur_graph = save_cur_graph;
        return v;
    } else
        return NULL;
}

/*:48*/
//#line 54 "../gb_graph.w"


/*:3*/
