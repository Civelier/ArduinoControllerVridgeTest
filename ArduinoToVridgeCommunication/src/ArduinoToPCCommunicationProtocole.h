// ArduinoToPCCommunicationProtocole.h

#ifndef _ARDUINOTOPCCOMMUNICATIONPROTOCOLE_h
#define _ARDUINOTOPCCOMMUNICATIONPROTOCOLE_h

#define ATPCCP ArduinoToPCCommunicationProtocole

#include "Arduino.h"
#include "IMoniteredValue.h"

class ArduinoToPCCommunicationProtocoleClass
{
protected:
	uint8_t m_dataCount;
	IMoniteredValue** m_monitoredValues;

public:
	void init(uint8_t dataCount);
	void Run();
};

extern ArduinoToPCCommunicationProtocoleClass ArduinoToPCCommunicationProtocole;

#endif

