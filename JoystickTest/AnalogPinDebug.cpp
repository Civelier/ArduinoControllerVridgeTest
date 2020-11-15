// 
// 
// 

#include "AnalogPinDebug.h"

int AnalogPinDebug::GetValue()
{
	return analogRead(Pin);
}

AnalogPinDebug::AnalogPinDebug(int pin)
{
	Pin = pin;
	pinMode(Pin, INPUT);
}

void AnalogPinDebug::Debug()
{
	int treshold = 2;
	int v = GetValue();
	if ((m_lastValue - treshold) > v || v > (m_lastValue + treshold))
	{
		m_lastValue = v;
		PrintValue();
	}
}

void AnalogPinDebug::PrintValue()
{
	Serial.print("A Pin : ");
	Serial.print(Pin);
	Serial.print("\tValue : ");
	Serial.println(GetValue());
}
