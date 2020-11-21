/*
 Name:		SimpleTransmit.ino
 Created:	11/19/2020 7:12:25 PM
 Author:	civel
*/

#include <Wire.h>
#include "Arduino.h"

#define BUTTONS_START 2
#define X_AXIS_PIN A0
#define Y_AXIS_PIN A1

#define MSG_START 0
#define MSG_DATA 1
#define MSG_STOP 2
#define MSG_START_LEN 1
#define MSG_DATA_LEN 9
#define MSG_STOP_LEN 0
#define MSG_TOTAL_LEN (MSG_START_LEN + MSG_DATA_LEN + MSG_STOP_LEN)
#define MessageSection(section) section == MSG_START ? 0 : section == MSG_DATA ? MSG_START_LEN : section == MSG_STOP ? MSG_START_LEN + MSG_DATA_LEN : -1

union IntByte
{
	byte buffer[4];
	int value;
};

// the setup function runs once when you press reset or power the board
void setup()
{
	Serial.begin(115200);
	for (size_t i = 0; i < 4; i++)
	{
		pinMode(BUTTONS_START + i, INPUT);
	}
	pinMode(X_AXIS_PIN, INPUT);
	pinMode(Y_AXIS_PIN, INPUT);
}

byte data[MSG_TOTAL_LEN];

union BoolByte
{
	bool bits[8];
	byte value;
};

void ButtonsToByte()
{
	for (size_t i = 0; i < 4; i++)
	{
		Serial.print(digitalRead(BUTTONS_START + i));
		Serial.print(';');
	}
}

void UpdateValues()
{
	ButtonsToByte();
	Serial.print(analogRead(X_AXIS_PIN));
	Serial.print(';');
	Serial.print(analogRead(Y_AXIS_PIN));
	Serial.println();
	/*IntByte x = { analogRead(X_AXIS_PIN) };
	IntByte y = { analogRead(Y_AXIS_PIN) };

	for (size_t i = 0; i < 4; i++)
	{
		data[MessageSection(MSG_DATA) + 1 + i] = x.buffer[i];
		data[MessageSection(MSG_DATA) + 5 + i] = y.buffer[i];
	}*/
}

void PrintValues()
{
	for (size_t i = 0; i < MSG_TOTAL_LEN; i++)
	{
		Serial.print(data[i]);
		Serial.print(" ");
	}
	Serial.println();
}

// the loop function runs over and over again until power down or reset
void loop()
{
	UpdateValues();
	//PrintValues();
	//Serial.write(data, 10);
	delay(10);
}
