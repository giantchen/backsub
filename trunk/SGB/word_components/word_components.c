#include "gb_graph.h" /* the GraphBase data structures */
#include "gb_words.h" /* the |words| routine */

/*
This simple demonstration program computes the connected
components of the GraphBase graph of five-letter words. It prints the
words in order of decreasing weight, showing the number of edges,
components, and isolated vertices present in the graph defined by the
first $n$ words for all~$n$.
*/

#define link z.V  /* link to next vertex in component (occupies utility field |z|) */
#define master y.V /* pointer to master vertex in component */
#define size x.I  /* size of component, kept up to date for master vertices only */

int main(void)
{
    Graph *g = words(0L, 0L, 0L, 0L); /* the graph we love */
    Vertex *v; /* the current vertex being added to the component structure */
    Arc *a;  /* the current arc of interest */
    long n = 0; /* the number of vertices in the component structure */
    long isol = 0; /* the number of isolated vertices in the component structure */
    long comp = 0;  /* the current number of components */
    long m = 0; /* the current number of edges */

    printf("Component analysis of %s\n", g->id);
    for (v = g->vertices; v < g->vertices + g->n; v++) {
        n++, printf("%4ld: %5ld %s", n, v->weight, v->name);

        /* Add vertex |v| to the component structure, printing out any components it joins */

        /* Make |v| a component all by itself */
        {
          v->link = v;
          v->master = v;
          v->size = 1;
          isol++;
          comp++;
        }
        
        a = v->arcs;
        while (a && a->tip > v)
            a = a->next;
        if (!a) printf("[1]"); /* indicate that this word is isolated */
        else {
            long c = 0;/* the number of merge steps performed because of |v| */

            for (; a; a = a->next) {
                register Vertex *u = a->tip;
                m++;
                
                /* Merge the components of |u| and |v|, if they differ */
                u = u->master;
                if (u != v->master) {
                    register Vertex *w = v->master, *t;

                    if (u->size < w->size) {
                        if (c++ > 0)
                            printf("%s %s[%ld]", (c == 2 ? " with" : ","), u->name, u->size);
                        w->size += u->size;
                        if (u->size == 1)
                            isol--;
                        for (t = u->link; t != u; t = t->link)
                            t->master = w;
                        u->master = w;
                    } else {
                        if (c++ > 0)
                            printf("%s %s[%ld]", (c == 2 ? " with" : ","), w->name, w->size);
                        if (u->size == 1)
                            isol--;
                        u->size += w->size;
                        if (w->size == 1)
                            isol--;
                        for (t = w->link; t != w; t = t->link)
                            t->master = u;
                        w->master = u;
                    }
                    t = u->link;
                    u->link = w->link;
                    w->link = t;
                    comp--;
                }
            }
            printf(" in %s[%ld]", v->master->name, v->master->size);
            /* show final component */
        }
        printf("; c=%ld,i=%ld,m=%ld\n", comp, isol, m);
    }

    /*Display all unusual components*/

    printf("\nThe following non-isolated words didn't join the giant component:\n");
    for (v = g->vertices; v < g->vertices + g->n; v++)
        if (v->master == v && v->size > 1 && v->size + v->size < g->n) {
            register Vertex *u;
            long c = 1; /* count of number printed on current line */

            printf("%s", v->name);
            for (u = v->link; u != v; u = u->link) {
                if (c++ == 12)
                    putchar('\n'), c = 1;
                printf(" %s", u->name);
            }
            putchar('\n');
        }

    return 0;
}
