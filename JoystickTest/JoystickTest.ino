/*
 Name:		JoystickTest.ino
 Created:	11/14/2020 10:32:42 PM
 Author:	civel
*/

#include "Arduino.h"
#include "ArduinoToPCCommunicationProtocole.h"

//Btn : P3
//X axis : P4
//Y axis : P5

// the setup function runs once when you press reset or power the board
void setup()
{
	Serial.begin(9600);
	ATPCCP.init(0);
	pinMode(13, OUTPUT);
}

// the loop function runs over and over again until power down or reset
void loop()
{
	ATPCCP.Run();
	delay(1000);
}
