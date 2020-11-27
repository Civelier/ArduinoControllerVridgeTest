/*
 Name:		MasterArduino.ino
 Created:	11/25/2020 10:34:37 AM
 Author:	civel
*/


#include <MPU6050_6Axis_MotionApps_V6_12.h>
#include <MPU6050.h>
#include <helper_3dmath.h>
#include <Wire.h>
#include "Addresses.h"
#include "ErrorCodes.h"

struct QuaternionData
{
	float w, x, y, z;
};

struct MPUData
{
	QuaternionData quat;
};

struct DataPacket
{
	uint32_t startBytes = 0xFFFFFFFF;
	byte error = ERR_NONE;
	Data rightArduino;
	Data leftArduino;
	MPUData rightMPU;
	MPUData leftMPU;
};

#define TOTAL_BUFFER_SIZE sizeof(DataPacket)

uint8_t sender;
size_t writeIndex = 0;
DataPacket* data = new DataPacket();
byte* rArduinoBuffer = new byte[CTRL_BUFFER_SIZE];
byte* rMPUBuffer = new byte[64];
byte* totalBuffer = new byte[TOTAL_BUFFER_SIZE];
bool TransmitDone = true;
uint8_t rMPUStatus;
uint8_t rTransmitError;

uint16_t packetSize;    // expected DMP packet size (default is 42 bytes)
uint16_t fifoCount;     // count of all bytes currently in FIFO
uint8_t fifoBuffer[64]; // FIFO storage buffer

// orientation/motion vars
Quaternion q;           // [w, x, y, z]         quaternion container
VectorInt16 aa;         // [x, y, z]            accel sensor measurements
VectorInt16 gy;         // [x, y, z]            gyro sensor measurements
VectorInt16 aaReal;     // [x, y, z]            gravity-free accel sensor measurements
VectorInt16 aaWorld;    // [x, y, z]            world-frame accel sensor measurements
VectorFloat gravity;    // [x, y, z]            gravity vector
float euler[3];         // [psi, theta, phi]    Euler angle container
float ypr[3];           // [yaw, pitch, roll]   yaw/pitch/roll container and gravity vector


MPU6050 rMPU = MPU6050();

#define PRINT_METHOD_READABLE 0
#define PRINT_METHOD_DATA 1
#define PRINT_METHOD_FAST_DATA 2

#define PRINT_METHOD PRINT_METHOD_FAST_DATA

void Print()
{
	Data* d = (Data*)rArduinoBuffer;
	Serial.print(d->x);
	Serial.print("\t");
	Serial.print(d->y);
	Serial.print("\t");
	Serial.println((int)(d->btns));
	for (size_t i = 0; i < sizeof(Data); i++)
	{
		Serial.print(rArduinoBuffer[i]);
		Serial.print("\t");
	}
	Serial.println();
}

void requestEvent(int count)
{
	while (Wire.available() > 0)
	{
		rArduinoBuffer[writeIndex] = (byte)Wire.read();
		writeIndex++;
		if (writeIndex >= CTRL_BUFFER_SIZE)
		{
#if PRINT_METHOD == PRINT_METHOD_DATA
			Serial.write(buffer, CTRL_BUFFER_SIZE);
#endif
#if PRINT_METHOD == PRINT_METHOD_READABLE
			Print();
#endif
			TransmitDone = true;
			writeIndex = 0;
			break;
		}
	}
	while (Wire.available() > 0) Wire.read();
}


const byte sig[5] =
{
	12,
	35,
	253,
	95,
	129
};

#if PRINT_METHOD == PRINT_METHOD_DATA || PRINT_METHOD == PRINT_METHOD_FAST_DATA
void serialEvent()
{
	int b = Serial.read();
#if PRINT_METHOD == PRINT_METHOD_DATA
	if (b == 1) Serial.write(buffer, CTRL_BUFFER_SIZE);
	if (b == 2) Serial.write(sig, 5);
#endif
#if PRINT_METHOD == PRINT_METHOD_FAST_DATA
	if (b == 1)
	{
		rMPU.CalibrateAccel();
		rMPU.CalibrateGyro();
	}
#endif
	while (Serial.available()) Serial.read();
}
#endif

// the setup function runs once when you press reset or power the board
void setup()
{
	Serial.begin(115200);
	Serial.println("Hello");
	Wire.begin();
	Wire.setClock(400000);
	//Wire.onReceive(requestEvent);
	rMPU.initialize();
	if (!rMPU.testConnection())
	{
		data->error = ERR_RIGHT | ERR_MPU | ERR_ADDRESS_NACK;
		return;
	}
	rMPUStatus = rMPU.dmpInitialize();
	if (rMPUStatus == 1)
	{
		data->error = ERR_RIGHT | ERR_MPU_INIT_MEM_LOAD_FAIL;
		return;
	}
	if (rMPUStatus == 2)
	{
		data->error = ERR_RIGHT | ERR_MPU_DMP_CONFIG;
		return;
	}

	delay(100);
}

// the loop function runs over and over again until power down or reset
void loop()
{
	while (!TransmitDone) delay(5);
	TransmitDone = false;
	sender = RIGHT_SLAVE_ADDRESS;
	rTransmitError = Wire.requestFrom((byte)RIGHT_SLAVE_ADDRESS, CTRL_BUFFER_SIZE);

	if (rTransmitError == 1) data->error = ERR_RIGHT | ERR_ARDUINO | ERR_ADDRESS_NACK;
	if (rTransmitError == 2) data->error = ERR_RIGHT | ERR_ARDUINO | ERR_REG_NACK;

	while (Wire.available() > 0)
	{
		rArduinoBuffer[writeIndex] = (byte)Wire.read();
		writeIndex++;
		if (writeIndex >= CTRL_BUFFER_SIZE)
		{
#if PRINT_METHOD == PRINT_METHOD_READABLE
			Print();
			delay(100);
#endif
			data->rightArduino = (*(Data*)rArduinoBuffer);
			TransmitDone = true;
			writeIndex = 0;
			break;
		}
	}

	if (rMPU.dmpGetCurrentFIFOPacket(fifoBuffer))
	{
		rMPU.dmpGetQuaternion(&q, fifoBuffer);
		data->rightMPU.quat.w = q.w;
		data->rightMPU.quat.x = q.x;
		data->rightMPU.quat.y = q.y;
		data->rightMPU.quat.z = q.z;
	}

#if PRINT_METHOD == PRINT_METHOD_FAST_DATA
	while (Serial.available()) Serial.read();
	Serial.write((byte*)data, TOTAL_BUFFER_SIZE);
	while (Serial.available()) Serial.read();
#endif
	while (Wire.available() > 0) Wire.read();
}
