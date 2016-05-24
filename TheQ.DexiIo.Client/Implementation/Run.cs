using Newtonsoft.Json;


namespace TheQ.DexiIo.Client.Implementation
{
	/// <summary>
	///     Cloud Scrape Run DTO
	/// </summary>
	public class Run
	{
		/// <summary>
		///     The ID of the run
		/// </summary>
		[JsonProperty("id")]
		public string Id { get; set; }


		/// <summary>
		///     Name of the run
		/// </summary>
		[JsonProperty("name")]
		public string Name { get; set; }
	}
}