/*
 Name:		SlaveArduino.ino
 Created:	11/25/2020 10:38:36 AM
 Author:	civel
*/

#include <Wire.h>
#include "Addresses.h"

#define ADDRESS LEFT_SLAVE_ADDRESS

#define BUTTONS_START 2
#define X_AXIS_PIN A0
#define Y_AXIS_PIN A1

typedef byte* buffer_t;

buffer_t Buffer = new byte[CTRL_BUFFER_SIZE];
Data* data = new Data();
uint8_t wireStatus;

void ToByte()
{
    data->btns = 0;
    for (int i = 0; i < 8; ++i)
        if (digitalRead(BUTTONS_START + i))
            data->btns |= 1 << i;
}

#define PrintSizeof(type) Serial.print(#type);\
Serial.print(": ");\
Serial.println(sizeof(type))

void Update()
{
    ToByte();
    data->x = analogRead(X_AXIS_PIN);
    data->y = analogRead(Y_AXIS_PIN);
    Buffer = (buffer_t)data;
}

void requestEvent()
{
    Update();
    //Wire.beginTransmission(MASTER_ADDRESS);
    Wire.write(Buffer, CTRL_BUFFER_SIZE);
    //wireStatus = Wire.endTransmission();
    //TransmitionSuccess(wireStatus);
}

// the setup function runs once when you press reset or power the board
void setup()
{
    Wire.begin(RIGHT_SLAVE_ADDRESS);
    Serial.begin(115200);
    Wire.onRequest(requestEvent);
    PrintSizeof(Data);
    //PrintSizeof(int8_t);
    //PrintSizeof(int16_t);
    //PrintSizeof(int32_t);
    //PrintSizeof(int64_t);
    delay(1000);

    for (size_t i = 0; i < 5; i++)
    {
        pinMode(BUTTONS_START + i, INPUT);
    }
    pinMode(X_AXIS_PIN, INPUT);
    pinMode(Y_AXIS_PIN, INPUT);
}

void Print()
{
    Data* d = (Data*)Buffer;
    Serial.print(d->x);
    Serial.print("\t");
    Serial.print(d->y);
    Serial.print("\t");
    Serial.println((int)(d->btns));
    for (size_t i = 0; i < sizeof(Data); i++)
    {
        Serial.print(Buffer[i]);
        Serial.print("\t");
    }
    Serial.println();
}

// the loop function runs over and over again until power down or reset
void loop()
{
    //Update();
    //Print();
    //delay(100);
}
