using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace HttpClient
{
	class HttpWebRequestBuilder
	{
		const string NuanceHostName = "dictation.nuancemobility.net";
		const string NuancePath = "/NMDPAsrCmdServlet/dictation";

		static Uri BuildUri()
		{
			var uriBuilder = new UriBuilder
			{
				Scheme = Uri.UriSchemeHttps,
				Host = NuanceHostName,
				Path = NuancePath,
				Query = BuildUrlQuery(new {appId = "MY_APP", appKey = "777", id = "111"})
			};
			return uriBuilder.Uri;
		}

		static string BuildUrlQuery(object values)
		{
			var type = values.GetType();
			var properties = type.GetProperties();
			var resultBuilder = new StringBuilder(32);
			foreach (var property in properties)
			{
				var key = property.Name;
				var value = type.GetProperty(key)?.GetValue(values).ToString();

				resultBuilder.Append(HttpUtility.UrlEncode(key));
				resultBuilder.Append("=");
				resultBuilder.Append(HttpUtility.UrlEncode(value));
				resultBuilder.Append("&");
			}
			resultBuilder.Length--; // delete trailing '&'

			return resultBuilder.ToString();
		}

		internal HttpWebRequest Build()
		{
			var url = BuildUri();
			var requestMethod = "POST";
			var isChunked = true;
			var allowWriteStreamBuffer = false;

			// Possible formats for Content-Type header
			// audio/x-wav;codec=pcm;bit=16;rate=8000  - it is default
			// audio/x-wav;codec=pcm;bit=16;rate=16000
			// audio/x-wav;codec=pcm;bit=16;rate=22000
			// audio/x-speex;rate=8000
			// audio/x-speex;rate=11025
			// audio/x-speex;rate=16000
			// audio/x-speex;rate=16000
			// audio / amr
			// audio/qcelp
			// audio/evrc
			var contentType = "audio/x-wav;codec=pcm;bit=16;rate=8000";
			var accept = "text/plain"; // default is 'text/plain'. Other value is 'application/xml' (not supported yet)
			var acceptLanguage = "en-US";
				// default is 'enus'. Supported values - https://developer.nuance.com/public/index.php?task=supportedLanguages
			var acceptTopic = "Dictation";
				// default is 'Dictation'. Other values are Dictation, WebSearch and DTV-Search (beta only).
			var xDictationNBestListSize = 5; // default is '10'. Valid values are 1-10.
			var xDictationAudioSource = "SpeakerAndMicrophone";
				// SpeakerAndMicrophone, HeadsetInOut, HeadsetBT, HeadPhone, LineOut




			var requestUserAgent = "myAppUserAgent";

			var request = (HttpWebRequest)WebRequest.Create(url);
			request.Method = requestMethod;
			request.AllowWriteStreamBuffering = allowWriteStreamBuffer;
			request.SendChunked = isChunked;

			request.ContentType = contentType;
			request.Accept = accept;
			request.Headers.Add($"Accept-Language: {acceptLanguage}");
			request.Headers.Add($"Accept-Topic: {acceptTopic}");
			request.Headers.Add($"X-Dictation-NBestListSize: {xDictationNBestListSize}");
			request.Headers.Add($"X-Dictation-AudioSource: {xDictationAudioSource}");


			request.UserAgent = requestUserAgent;

			return request;
		}
	}
}
