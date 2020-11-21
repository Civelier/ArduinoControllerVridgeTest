// 
// 
// 

#include "ArduinoToPCCommunicationProtocole.h"

//Command ID : 0
void Handshake(BinarySerializer* args)
{
	byte arr[8] =
	{
		8,
		0,
		DEVICE_TYPE,
		ACCESS_CODE1,
		ACCESS_CODE2,
		PROTOCOL_VERSION_MAJOR,
		PROTOCOL_VERSION_MINOR,
		PROTOCOL_VERSION_PATCH
	};
	Serial.write(arr, 8);
	while (!Serial.availableForWrite());
	DebugLED.Flash(DEBUG_HANDSHAKE_RECIEVED);
}

//Command ID : 1
void Ping(BinarySerializer* args)
{
	ULongByteUnion u = { millis() };
	byte arr[6] = 
	{
		6,
		0,
		u.asBytes[0],
		u.asBytes[1],
		u.asBytes[2],
		u.asBytes[3]
	};
	Serial.write(arr, 6);
	DebugLED.Flash(DEBUG_PING_RECIEVED);
}

//Command ID : 2
void Activate(BinarySerializer* args)
{
	DebugLED.Flash(DEBUG_ACTIVATED);
	ATPCCP.IsActive = true;
}

//Command ID : 3
void Deactivate(BinarySerializer* args)
{
	DebugLED.Flash(DEBUG_DEACTIVATED);
	ATPCCP.IsActive = false;
}

//Command ID : 4
void GetVersion(BinarySerializer* args)
{
	byte arr[5] =
	{
		5,
		0,
		FIRMWARE_VERSION_MAJOR,
		FIRMWARE_VERSION_MINOR,
		FIRMWARE_VERSION_PATCH
	};
	Serial.write(arr, 5);
}

ArduinoToPCCommunicationProtocoleClass ArduinoToPCCommunicationProtocole;

void ArduinoToPCCommunicationProtocoleClass::init(uint8_t dataCount)
{
	m_dataCapacity = dataCount;
	m_monitoredValues = (IMoniteredValue**)malloc(dataCount * sizeof(IMoniteredValue*));
	m_commands = new command_t[5]
	{
		Handshake,
		Ping,
		Activate,
		Deactivate,
		GetVersion
	};
}

void ArduinoToPCCommunicationProtocoleClass::Run()
{
	if (Serial.available())
	{
		DebugLED.Flash(DEBUG_MESSAGE_RECIEVED);
		byte arr[2];
		Serial.readBytes(arr, 2);
		byte length = arr[0] - 2;
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
