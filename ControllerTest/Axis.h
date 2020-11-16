#pragma once

#ifndef _AXIS_h
#define _AXIS_h

#if defined(ARDUINO) && ARDUINO >= 100
#include "arduino.h"
#else
#include "WProgram.h"
#endif

class Axis
{
private:
	float m_value;
public:
	Axis(float value);

	void SetValue(float value);
	void SetValueFrom(int value, int maxValue);
	float GetValue();
	int GetValue(int maxValue);
};

#endif