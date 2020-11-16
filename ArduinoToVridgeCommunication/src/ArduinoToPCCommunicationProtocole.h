// ArduinoToPCCommunicationProtocole.h

#ifndef _ARDUINOTOPCCOMMUNICATIONPROTOCOLE_h
#define _ARDUINOTOPCCOMMUNICATIONPROTOCOLE_h

#define ATPCCP ArduinoToPCCommunicationProtocole

#include "IMoniteredValue.h"
#include "ErrorCodes.h"

typedef void(*command_t)(BinarySerializer*);

void Handshake(BinarySerializer* args);
void Ping(BinarySerializer* args);
void Activate(BinarySerializer* args);
void Deactivate(BinarySerializer* args);

class ArduinoToPCCommunicationProtocoleClass
{
protected:
	uint8_t m_dataCount;
	uint8_t m_dataCapacity;
	IMoniteredValue** m_monitoredValues;
	command_t* m_commands;

public:
	bool IsActive;

	void init(uint8_t dataCount);
	void Run();
	void Assign(IMoniteredValue* monitoredValue);
};

extern ArduinoToPCCommunicationProtocoleClass ArduinoToPCCommunicationProtocole;

#endif

