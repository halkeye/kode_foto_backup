/*
 * Created by SharpDevelop.
 * User: halkeye
 * Date: 7/2/2007
 * Time: 7:03 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using System.Collections;
using System.IO;
using System.Drawing.Imaging;
using System.Text;

namespace KodeFotoBackup
{
	/// <summary>
	/// Description of BackupFotos.
	/// </summary>
	public class BackupFotos
	{
		public BackupFotos()
		{
		}
		
		public static int prefixAmount;
		public static string outputPath;
		public static string xmlFile;
		
		public static void DoWork() {
			try {
				FileInfo t = new FileInfo(Path.Combine(BackupFotos.outputPath,"missing.txt"));
				StreamWriter missingFiles =t.CreateText();
				missingFiles.Write("----[ " + DateTime.Now.ToString() + " ] ----" + Environment.NewLine);
				missingFiles.Flush();
				
				System.IO.FileStream xml = new System.IO.FileStream(xmlFile, FileMode.Open);
				ArrayList galleries = new ArrayList();
				Hashtable pics = new Hashtable();
				System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
				MainForm.Instance.setStatus("Loading XML");
				doc.Load(xml);
				
				System.Xml.XmlNode picsContainer = doc.GetElementsByTagName("pics")[0];
				System.Xml.XmlNode galleryContainer = doc.GetElementsByTagName("galleries")[0];
				if (picsContainer == null || galleryContainer == null) {
					// throw exception
				}
				MainForm.Instance.setStatus("Starting Parsing XML");
				for(int x = 0; x < galleryContainer.ChildNodes.Count; x++) {
					Gallery gallery = new Gallery(galleryContainer.ChildNodes[x]);
					galleries.Add(gallery);
				}
				MainForm.Instance.setStatus("Done Fetching Galls");
				for(int x = 0; x < picsContainer.ChildNodes.Count; x++) {
					Picture pic = new Picture(picsContainer.ChildNodes[x]);
					pics.Add(pic.id,pic);
				}
				MainForm.Instance.setStatus("Done Fetching Pics");
				try {
					Directory.CreateDirectory(BackupFotos.outputPath);
				}
				catch(System.IO.IOException ex) {
					// Don't care really, its likely we can't create the directory we are using anyways.
				}
				foreach ( Gallery gal in galleries )
				{
					try {
						string galname = gal.Name;
						foreach (char invalidChar in Path.GetInvalidPathChars()) {
							galname.Replace(invalidChar,'_');
						}
						galname.Replace(':','_');
						galname = System.Text.RegularExpressions.Regex.Replace(galname, "[^A-Za-z0-9_\\-' \\.\\@]+", "_");
						string path = Path.Combine(BackupFotos.outputPath, galname);
						
						
						MainForm.Instance.setStatus("Now Writing " + gal.Name);
						Directory.CreateDirectory(path);
						int count = 0;
						foreach ( int id in gal.pictures ) {
							MainForm.Instance.setProgress(count, gal.pictures.Count);
							Picture p = (Picture) pics[id];
							string filename = p.getFilename(BackupFotos.prefixAmount);
							MainForm.Instance.setStatus("Now Writing " + gal.Name + ": " + filename);
							System.Drawing.Image i = p.getImage();
							if (i == null) {
								missingFiles.Write(gal.Name + " - Missing File - " + p.id + " - " + p.url.ToString() + Environment.NewLine);
								missingFiles.Flush();
								continue;
							}
							i.Save(path + Path.DirectorySeparatorChar + filename);
							p.cleanImage();
							count++;
						}
						MainForm.Instance.setStatus("Now Writing " + gal.Name + " - Done");
						MainForm.Instance.hideProgress();
					}
					catch (System.Threading.ThreadAbortException ex) {
					}
					catch(System.Exception e) {
						/* FIXME: this needs to be non c# specific */
						System.Windows.Forms.MessageBox.Show(e.ToString());
					}
				}
				MainForm.Instance.setStatus("Done all galleries");
				MainForm.Instance.done();
				xml.Close();
				missingFiles.Close();
			}
			catch (System.Threading.ThreadAbortException ex) {
			}

		}
		
	}
}
