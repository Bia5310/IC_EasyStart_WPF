#include "pch.h"

#include "VideoWriterWMF.h"

namespace VideoWriterWMF
{
    VideoWriterWMF::VideoWriterWMF()
    {
    }

    VideoWriterWMF::~VideoWriterWMF()
    {
        Close();
        DeleteCriticalSection(&m_critsec);
    }

    HRESULT VideoWriterWMF::WriteFrame(BYTE* pcbSrcBuffer, UINT32 len, LONGLONG timestamp)
    {
        HRESULT hr = S_OK;

        EnterCriticalSection(&m_critsec);

        if (m_pWriter == NULL)
        {
            LeaveCriticalSection(&m_critsec);
            return S_FALSE;
        }

        BYTE* pDestBuffer = NULL;
        DWORD curLen = 0;
        DWORD maxLen = 0;

        hr = m_pMediaBuffer->Lock(&pDestBuffer, &maxLen, &curLen);

        if (len != curLen)
        {
            hr = S_FALSE;
        }

        if (SUCCEEDED(hr))
        {
            memcpy(pDestBuffer, pcbSrcBuffer, len);
        }

        if (SUCCEEDED(hr))
        {
            hr = m_pMediaBuffer->Unlock();
        }

        IMFSample* pSample = NULL;

        if (SUCCEEDED(hr))
            hr = MFCreateSample(&pSample);

        if (SUCCEEDED(hr))
        {
            pSample->AddBuffer(m_pMediaBuffer);
        }
        if (SUCCEEDED(hr))
        {
            hr = pSample->SetSampleTime(timestamp);
        }
        if (SUCCEEDED(hr))
        {
            hr = m_pWriter->WriteSample(0, pSample);
        }

        SafeRelease(&pSample);

        LeaveCriticalSection(&m_critsec);
        return hr;
    }

	HRESULT VideoWriterWMF::Open(const WCHAR* pwszFileName, const EncodingParameters& encodingParameters)
	{
		HRESULT hr = S_OK;

        EnterCriticalSection(&m_critsec);

		hr = MFCreateSinkWriterFromURL(pwszFileName,
			NULL,
			NULL,
			&m_pWriter);

		if (SUCCEEDED(hr))
		{
            hr = ConfigureSinkWriter(encodingParameters);
		}

        if (SUCCEEDED(hr))
        {
            MFCreateMemoryBuffer(encodingParameters.height*encodingParameters.width*4, &m_pMediaBuffer);
        }

        if (SUCCEEDED(hr))
        {
            m_initialized = true;
        }

        LeaveCriticalSection(&m_critsec);
		return hr;
	}

    HRESULT VideoWriterWMF::ConfigureSinkWriter(const EncodingParameters& params)
    {
        HRESULT hr = S_OK;
        DWORD sink_stream = 0;

        IMFMediaType* pType = NULL;
        MFCreateMediaType(&pType);
        MFSetAttributeSize(pType, MF_MT_FRAME_SIZE, params.width, params.height);
        pType->SetGUID(MF_MT_MAJOR_TYPE, MFMediaType_Video);
        pType->SetGUID(MF_MT_SUBTYPE, MFVideoFormat_ARGB32);

        if (SUCCEEDED(hr))
        {
            hr = ConfigureEncoder(params, pType, m_pWriter, &sink_stream);
        }

        if (SUCCEEDED(hr))
        {
            // Register the color converter DSP for this process, in the video 
            // processor category. This will enable the sink writer to enumerate
            // the color converter when the sink writer attempts to match the
            // media types.

            hr = MFTRegisterLocalByCLSID(
                __uuidof(CColorConvertDMO),
                MFT_CATEGORY_VIDEO_PROCESSOR,
                L"",
                MFT_ENUM_FLAG_SYNCMFT,
                0,
                NULL,
                0,
                NULL
            );
        }

        if (SUCCEEDED(hr))
        {
            hr = m_pWriter->SetInputMediaType(sink_stream, pType, NULL);
        }

        if (SUCCEEDED(hr))
        {
            hr = m_pWriter->BeginWriting();
        }

        SafeRelease(&pType);
        return hr;
    }

    HRESULT VideoWriterWMF::ConfigureEncoder(
        const EncodingParameters& params,
        IMFMediaType* pType,
        IMFSinkWriter* pWriter,
        DWORD* pdwStreamIndex
    )
    {
        HRESULT hr = S_OK;

        IMFMediaType* pType2 = NULL;

        hr = MFCreateMediaType(&pType2);

        if (SUCCEEDED(hr))
        {
            hr = pType2->SetGUID(MF_MT_MAJOR_TYPE, MFMediaType_Video);
        }

        if (SUCCEEDED(hr))
        {
            hr = pType2->SetGUID(MF_MT_SUBTYPE, MFVideoFormat_WMV3);
        }

        if (SUCCEEDED(hr))
        {
            hr = pType2->SetUINT32(MF_MT_AVG_BITRATE, params.bitrate);
        }

        if (SUCCEEDED(hr))
        {
            hr = MFSetAttributeSize(pType2, MF_MT_FRAME_SIZE, params.width, params.height);
            //hr = CopyAttribute(pType, pType2, MF_MT_FRAME_SIZE);
        }

        if (SUCCEEDED(hr))
        {
            hr = MFGetAttributeUINT32(pType2, MF_MT_FRAME_RATE, params.fps);
        }

        if (SUCCEEDED(hr))
        {
            /*UINT32 w = 0, h = 0;
            MFGetAttributeSize(pType, MF_MT_FRAME_SIZE, &w, &h);
            hr = pType2->SetUINT32(MF_MT_DEFAULT_STRIDE, w*4);*/
        }

        /*if (SUCCEEDED(hr))
        {
            hr = CopyAttribute(pType, pType2, MF_MT_FRAME_RATE);
        }

        if (SUCCEEDED(hr))
        {
            hr = CopyAttribute(pType, pType2, MF_MT_PIXEL_ASPECT_RATIO);
        }

        if (SUCCEEDED(hr))
        {
            hr = CopyAttribute(pType, pType2, MF_MT_INTERLACE_MODE);
        }*/

        if (SUCCEEDED(hr))
        {
            hr = pWriter->AddStream(pType2, pdwStreamIndex);
        }

        SafeRelease(&pType2);
        return hr;
    }

	HRESULT VideoWriterWMF::Close()
	{
        HRESULT hr = S_OK;

        EnterCriticalSection(&m_critsec);

        if (m_pWriter)
        {
            hr = m_pWriter->Finalize();
            m_initialized = false;
        }

        if (m_pMediaBuffer)
        {
            SafeRelease(&m_pMediaBuffer);
            m_pMediaBuffer = NULL;
        }

        LeaveCriticalSection(&m_critsec);
        return hr;
	}
}