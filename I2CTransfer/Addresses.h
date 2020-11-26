#pragma once

#ifndef _ADDRESSES_h
#define _ADDRESSES_h

#include "Arduino.h"

struct Data
{
    byte btns;
    int16_t x;
    int16_t y;
};

#define CTRL_ADDRESS_BUTTONS 0
#define CTRL_ADDRESS_XAXIS 1
#define CTRL_ADDRESS_YAXIS 5
#define CTRL_BUFFER_SIZE sizeof(Data)

#define MASTER_ADDRESS 10
#define LEFT_SLAVE_ADDRESS 9
#define RIGHT_SLAVE_ADDRESS 8


#define PrintTransmitStatus(status)\
switch (status)\
{\
case 1:\
    Serial.println("Data too long");\
    break;\
case 2:\
    Serial.println("Master did not ack");\
    break;\
case 3:\
    Serial.println("Master reg did not ack");\
    break;\
case 4:\
    Serial.println("Other error");\
    break;\
default:\
    break;\
}

#endif
