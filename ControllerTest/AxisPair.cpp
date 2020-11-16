#include "AxisPair.h"

size_t AxisPair::Size()
{
	return 2 * sizeof(float);
}

AxisPair::AxisPair()
{
}

AxisPair::AxisPair(float x, float y)
{
	m_x = x;
	m_y = y;
}

AxisPair::AxisPair(ByteArray* data)
{
	Deserialize(data);
}

ByteArray* AxisPair::Serialize()
{
	BinarySerializer* serializer = new BinarySerializer();
	serializer->AddFLoatSize();
	serializer->AddFLoatSize();
	serializer->GenerateArray();

	serializer->Add(m_x);
	serializer->Add(m_y);
	ByteArray* arr = serializer->GetArray();
	delete serializer;
	return arr;
}

void AxisPair::Deserialize(ByteArray* data)
{
	BinarySerializer* serializer = new BinarySerializer(data);
	m_x = serializer->GetFloat();
	m_y = serializer->GetFloat();
	delete serializer;
}

float AxisPair::GetX()
{
	return m_x;
}

int AxisPair::GetX(int xMax)
{
	return (int)roundf(m_x * xMax);
}

float AxisPair::GetY()
{
	return m_y;
}

int AxisPair::GetY(int yMax)
{
	return (int)roundf(m_y * yMax);
}

void AxisPair::SetX(float x)
{
	m_x = x;
}

void AxisPair::SetX(int x, int xMax)
{
	m_x = x / xMax;
}

void AxisPair::SetY(float y)
{
	m_y = y;
}

void AxisPair::SetY(int y, int yMax)
{
	m_y = y / yMax;
}
