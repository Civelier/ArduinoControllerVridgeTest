#pragma once

#ifndef _IMONITOREDVALUE_h
#define _IMONITOREDVALUE_h

#include "ISerializable.h"
#include "BinarySerializer.h"
#include "Definitions.h"

class IMoniteredValue : public virtual ISerializable
{
protected:
	bool m_hasChanged;

	IMoniteredValue()
	{
		ID = __COUNTER__;
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