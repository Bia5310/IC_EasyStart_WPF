#pragma once

#ifdef VIDEOWRITERWMF_EXPORTS
#define DLL_API _declspec(dllexport)
#else
#define DLL_API _declspec(dllimport)
#endif
