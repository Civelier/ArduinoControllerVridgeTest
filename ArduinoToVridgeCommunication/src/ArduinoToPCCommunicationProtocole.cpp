// 
// 
// 

#include "ArduinoToPCCommunicationProtocole.h"



ArduinoToPCCommunicationProtocoleClass ArduinoToPCCommunicationProtocole;

void ArduinoToPCCommunicationProtocoleClass::init(uint8_t dataCount)
{
	m_dataCount = dataCount;
	m_monitoredValues = (IMoniteredValue**)malloc(dataCount * sizeof(IMoniteredValue*));
}
