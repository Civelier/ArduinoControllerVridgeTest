#pragma once

#ifndef _TRANSMITTABLEAXISPAIR_h
#define _TRANSMITTABLEAXISPAIR_h

#include "ISerializable.h"
#include "AxisPair.h"
class TransmittableAxisPair : public virtual ISerializable
{
private:
protected:
public:
	AxisPair Pair;
	uint8_t ID;

	TransmittableAxisPair(AxisPair pair, uint8_t id);
	TransmittableAxisPair(ByteArray* data);
	virtual ByteArray* Serialize();
	virtual void Deserialize(ByteArray* data);
};

#endif