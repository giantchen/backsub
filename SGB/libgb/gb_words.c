#include "gb_io.h"  /* we will use the {\sc GB\_\,IO} routines for input */
#include "gb_flip.h" /* we will use the {\sc GB\_\,FLIP} routines for random numbers */

#include "gb_graph.h" /* we will use the {\sc GB\_\,GRAPH} data structures */
#include "gb_sort.h" /* and |gb_linksort| for sorting */
#define panic(c) {gb_free(node_blocks) ; \
panic_code= c;gb_trouble_code= 0;return NULL;} \

#define nodes_per_block 111 \

#define copy5(y,x) { \
*(y) = *(x) ; \
*((y) +1) = *((x) +1) ; \
*((y) +2) = *((x) +2) ; \
*((y) +3) = *((x) +3) ; \
*((y) +4) = *((x) +4) ; \
} \

#define hash_prime 6997  /* a prime number larger than the total number of words */

#define weight u.I
#define loc a.I \

#define mtch(i) (*(q+i) ==*(r+i) )
#define match(a,b,c,d) (mtch(a) &&mtch(b) &&mtch(c) &&mtch(d) )
#define store_loc_of_diff(k) cur_vertex->arcs->loc= (cur_vertex->arcs-1) ->loc= k
#define ch(q) ((long) *(q) )
#define hdown(k) h==htab[k]?h= htab[k+1]-1:h-- \


typedef struct node_struct {
    long key; /* the sort key (weight plus $2^{30}$) */
    struct node_struct *link; /* links the nodes together */
    char wd[5]; /* five-letter word
                   (which typically consumes eight bytes, too bad) */
} node;

typedef Vertex *hash_table[hash_prime];

static long max_c[] = { 15194, 3560, 4467, 460, 6976, 756, 362 }; /* maximum counts $C_j$ */

static long default_wt_vector[] = { 100, 10, 4, 2, 2, 1, 1, 1, 1 }; /* use this if |wt_vector=NULL| */

static Area node_blocks; /* the memory area for blocks of nodes */

static hash_table *htab; /* five dynamically allocated hash tables */

static double flabs(long x)
{
    if (x >= 0)
        return (double) x;
    return -((double) x);
}

static long iabs(long x)
{
    if (x >= 0)
        return (long) x;
    return -((long) x);
}

