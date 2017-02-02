using System;
using System.Text;
using System.Web;
using HttpClient.DTOs;

namespace HttpClient.Helpers
{
	static class UrlBuilder
	{
		internal static Uri BuildUri(UrlSettings urlSettings)
		{
			var uriBuilder = new UriBuilder
			{
				Scheme = urlSettings.Scheme,
				Host = urlSettings.Host,
				Path = urlSettings.Path,
				Query = BuildUrlQuery(new
				{
					appId = urlSettings.AppId,
					appKey = urlSettings.AppKey,
					id = urlSettings.Id
				})
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
	}
}
