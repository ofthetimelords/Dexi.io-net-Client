using System.Threading.Tasks;

using Newtonsoft.Json;


namespace TheQ.DexiIo.Client.Implementation
{
	/// <summary>
	///     Execute Cloud Scrape request
	/// </summary>
	public class ExecutionsProvider
	{
		public ExecutionsProvider(DexiIoProcessor processor)
		{
			this.Processor = processor;
		}



		public DexiIoProcessor Processor { get; }



		/// <summary>
		///     Get execution
		/// </summary>
		/// <param name="executionId"></param>
		/// <returns></returns>
		public async Task<Execution> GetAsync(string executionId)
		{
			var strResponse = await this.Processor.RequestJsonAsync("executions/" + executionId).ConfigureAwait(false);

			var result = JsonConvert.DeserializeObject<Execution>(strResponse);
			return result;
		}



		/// <summary>
		///     Delete execution permanently
		/// </summary>
		/// <param name="executionId"></param>
		/// <returns></returns>
		public Task<bool> RemoveAsync(string executionId)
		{
			return this.Processor.RequestBooleanAsync("executions/" + executionId, "DELETE");
		}



		/// <summary>
		///     Get the entire result of an execution.
		/// </summary>
		/// <param name="executionId"></param>
		/// <returns></returns>
		public async Task<Result> GetResultAsync(string executionId)
		{
			var strResponse = await this.Processor.RequestJsonAsync("executions/" + executionId + "/result").ConfigureAwait(false);
			var result = JsonConvert.DeserializeObject<Result>(strResponse);
			return result;
		}



		/// <summary>
		///     Get a file from a result set
		/// </summary>
		/// <param name="executionId"></param>
		/// <param name="fileId"></param>
		/// <returns></returns>
		public async Task<File> GetResultFileAsync(string executionId, string fileId)
		{
			var response = await this.Processor.RequestAsync("executions/" + executionId + "/file/" + fileId).ConfigureAwait(false);
			return new File(response.Headers["Content-Type"], response.Content);
		}



		/// <summary>
		///     Stop running execution
		/// </summary>
		/// <param name="executionId"></param>
		/// <returns></returns>
		public Task<bool> StopAsync(string executionId)
		{
			return this.Processor.RequestBooleanAsync("executions/" + executionId + "/stop", "POST");
		}



		/// <summary>
		///     Resume stopped execution
		/// </summary>
		/// <param name="executionId"></param>
		/// <returns></returns>
		public Task<bool> ResumeAsync(string executionId)
		{
			return this.Processor.RequestBooleanAsync("executions/" + executionId + "/continue", "POST");
		}
	}
}