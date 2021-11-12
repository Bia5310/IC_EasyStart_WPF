#include "pch.h"

#include "VideoWriterNET.h"

#include <msclr\marshal_cppstd.h>

namespace VideoWriterNET
{
	VideoWriter^ VideoWriter::CreateVideoWriter()
	{
		return gcnew VideoWriter();
	}

	void VideoWriter::Open(System::String^ filepath, EncodingParameters^ encodingParameters)
	{
		pin_ptr<const wchar_t> pFilePath = PtrToStringChars(filepath);

		VideoWriterWMF::EncodingParameters encParams;
		encParams.bitrate = encodingParameters->bitrate;
		encParams.fps = encodingParameters->fps;
		encParams.width = encodingParameters->width;
		encParams.height = encodingParameters->height;
		
		//Close();
		if(m_pVideoWriter != NULL)
			m_pVideoWriter->Open(pFilePath, encParams);
	}

	void VideoWriter::Close()
	{
		if(m_pVideoWriter != NULL)
			m_pVideoWriter->Close();
	}

	void VideoWriter::WriteFrame(array<byte>^ data, long timestamp)
	{
		if (m_pVideoWriter == NULL)
			return;

		pin_ptr<BYTE> pData = &data[0];
		m_pVideoWriter->WriteFrame(pData, data->Length, timestamp);
		
	}

	VideoWriter::VideoWriter()
	{
		m_pVideoWriter = new VideoWriterWMF::VideoWriterWMF();
	}

	VideoWriter::~VideoWriter()
	{
		this->!VideoWriter();
	}

	VideoWriter::!VideoWriter()
	{
		//delete internal pointer
		if (m_pVideoWriter != NULL)
		{
			m_pVideoWriter->Close();
		}
		free(m_pVideoWriter);
	}
}