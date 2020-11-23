#include <fstream>
#include <string>
#include <vector>
#include <cassert>
#include "RogKeyEnum.h"

struct RawKeyMapFromBinaryStruct {
	uint8_t X;
	uint8_t unk1;
	uint8_t Y;
	uint8_t unk2;
};

struct MappedKeyStruct {
	uint8_t X;
	uint8_t Y;
	uint16_t KeyCode;
};

void GenerateKeyMap(MappedKeyStruct* keyMappingArray, RawKeyMapFromBinaryStruct* rawKeyMap, size_t* lenOutParam = nullptr) {
	size_t rawMapIndex = 0;
	size_t arrayLen = 0;

	while ((rawKeyMap[rawMapIndex].Y != 0xff) || (rawKeyMap[rawMapIndex].unk1 != 0xff)) {
		if ((rawKeyMap[rawMapIndex].X != 0xff) && (rawKeyMap[rawMapIndex].Y != 0xff)) {
			if (keyMappingArray != nullptr) {
				keyMappingArray[arrayLen].KeyCode = rawMapIndex;
				keyMappingArray[arrayLen].Y = rawKeyMap[rawMapIndex].Y;
				keyMappingArray[arrayLen].X = rawKeyMap[rawMapIndex].X;
			}
			arrayLen++;
		}
		rawMapIndex++;
	}

	if (lenOutParam != nullptr) {
		*lenOutParam = arrayLen;
	}
}

int main(int argc, char* argv[])
{
	if (argc < 2) {
		return 1;
	}

	const std::string inputPath = argv[1];
	const std::string genericMappingOutPath = "claymoreMapDump.txt";
	const std::string claymoreMappingOutPath = "genericMapDump.txt";

	//AacKbHal_x64.dll (SHA1 7EF92EFA38B788692B2910A542F4F1F3E3913902)
	const size_t genericRawKeymapOffset = 0x86990;
	const size_t claymoreRawKeymapOffset = 0x74580;

	if (auto fstrm = std::ifstream(inputPath, std::ios::binary)) {
		fstrm.seekg(0, std::ios::end);
		auto size = fstrm.tellg();
		fstrm.seekg(0, std::ios::beg);

		std::vector<uint8_t> vec(size);
		fstrm.read((char*)vec.data(), size);

		if (auto outFile = std::ofstream(genericMappingOutPath)) {
			size_t len = 0;
			auto rawKeyMapAddr = reinterpret_cast<RawKeyMapFromBinaryStruct*>(&vec[genericRawKeymapOffset]);
			GenerateKeyMap(nullptr, rawKeyMapAddr, &len);
			std::vector<MappedKeyStruct> arrayOut(len);
			GenerateKeyMap(arrayOut.data(), rawKeyMapAddr);

			for (auto& key : arrayOut)
			{
				//int index = key.Y * 23 + key.X;
				if (key.KeyCode == static_cast<uint16_t>(RogKeys::ROG_KEY_A)) {
					assert(key.X == 2 && key.Y == 3);
				}

				outFile << "X: " << static_cast<int>(key.X) << " Y: " << static_cast<int>(key.Y) << " KeyCode: 0x" << std::hex << key.KeyCode << std::dec << '\n';
			}
		}

		if (auto outFile = std::ofstream(claymoreMappingOutPath)) {
			size_t len = 0;
			auto rawKeyMapAddr = reinterpret_cast<RawKeyMapFromBinaryStruct*>(&vec[claymoreRawKeymapOffset]);
			GenerateKeyMap(nullptr, rawKeyMapAddr, &len);
			std::vector<MappedKeyStruct> arrayOut(len);
			GenerateKeyMap(arrayOut.data(), rawKeyMapAddr);

			for (auto& key : arrayOut)
			{
				//int index = key.X * 8 + key.Y;
				if (key.KeyCode == static_cast<uint16_t>(RogKeys::ROG_KEY_A)) {
					assert(key.X == 1 && key.Y == 3);
				}

				outFile << "X: " << static_cast<int>(key.X) << " Y: " << static_cast<int>(key.Y) << " KeyCode: 0x" << std::hex << key.KeyCode << std::dec << '\n';
			}
		}

	}
}
