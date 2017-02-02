using System.Net;
using HttpClient.DTOs;

namespace HttpClient.Extensions
{
	static class HttpWebRequestExtensions
	{
		internal static HttpWebRequest BuildHeaders(this HttpWebRequest request, HttpWebRequestHeadersSettings settings)
		{
			request.SendChunked = settings.IsChunked;
			request.AllowWriteStreamBuffering = !settings.IsChunked; // do not use buffer when chunked message
			request.ContentType = settings.ContentType;
			request.Accept = settings.Accept;
			request.Headers.Add($"Accept-Language: {settings.AcceptLanguage}");
			request.Headers.Add($"Accept-Topic: {settings.AcceptTopic}");
			request.Headers.Add($"X-Dictation-NBestListSize: {settings.XDictationNBestListSize}");
			request.Headers.Add($"X-Dictation-AudioSource: {settings.XDictationAudioSource}");
			return request;
		}

		internal static HttpWebRequest BuildGeneralValues(this HttpWebRequest request)
		{
			request.Method = "POST";
			request.UserAgent = "myAppUserAgent";
			return request;
		}
	}
}
