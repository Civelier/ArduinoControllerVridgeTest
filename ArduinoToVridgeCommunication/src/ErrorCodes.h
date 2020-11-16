#pragma once

#ifndef _ERRORCODES_h
#define _ERRORCODES_h

#include "Definitions.h"

#define SendError(errorCode)\
{\
	ByteArray* _arr = new ByteArray(2);\
	_arr->Append((byte)255);\
	_arr->Append((byte)errorCode);\
	SendToPC(_arr);\
	delete _arr;\
}

#define ERR_OUT_OF_RANGE (byte)0
#define ERR_INVALID_COMMAND (byte)1

#endif