Graph *words(unsigned long n, long wt_vector[], long wt_threshold, long seed)
//  unsigned long n; /* maximum number of vertices desired */
//  long wt_vector[]; /* pointer to array of weights */
//  long wt_threshold; /* minimum qualifying weight */
//  long seed; /* random number seed */
{                               /*8: */
        
    Graph *new_graph; /* the graph constructed by |words| */

register long wt; /* the weight of the current word */
char word[5]; /* the current five-letter word */
long nn=0; /* the number of qualifying words found so far */

node *next_node; /* the next node available for allocation */
node *bad_node; /* if |next_node=bad_node|, the node isn't really there */
node *stack_ptr; /* the most recently created node */
node *cur_node; /* current node being created or examined */

Vertex *cur_vertex; /* the current vertex being created or examined */
char *next_string; /* where we'll store the next five-letter word */

/*:24*/
//#line 167 "../gb_words.w"

    gb_init_rand(seed);

/* Check that |wt_vector| is valid */
    if (!wt_vector)
        wt_vector = default_wt_vector;
    else {
        register double flacc;
        register long *p, *q;
        register long acc;

/* Use floating point arithmetic to check that |wt_vector| isn't totally off base */

        p = wt_vector;
        flacc = flabs(*p++);
        if (flacc < flabs(*p))
            flacc = flabs(*p);

        for (q = &max_c[0]; q < &max_c[7]; q++)
            flacc += *q * flabs(*++p);
        if (flacc >= (double) 0x60000000)

            panic(very_bad_specs);

/* Use integer arithmetic to check that |wt_vector| is truly OK */

        p = wt_vector;
        acc = iabs(*p++);
        if (acc < iabs(*p))
            acc = iabs(*p);

        for (q = &max_c[0]; q < &max_c[7]; q++)
            acc += *q * iabs(*++p);
        if (acc >= 0x40000000)
            panic(bad_specs);
    }

    /* Input the qualifying words to a linked list, computing their weights */
    next_node = bad_node = stack_ptr = NULL;
    if (gb_open("words.dat") != 0)
        panic(early_data_fault);
        /* couldn't open |"words.dat"| using GraphBase conventions;
                |io_errors| tells why */

    do /* Read one word, and put it on the stack if it qualifies */
    {
        register long j;

        for (j = 0; j < 5; j++)
            word[j] = gb_char();

        /* Compute the weight |wt| */
        {
            register long *p, *q; /* pointers to $C_j$ and $w_j$ */
            register long c; /* current count */

            switch (gb_char()) {
            case '*': /* `common' word */
                wt = wt_vector[0];
                break;
            case '+': /* `advanced' word */
                wt = wt_vector[1];
                break;
            case ' ':  /* `unusual' word */
            case '\n':
                wt = 0;
                break;
            default: /* unknown type of word */
                panic(syntax_error);
            }
            p = &max_c[0];
            q = &wt_vector[2];
            do {
                if (p == &max_c[7])  /* too many counts */
                    panic(syntax_error + 1);
                c = gb_number(10);
                if (c > *p++) /* count too large */
                    panic(syntax_error + 2);
                wt += c ** q++;
            } while (gb_char() == ',');
        }

        if (wt >= wt_threshold) {/* it qualifies */

            /* Install |word| and |wt| in a new node */

            if (next_node == bad_node) {
                cur_node = gb_typed_alloc(nodes_per_block, node, node_blocks);
                if (cur_node == NULL) /* out of memory already */
                    panic(no_room + 1);
                next_node = cur_node + 1;
                bad_node = cur_node + nodes_per_block;
            } else
                cur_node = next_node++;
            cur_node->key = wt + 0x40000000;
            cur_node->link = stack_ptr;
            copy5(cur_node->wd, word);
            stack_ptr = cur_node;

            nn++;
        }
        gb_newline();
    }
    while (!gb_eof());
    if (gb_close() != 0)
        panic(late_data_fault);
    /* something's wrong with |"words.dat"|; see |io_errors| */

    // Input the qualifying words to a linked list, computing their weights

    /* Sort and output the words, determining adjacencies */
    gb_linksort(stack_ptr);
    
    /* Allocate storage for the new graph; adjust |n| if it is zero or too large */
    if (n == 0 || nn < n)
        n = nn;
    new_graph = gb_new_graph(n);
    if (new_graph == NULL)
        panic(no_room); /* out of memory before we're even started */
    if (wt_vector == default_wt_vector)
        sprintf(new_graph->id, "words(%lu,0,%ld,%ld)", n, wt_threshold, seed);
    else
        sprintf(new_graph->id,
                "words(%lu,{%ld,%ld,%ld,%ld,%ld,%ld,%ld,%ld,%ld},%ld,%ld)",
                n, wt_vector[0], wt_vector[1], wt_vector[2], wt_vector[3], wt_vector[4],
                wt_vector[5], wt_vector[6], wt_vector[7], wt_vector[8], wt_threshold, seed);
    strcpy(new_graph->util_types, "IZZZZZIZZZZZZZ");
    cur_vertex = new_graph->vertices;
    next_string = gb_typed_alloc(6 * n, char, new_graph->data);

    htab = gb_typed_alloc(5, hash_table, new_graph->aux_data);

    // Allocate storage for the new graph; adjust |n| if it is zero or too large
    if (gb_trouble_code == 0 && n) {
  register long j; /* runs through sorted lists */
  register node *p; /* the current node being output */

        nn = n;
        for (j = 127; j >= 0; j--)
            for (p = (node *) gb_sorted[j]; p; p = p->link) {
            /* Add the word |p->wd| to the graph */
                {
                    register char *q; /* the new word */

                    q = cur_vertex->name = next_string;
                    next_string += 6;
                    copy5(q, p->wd);
                    cur_vertex->weight = p->key - 0x40000000;
                    
                    /* Add edges for all previous words |r| that nearly match |q| */
                    {
register char *r; /* previous word possibly adjacent to |q| */
  register Vertex **h; /* hash address for linear probing */
  register long raw_hash; /* five-letter hash code before remaindering */

                        raw_hash =
                            (((((((ch(q) << 5) + ch(q + 1)) << 5) + ch(q + 2)) << 5) +
                              ch(q + 3)) << 5) + ch(q + 4);
                        for (h = htab[0] + (raw_hash - (ch(q) << 20)) % hash_prime; *h; hdown(0)) {
                            r = (*h)->name;
                            if (match(1, 2, 3, 4))
                                gb_new_edge(cur_vertex, *h, 1L), store_loc_of_diff(0);
                        }
                        *h = cur_vertex;
                        for (h = htab[1] + (raw_hash - (ch(q + 1) << 15)) % hash_prime; *h;
                             hdown(1)) {
                            r = (*h)->name;
                            if (match(0, 2, 3, 4))
                                gb_new_edge(cur_vertex, *h, 1L), store_loc_of_diff(1);
                        }
                        *h = cur_vertex;
                        for (h = htab[2] + (raw_hash - (ch(q + 2) << 10)) % hash_prime; *h;
                             hdown(2)) {
                            r = (*h)->name;
                            if (match(0, 1, 3, 4))
                                gb_new_edge(cur_vertex, *h, 1L), store_loc_of_diff(2);
                        }
                        *h = cur_vertex;
                        for (h = htab[3] + (raw_hash - (ch(q + 3) << 5)) % hash_prime; *h; hdown(3)) {
                            r = (*h)->name;
                            if (match(0, 1, 2, 4))
                                gb_new_edge(cur_vertex, *h, 1L), store_loc_of_diff(3);
                        }
                        *h = cur_vertex;
                        for (h = htab[4] + (raw_hash - ch(q + 4)) % hash_prime; *h; hdown(4)) {
                            r = (*h)->name;
                            if (match(0, 1, 2, 3))
                                gb_new_edge(cur_vertex, *h, 1L), store_loc_of_diff(4);
                        }
                        *h = cur_vertex;
                    }
                    // Add edges for all previous words |r| that nearly match |q|
                    cur_vertex++;
                }
                // Add the word |p->wd| to the graph
                
                if (--nn == 0)
                    goto done;
            }
    }
  done:gb_free(node_blocks);
// Sort and output the words, determining adjacencies

    if (gb_trouble_code) {
        gb_recycle(new_graph);
        panic(alloc_fault); /* oops, we ran out of memory somewhere back there */
    }
    return new_graph;
}

