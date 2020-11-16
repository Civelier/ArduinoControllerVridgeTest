// ArduinoToPCCommunicationProtocole.h

#ifndef _ARDUINOTOPCCOMMUNICATIONPROTOCOLE_h
#define _ARDUINOTOPCCOMMUNICATIONPROTOCOLE_h

#define ATPCCP ArduinoToPCCommunicationProtocole

#if defined(ARDUINO) && ARDUINO >= 100
	#include "arduino.h"
#else
	#include "WProgram.h"
#endif

class ArduinoToPCCommunicationProtocoleClass
{
protected:
	uint8_t m_dataCount;


public:
	void init(uint8_t dataCount);
	void Run();
};

extern ArduinoToPCCommunicationProtocoleClass ArduinoToPCCommunicationProtocole;

#endif

