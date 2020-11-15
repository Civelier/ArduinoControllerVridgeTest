#pragma once

#ifndef _DIGITALPINDEBUG_h
#define _DIGITALPINDEBUG_h

#include "DebugPin.h"
class DigitalPinDebug : public virtual DebugPin
{
protected:
	virtual int GetValue() override;
public:
	DigitalPinDebug(int pin);
	virtual void PrintValue() override;
};

#endif