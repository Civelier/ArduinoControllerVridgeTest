#include "Axis.h"

Axis::Axis(float value)
{
	m_value = value;
}

void Axis::SetValue(float value)
{
	m_value = value;
}

void Axis::SetValueFrom(int value, int maxValue)
{
	m_value = value / maxValue;
}

float Axis::GetValue()
{
	return m_value;
}

int Axis::GetValue(int maxValue)
{
	return (int)roundf(m_value * maxValue);
}
