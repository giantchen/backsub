#include <vector>

class Overflow{};
class Underflow{};

class Queue
{
public:
	explicit Queue(int capacity) :capacity_(capacity)
	{
		array_.resize(capacity);
		front_ = 0;
		back_ = -1;
		size_ = 0;
	}
	
	void Push(int value)
	{
		if (size_ >= capacity_)
			throw Overflow();
		back_++;
        if (back_ == capacity_)
			back_ = 0;
		array_[back_] = value;
		size_++;
	}

	int Pop()
	{
		if (size_ <= 0)
			throw Underflow();
		int value = array_[front_];
		front_++;
		if (front_ == capacity_)
			front_ = 0;
		size_--;
		return value;
	}

	int Size()
	{
		return size_;
	}

	int Get(int n)
	{
		if (n >= size_)
			throw Overflow();
		if (n < 0)
			throw Underflow();
		int i = front_ + n;
		if (i >= capacity_)
			i -= capacity_;
		return array_[i];
	}

private:
	int capacity_;
	std::vector<int> array_;
	int front_, back_, size_;
};