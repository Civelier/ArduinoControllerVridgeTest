// 
// 
// 

#include "ArduinoToPCCommunicationProtocole.h"



ArduinoToPCCommunicationProtocoleClass ArduinoToPCCommunicationProtocole;

void ArduinoToPCCommunicationProtocoleClass::init(uint8_t dataCount)
{
	m_dataCapacity = dataCount;
	m_monitoredValues = (IMoniteredValue**)malloc(dataCount * sizeof(IMoniteredValue*));
}

void ArduinoToPCCommunicationProtocoleClass::Run()
{
	if (Serial.available())
	{
		int r = Serial.write()
	}

	BinarySerializer* serializer = new BinarySerializer();
	for (size_t i = 0; i < m_dataCapacity; i++)
	{
		if (m_monitoredValues[i]->HasChanged())
		{
			serializer->AddSize(m_monitoredValues[i]);
		}
	}
	serializer->GenerateArray();
	for (size_t i = 0; i < m_dataCapacity; i++)
	{
		if (m_monitoredValues[i]->HasChanged())
		{
			serializer->Add(m_monitoredValues[i]);
		}
	}
}

void ArduinoToPCCommunicationProtocoleClass::Assign(IMoniteredValue* monitoredValue)
{
	if (m_dataCount >= m_dataCapacity)
	{
		SendError(ERR_OUT_OF_RANGE);
		return;
	}
	m_monitoredValues[m_dataCount] = monitoredValue;
	m_dataCount++;
}
