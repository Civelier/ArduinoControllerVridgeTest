// ArduinoToPCCommunicationProtocole.h

#ifndef _ARDUINOTOPCCOMMUNICATIONPROTOCOLE_h
#define _ARDUINOTOPCCOMMUNICATIONPROTOCOLE_h

#define ATPCCP ArduinoToPCCommunicationProtocole

#include "Arduino.h"
#include "IMoniteredValue.h"
#include "ErrorCodes.h"

class ArduinoToPCCommunicationProtocoleClass
{
protected:
	uint8_t m_dataCount;
	uint8_t m_dataCapacity;
	IMoniteredValue** m_monitoredValues;

public:
	void init(uint8_t dataCount);
	void Run();
	void Assign(IMoniteredValue* monitoredValue);
};

extern ArduinoToPCCommunicationProtocoleClass ArduinoToPCCommunicationProtocole;

#endif

