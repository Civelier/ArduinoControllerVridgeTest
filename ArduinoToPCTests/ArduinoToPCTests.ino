/*
 Name:		ArduinoToPCTests.ino
 Created:	11/15/2020 6:13:59 PM
 Author:	civel
*/

#define TEST
//#define _DEBUG

#include "ArduinoToPCCommunicationProtocole.h"

// the setup function runs once when you press reset or power the board
void setup()
{
	Serial.begin(115200);
	ATPCCP.init(0);
	DebugLED.init();
	DebugLED.Flash(0b10110011);
}

// the loop function runs over and over again until power down or reset
void loop()
{
	ATPCCP.Run();
}
