/*
 Name:		JoystickTest.ino
 Created:	11/14/2020 10:32:42 PM
 Author:	civel
*/

#include "PinDebugger.h"
#include "AnalogPinDebug.h"
#include "DigitalPinDebug.h"
#include "Arduino.h"

//Btn : P3
//X axis : P4
//Y axis : P5

// the setup function runs once when you press reset or power the board
void setup()
{
	AnalogPinDebug* XAxisDebug = new AnalogPinDebug(A0);
	AnalogPinDebug* YAxisDebug = new AnalogPinDebug(A1);
	DigitalPinDebug* BtnDebug = new DigitalPinDebug(PORTD2);

	PinDebugger.init(3);
	PinDebugger.Track(XAxisDebug);
	PinDebugger.Track(YAxisDebug);
	PinDebugger.Track(BtnDebug);
}

// the loop function runs over and over again until power down or reset
void loop()
{
	PinDebugger.Debug();
	delay(2);
}
