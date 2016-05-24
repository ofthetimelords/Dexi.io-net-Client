using System;

using Newtonsoft.Json;

using TheQ.DexiIo.Client.Support;


namespace TheQ.DexiIo.Client.Implementation
{
	/// <summary>
	///     Cloud Scrape Execution DTO class
	/// </summary>
	internal class Execution
	{
		public const string Queued = "QUEUED";
		public const string Pending = "PENDING";
		public const string Running = "RUNNING";
		public const string Failed = "FAILED";
		public const string Stopped = "STOPPED";
		public const string OK = "OK";


		/// <summary>
		///     The ID of the execution
		/// </summary>
		[JsonProperty("_id")]
		public string Id { get; set; }


		/// <summary>
		///     State of the executions. See const definitions on class to see options
		/// </summary>
		[JsonProperty("state")]
		public string State { get; set; }


		/// <summary>
		///     Time the executions was started - in milliseconds since unix epoch
		/// </summary>
		[JsonProperty("starts")]
		[JsonConverter(typeof (DexiIoDateConverter))]
		public DateTimeOffset Starts { get; set; }


		/// <summary>
		///     Time the executions finished - in milliseconds since unix epoch.
		/// </summary>
		[JsonProperty("finished")]
		[JsonConverter(typeof (DexiIoDateConverter))]
		public DateTimeOffset? Finished { get; set; }
	}



	/// <summary>
	///     Cloud Scrape Execution List DTO
	/// </summary>
	internal class ExecutionList
	{
		/// <summary>
		///     off set
		/// </summary>
		[JsonProperty("offset")]
		public int Offset { get; set; }


		/// <summary>
		///     total rows
		/// </summary>
		[JsonProperty("totalRows")]
		public int TotalRows { get; set; }


		/// <summary>
		///     array of rows
		/// </summary>
		[JsonProperty("rows")]
		public Execution[] Rows { get; set; }
	}
}