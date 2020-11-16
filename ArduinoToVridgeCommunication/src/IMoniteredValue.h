#pragma once

#ifndef _IMONITOREDVALUE_h
#define _IMONITOREDVALUE_h

#include "ISerializable.h"
#include "BinarySerializer.h"
#include "Definitions.h"
#include "DeviceInfo.h"

class IMoniteredValue : public virtual ISerializable
{
protected:
	bool m_hasChanged;

	IMoniteredValue()
	{
		ID = __COUNTER__ + 1;
	}
public:
	byte ID;
	virtual bool HasChanged() abstract;
	void Send()
	{
		m_hasChanged = false;
		ByteArray* data = Serialize();
		
		delete data;
	}
};

#endif