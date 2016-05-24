using System.Net;


namespace TheQ.DexiIo.Client.Implementation
{
	/// <summary>
	///     Cloud Response details
	/// </summary>
	public class Response
	{
		public string Content { get; set; }
		public WebHeaderCollection Headers { get; set; }
		public HttpStatusCode StatusCode { get; set; }
		public string StatusDescription { get; set; }
	}
}