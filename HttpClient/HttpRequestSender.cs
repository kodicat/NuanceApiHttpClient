using System;
using System.IO;
using System.Net;
using HttpClient.DTOs;

namespace HttpClient
{
	class HttpRequestSender
	{
		HttpWebRequest request;
		Stream stream;

		public void BuildRequest()
		{
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
			request = new HttpWebRequestBuilder().Build(urlSettings, headersSettings);
		}

		public void SendChunked(byte[] buffer, int bufferLength)
		{
			stream = request.GetRequestStream();
			Console.WriteLine($"Sending actual length {buffer.Length}, declared length {bufferLength}");
			stream.Write(buffer, 0, bufferLength);
			stream.Flush();
		}

		public void CloseConnection()
		{
			stream.Close();
		}
	}
}
