#include <assert.h>
#include "queue.h"
#include "algorithms.h"

class FrameQueue
{
public:
	FrameQueue(int capacity, int width, int height)
		: capacity_(capacity), size_(0), 
		frames_(height * width, Queue(capacity))	
	{}
    
	void Enqueue(const std::vector<int>& frame)
	{
        assert(frame.size() == frames_.size());

		if (size_ == capacity_) {
			for (size_t i = 0; i < frames_.size(); i++)
				frames_[i].Pop();
		} else {
			size_++;
		}
		for (size_t i = 0; i < frames_.size(); i++)
		{
			frames_[i].Push(frame[i]);
		}
		
	}
	
	std::vector<int> Apply(Operation op)
	{
		std::vector<int> result(frames_.size());

		for (size_t i = 0; i < frames_.size(); i++)
			result[i] = op(frames_[i]);

		return result;
	}

private:
	int capacity_, size_;
	std::vector<Queue> frames_;
};