using ImageMagick;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Diagnostics;

namespace HeicToPngConverter
{
	/// <summary>
	/// ����������, �������������� .heic ����������� � .png
	/// </summary>
	public partial class Application : Form
	{
		private bool processInProgress;
		private bool backgroundWorkerCancelationByError;
		private string backgroundWorkerCancelationMessage;

		private OpenFileDialog openFileDialog;
		private FolderBrowserDialog openFolderDialog;
		private ConcurrentDictionary<int, string> lastProcessedFiles;

		public Application()
		{
			InitializeComponent();

			backgroundWorker.ProgressChanged += BackgroundWorker_ProgressChanged;
			backgroundWorker.RunWorkerCompleted += BackgroundWorker_Ready;
			backgroundWorker.DoWork += BackgroundWorker_DoWork;

			openFileDialog = new OpenFileDialog() { Title = "�������� .heic ����������� / Select .heic images", Filter = "Heic images|*.heic", Multiselect = true };
			openFolderDialog = new FolderBrowserDialog() { Description = "�������� ����� ��� ���������� .png / Select folder to save .png", UseDescriptionForTitle = true };
		}

		/// <summary>
		/// �����, �������������� ������� �� ������ �����������
		/// </summary>
		private void convertButton_Click(object sender, EventArgs e)
		{
			if (!processInProgress)
			{
				convertButton.Text = "������ (Cancel)";

				processInProgress = true;
				var openFilesResult = openFileDialog.ShowDialog();

				if (openFilesResult == DialogResult.OK)
				{
					var openFolderResult = openFolderDialog.ShowDialog();

					if (openFolderResult == DialogResult.OK)
					{
						var directory = new DirectoryInfo(openFolderDialog.SelectedPath);

						var backgroundWorkerDataTransferObject = new BackgroundWorkerDataTransferObject
						{
							TargetPath = directory.FullName,
							Files = openFileDialog.FileNames.Select(x => new FileInfo(x)).Where(x => x.Exists).ToList(),
						};

						progressBar.Maximum = backgroundWorkerDataTransferObject.Files.Count;
						progressBar.Minimum = 0;
						progressBar.Step = 1;

						lastProcessedFiles = new ConcurrentDictionary<int, string>();

						backgroundWorker.RunWorkerAsync(backgroundWorkerDataTransferObject);
					}
				}
			}
			else
			{
				backgroundWorkerCancelationMessage = "������� ����������� ���������� (Convertation process was canceled)";
				backgroundWorker.CancelAsync();
				convertButton.Enabled = false;
			}
		}

		/// <summary>
		/// ����� �������� �������, �������������� ��������� ������
		/// </summary>
		private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			try
			{
				var transferData = e.Argument as BackgroundWorkerDataTransferObject;
				var worker = sender as BackgroundWorker;

				Parallel.For(0, transferData.Files.Count, (i, state) =>
				{
					if (worker.CancellationPending) 
						state.Break();

					var fileName = transferData.Files[i].Name.Replace(transferData.Files[i].Extension, "");
					var targetPath = $"{transferData.TargetPath}\\{fileName}.png";

					RemoveFileIfExists(targetPath);

					using (MagickImage image = new MagickImage(transferData.Files[i].FullName))
					{
						lastProcessedFiles.TryAdd(i, targetPath);

						image.Format = MagickFormat.Png;
						image.Write(targetPath);
						worker.ReportProgress(1);

						lastProcessedFiles.TryRemove(lastProcessedFiles.First(x => x.Key == i));
					}
				});
			} 
			catch
			{
				backgroundWorkerCancelationMessage = "������ � �������� ������ ������. ��������� ��������� ����� �� ����� � ��������� ����������� ������ � ��������� ����� �������� ������������ (Error during file converting progress, check for free disk space and current user write access to selected folder)";
				backgroundWorkerCancelationByError = true;
			}
		}

		/// <summary>
		/// �����, ���������� �������� ��������� ��������� ���������� (��� ���������� �������� ����)
		/// </summary>
		private void BackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			progressBar.Value += e.ProgressPercentage;
		}

		/// <summary>
		/// �����, ������������� �� �������, �������������� ��� ���������� �������� ����������� ������� ��������
		/// </summary>
		private void BackgroundWorker_Ready(object sender, RunWorkerCompletedEventArgs e)
		{
			BackgroundWorker_Ready();
		}

		/// <summary>
		/// �����, �������������� ��� ���������� �������� ����������� ������� ��������
		/// </summary>
		private void BackgroundWorker_Ready()
		{
			var messageBoxText = backgroundWorkerCancelationMessage ?? "��� ����� ��������������� ������� (All files converted successfully)";
			var messageBoxTittle = string.IsNullOrEmpty(backgroundWorkerCancelationMessage) ? "������" : "��������";
			var dialogResult = MessageBox.Show(messageBoxText, messageBoxTittle, MessageBoxButtons.OK);

			if (dialogResult == DialogResult.OK)
			{
				ApplicationWindowSetInitialState();
				backgroundWorkerCancelationMessage = null;

				if (backgroundWorkerCancelationByError == true)
				{
					foreach (var record in lastProcessedFiles)
					{
						RemoveFileIfExists(record.Value);
					}

					backgroundWorkerCancelationByError = false;
				}
			}
		}

		/// <summary>
		/// ������� ����, ���� �� ���������� (� ������ ������ �����������, ������������ �����, ��� ������������ ����� ��� ������ �����������)
		/// </summary>
		private void RemoveFileIfExists(string path)
		{
			if (!string.IsNullOrEmpty(path))
			{
				var lastFile = new FileInfo(path);

				if (lastFile.Exists)
					lastFile.Delete();
			}
		}

		/// <summary>
		/// ���������� ���������� ����� � ��������������� ���� (����� ������������ ��� ��������� �����������)
		/// </summary>
		private void ApplicationWindowSetInitialState()
		{
			progressBar.Value = 0;
			processInProgress = false;
			convertButton.Enabled = true;
			convertButton.Text = "�������������� .heic to .png (Convert .heic to .png)";
		}

		/// <summary>
		/// ������ �� ����� Binance
		/// </summary>
		private void binanceLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			var cryptoWalletsForm = new CryptoWallets();

			cryptoWalletsForm.Show();
		}

		/// <summary>
		/// ������ �� �����
		/// </summary>
		private void mailLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			OpenLink("mailto:medvedevpetr[at]yandex.ru?subject=Heic to png converter");
		}

		/// <summary>
		/// ������ �� ����� yoomoney
		/// </summary>
		private void ymoneyLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			OpenLink("https://yoomoney.ru/to/41001350655590");
		}

		/// <summary>
		/// ������ �� ������ ����������
		/// </summary>
		private void githubLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			OpenLink("https://github.com/ATRedline/SimpleHeicToPngConverter");
		}

		/// <summary>
		/// �����, ����������� ������ � ��������
		/// </summary>
		private void OpenLink(string link)
		{
			Process.Start(new ProcessStartInfo(link) { UseShellExecute = true });
		}

		#region ���-��, ���� �� ������� � ��������� ������� winform ���������� .Net 6 (Something, what missed in default preset of .Net 6 winforms application)

		public static void SetCompatibleTextRenderingDefault(bool defaultValue) { }
		public static bool SetHighDpiMode(HighDpiMode highDpiMode) => false;
		public static void EnableVisualStyles() { }

		#endregion
	}
}