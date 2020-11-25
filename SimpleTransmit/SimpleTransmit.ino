/*
 Name:		SimpleTransmit.ino
 Created:	11/19/2020 7:12:25 PM
 Author:	civel
*/

//#include <Wire.h>

#include <I2Cdev.h>
#include <MPU6050.h>
#include <helper_3dmath.h>
#include "Arduino.h"


#define BUTTONS_START PIND2
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

class Data
{
public:
	bool Button1;
	bool Button2;
	bool Button3;
	bool Button4;
	bool Stick;
	int XStick;
	int YStick;
};

#define DEBUG

MPU6050 MPU = MPU6050();

void serialEvent()
{
#ifndef DEBUG
	int b = Serial.read();

	if (b == 2)
	{
		MPU.CalibrateAccel();
		MPU.CalibrateGyro();
	}
	else
	{
		UpdateValues();
		PrintValues();
		delay(5);
		Serial.flush();
	}
#else
	String s = Serial.readString();
	if (s == "Calibrate")
	{
		MPU.CalibrateAccel();
		Serial.println("Accel calibrated");
		MPU.CalibrateGyro();
		Serial.println("Gyro calibrated");
	}
	if (s == "ReadPos")
	{
		Serial.println(MPU.Get)
	}
#endif // DEBUG
}


// the setup function runs once when you press reset or power the board
void setup()
{
	Serial.begin(115200);
	for (size_t i = 0; i < 5; i++)
	{
		pinMode(BUTTONS_START + i, INPUT);
	}
	pinMode(X_AXIS_PIN, INPUT);
	pinMode(Y_AXIS_PIN, INPUT);

	MPU.initialize();
	MPU.setDMPEnabled(true);
}

//byte data[MSG_TOTAL_LEN];

Data* data = new Data();

void UpdateValues()
{
	/*ButtonsToByte();
	Serial.print(analogRead(X_AXIS_PIN));
	Serial.print(';');
	Serial.print(analogRead(Y_AXIS_PIN));
	Serial.println();*/
	data->Button1 = digitalRead(BUTTONS_START);
	data->Button2 = digitalRead(BUTTONS_START + 1);
	data->Button3 = digitalRead(BUTTONS_START + 2);
	data->Button4 = digitalRead(BUTTONS_START + 3);
	data->Stick = !digitalRead(BUTTONS_START + 4);
	data->XStick = analogRead(X_AXIS_PIN);
	data->YStick = analogRead(Y_AXIS_PIN);

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
	byte* buffer = (byte*)data;
	Serial.write(buffer, 9);
	//for (size_t i = 0; i < sizeof(*data); i++)
	//{
	//	//Serial.print(buffer[i]);
	//	//Serial.print(" ");
	//}
	//Serial.println();
}

// the loop function runs over and over again until power down or reset
void loop()
{
	//Serial.write(data, 10);
}
