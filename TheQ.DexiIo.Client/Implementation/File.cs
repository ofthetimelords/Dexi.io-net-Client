namespace TheQ.DexiIo.Client.Implementation
{
	/// <summary>
	///     Cloud Scrape File DTO
	/// </summary>
	public class File
	{
		/// <summary>
		///     Constructor
		/// </summary>
		/// <param name="mimeType"></param>
		/// <param name="contents"></param>
		public File(string mimeType, string contents)
		{
			this.MimeType = mimeType;
			this.Contents = contents;
		}



		/// <summary>
		///     The type of file
		/// </summary>
		public string MimeType { get; }


		/// <summary>
		///     The contents of the file
		/// </summary>
		public string Contents { get; }
	}
}