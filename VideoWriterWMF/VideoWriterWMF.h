#pragma once

#include "Helper.h"

namespace VideoWriterWMF
{
	struct DLL_API EncodingParameters
	{
		UINT32  bitrate;
		UINT32	width;
		UINT32	height;
		DOUBLE	fps;
	};

	class DLL_API VideoWriterWMF
	{
	public:
		VideoWriterWMF();

		HRESULT Open(const WCHAR* pwszFileName, const EncodingParameters& params);
		HRESULT Close();

		HRESULT WriteFrame(BYTE* pcbBuffer, UINT32 len, LONGLONG timestamp);

	protected:
		~VideoWriterWMF();

		HRESULT ConfigureSinkWriter(const EncodingParameters& params);
		HRESULT ConfigureEncoder(
			const EncodingParameters& params,
			IMFMediaType* pType,
			IMFSinkWriter* pWriter,
			DWORD* pdwStreamIndex
		);

		IMFSinkWriter*			m_pWriter;
		IMFMediaBuffer*			m_pMediaBuffer = NULL;

		CRITICAL_SECTION        m_critsec;
		BOOL					m_initialized = false;
	};
}