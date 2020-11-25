/*
 Name:		MasterArduino.ino
 Created:	11/25/2020 10:34:37 AM
 Author:	civel
*/

#include <Wire.h>


#define LEFT_SLAVE_ADDRESS 9
#define RIGHT_SLAVE_ADDRESS 9
#define LED 13
int x = 0;

// the setup function runs once when you press reset or power the board
void setup()
{
	Wire.begin();
	pinMode(LED, OUTPUT);
	digitalWrite(LED, HIGH);
	delay(1000);
	digitalWrite(LED, LOW);
}

// the loop function runs over and over again until power down or reset
void loop()
{
	Wire.beginTransmission(LEFT_SLAVE_ADDRESS);
	Wire.write(x);
	Wire.endTransmission();
	x = (x + 1) % 6;
	delay(500);
}
