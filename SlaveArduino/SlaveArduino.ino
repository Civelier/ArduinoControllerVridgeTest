/*
 Name:		SlaveArduino.ino
 Created:	11/25/2020 10:38:36 AM
 Author:	civel
*/

#include <Wire.h>;

#define LED 13

int x = 0;
// the setup function runs once when you press reset or power the board
void setup()
{
	pinMode(LED, OUTPUT);
	Wire.begin(9);
	Wire.onRequest(requestEvent);
	digitalWrite(LED, HIGH);
	delay(2000);
	digitalWrite(LED, LOW);
}

void requestEvent()
{
	int count = Wire.read();
	
}

// the loop function runs over and over again until power down or reset
void loop()
{
	if (x == 0)
	{
		digitalWrite(LED, HIGH);
		delay(200);
		digitalWrite(LED, LOW);
		delay(200);
	}

	if (x == 3) {
		digitalWrite(LED, HIGH);
		delay(400);
		digitalWrite(LED, LOW);
		delay(400);
	}
}
