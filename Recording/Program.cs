using System;
using NAudio.Wave;

namespace Recording
{
	class Program
	{
		static WaveInEvent waveSource;
		static WaveFileWriter waveFile;

		static void Main(string[] args)
		{
			Start();
			Console.ReadKey();
			Stop();
			Console.ReadKey();
		}

		static void Start()
		{
			waveSource = new WaveInEvent();
			waveSource.WaveFormat = new WaveFormat(8000, 16, 2);

			waveSource.DataAvailable += new EventHandler<WaveInEventArgs>(OnDataAvailable);
			waveSource.RecordingStopped += new EventHandler<StoppedEventArgs>(OnRecordingStopped);

			var fileName = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-fff");
			waveFile = new WaveFileWriter($@"C:\Temp\{fileName}.wav", waveSource.WaveFormat);

			waveSource.StartRecording();
		}

		static void Stop()
		{
			waveSource.StopRecording();
		}

		static void OnDataAvailable(object sender, WaveInEventArgs e)
		{
			if (waveFile != null)
			{
				waveFile.Write(e.Buffer, 0, e.BytesRecorded);
				waveFile.Flush();
			}
		}

		static void OnRecordingStopped(object sender, StoppedEventArgs e)
		{
			if (waveSource != null)
			{
				waveSource.Dispose();
				waveSource = null;
			}

			if (waveFile != null)
			{
				waveFile.Dispose();
				waveFile = null;
			}
		}
	}
}