Vertex *find_word(char *q, void (*f) (Vertex *))
/* |*f| should take one argument, of type |Vertex *|, or |f| should be |NULL| */
{
    register char *r; /* previous word possibly adjacent to |q| */
  register Vertex **h; /* hash address for linear probing */
  register long raw_hash; /* five-letter hash code before remaindering */


    raw_hash =
        (((((((ch(q) << 5) + ch(q + 1)) << 5) + ch(q + 2)) << 5) + ch(q + 3)) << 5) + ch(q + 4);
    for (h = htab[0] + (raw_hash - (ch(q) << 20)) % hash_prime; *h; hdown(0)) {
        r = (*h)->name;
        if (mtch(0) && match(1, 2, 3, 4))
            return *h;
    }

    /* Invoke |f| on every vertex that is adjacent to word~|q| */
    if (f) {
        for (h = htab[0] + (raw_hash - (ch(q) << 20)) % hash_prime; *h; hdown(0)) {
            r = (*h)->name;
            if (match(1, 2, 3, 4))
                (*f) (*h);
        }
        for (h = htab[1] + (raw_hash - (ch(q + 1) << 15)) % hash_prime; *h; hdown(1)) {
            r = (*h)->name;
            if (match(0, 2, 3, 4))
                (*f) (*h);
        }
        for (h = htab[2] + (raw_hash - (ch(q + 2) << 10)) % hash_prime; *h; hdown(2)) {
            r = (*h)->name;
            if (match(0, 1, 3, 4))
                (*f) (*h);
        }
        for (h = htab[3] + (raw_hash - (ch(q + 3) << 5)) % hash_prime; *h; hdown(3)) {
            r = (*h)->name;
            if (match(0, 1, 2, 4))
                (*f) (*h);
        }
        for (h = htab[4] + (raw_hash - ch(q + 4)) % hash_prime; *h; hdown(4)) {
            r = (*h)->name;
            if (match(0, 1, 2, 3))
                (*f) (*h);
        }
    }
    return NULL;
}

