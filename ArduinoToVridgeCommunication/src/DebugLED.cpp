// 
// 
// 

#include "DebugLED.h"

void DebugLEDClass::init()
{
	pinMode(13, OUTPUT);
}

void DebugLEDClass::Flash(byte code)
{
#if DEBUG_LEVEL > 0
	bool lastBit;
	for (size_t i = 0; i < 8; i++)
	{
		bool bit = bitRead(code, i);
		digitalWrite(13, bit);
		if (lastBit && bit) delay(200);
		else delay(100);
		lastBit = bitRead(code, i);
	}
	digitalWrite(13, 0);
#endif
}


DebugLEDClass DebugLED;

