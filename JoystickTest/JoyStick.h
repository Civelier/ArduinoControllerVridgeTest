#pragma once

#ifndef _JOYSTICK_h
#define _JOYSTICK_h

#if defined(ARDUINO) && ARDUINO >= 100
#include "arduino.h"
#else
#include "WProgram.h"
#endif

#include "AxisPair.h"
#include "IMoniteredValue.h"

class JoyStick : public virtual IMoniteredValue
{
private:
	uint8_t m_buttonPin;
	uint8_t m_xAxisPin;
	uint8_t m_yAxisPin;
	int m_lastX;
	int m_lastY;
	AxisPair m_axies;
	bool m_lastButtonState;
public:
	bool HasChanged;

	JoyStick(uint8_t buttonPin, uint8_t xAxisPin, uint8_t yAxisPin);

	virtual bool HasChanged() override;
	virtual ByteArray* Serialize() override;
	virtual void Deserialize(ByteArray* data) override {};
};

#endif