#include <algorithm>

#include "queue.h"
#include "algorithms.h"

using namespace std;

int Average(const Queue& q)
{
	int sum = 0;
	for (int i = 0; i < q.Size(); ++i)
		sum += q.Get(i);
	return sum / q.Size();
}

int Median(const Queue& q)
{
	int size = q.Size();
	
	if (size == 0)
		return 0;
	
	vector<int> buf(size);
	
	for (int i = 0; i < size; ++i)
		buf[i] = q.Get(i);
	
	sort(buf.begin(), buf.end());

	if (size % 2 == 0) {
		return (buf[size/2] + buf[size/2 + 1]) / 2;
	} else {
		return buf[size/2];
	}
}
