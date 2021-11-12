#pragma once
using System::String;

#include "EncodingParameters.h"

#include "../VideoWriterWMF/VideoWriterWMF.h"

namespace VideoWriterNET
{
	public ref class VideoWriter
	{
	public:
		static VideoWriter^ CreateVideoWriter();

		void Open(System::String^ filepath, EncodingParameters^ encodingParameters);
		void Close();
		void WriteFrame(array<byte>^ data, long timestamp);
	
	protected:
		VideoWriter();
		~VideoWriter();
		!VideoWriter();

		VideoWriterWMF::VideoWriterWMF* m_pVideoWriter = NULL;
	};
}