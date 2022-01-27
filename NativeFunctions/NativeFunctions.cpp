#include "pch.h"
#include "NativeFunctions.h"

void CopyImageRGB32toRGB32(char* srcPtr, int srcStride, char* destPtr, int destStride, int lines, bool downToUp)
{
	char* src = srcPtr;
	char* dest = destPtr;
	if (downToUp)
	{
		srcPtr += (lines - 1) * srcStride;
		srcStride *= -1;
	}
	for (int h = 0; h < lines; h++)
	{
		dest = destPtr + h * destStride;
		src = srcPtr + h * srcStride;
		memcpy(dest, src, srcStride);
	}
}

void CopyImageRGB24toRGB32(char* srcPtr, int srcStride, char* destPtr, int destStride, int lines, bool downToUp)
{
	char* src = srcPtr;
	char* dest = destPtr;
	if (downToUp)
	{
		srcPtr += (lines - 1) * srcStride;
		srcStride *= -1;
	}
	for (int h = 0; h < lines; h++)
	{
		dest = destPtr + h * destStride;
		src = srcPtr + h * srcStride;
		for (int w = 0; w < srcStride; w += 3)
		{
			dest[0] = src[0];
			dest[1] = src[1];
			dest[2] = src[2];
			dest[3] = 0xFF;
			dest += 4;
			src += 3;
		}
	}
}

void CopyImageGray16toRGB32(char* srcPtr, int srcStride, char* destPtr, int destStride, int lines, bool downToUp)
{
	char* src = srcPtr;
	char* dest = destPtr;
	if (downToUp)
	{
		srcPtr += (lines - 1) * srcStride;
		srcStride *= -1;
	}
	for (int h = 0; h < lines; h++)
	{
		dest = destPtr + h * destStride;
		src = srcPtr + h * srcStride;
		for (int w = 0; w < srcStride; w += 2)
		{
			dest[0] = src[1];
			dest[1] = dest[0];
			dest[2] = dest[0];
			dest[3] = 0xFF;
			dest += 4;
			src += 2;
		}
	}
}

void CopyImageGray8toRGB32(char* srcPtr, int srcStride, char* destPtr, int destStride, int lines, bool downToUp)
{
	char* src = srcPtr;
	char* dest = destPtr;
	if (downToUp)
	{
		srcPtr += (lines - 1) * srcStride;
		srcStride *= -1;
	}
	for (int h = 0; h < lines; h++)
	{
		dest = destPtr + h * destStride;
		src = srcPtr + h * srcStride;
		for (int w = 0; w < srcStride; ++w)
		{
			dest[0] = src[0];
			dest[1] = dest[0];
			dest[2] = dest[0];
			dest[3] = 0xFF;
			dest += 4;
			++src;
		}
	}
}