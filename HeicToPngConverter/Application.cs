using ImageMagick;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Diagnostics;

namespace HeicToPngConverter
{
	/// <summary>
	/// Приложение, конвертирующее .heic изображения в .png
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

			openFileDialog = new OpenFileDialog() { Title = "Выберите .heic изображения / Select .heic images", Filter = "Heic images|*.heic", Multiselect = true };
			openFolderDialog = new FolderBrowserDialog() { Description = "Выберите папку для сохранения .png / Select folder to save .png", UseDescriptionForTitle = true };
		}

		/// <summary>
		/// Метод, отрабатывающий нажатие на кнопку конвертации
		/// </summary>
		private void convertButton_Click(object sender, EventArgs e)
		{
			if (!processInProgress)
			{
				convertButton.Text = "Отмена (Cancel)";

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
				backgroundWorkerCancelationMessage = "Процесс конвертации остановлен (Convertation process was canceled)";
				backgroundWorker.CancelAsync();
				convertButton.Enabled = false;
			}
		}

		/// <summary>
		/// Метод фонового воркера, осуществляющий обработку файлов
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
				backgroundWorkerCancelationMessage = "Ошибка в процессе записи файлов. Проверьте свободное место на диске и проверьте доступность записи в выбранную папку текущего пользователя (Error during file converting progress, check for free disk space and current user write access to selected folder)";
				backgroundWorkerCancelationByError = true;
			}
		}

		/// <summary>
		/// Метод, вызываемый событием изменения прогресса выполнения (для заполнения прогресс бара)
		/// </summary>
		private void BackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			progressBar.Value += e.ProgressPercentage;
		}

		/// <summary>
		/// Метод, подписываемый на событие, отрабатывающее при завершении процесса конвертации фоновым воркером
		/// </summary>
		private void BackgroundWorker_Ready(object sender, RunWorkerCompletedEventArgs e)
		{
			BackgroundWorker_Ready();
		}

		/// <summary>
		/// Метод, отрабатывающий при завершении процесса конвертации фоновым воркером
		/// </summary>
		private void BackgroundWorker_Ready()
		{
			var messageBoxText = backgroundWorkerCancelationMessage ?? "Все файлы сконвертированы успешно (All files converted successfully)";
			var messageBoxTittle = string.IsNullOrEmpty(backgroundWorkerCancelationMessage) ? "Готово" : "Внимание";
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
		/// Удаляет файл, если он существует (в случае ошибки конвертации, недописанные файлы, или существующие файлы при начале конвертации)
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
		/// Возвращает компоненты формы к первоначальному виду (после отработанной или ошибочной конвертации)
		/// </summary>
		private void ApplicationWindowSetInitialState()
		{
			progressBar.Value = 0;
			processInProgress = false;
			convertButton.Enabled = true;
			convertButton.Text = "Конвертировать .heic to .png (Convert .heic to .png)";
		}

		/// <summary>
		/// Ссылка на донат Binance
		/// </summary>
		private void binanceLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			var cryptoWalletsForm = new CryptoWallets();

			cryptoWalletsForm.Show();
		}

		/// <summary>
		/// Ссылка на почту
		/// </summary>
		private void mailLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			OpenLink("mailto:medvedevpetr[at]yandex.ru?subject=Heic to png converter");
		}

		/// <summary>
		/// Ссылка на донат yoomoney
		/// </summary>
		private void ymoneyLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			OpenLink("https://yoomoney.ru/to/41001350655590");
		}

		/// <summary>
		/// Ссылка на гитхаб приложения
		/// </summary>
		private void githubLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			OpenLink("https://github.com/ATRedline/SimpleHeicToPngConverter");
		}

		/// <summary>
		/// Метод, открывающий ссылку в браузере
		/// </summary>
		private void OpenLink(string link)
		{
			Process.Start(new ProcessStartInfo(link) { UseShellExecute = true });
		}

		#region Что-то, чего не хватало в дефолтном пресете winform приложения .Net 6 (Something, what missed in default preset of .Net 6 winforms application)

		public static void SetCompatibleTextRenderingDefault(bool defaultValue) { }
		public static bool SetHighDpiMode(HighDpiMode highDpiMode) => false;
		public static void EnableVisualStyles() { }

		#endregion
	}
}