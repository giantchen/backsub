#include <algorithm>

#include "queue.h"
#include "algorithms.h"
#include "math.h"

using namespace std;

/*
  Ahmed Elgammal 'Non-parametric Model for Background Subtraction'
  用非参数模型估计背景
  parzen窗估计，选窗函数为正态窗函数
*/

int NonPara( const Queue& q)
{
	int size = q.Size();
	if( size == 0 )
		return 0;
	else if( size == 1)
		return q.Get(0);

	int cur = q.Get(0);    //current frame
	
	vector<int> buf(size-1);
	for(int i = 0; i < (size-1); i++)
		buf[i] = q.Get(i+1);

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
	vector<int> subcur(size-1);
	const double pi = 3.1415926;
	double sum = 0;
	for( int i = 0; i < (size-1); i++)
	{
		subcur[i] = ( cur - buf[i] ) * ( cur - buf[i] );
		double temp1 = exp( -subcur[i]/dev/dev/2);
		double temp2 = temp1/sqrt(2 * pi)/dev;
		sum += temp2;
	}

	//return the Pr(xt)
	return (int) sum/(size-1);	

}