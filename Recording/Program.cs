using System;
using NAudio.Wave;

namespace Recording
{
	class Program
	{
		static WaveInEvent waveSource;
		public static event EventHandler<StreamingAudioBufferAvailableEventArgs> DataAvailable;
		public static event EventHandler<StreamingAudioStoppedEventArgs> StreamingStopped;
		//static WaveFileWriter waveFile;

		static void Main()
		{
			Start();
			Console.ReadKey();
			Stop();
			Console.ReadKey();
		}

		static void Start()
		{
			waveSource = new WaveInEvent();
			//waveSource.WaveFormat = new WaveFormat(8000, 16, 1); // default
			waveSource.BufferMilliseconds = 260;

			waveSource.DataAvailable += OnDataAvailable;
			waveSource.RecordingStopped += OnRecordingStopped;

			//var fileName = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-fff");
			//waveFile = new WaveFileWriter($@"C:\Temp\{fileName}.wav", waveSource.WaveFormat);

			waveSource.StartRecording();
		}

		static void Stop()
		{
			waveSource.StopRecording();
		}

		static void OnDataAvailable(object sender, WaveInEventArgs e)
		{
			DataAvailable?.Invoke(sender, new StreamingAudioBufferAvailableEventArgs(e.Buffer, e.BytesRecorded));
			//if (waveFile != null)
			//{
			//	waveFile.Write(e.Buffer, 0, e.BytesRecorded);
			//	waveFile.Flush();
			//}
		}

		static void OnRecordingStopped(object sender, StoppedEventArgs e)
		{
			if (waveSource != null)
			{
				waveSource.Dispose();
				waveSource = null;
			}

			StreamingStopped?.Invoke(sender, new StreamingAudioStoppedEventArgs());

			//if (waveFile != null)
			//{
			//	waveFile.Dispose();
			//	waveFile = null;
			//}
		}
	}
}
