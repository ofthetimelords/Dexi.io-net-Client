using System.Threading.Tasks;

using Newtonsoft.Json;


namespace TheQ.DexiIo.Client.Implementation
{
	/// <summary>
	///     Run Cloud Scrape robots
	/// </summary>
	internal class RunsProvider
	{
		/// <summary>
		///     Constructor
		/// </summary>
		/// <param name="processor"></param>
		public RunsProvider(DexiIoProcessor processor)
		{
			this.Processor = processor;
		}



		public DexiIoProcessor Processor { get; }



		/// <summary>
		///     Get Run Detail
		/// </summary>
		/// <param name="runId"></param>
		/// <returns></returns>
		public async Task<Run> GetAsync(string runId)
		{
			var strResponse = await this.Processor.RequestJsonAsync("runs/" + runId).ConfigureAwait(false);
			var result = JsonConvert.DeserializeObject<Run>(strResponse);
			return result;
		}



		/// <summary>
		///     Permanently delete run
		/// </summary>
		/// <param name="runId"></param>
		/// <returns></returns>
		public Task<bool> RemoveAsync(string runId)
		{
			return this.Processor.RequestBooleanAsync("runs/" + runId, "DELETE");
		}



		/// <summary>
		///     Start new execution of the run
		/// </summary>
		/// <param name="runId"></param>
		/// <returns></returns>
		public async Task<Execution> ExecuteAsync(string runId)
		{
			var strResponse = await this.Processor.RequestJsonAsync("runs/" + runId + "/execute", "POST").ConfigureAwait(false);
			var result = JsonConvert.DeserializeObject<Execution>(strResponse);
			return result;
		}



		/// <summary>
		///     Start new execution of the run, and wait for it to finish before returning the result.
		///     The execution and result will be automatically deleted from CloudScrape completion
		///     both successful and failed.
		/// </summary>
		/// <param name="runId"></param>
		/// <returns></returns>
		public async Task<Result> ExecuteSyncAsync(string runId)
		{
			var strResponse = await this.Processor.RequestJsonAsync("runs/" + runId + "/execute/wait", "POST").ConfigureAwait(false);
			var result = JsonConvert.DeserializeObject<Result>(strResponse);
			return result;
		}



		/// <summary>
		///     Starts new execution of run with given inputs
		/// </summary>
		/// <param name="runId"></param>
		/// <param name="inputs"></param>
		/// <returns></returns>
		public async Task<Execution> ExecuteWithInputAsync(string runId, string inputs)
		{
			var strResponse = await this.Processor.RequestJsonAsync("runs/" + runId + "/execute/inputs", "POST", inputs).ConfigureAwait(false);
			var result = JsonConvert.DeserializeObject<Execution>(strResponse);
			return result;
		}



		/// <summary>
		///     Starts new execution of run with given inputs, and wait for it to finish before returning the result.
		///     The inputs, execution and result will be automatically deleted from CloudScrape upon completion - both successful and failed.
		/// </summary>
		/// <param name="runId"></param>
		/// <param name="inputs"></param>
		/// <returns></returns>
		public async Task<Result> ExecuteWithInputSyncAsync(string runId, string inputs)
		{
			var strResponse = await this.Processor.RequestJsonAsync("runs/" + runId + "/execute/inputs/wait", "POST", inputs).ConfigureAwait(false);
			var result = JsonConvert.DeserializeObject<Result>(strResponse);
			return result;
		}



		/// <summary>
		///     Get the result from the latest execution of the given run.
		/// </summary>
		/// <param name="runId"></param>
		/// <returns></returns>
		public async Task<Result> GetLatestResultAsync(string runId)
		{
			var strResponse = await this.Processor.RequestJsonAsync("runs/" + runId + "/latest/result").ConfigureAwait(false);
			var result = JsonConvert.DeserializeObject<Result>(strResponse);
			return result;
		}



		/// <summary>
		///     Get executions for the given run.
		/// </summary>
		/// <param name="runId"></param>
		/// <param name="offset"></param>
		/// <param name="limit"></param>
		/// <returns></returns>
		public async Task<ExecutionList> GetExecutionsAsync(string runId, int offset = 0, int limit = 30)
		{
			var strResponse = await this.Processor.RequestJsonAsync("runs/" + runId + "/executions?offset=" + offset + "&limit=" + limit).ConfigureAwait(false);
			var result = JsonConvert.DeserializeObject<ExecutionList>(strResponse);
			return result;
		}
	}
}