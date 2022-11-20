namespace HeicToPngConverter
{
	partial class CryptoWallets
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
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
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.binancePicture = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.binancePicture)).BeginInit();
			this.SuspendLayout();
			// 
			// binancePicture
			// 
			this.binancePicture.Image = global::HeicToPngConverter.Properties.Resources.BinancePayQR;
			this.binancePicture.Location = new System.Drawing.Point(12, 12);
			this.binancePicture.Name = "binancePicture";
			this.binancePicture.Size = new System.Drawing.Size(378, 487);
			this.binancePicture.TabIndex = 5;
			this.binancePicture.TabStop = false;
			// 
			// CryptoWallets
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(402, 509);
			this.Controls.Add(this.binancePicture);
			this.Name = "CryptoWallets";
			this.Text = "Crypto Requisites";
			((System.ComponentModel.ISupportInitialize)(this.binancePicture)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion
		private PictureBox binancePicture;
	}
}