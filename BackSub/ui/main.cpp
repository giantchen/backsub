#include <stdio.h>
#include <vector>

#include "cv.h"
#include "highgui.h"

using namespace std;

void show_image(const char* windowName, IplImage* img)
{
	cvNamedWindow(windowName, CV_WINDOW_AUTOSIZE);
	cvMoveWindow(windowName, 100, 100);
	cvShowImage(windowName, img);
}

void test_show_image()
{
	IplImage* img = NULL;
	
	img = cvLoadImage("E:\\575.jpg",0);
	
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
	CvCapture* cap = cvCaptureFromAVI("E:\\video\\split002.avi");
	if (!cap)
		return;

	IplImage* img = NULL;

	cvNamedWindow("mainWin", CV_WINDOW_AUTOSIZE);
	cvMoveWindow("mainWin", 100, 100);

	cvNamedWindow("subWin", CV_WINDOW_AUTOSIZE);
	cvMoveWindow("subWin", 500, 100);

	cvQueryFrame(cap);
	int frameHeight = (int)cvGetCaptureProperty(cap, CV_CAP_PROP_FRAME_HEIGHT);
	int frameWidth = (int)cvGetCaptureProperty(cap, CV_CAP_PROP_FRAME_WIDTH);
	int nFrames = (int)cvGetCaptureProperty(cap, CV_CAP_PROP_FRAME_COUNT);
	printf("nFrames = %d\n", nFrames);

	//CvMat* average = cvCreateMat(frameHeight, frameWidth, CV_16UC1);
	vector<vector<int> > average(frameHeight, vector<int>(frameWidth, 0));
	const int AVERAGE_FRAMES = 80;

	for (int currFrame = 0; currFrame < nFrames; ++currFrame) {
		if (!cvGrabFrame(cap)) {
			printf("Can not grab a frame\n");
			return;
		}
		img = cvRetrieveFrame(cap);
		IplImage* imgGray = cvCreateImage(cvSize(img->width, img->height), img->depth, 1);
		cvConvertImage(img, imgGray, CV_CVTIMG_FLIP);
		cvShowImage("mainWin", imgGray);

		IplImage* imgAverage = cvCreateImage(cvSize(img->width, img->height), img->depth, 1);
		for (int i = 0; i < img->height; ++i) {
			for (int j = 0; j < img->width; ++j) {
				average[i][j] += ((uchar*)imgGray->imageData)[i*imgGray->widthStep+j];
				//((uchar*)imgAverage->imageData)[i*imgAverage->widthStep+j] = ((uchar*)imgGray->imageData)[i*imgGray->widthStep+j];
			}
		}
		
		cvReleaseImage(&imgGray);

		if (currFrame % AVERAGE_FRAMES == 0) {

			for (int i = 0; i < img->height; ++i) {
				for (int j = 0; j < img->width; ++j) {
					((uchar*)imgAverage->imageData)[i*imgAverage->widthStep+j] = average[i][j] / AVERAGE_FRAMES;
					average[i][j] = 0;
				}
			}

			cvShowImage("subWin", imgAverage);
		}
		cvReleaseImage(&imgAverage);

		int key = cvWaitKey(20);
		if (27 == key)
			break;
	}
	cvReleaseCapture(&cap);
	cvWaitKey(0);
}


void test_element_accessing()
{
	int i=3, j=6;
   IplImage* img = cvCreateImage(cvSize(640,480), IPL_DEPTH_8U,1);
	CvScalar s;
	//img->
	s = cvGet2D(img,i,j);
	printf("intensity = %f\n", s.val[0]);
	s.val[0] = 111;
	cvSet2D(img,i,j,s);
}
void trackbarHandler(int pos)
{
	printf("Trackbar position: %d\n", pos);
}

int main()
{
//	test_show_image();
	test_show_avi();
//   test_element_accessing();
	
}