using System.ComponentModel;

namespace HeicToPngConverter
{
	partial class Application
	{
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///  Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		/// 
		private void InitializeComponent()
		{
			this.convertButton = new System.Windows.Forms.Button();
			this.progressBar = new System.Windows.Forms.ProgressBar();
			this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
			this.ymoneyLink = new System.Windows.Forms.LinkLabel();
			this.binanceLink = new System.Windows.Forms.LinkLabel();
			this.mailLink = new System.Windows.Forms.LinkLabel();
			this.githubLink = new System.Windows.Forms.LinkLabel();
			this.heicImage = new System.Windows.Forms.PictureBox();
			this.pngImage = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.heicImage)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pngImage)).BeginInit();
			this.SuspendLayout();
			// 
			// convertButton
			// 
			this.convertButton.Location = new System.Drawing.Point(12, 205);
			this.convertButton.Name = "convertButton";
			this.convertButton.Size = new System.Drawing.Size(462, 51);
			this.convertButton.TabIndex = 0;
			this.convertButton.Text = "Конвертировать .heic to .png (Convert .heic to .png)";
			this.convertButton.UseVisualStyleBackColor = true;
			this.convertButton.Click += new System.EventHandler(this.convertButton_Click);
			// 
			// progressBar
			// 
			this.progressBar.Location = new System.Drawing.Point(1, 269);
			this.progressBar.Name = "progressBar";
			this.progressBar.Size = new System.Drawing.Size(483, 20);
			this.progressBar.TabIndex = 1;
			// 
			// backgroundWorker
			// 
			this.backgroundWorker.WorkerReportsProgress = true;
			this.backgroundWorker.WorkerSupportsCancellation = true;
			// 
			// ymoneyLink
			// 
			this.ymoneyLink.AutoSize = true;
			this.ymoneyLink.Location = new System.Drawing.Point(307, 9);
			this.ymoneyLink.Name = "ymoneyLink";
			this.ymoneyLink.Size = new System.Drawing.Size(98, 15);
			this.ymoneyLink.TabIndex = 2;
			this.ymoneyLink.TabStop = true;
			this.ymoneyLink.Text = "Сказать спасибо";
			this.ymoneyLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ymoneyLink_LinkClicked);
			// 
			// binanceLink
			// 
			this.binanceLink.AutoSize = true;
			this.binanceLink.Location = new System.Drawing.Point(411, 9);
			this.binanceLink.Name = "binanceLink";
			this.binanceLink.Size = new System.Drawing.Size(63, 15);
			this.binanceLink.TabIndex = 3;
			this.binanceLink.TabStop = true;
			this.binanceLink.Text = "Say thanks";
			this.binanceLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.binanceLink_LinkClicked);
			// 
			// mailLink
			// 
			this.mailLink.AutoSize = true;
			this.mailLink.Location = new System.Drawing.Point(156, 9);
			this.mailLink.Name = "mailLink";
			this.mailLink.Size = new System.Drawing.Size(145, 15);
			this.mailLink.TabIndex = 4;
			this.mailLink.TabStop = true;
			this.mailLink.Text = "medvedevpetr@yandex.ru";
			this.mailLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.mailLink_LinkClicked);
			// 
			// githubLink
			// 
			this.githubLink.AutoSize = true;
			this.githubLink.Location = new System.Drawing.Point(105, 9);
			this.githubLink.Name = "githubLink";
			this.githubLink.Size = new System.Drawing.Size(45, 15);
			this.githubLink.TabIndex = 5;
			this.githubLink.TabStop = true;
			this.githubLink.Text = "GitHub";
			this.githubLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.githubLink_LinkClicked);
			// 
			// heicImage
			// 
			this.heicImage.Image = global::HeicToPngConverter.Properties.Resources.HEIC;
			this.heicImage.Location = new System.Drawing.Point(68, 38);
			this.heicImage.Name = "heicImage";
			this.heicImage.Size = new System.Drawing.Size(186, 153);
			this.heicImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.heicImage.TabIndex = 6;
			this.heicImage.TabStop = false;
			// 
			// pngImage
			// 
			this.pngImage.Image = global::HeicToPngConverter.Properties.Resources.PNG;
			this.pngImage.Location = new System.Drawing.Point(270, 27);
			this.pngImage.Name = "pngImage";
			this.pngImage.Size = new System.Drawing.Size(145, 172);
			this.pngImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pngImage.TabIndex = 7;
			this.pngImage.TabStop = false;
			// 
			// Application
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(486, 291);
			this.Controls.Add(this.pngImage);
			this.Controls.Add(this.heicImage);
			this.Controls.Add(this.githubLink);
			this.Controls.Add(this.mailLink);
			this.Controls.Add(this.binanceLink);
			this.Controls.Add(this.ymoneyLink);
			this.Controls.Add(this.progressBar);
			this.Controls.Add(this.convertButton);
			this.Name = "Application";
			this.Text = "Simle HEIC to PNG converter";
			((System.ComponentModel.ISupportInitialize)(this.heicImage)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pngImage)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Button convertButton;
		private ProgressBar progressBar;
		private BackgroundWorker backgroundWorker;
		private LinkLabel ymoneyLink;
		private LinkLabel binanceLink;
		private LinkLabel mailLink;
		private LinkLabel githubLink;
		private PictureBox heicImage;
		private PictureBox pngImage;
	}
}