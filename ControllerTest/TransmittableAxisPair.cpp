#include "TransmittableAxisPair.h"

TransmittableAxisPair::TransmittableAxisPair(AxisPair pair, uint8_t id)
{
	Pair = pair;
	ID = id;
}

TransmittableAxisPair::TransmittableAxisPair(ByteArray* data)
{
	Deserialize(data);
}

ByteArray* TransmittableAxisPair::Serialize()
{
	BinarySerializer* serializer = new BinarySerializer();
	serializer->AddByteSize();
	serializer->AddSize(&Pair);
	serializer->GenerateArray();

	serializer->Add(ID);
	serializer->Add(&Pair);
	ByteArray* arr = serializer->GetArray();
	delete serializer;
	return arr;
}

void TransmittableAxisPair::Deserialize(ByteArray* data)
{
	BinarySerializer* serializer = new BinarySerializer(data);
	ID = serializer->GetByte();
	AxisPair* pair = new AxisPair();
	serializer->GetSerializable(pair);
	Pair = *pair;
	delete pair;
	delete serializer;
}
