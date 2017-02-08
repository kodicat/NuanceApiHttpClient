using System;
using NAudio.Wave;

namespace HttpClient
{
	class Program
	{
		static WaveInEvent waveSource;
		static HttpRequestSender requestSender;

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
			waveSource.BufferMilliseconds = 260;

			waveSource.DataAvailable += OnDataAvailable;
			waveSource.RecordingStopped += OnRecordingStopped;

			requestSender = new HttpRequestSender();
			requestSender.BuildRequest();

			waveSource.StartRecording();
		}

		static void Stop()
		{
			waveSource.StopRecording();
		}

		static void OnDataAvailable(object sender, WaveInEventArgs e)
		{
			requestSender.SendChunked(e.Buffer, e.BytesRecorded);
		}

		static void OnRecordingStopped(object sender, StoppedEventArgs e)
		{
			requestSender.CloseConnection();

			waveSource?.Dispose();
			waveSource = null;
		}
	}
}
