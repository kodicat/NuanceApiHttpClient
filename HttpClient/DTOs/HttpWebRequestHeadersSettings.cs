namespace HttpClient.DTOs
{
	class HttpWebRequestHeadersSettings
	{
		public bool IsChunked { get; set; }
		public string ContentType { get; set; }
		public string Accept { get; set; }
		public string AcceptLanguage { get; set; }
		public string AcceptTopic { get; set; }
		public int XDictationNBestListSize { get; set; }
		public string XDictationAudioSource { get; set; }
	}
}
