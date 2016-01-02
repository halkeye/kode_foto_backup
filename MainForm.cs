/*
 * Created by SharpDevelop.
 * User: halkeye
 * Date: 7/1/2007
 * Time: 1:26 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;
using System.Threading;

namespace KodeFotoBackup
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		private static readonly MainForm mf = new MainForm();
		public static MainForm Instance {
			get
			{
				return mf;
			}
		}
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		public void setStatus(string message) {
			this.Invoke((MethodInvoker)delegate { _setStatus(message); }, new object[] {});
		}
		public void setProgress(int value, int max) {
			this.Invoke((MethodInvoker)delegate { _setProgress(value,max); }, new object[] {});
		}
		public void hideProgress() {
			this.Invoke((MethodInvoker)delegate { _hideProgress(); }, new object[] {});
		}
		public void done() {
			this.Invoke((MethodInvoker)delegate { _done(); }, new object[] {});
		}
		private void _done() {
			btnGo.Enabled = true;
		}
		private void _hideProgress() {
			//stStatusBarProgress.Visible = false;
		}
		private void _setProgress(int value, int max)
		{
			//stStatusBarProgress.Visible = true;
			stStatusBarProgress.Maximum = max;
			stStatusBarProgress.Value = value;
		}
		private void _setStatus(string message) {
			stStatusBarLabel.Text = message;
		}
		
	}
}
