/*
 Name:		MasterArduino.ino
 Created:	11/25/2020 10:34:37 AM
 Author:	civel
*/

#include <Wire.h>
#include "Addresses.h"
#include "I2CController.h"


uint8_t sender;
size_t writeIndex = 0;
byte* buffer = new byte[CTRL_BUFFER_SIZE];
bool TransmitDone = true;

#define PRINT_METHOD_READABLE 0
#define PRINT_METHOD_DATA 1

#define PRINT_METHOD PRINT_METHOD_DATA

void Print()
{
	Data* d = (Data*)buffer;
	Serial.print(d->x);
	Serial.print("\t");
	Serial.print(d->y);
	Serial.print("\t");
	Serial.println((int)(d->btns));
	for (size_t i = 0; i < sizeof(Data); i++)
	{
		Serial.print(buffer[i]);
		Serial.print("\t");
	}
	Serial.println();
}

void requestEvent(int count)
{
	while (Wire.available() > 0)
	{
		buffer[writeIndex] = (byte)Wire.read();
		writeIndex++;
		if (writeIndex >= CTRL_BUFFER_SIZE)
		{
#if PRINT_METHOD == PRINT_METHOD_DATA
			Serial.write(buffer, CTRL_BUFFER_SIZE);
#endif
#if PRINT_METHOD == PRINT_METHOD_READABLE
			Print();
#endif
			TransmitDone = true;
			writeIndex = 0;
			break;
		}
	}
	while (Wire.available() > 0) Wire.read();
}


const byte sig[5] =
{
	12,
	35,
	253,
	95,
	129
};

#if PRINT_METHOD == PRINT_METHOD_DATA
void serialEvent()
{
	int b = Serial.read();
	if (b == 1) Serial.write(buffer, CTRL_BUFFER_SIZE);
	if (b == 2) Serial.write(sig, 5);
	while (Serial.available()) Serial.read();
}
#endif

// the setup function runs once when you press reset or power the board
void setup()
{
	Serial.begin(115200);
	Serial.println("Hello");
	Wire.begin();
	Wire.setClock(400000);
	//Wire.onReceive(requestEvent);
}

// the loop function runs over and over again until power down or reset
void loop()
{
	while (!TransmitDone) delay(10);
	TransmitDone = false;
	sender = RIGHT_SLAVE_ADDRESS;
	Wire.requestFrom((byte)RIGHT_SLAVE_ADDRESS, CTRL_BUFFER_SIZE);

	while (Wire.available() > 0)
	{
		buffer[writeIndex] = (byte)Wire.read();
		writeIndex++;
		if (writeIndex >= CTRL_BUFFER_SIZE)
		{
#if PRINT_METHOD == PRINT_METHOD_READABLE
			Print();
			delay(100);
#endif
			TransmitDone = true;
			writeIndex = 0;
			break;
		}
	}
	while (Wire.available() > 0) Wire.read();
}
