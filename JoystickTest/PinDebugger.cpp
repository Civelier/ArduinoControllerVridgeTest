// 
// 
// 

#include "PinDebugger.h"

void PinDebuggerClass::init(uint8_t capacity)
{
	m_capacity = capacity;
	m_pins_arr = (DebugPin**)malloc(capacity * sizeof(DebugPin*));
}

void PinDebuggerClass::Debug()
{
	for (size_t i = 0; i < m_count; i++)
	{
		m_pins_arr[i]->	Debug();
	}
}

void PinDebuggerClass::Track(DebugPin* pin)
{
	m_pins_arr[m_count] = pin;
	Serial.print("Tracking pin : ");
	Serial.println((int)pin->Pin);
	m_count++;
}


PinDebuggerClass PinDebugger;

