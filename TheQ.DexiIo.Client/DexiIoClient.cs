using TheQ.DexiIo.Client.Implementation;


namespace TheQ.DexiIo.Client
{
	internal class DexiIoClient
	{
		public DexiIoClient(string apiKey, string accountId)
		{
			this.Processor = new DexiIoProcessor(apiKey, accountId);
		}

		public DexiIoClient(string apiKey, string accountId, string userAgent)
		{
			this.Processor = new DexiIoProcessor(apiKey, accountId, userAgent);
		}



		public DexiIoProcessor Processor { get; }


		/// <summary>
		///     Execute Method
		/// </summary>
		/// <returns></returns>
		public ExecutionsProvider Executions => this.Processor.Executions;


		/// <summary>
		///     Run Method
		/// </summary>
		/// <returns></returns>
		public RunsProvider Runs => this.Processor.Runs;
	}
}