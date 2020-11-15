#include "DigitalPinDebug.h"

int DigitalPinDebug::GetValue()
{
    return (bool)digitalRead(Pin);
}

DigitalPinDebug::DigitalPinDebug(int pin)
{
    Pin = pin;
    pinMode(pin, INPUT);
}

void DigitalPinDebug::PrintValue()
{
    Serial.print("A Pin : ");
    Serial.print(Pin);
    Serial.print("\tValue : ");
    Serial.println(GetValue());
}
