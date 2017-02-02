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
		static void Main(string[] args)
		{
			var request = new HttpWebRequestBuilder().Build();
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
