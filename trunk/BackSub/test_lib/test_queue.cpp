#include <stdio.h>
#include "queue.h"

int main()
{
	const int capacity = 40;
	Queue queue(capacity);
	for(int i=0; i<capacity; i++)
		queue.Push(i);
	
	for(int i=0; i<20; i++)
	{
		printf("%d\n", queue.Pop());
		queue.Push(i+50);
	}
	int size = queue.Size();
	printf("size = %d\n", size);
	for(int i=0; i<size; i++)
		printf("%d = %d\n", i,queue.Get(i));

	for(int i=0; i<size; i++)
		printf("pop %d = %d\n", i,queue.Pop());
}