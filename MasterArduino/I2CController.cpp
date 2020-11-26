#include "I2CController.h"

#define INDEX_MASK 0b10000000
#define MIDDLE_MASK 0b01000000
#define RING_MASK 0b00100000
#define PINKY_MASK 0b00010000
#define STICK_MASK 0b00001000

#define ContainsBit(value, match) ((value & match) == match)

I2CController::I2CController(uint8_t address)
{
	m_address = address;
}

void I2CController::Update()
{
	readBytes(m_address, CTRL_ADDRESS_BUTTONS, CTRL_BUFFER_SIZE, Buffer);
	data = (Data*)Buffer;
	IndexButtton = ContainsBit(data->btns, INDEX_MASK);
	MiddleButtton = ContainsBit(data->btns, MIDDLE_MASK);
	MiddleButtton = ContainsBit(data->btns, RING_MASK);
	PinkyButtton = ContainsBit(data->btns, PINKY_MASK);
	Stick = ContainsBit(data->btns, STICK_MASK);
	XAxis = data->x;
	YAxis = data->y;
}
