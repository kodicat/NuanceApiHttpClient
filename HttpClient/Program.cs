using System;
using System.Text;
using HttpClient.DTOs;

namespace HttpClient
{
	class Program
	{
		static void Main(string[] args)
		{
			// TODO: ii: move to some factory
			var urlSettings = new UrlSettings
			{
				Scheme = Uri.UriSchemeHttps,
				Host = "dictation.nuancemobility.net",
				Path = "/NMDPAsrCmdServlet/dictation",
				AppId = "MY_APP",
				AppKey = "777",
				Id = "111"
			};
			var requestSettingsProvider = new ChunkedRequestSettingsProvider();
			var headersSettings = requestSettingsProvider.Provide();
			var request = new HttpWebRequestBuilder().Build(urlSettings, headersSettings);
			// chunk size - 4160 bytes  of PCM 16 bits 8 kHz
			var stream = request.GetRequestStream();

			string nextLine;
			int totalBytes = 0;

			// Read a series of lines from the console and transmit them to the server.
			while (!string.IsNullOrEmpty(nextLine = Console.ReadLine()))
			{
				var bytes = Encoding.UTF8.GetBytes(nextLine);
				totalBytes += bytes.Length;
				Console.WriteLine($"CLIENT: Sending {bytes.Length} bytes ({totalBytes} total)");
				stream.Write(bytes, 0, bytes.Length);
				stream.Flush();
			}

			try
			{
				var response = request.GetResponse();
				Console.WriteLine(response);
			}
			catch (Exception exception)
			{
				Console.WriteLine(exception.Message);
			}
		}
	}
}
