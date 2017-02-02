using System.Net;
using HttpClient.DTOs;
using HttpClient.Extensions;
using HttpClient.Helpers;

namespace HttpClient
{
	class HttpWebRequestBuilder
	{
		internal HttpWebRequest Build(UrlSettings urlSettings, HttpWebRequestHeadersSettings headersSettings)
		{
			var url = UrlBuilder.BuildUri(urlSettings);
			return ((HttpWebRequest)WebRequest.Create(url))
				.BuildGeneralValues()
				.BuildHeaders(headersSettings);
				//.BuildBody();
		}
	}
}
