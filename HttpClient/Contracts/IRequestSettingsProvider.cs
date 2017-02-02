using HttpClient.DTOs;

namespace HttpClient.Contracts
{
	interface IRequestSettingsProvider
	{
		HttpWebRequestHeadersSettings Provide();
	}
}
