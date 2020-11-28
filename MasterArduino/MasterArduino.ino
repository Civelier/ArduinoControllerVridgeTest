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
#include "Utilities.h"

// Printing method
#define PRINT_METHOD_READABLE 0 //Print as a human readable report
#define PRINT_METHOD_DATA 1 //Print bytes using old method (deprecated)
#define PRINT_METHOD_FAST_DATA 2 //Print bytes continuously

// Selected printing method
#define PRINT_METHOD PRINT_METHOD_READABLE

// Run mode
#define RUN_MODE_NORMAL 0 //Normal execution (release version)
#define RUN_MODE_MEMORY_TEST 1 //Testing memory exectution

// Selected run mode
#define RUN_MODE RUN_MODE_NORMAL

#if RUN_MODE != RUN_MODE_NORMAL
// Tests
#define TEST_TYPE_1 0 //Latest version with data not being a pointer
#define TEST_TYPE_2 1 //Old version with data being a pointer

// Selected test
#define TEST_TYPE TEST_TYPE_2
#endif

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

#if !defined(TEST_TYPE) || defined(TEST_TYPE) && TEST_TYPE == TEST_TYPE_1
DataPacket data = DataPacket();
#endif
#if defined(TEST_TYPE) && TEST_TYPE == TEST_TYPE_2
DataPacket* data = new DataPacket();
#endif
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
//VectorInt16 aa;         // [x, y, z]            accel sensor measurements
//VectorInt16 gy;         // [x, y, z]            gyro sensor measurements
//VectorInt16 aaReal;     // [x, y, z]            gravity-free accel sensor measurements
//VectorInt16 aaWorld;    // [x, y, z]            world-frame accel sensor measurements
//VectorFloat gravity;    // [x, y, z]            gravity vector
//float euler[3];         // [psi, theta, phi]    Euler angle container
//float ypr[3];           // [yaw, pitch, roll]   yaw/pitch/roll container and gravity vector


MPU6050 rMPU = MPU6050();

void AlertMemoryGain()
{
	static int mem = freeMemory();
	if (mem != freeMemory())
	{
#if !defined(TEST_TYPE) || defined(TEST_TYPE) && TEST_TYPE == TEST_TYPE_1
		data.error = ERR_MASTER_MEM_FAIL;
#endif
#if defined(TEST_TYPE) && TEST_TYPE == TEST_TYPE_2
		data->error = ERR_MASTER_MEM_FAIL;
#endif
	}
}

void Print()
{
	Data* d = (Data*)rArduinoBuffer;
	Serial.print(d->x);
	Serial.print("\t");
	Serial.print(d->y);
	Serial.print("\t");
	Serial.println((int)(d->btns));
	for (size_t i = 0; i < TOTAL_BUFFER_SIZE; i++)
	{
		Serial.print(totalBuffer[i]);
		Serial.print("\t");
	}
	Serial.println();
}

//void requestEvent(int count)
//{
//	while (Wire.available() > 0)
//	{
//		rArduinoBuffer[writeIndex] = (byte)Wire.read();
//		writeIndex++;
//		if (writeIndex >= CTRL_BUFFER_SIZE)
//		{
//#if PRINT_METHOD == PRINT_METHOD_DATA
//			Serial.write(buffer, CTRL_BUFFER_SIZE);
//#endif
//#if PRINT_METHOD == PRINT_METHOD_READABLE
//			Print();
//#endif
//			TransmitDone = true;
//			writeIndex = 0;
//			break;
//		}
//	}
//	while (Wire.available() > 0) Wire.read();
//}


const byte sig[5] =
{
	12,
	35,
	253,
	95,
	129
};

void CalibrateDMP()
{
	rMPU.CalibrateAccel();
	rMPU.CalibrateGyro();
}

