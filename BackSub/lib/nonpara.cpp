#include <algorithm>

#include "queue.h"
#include "algorithms.h"
#include "math.h"

using namespace std;

/*
  Ahmed Elgammal 'Non-parametric Model for Background Subtraction'
  �÷ǲ���ģ�͹��Ʊ���
  parzen�����ƣ�ѡ������Ϊ��̬������
*/

int NonPara( const Queue& q)
{
	int size = q.Size();
	if( size == 0 )
		return 0;
	else if( size == 1)
		return q.Get(0);
	else if( size == 2)
		return (q.Get(0) + q.Get(1)) / 2;

	int cur = q.Get(size-1);    //current frame
	
	vector<int> buf(size-1);
	for(int i = 0; i < (size-1); i++)
		buf[i] = q.Get(i);

	//calculate the median of |x(i) - x(i-1)|
	vector<int> sub(size-2);
	for( int i = 0; i < (size-2); i++)
		sub[i] = abs(buf[i] - buf[i+1]);
    
	sort(sub.begin(), sub.end());
	double m;
	if (size % 2 == 0) {
		m = (sub[size/2 - 1] + sub[size/2]) / 2;
	} else {
		m = sub[size/2 - 1];
	}

	double dev = m / ( 0.68 * sqrt(2.0) );

    //calculate |xt - xi|^2
	int subcur;
	const double pi = 3.1415926;
	double sum = 0;
	for( int i = 0; i < (size-1); i++)
	{
		subcur = ( cur - buf[i] ) * ( cur - buf[i] );
		double temp1 = exp( -subcur/dev/dev/2);
		double temp2 = temp1/sqrt(2 * pi)/dev;
		sum += temp2;
	}

	//return the Pr(xt)
	return (int) sum / (size-1) * 255;	
}