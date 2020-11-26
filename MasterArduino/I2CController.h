#pragma once

#ifndef _I2CCONTROLLER_h
#define _I2CCONTROLLER_h

#include "I2Cdev.h"
#include "Addresses.h"

class I2CController :
    public I2Cdev
{
private:
    uint8_t m_address;
public:
    byte Buffer[CTRL_BUFFER_SIZE];
    Data* data;
    bool IndexButtton;
    bool MiddleButtton;
    bool RingButtton;
    bool PinkyButtton;
    bool Stick;
    int XAxis;
    int YAxis;
public:
    I2CController(uint8_t address);
    void Update();
};

#endif