#pragma once

#ifndef _AXISPAIR_h
#define _AXISPAIR_h

#include "ISerializable.h"
#include "BinarySerializer.h"

#if defined(ARDUINO) && ARDUINO >= 100
#include "arduino.h"
#else
#include "WProgram.h"
#endif

class AxisPair : public virtual ISerializable
{
private:
	float m_x;
	float m_y;
public:
	AxisPair();
	AxisPair(float x, float y);
	AxisPair(ByteArray* data);

	virtual size_t Size() override;
	ByteArray* Serialize();
	virtual void Deserialize(ByteArray* data) override;

	float GetX();
	int GetX(int xMax);
	float GetY();
	int GetY(int yMax);

	void SetX(float x);
	void SetX(int x, int xMax);
	void SetY(float y);
	void SetY(int y, int yMax);

	
};

#endif