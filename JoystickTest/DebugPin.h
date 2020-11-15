#pragma once

#ifndef _DEBUGPIN_h
#define _DEBUGPIN_h

#if defined(ARDUINO) && ARDUINO >= 100
#include "arduino.h"
#else
#include "WProgram.h"
#endif

class DebugPin
{
protected:
	int m_lastValue;
	virtual int GetValue() { }
public:
	int Pin;
	virtual void Debug()
	{
		int v = GetValue();
		if (m_lastValue != v)
		{
			m_lastValue = v;
			PrintValue();
		}
	}
	virtual void PrintValue() { };
};

#endif

