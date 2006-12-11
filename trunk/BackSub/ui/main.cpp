#include <stdio.h>

#include "cv.h"
#include "highgui.h"

void show_image(const char* windowName, IplImage* img)
{
	cvNamedWindow(windowName, CV_WINDOW_AUTOSIZE);
	cvMoveWindow(windowName, 100, 100);
	cvShowImage(windowName, img);
}

void test_show_image()
{
	IplImage* img = NULL;
	
	img = cvLoadImage("E:\\575.jpg");
	
	if (!img) {
		printf("Open image failed.\n");
		return;
	}
	show_image("mainWin", img);
	cvWaitKey(0);
	cvReleaseImage(&img);
}

void test_show_avi()
{
	CvCapture* cap = cvCaptureFromAVI("E:\\split020_001.avi");

	IplImage* img = NULL;
	cvNamedWindow("mainWin", CV_WINDOW_AUTOSIZE);
	cvMoveWindow("mainWin", 100, 100);

	cvNamedWindow("subWin", CV_WINDOW_AUTOSIZE);
	cvMoveWindow("subWin", 500, 100);

	int nFrames = (int)cvGetCaptureProperty(cap, CV_CAP_PROP_FRAME_COUNT);
	for (int i = 0; i < nFrames; ++i) {
		if (!cvGrabFrame(cap)) {
			printf("Can not grab a grame\n");
			return;
		}
		img = cvRetrieveFrame(cap);
		cvShowImage("mainWin", img);
		if (i % 25 == 0) {
			cvShowImage("subWin", img);
		}
		cvWaitKey(40);
	}
	cvReleaseCapture(&cap);
	cvWaitKey(0);
}

int main()
{
	test_show_image();
}