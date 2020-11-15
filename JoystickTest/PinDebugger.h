// PinDebugger.h

#ifndef _PINDEBUGGER_h
#define _PINDEBUGGER_h

#include "DebugPin.h"

#if defined(ARDUINO) && ARDUINO >= 100
	#include "arduino.h"
#else
	#include "WProgram.h"
#endif

class PinDebuggerClass
{
protected:
	DebugPin** m_pins_arr;
	uint8_t m_count;
	uint8_t m_capacity;

public:
	void init(uint8_t capacity);
	void Debug();
	void Track(DebugPin* pin);
};

extern PinDebuggerClass PinDebugger;

#endif