#if PRINT_METHOD == PRINT_METHOD_DATA || PRINT_METHOD == PRINT_METHOD_FAST_DATA
//void serialEvent()
//{
//	delay(500);
//	int b = Serial.read();
//#if PRINT_METHOD == PRINT_METHOD_DATA
//	if (b == 1) Serial.write(buffer, CTRL_BUFFER_SIZE);
//	if (b == 2) Serial.write(sig, 5);
//#endif
//#if PRINT_METHOD == PRINT_METHOD_FAST_DATA
//	if (b == 1)
//	{
//		
//	}
//	rMPU.CalibrateAccel();
//	rMPU.CalibrateGyro();
//	data.error = ERR_MASTER_MEM_FAIL;
//#endif
//	while (Serial.available()) Serial.read();
//}
#endif

#define PrintSizeof(type) Serial.print(#type);\
Serial.print(": ");\
Serial.println(sizeof(type))

//Data: 5
//MPUData: 16
//QuaternionData : 16
//DataPacket : 47

bool rMPUInitialized = false;

bool TryInitializeMPU()
{
	if (rMPUInitialized && (data.error & (ERR_RIGHT | ERR_MPU)) != (ERR_RIGHT | ERR_MPU))
	{
		if (rMPU.testConnection())
		{
#if PRINT_METHOD == PRINT_METHOD_READABLE
			Serial.println("MPU connected");
#endif
			return true;
		}
		else
		{
			rMPUInitialized = false;
			data.error = ERR_RIGHT | ERR_MPU | ERR_ADDRESS_NACK;
			return false;
		}
	}
	if (!rMPU.testConnection())
	{
#if !defined(TEST_TYPE) || defined(TEST_TYPE) && TEST_TYPE == TEST_TYPE_1
		data.error = ERR_RIGHT | ERR_MPU | ERR_ADDRESS_NACK;
#endif
#if defined(TEST_TYPE) && TEST_TYPE == TEST_TYPE_2
		data->error = ERR_RIGHT | ERR_MPU | ERR_ADDRESS_NACK;
#endif
		rMPUInitialized = false;
		return false;
	}
	rMPUStatus = rMPU.dmpInitialize();
	if (rMPUStatus == 1)
	{
#if !defined(TEST_TYPE) || defined(TEST_TYPE) && TEST_TYPE == TEST_TYPE_1
		data.error = ERR_RIGHT | ERR_MPU_INIT_MEM_LOAD_FAIL;
#endif
#if defined(TEST_TYPE) && TEST_TYPE == TEST_TYPE_2
		data->error = ERR_RIGHT | ERR_MPU_INIT_MEM_LOAD_FAIL;
#endif
		rMPUInitialized = false;
		return false;
	}
	if (rMPUStatus == 2)
	{
#if !defined(TEST_TYPE) || defined(TEST_TYPE) && TEST_TYPE == TEST_TYPE_1
		data.error = ERR_RIGHT | ERR_MPU_DMP_CONFIG;
#endif
#if defined(TEST_TYPE) && TEST_TYPE == TEST_TYPE_2
		data->error = ERR_RIGHT | ERR_MPU_DMP_CONFIG;
#endif
		rMPUInitialized = false;
		return false;
	}
	rMPU.setIntDMPEnabled(false);
	rMPU.setDMPEnabled(true);
	if ((data.error & (ERR_RIGHT | ERR_MPU)) == (ERR_RIGHT | ERR_MPU)) data.error = 0;
	rMPUInitialized = true;
	return true;
}

bool TestRArduinoConnection()
{

}

// the setup function runs once when you press reset or power the board
void setup()
{
	Serial.begin(115200);
	Serial.println("Hello");
	PrintSizeof(Data);
	PrintSizeof(MPUData);
	PrintSizeof(QuaternionData);
	PrintSizeof(DataPacket);
	delay(2000);
	Wire.begin();
	Wire.setClock(400000);
	//Wire.onReceive(requestEvent);
	rMPU.initialize();
	delay(100);
}

