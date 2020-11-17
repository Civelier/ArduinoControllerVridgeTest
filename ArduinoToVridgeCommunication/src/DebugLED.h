// DebugLED.h

#ifndef _DEBUGLED_h
#define _DEBUGLED_h

#include "Definitions.h"

#if defined(ARDUINO) && ARDUINO >= 100
	#include "arduino.h"
#else
	#include "WProgram.h"
#endif

class DebugLEDClass
{
protected:


public:
	void init();
	void Flash(byte code);
};

extern DebugLEDClass DebugLED;

#endif

