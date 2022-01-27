#pragma once

extern "C" __declspec(dllexport) void CopyImageRGB32toRGB32(char* srcPtr, int srcStride, char* destPtr, int destStride, int lines, bool downToUp);
extern "C" __declspec(dllexport) void CopyImageRGB24toRGB32(char* srcPtr, int srcStride, char* destPtr, int destStride, int lines, bool downToUp);
extern "C" __declspec(dllexport) void CopyImageGray8toRGB32(char* srcPtr, int srcStride, char* destPtr, int destStride, int lines, bool downToUp);
extern "C" __declspec(dllexport) void CopyImageGray16toRGB32(char* srcPtr, int srcStride, char* destPtr, int destStride, int lines, bool downToUp);

