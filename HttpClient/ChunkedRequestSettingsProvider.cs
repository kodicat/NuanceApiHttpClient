using System;
using HttpClient.Contracts;
using HttpClient.DTOs;
using HttpClient.DTOs.Enums.Headers;

namespace HttpClient
{
	class ChunkedRequestSettingsProvider : IRequestSettingsProvider
	{
		public HttpWebRequestHeadersSettings Provide()
		{
			return new HttpWebRequestHeadersSettings
			{
				IsChunked = true,
				Accept = Accept.TextPlain,
				AcceptLanguage = AcceptLanguage.Ukrainian,
				AcceptTopic = AcceptTopic.Dictation,
				ContentType = ContentType.Pcm8,
				XDictationAudioSource = XDictationAudioSource.SpeakerAndMicrophone,
				XDictationNBestListSize = 5 // default is '10'.Valid values are 1 - 10.
			};
		}
	}
}
