using System;

namespace Recording
{
	class StreamingAudioBufferAvailableEventArgs : EventArgs
	{
		public StreamingAudioBufferAvailableEventArgs(byte[] buffer, int bytes)
		{
			Buffer = buffer;
			RecorderLength = bytes;
		}

		public byte[] Buffer { get; }
		public int RecorderLength { get; }
	}
}
