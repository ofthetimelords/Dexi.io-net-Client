using System;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;


namespace TheQ.DexiIo.Client.Implementation
{
	/// <summary>
	///     Cloud Scrape Client
	/// </summary>
	public class DexiIoProcessor
	{
		private readonly string _accessKey;
		private readonly string _accountId;



		public DexiIoProcessor(string apiKey, string accountId, string userAgent) : this(apiKey, accountId)
		{
			this.UserAgent = userAgent;
		}

		public DexiIoProcessor(string apiKey, string accountId)
		{
			if (string.IsNullOrWhiteSpace(accountId))
				throw new ArgumentException("A non-empty value for parameter accountId must be provided.", nameof(accountId));
			if (string.IsNullOrWhiteSpace(apiKey))
				throw new ArgumentException("A non-empty value for parameter apiKey must be provided.", nameof(apiKey));

			this._accountId = accountId;
			this._accessKey = this.CreateMD5(accountId + apiKey).ToLower();
			this.Executions = new ExecutionsProvider(this);
			this.Runs = new RunsProvider(this);
		}



		internal ExecutionsProvider Executions { get; }

		internal RunsProvider Runs { get; }


		/// <summary>
		///     Endpoint / base url of requests
		/// </summary>
		public string EndPoint { get; set; } = "https://api.dexi.io/";


		/// <summary>
		///     User agent of requests
		/// </summary>
		public string UserAgent { get; set; } = "C#-CLIENT/0.9";


		/// <summary>
		///     Set request timeout. Defaults to 1 hour
		///     Note: If you are using the sync methods and some requests are running for very long you need to increase this value.
		/// </summary>
		public int RequestTimeout { get; set; } = 60*60*1000;



		/// <summary>
		///     Make a call to the CloudScrape API
		/// </summary>
		/// <param name="url"></param>
		/// <param name="method"></param>
		/// <param name="body"></param>
		/// <returns></returns>
		public async Task<Response> RequestAsync(string url, string method = "GET", string body = null)
		{
			var req = WebRequest.Create(this.EndPoint + url) as HttpWebRequest;

			req.Headers.Add("X-DexiIO-Access", this._accessKey);
			req.Headers.Add("X-DexiIO-Account", this._accountId);
			req.UserAgent = this.UserAgent;
			req.Timeout = this.RequestTimeout;
			req.Accept = "application/json";
			req.ContentType = "application/json";
			req.Method = method;

			if (body != null)
				using (var streamWriter = new StreamWriter(await req.GetRequestStreamAsync().ConfigureAwait(false)))
				{
					await streamWriter.WriteAsync(body).ConfigureAwait(false);
					await streamWriter.FlushAsync().ConfigureAwait(false);
				}


			using (var response = (HttpWebResponse) await req.GetResponseAsync().ConfigureAwait(false))
			{
				var objResponse = new Response {StatusCode = response.StatusCode, Headers = response.Headers, StatusDescription = response.StatusDescription};


				using (var receiveStream = response.GetResponseStream())
				using (var readStream = new StreamReader(receiveStream, Encoding.UTF8))
					objResponse.Content = await readStream.ReadToEndAsync().ConfigureAwait(false);

				return objResponse;
			}
		}



		/// <summary>
		///     MD5 Encryption
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		private string CreateMD5(string input)
		{
			// Use input string to calculate MD5 hash
			using (var md5 = MD5.Create())
			{
				var inputBytes = Encoding.ASCII.GetBytes(input);
				var hashBytes = md5.ComputeHash(inputBytes);

				// Convert the byte array to hexadecimal string
				var sb = new StringBuilder();
				for (var i = 0; i < hashBytes.Length; i++)
					sb.Append(hashBytes[i].ToString("X2"));

				return sb.ToString();
			}
		}



		/// <summary>
		///     Execute request
		/// </summary>
		/// <param name="url"></param>
		/// <param name="method"></param>
		/// <param name="body"></param>
		/// <returns></returns>
		public async Task<string> RequestJsonAsync(string url, string method = "GET", string body = null)
		{
			var response = await this.RequestAsync(url, method, body).ConfigureAwait(false);
			return response.Content;
		}



		/// <summary>
		///     Get response boolean value
		/// </summary>
		/// <param name="url"></param>
		/// <param name="method"></param>
		/// <param name="body"></param>
		/// <returns></returns>
		public async Task<bool> RequestBooleanAsync(string url, string method = "GET", string body = null)
		{
			await this.RequestAsync(url, method, body).ConfigureAwait(false);
			return true;
		}
	}
}