// AnalogPinDebug.h

#ifndef _ANALOGPINDEBUG_h
#define _ANALOGPINDEBUG_h

#include "DebugPin.h"

#if defined(ARDUINO) && ARDUINO >= 100
	#include "arduino.h"
#else
	#include "WProgram.h"
#endif

class AnalogPinDebug : public virtual DebugPin
{
protected:
	virtual int GetValue() override;
public:
	AnalogPinDebug(int pin);
	virtual void Debug();
	virtual void PrintValue() override;
};

#endif

