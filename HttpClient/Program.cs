using System;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Web;

namespace HttpClient
{
	class Program
	{
		const string NuanceHostName = "dictation.nuancemobility.net";
		const string NuancePath = "/NMDPAsrCmdServlet/dictation";

		static Uri BuildUri()
		{
			var uriBuilder = new UriBuilder();
			uriBuilder.Scheme = Uri.UriSchemeHttps;
			uriBuilder.Host = NuanceHostName;
			uriBuilder.Path = NuancePath;
			uriBuilder.Query = BuildUrlQuery(new { appId = "MY_APP", appKey = "777", id = "111" });
			return uriBuilder.Uri;
		}

		static string BuildUrlQuery(object values)
		{
			var type = values.GetType();
			var fields = type.GetFields();
			var resultBuilder = new StringBuilder("?", 32);
			foreach (var field in fields)
			{
				var key = field.Name;
				var value = type.GetField(key)?.GetValue(values);

				resultBuilder.Append(HttpUtility.UrlEncode(key));
				resultBuilder.Append("=");
				resultBuilder.Append(HttpUtility.UrlEncode((string) value));
				resultBuilder.Append("&");
			}
			resultBuilder.Length--;

			return resultBuilder.ToString();
		}

		static void Main(string[] args)
		{
			var url = BuildUri();
			var requestMethod = "POST";
			var requestContentType = "application/octet-stream";
			var isChunked = true;
			var allowWriteStreamBuffer = false;
			var requestUserAgent = "mine";
			var requestAcceptHeader = "text/plain";
			var AcceptLanguage = "en-US";

			var request = (HttpWebRequest)WebRequest.Create(url);
			request.Method = requestMethod;
			request.AllowWriteStreamBuffering = allowWriteStreamBuffer;
			request.SendChunked = isChunked;

			request.ContentType = requestContentType;
			request.UserAgent = requestUserAgent;
			request.Accept = requestAcceptHeader;
			request.Headers.Add($"Accept-Language: {AcceptLanguage}");

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
