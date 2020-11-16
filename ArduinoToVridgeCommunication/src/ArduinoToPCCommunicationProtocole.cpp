// 
// 
// 

#include "ArduinoToPCCommunicationProtocole.h"

//Command ID : 0
void Handshake(BinarySerializer* args)
{
	ByteArray* arr = new ByteArray(2);
	arr->Append((byte)0);
	arr->Append(DEVICE_TYPE);
	SendToPC(arr);
	delete arr;
}

//Command ID : 1
void Ping(BinarySerializer* args)
{
	BinarySerializer serializer = BinarySerializer();
	serializer.AddLongSize();
	serializer.GenerateArray();
	serializer.Add(millis());
	ByteArray* arr = serializer.GetArray();
	SendToPC(arr);
	delete arr;
}

//Command ID : 2
void Activate(BinarySerializer* args)
{
	ATPCCP.IsActive = true;
}

//Command ID : 3
void Deactivate(BinarySerializer* args)
{
	ATPCCP.IsActive = false;
}

ArduinoToPCCommunicationProtocoleClass ArduinoToPCCommunicationProtocole;

void ArduinoToPCCommunicationProtocoleClass::init(uint8_t dataCount)
{
	m_dataCapacity = dataCount;
	m_monitoredValues = (IMoniteredValue**)malloc(dataCount * sizeof(IMoniteredValue*));
	m_commands = new command_t[4]
	{
		Handshake,
		Ping,
		Activate,
		Deactivate
	};
}

void ArduinoToPCCommunicationProtocoleClass::Run()
{
	if (Serial.available())
	{
		digitalWrite(13, 1);
		delay(200);
		digitalWrite(13, 0);
		byte arr[2];
		Serial.readBytes(arr, 2);
		byte length = arr[0];
		byte funcID = arr[1];

		ByteArray* data = new ByteArray(length);
		for (size_t i = 0; i < length; i++)
		{
			data[i] = Serial.read();
		}
		BinarySerializer* deserializer = length > 0 ? new BinarySerializer(data) : nullptr;

		command_t cmd = m_commands[funcID];

		if (cmd == nullptr) SendError(ERR_INVALID_COMMAND)
		else cmd(deserializer);

		if (length > 0 ) delete deserializer;
		delete data;
	}

	if (IsActive)
	{
		BinarySerializer* serializer = new BinarySerializer();
		serializer->AddLongSize();
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
		serializer->Add(millis());
		ByteArray* arr = serializer->GetArray();
		arr->Print();
		delete serializer;
		delete arr;
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
