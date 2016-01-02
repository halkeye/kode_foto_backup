/*
 * Created by SharpDevelop.
 * User: halkeye
 * Date: 7/1/2007
 * Time: 1:17 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System.Threading;
namespace KodeFotoBackup
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.stStatusBar = new System.Windows.Forms.StatusStrip();
			this.stStatusBarLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.stStatusBarProgress = new System.Windows.Forms.ToolStripProgressBar();
			this.lbExportFile = new System.Windows.Forms.Label();
			this.txtExportFile = new System.Windows.Forms.TextBox();
			this.btn_export = new System.Windows.Forms.Button();
			this.lbExportDest = new System.Windows.Forms.Label();
			this.txtExportDir = new System.Windows.Forms.TextBox();
			this.btnExportDir = new System.Windows.Forms.Button();
			this.btnGo = new System.Windows.Forms.Button();
			this.dlgFileBrowser = new System.Windows.Forms.FolderBrowserDialog();
			this.label2 = new System.Windows.Forms.Label();
			this.txtPrefixDigits = new System.Windows.Forms.NumericUpDown();
			this.ttPrefixDigits = new System.Windows.Forms.ToolTip(this.components);
			this.stStatusBar.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.txtPrefixDigits)).BeginInit();
			this.SuspendLayout();
			// 
			// stStatusBar
			// 
			this.stStatusBar.AutoSize = false;
			this.stStatusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.stStatusBarLabel,
									this.stStatusBarProgress});
			this.stStatusBar.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
			this.stStatusBar.Location = new System.Drawing.Point(0, 244);
			this.stStatusBar.Name = "stStatusBar";
			this.stStatusBar.Size = new System.Drawing.Size(292, 22);
			this.stStatusBar.TabIndex = 0;
			// 
			// stStatusBarLabel
			// 
			this.stStatusBarLabel.Name = "stStatusBarLabel";
			this.stStatusBarLabel.Size = new System.Drawing.Size(0, 17);
			// 
			// stStatusBarProgress
			// 
			this.stStatusBarProgress.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.stStatusBarProgress.Name = "stStatusBarProgress";
			this.stStatusBarProgress.Size = new System.Drawing.Size(100, 16);
			// 
			// lbExportFile
			// 
			this.lbExportFile.Location = new System.Drawing.Point(12, 9);
			this.lbExportFile.Name = "lbExportFile";
			this.lbExportFile.Size = new System.Drawing.Size(68, 17);
			this.lbExportFile.TabIndex = 1;
			this.lbExportFile.Text = "Export XML:";
			// 
			// txtExportFile
			// 
			this.txtExportFile.Location = new System.Drawing.Point(86, 6);
			this.txtExportFile.Name = "txtExportFile";
			this.txtExportFile.Size = new System.Drawing.Size(100, 20);
			this.txtExportFile.TabIndex = 2;
			// 
			// btn_export
			// 
			this.btn_export.Location = new System.Drawing.Point(205, 4);
			this.btn_export.Name = "btn_export";
			this.btn_export.Size = new System.Drawing.Size(75, 23);
			this.btn_export.TabIndex = 3;
			this.btn_export.Text = "&Browse";
			this.btn_export.UseVisualStyleBackColor = true;
			this.btn_export.Click += new System.EventHandler(this.Btn_exportClick);
			// 
			// lbExportDest
			// 
			this.lbExportDest.Location = new System.Drawing.Point(12, 38);
			this.lbExportDest.Name = "lbExportDest";
			this.lbExportDest.Size = new System.Drawing.Size(100, 23);
			this.lbExportDest.TabIndex = 4;
			this.lbExportDest.Text = "Export To:";
			// 
			// txtExportDir
			// 
			this.txtExportDir.Location = new System.Drawing.Point(86, 35);
			this.txtExportDir.Name = "txtExportDir";
			this.txtExportDir.Size = new System.Drawing.Size(100, 20);
			this.txtExportDir.TabIndex = 5;
			this.ttPrefixDigits.SetToolTip(this.txtExportDir, "Path to export files to");
			// 
			// btnExportDir
			// 
			this.btnExportDir.Location = new System.Drawing.Point(205, 32);
			this.btnExportDir.Name = "btnExportDir";
			this.btnExportDir.Size = new System.Drawing.Size(75, 23);
			this.btnExportDir.TabIndex = 6;
			this.btnExportDir.Text = "&Browse";
			this.btnExportDir.UseVisualStyleBackColor = true;
			this.btnExportDir.Click += new System.EventHandler(this.BtnExportDirClick);
			// 
			// btnGo
			// 
			this.btnGo.Location = new System.Drawing.Point(205, 218);
			this.btnGo.Name = "btnGo";
			this.btnGo.Size = new System.Drawing.Size(75, 23);
			this.btnGo.TabIndex = 7;
			this.btnGo.Text = "&Go";
			this.btnGo.UseVisualStyleBackColor = true;
			this.btnGo.Click += new System.EventHandler(this.BtnGoClick);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(12, 61);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(68, 23);
			this.label2.TabIndex = 9;
			this.label2.Text = "Prefix Digits";
			// 
			// txtPrefixDigits
			// 
			this.txtPrefixDigits.Location = new System.Drawing.Point(86, 59);
			this.txtPrefixDigits.Name = "txtPrefixDigits";
			this.txtPrefixDigits.Size = new System.Drawing.Size(100, 20);
			this.txtPrefixDigits.TabIndex = 12;
			this.ttPrefixDigits.SetToolTip(this.txtPrefixDigits, "How many digits you want to pad the filename with. ie: 0 = \'filename\', 1 = \'1_fil" +
						"ename\', 2 = \'01_filename\' ...");
			this.txtPrefixDigits.Value = new decimal(new int[] {
									3,
									0,
									0,
									0});
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(292, 266);
			this.Controls.Add(this.txtPrefixDigits);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.btnGo);
			this.Controls.Add(this.btnExportDir);
			this.Controls.Add(this.txtExportDir);
			this.Controls.Add(this.lbExportDest);
			this.Controls.Add(this.btn_export);
			this.Controls.Add(this.txtExportFile);
			this.Controls.Add(this.lbExportFile);
			this.Controls.Add(this.stStatusBar);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "MainForm";
			this.Text = "Kode Foto Backup";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainFormFormClosing);
			this.stStatusBar.ResumeLayout(false);
			this.stStatusBar.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.txtPrefixDigits)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.ToolStripProgressBar stStatusBarProgress;
		private System.Windows.Forms.ToolStripStatusLabel stStatusBarLabel;
		private System.Windows.Forms.StatusStrip stStatusBar;
		private System.Windows.Forms.ToolTip ttPrefixDigits;
		private System.Windows.Forms.NumericUpDown txtPrefixDigits;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.FolderBrowserDialog dlgFileBrowser;
		private System.Windows.Forms.TextBox txtExportFile;
		private System.Windows.Forms.Button btnGo;
		private System.Windows.Forms.Button btnExportDir;
		private System.Windows.Forms.TextBox txtExportDir;
		private System.Windows.Forms.Label lbExportDest;
		private System.Windows.Forms.Label lbExportFile;
		
		private System.Windows.Forms.Button btn_export;
		
		void Btn_exportClick(object sender, System.EventArgs e)
		{
			System.Windows.Forms.OpenFileDialog dlg_open;
			dlg_open = new System.Windows.Forms.OpenFileDialog();

			// 
			// dlg_open
			// 
			dlg_open.Filter = "XML Files|*.xml";
			dlg_open.InitialDirectory = "xml";
			System.Windows.Forms.DialogResult dr = dlg_open.ShowDialog();
			System.Console.WriteLine(dr.ToString());

			if (dr == System.Windows.Forms.DialogResult.OK) {
				txtExportFile.Text = dlg_open.FileName;
			}
		}
		
		void BtnExportDirClick(object sender, System.EventArgs e)
		{
			System.Windows.Forms.DialogResult result = dlgFileBrowser.ShowDialog();
			if( result == System.Windows.Forms.DialogResult.OK ) {
				txtExportDir.Text = dlgFileBrowser.SelectedPath;
				// No file is opened, bring up openFileDialog in selected path.
				//dlgFileBrowser.InitialDirectory = dlgFileBrowser.SelectedPath;
				//dlgFileBrowser.FileName = null;
			}
		}
		
		private static Thread goThread = null;
		void BtnGoClick(object sender, System.EventArgs e)
		{
			try {
				/* FIXME: Check to see if both dir and file are specified and readable */
				BackupFotos.outputPath = txtExportDir.Text;
				BackupFotos.prefixAmount = System.Convert.ToInt32(txtPrefixDigits.Value);
				BackupFotos.xmlFile = txtExportFile.Text;
				// TODO: try: http://www.java2s.com/Code/CSharp/Thread/ThreadandGUI.htm
				ThreadStart threadDelegate = new ThreadStart(BackupFotos.DoWork);
				goThread = new Thread(threadDelegate);
				btnGo.Enabled = false; // FIXME: find a way to re-enable post upload
				goThread.Start();
			}
			catch (System.Exception ex)
			{
				System.Windows.Forms.MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
			}
			
		}
		
		void MainFormFormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
		{
			if (goThread != null) {
				goThread.Abort();
			}
		}
	}
}
