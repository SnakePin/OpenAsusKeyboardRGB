#include <fstream>
#include <string>
#include <vector>
#include <cassert>
#include <iostream>
#include "RogKeyEnum.h"

struct RawKeyMapFromBinaryStruct {
	uint8_t Y;
	uint8_t unk1;
	uint8_t X;
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
	if (argc < 4) {
		std::cerr << "Invalid argument count.\n" "Usage: " << argv[0] << " rawMapAddressInHex inputFilePath outFilePath\n";
		return 1;
	}

	//Usage: dumper.exe 0xDEADBEEF inputFile.bin outFile.txt

	//AacKbHal_x64.dll (SHA1 7EF92EFA38B788692B2910A542F4F1F3E3913902)
	//Generic offset = 0x86990
	//Claymore offset = 0x74580

	//AacKbHal_x64.dll (SHA1 FAAE294497CEF9D4C62D30A221818078515D4AB7) (Armoury Crate as of 1/12/2020)
	//Generic offset = 0x114720
	//Claymore offset = 0xED720
	//Falchion(M601) offset = 0x1142F0

	const size_t rawKeymapOffset = std::stoul(argv[1], nullptr, 16);
	const std::string inputPath = argv[2];
	const std::string outFilePath = argv[3];

	if (auto fstrm = std::ifstream(inputPath, std::ios::binary)) {
		fstrm.seekg(0, std::ios::end);
		auto size = fstrm.tellg();
		fstrm.seekg(0, std::ios::beg);

		std::vector<uint8_t> vec(size);
		fstrm.read((char*)vec.data(), size);

		if (auto outFile = std::ofstream(outFilePath)) {
			size_t len = 0;
			auto rawKeyMapAddr = reinterpret_cast<RawKeyMapFromBinaryStruct*>(&vec[rawKeymapOffset]);
			GenerateKeyMap(nullptr, rawKeyMapAddr, &len);
			std::vector<MappedKeyStruct> arrayOut(len);
			GenerateKeyMap(arrayOut.data(), rawKeyMapAddr);

			for (auto& key : arrayOut)
			{
				outFile << "X: " << static_cast<int>(key.X) << " Y: " << static_cast<int>(key.Y) << " KeyCode: 0x" << std::hex << key.KeyCode << std::dec << '\n';
			}
		}

	}
}
