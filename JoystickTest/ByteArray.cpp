#include "..\ArduinoToVridgeCommunication\src\ByteArray.h"

ByteArray::operator byte* ()
{
	return Array;
}

void ByteArray::Print()
{
	Serial.write(Array, Length);
}
