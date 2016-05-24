namespace TheQ.DexiIo.Client.Implementation
{
	/// <summary>
	///     Cloud Scrape Result DTO
	/// </summary>
	public class Result
	{
		public string[] Headers { get; set; }

		public string[][] Rows { get; set; }

		public int TotalRows { get; set; }
	}
}