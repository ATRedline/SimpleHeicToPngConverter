namespace HeicToPngConverter
{
	public class BackgroundWorkerDataTransferObject
	{
		public string TargetPath { get; set; }
		public List<FileInfo> Files { get; set; }
	}
}