// the loop function runs over and over again until power down or reset
void loop()
{
	AlertMemoryGain();
	
	//TestMemory(true); //querry 1
	while (Serial.available())
	{
		byte b = Serial.read();
#if PRINT_METHOD == PRINT_METHOD_READABLE
		if (b == '1') CalibrateDMP();
#else
		if (b == 1) CalibrateDMP();
#endif
	}
	//while (!TransmitDone) delay(5);
	//TransmitDone = false;
	sender = RIGHT_SLAVE_ADDRESS;
	rTransmitError = Wire.requestFrom((byte)RIGHT_SLAVE_ADDRESS, CTRL_BUFFER_SIZE);

#if !defined(TEST_TYPE) || defined(TEST_TYPE) && TEST_TYPE == TEST_TYPE_1
	if (rTransmitError == 1) data.error = ERR_RIGHT | ERR_ARDUINO | ERR_ADDRESS_NACK;
	if (rTransmitError == 2) data.error = ERR_RIGHT | ERR_ARDUINO | ERR_REG_NACK;
#endif
#if defined(TEST_TYPE) && TEST_TYPE == TEST_TYPE_2
	if (rTransmitError == 1) data->error = ERR_RIGHT | ERR_ARDUINO | ERR_ADDRESS_NACK;
	if (rTransmitError == 2) data->error = ERR_RIGHT | ERR_ARDUINO | ERR_REG_NACK;
#endif

	//TestMemory(); //querry 2
	while (Wire.available() > 0)
	{
		int b = Wire.read();
		if (b == -1)
		{
			data.error = ERR_RIGHT | ERR_ARDUINO | ERR_STREAM_ENDED;
			break;
		}
		rArduinoBuffer[writeIndex] = (byte)b;
		writeIndex++;
		if (writeIndex >= CTRL_BUFFER_SIZE)
		{
#if !defined(TEST_TYPE) || defined(TEST_TYPE) && TEST_TYPE == TEST_TYPE_1
			data.rightArduino = (*(Data*)rArduinoBuffer);
#endif
#if defined(TEST_TYPE) && TEST_TYPE == TEST_TYPE_2
			data->rightArduino = (*(Data*)rArduinoBuffer);
#endif
			TransmitDone = true;
			writeIndex = 0;
			//TestMemory(); //querry 3
			break;
		}
	}

	if (TryInitializeMPU())
	{
		rTransmitError = rMPU.dmpGetCurrentFIFOPacket(fifoBuffer);
		if (rTransmitError)
		{
			rMPU.dmpGetQuaternion(&q, fifoBuffer);
#if !defined(TEST_TYPE) || defined(TEST_TYPE) && TEST_TYPE == TEST_TYPE_1
			data.rightMPU.quat.w = q.w;
			data.rightMPU.quat.x = q.x;
			data.rightMPU.quat.y = q.y;
			data.rightMPU.quat.z = q.z;
#endif
#if defined(TEST_TYPE) && TEST_TYPE == TEST_TYPE_2
			data->rightMPU.quat.w = q.w;
			data->rightMPU.quat.x = q.x;
			data->rightMPU.quat.y = q.y;
			data->rightMPU.quat.z = q.z;
#endif
		}
		else
		{
			if (rTransmitError == 1) data.error = ERR_RIGHT | ERR_MPU | ERR_ADDRESS_NACK;
			if (rTransmitError == 2) data.error = ERR_RIGHT | ERR_MPU | ERR_REG_NACK;
			while (!TryInitializeMPU());
		}
		//TestMemory(); //querry 4
	}

	totalBuffer = reinterpret_cast<byte*>(&data);
	//totalBuffer = (byte*)data;
	//TestMemory(); //querry 5
#if PRINT_METHOD == PRINT_METHOD_FAST_DATA
	//while (Serial.available()) Serial.read();
	Serial.write(totalBuffer, TOTAL_BUFFER_SIZE);
	//while (Serial.available()) Serial.read();
#endif
#if PRINT_METHOD == PRINT_METHOD_READABLE
	Print();
	//TestMemory(); //querry 6
	delay(200);
#endif
	//while (Wire.available() > 0) Wire.read();
}
