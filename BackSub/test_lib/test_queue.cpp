#include <stdio.h>
//#include "queue.h"
#include "FrameQueue.h"

using namespace std;

int Display(const Queue& q)
{
	if (q.Size() > 0)
		printf("%d(%d), ", q.Size(), q.Get(0));
	else 
		printf("%d(__), ", q.Size());
	return 0;
}

void test_frame_queue()
{
	const int capacity = 40;

	FrameQueue fq(capacity, 3, 4);

	vector<int> frame(3*4);
	for (int i = 0; i < frame.size(); ++i) 
		frame[i] = i+1;
	
	fq.Apply(Display);

	fq.Enqueue(frame);
	printf("\n");
	fq.Apply(Display);
	
	for (int i = 0; i < 30; ++i)
		fq.Enqueue(frame);
	
	printf("\n");
	fq.Apply(Display);

	for (int i = 0; i < 30; ++i)
		fq.Enqueue(frame);
	
	printf("\n");
	fq.Apply(Display);
	fq.Apply(Average);
	fq.Apply(Median);
}

void test_queue()
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

int main()
{
	test_frame_queue();
}