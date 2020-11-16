#pragma once

#ifndef _IMONITOREDVALUE_h
#define _IMONITOREDVALUE_h

#include "ISerializable.h"

class IMoniteredValue : public virtual ISerializable
{
public:
	byte ID;
	bool HasChanged;
};

#endif