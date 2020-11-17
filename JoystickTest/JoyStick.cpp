#include "JoyStick.h"

JoyStick::JoyStick(uint8_t buttonPin, uint8_t xAxisPin, uint8_t yAxisPin)
{
	m_buttonPin = buttonPin;
	m_xAxisPin = xAxisPin;
	m_yAxisPin = yAxisPin;
}

bool JoyStick::HasChanged()
{
	int x = analogRead(m_xAxisPin);
	int y = analogRead(m_yAxisPin);
	bool btn = digitalRead(m_buttonPin);
	if (m_lastX != x)
	{
		m_lastX = x;
		m_hasChanged = true;
	}
	if (m_lastY != y)
	{
		m_lastY = y;
		m_hasChanged = true;
	}
	if (m_lastButtonState != btn)
	{
		m_lastButtonState = btn;
		m_hasChanged = true;
	}
	return m_hasChanged;
}

ByteArray* JoyStick::Serialize()
{
	return nullptr;
}
