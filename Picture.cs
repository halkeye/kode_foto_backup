/*
 * Created by SharpDevelop.
 * User: halkeye
 * Date: 7/2/2007
 * Time: 2:07 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using System.Net;
using System.Xml;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections;

namespace KodeFotoBackup
{
	/// <summary>
	/// Description of Picture.
	/// </summary>
	public class Picture
	{
		
		public int id;
		public int width;
		public int height;
		public int bytes;
		public string format;
		public string md5;
		public Uri url;
		public string description;
		public Hashtable props = new Hashtable();
		
		
		private System.Drawing.Image _img = null;
		
		public Picture()
		{
		}
		
		public Picture(XmlNode node)
		{
			init(node);
		}
		
		public void init(XmlNode node)
		{
			/*
			<pic picid="1">
				<secid>255</secid>
				<width>1280</width>
				<height>1024</height>
				<bytes>1288324</bytes>
				<format>image/jpeg</format>
				<md5>1e27ef6a2476262054f6eb5c03572030</md5>
				<url>http://pics.livejournal.com/halkeye/pic/00001wtb</url>
				<prop name="pictitle">test</prop>
				<des>test</des>
			</pic>
			 */
			
			if (node == null) return;
			XmlAttribute att = node.Attributes["picid"];
			if (att != null) id = Convert.ToInt32(att.Value);

			for (int i = 0; i < node.ChildNodes.Count; i++)
			{
				XmlNode item = node.ChildNodes[i];
				if (item.InnerText.Length <= 0) continue;
				
				if (item.Name.ToLower() == "secid") {
					/* Security */
				}
				else if (item.Name.ToLower() == "width") {
					width = Convert.ToInt32(item.InnerText);
				}
				else if (item.Name.ToLower() == "height") {
					height = Convert.ToInt32(item.InnerText);
				}
				else if (item.Name.ToLower() == "bytes") {
					bytes = Convert.ToInt32(item.InnerText);
				}
				else if (item.Name.ToLower() == "format") {
					format = item.InnerText;
				}
				else if (item.Name.ToLower() == "md5") {
					md5 = item.InnerText;
				}
				else if (item.Name.ToLower() == "url") {
					url = new Uri(item.InnerText);
				}
				else if (item.Name.ToLower() == "prop") {
					props[item.Attributes["name"].Value] = item.InnerText;
				}
			}
		}
//		public static PropertyItem[] GetExifProperties(string fileName) {
//			using (FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
//				using (Image image = Image.FromStream(stream,
//				                                      /* useEmbeddedColorManagement = */ true,
//				                                      /* validateImageData = */ false))
//				return image.PropertyItems;
//		}
//
//		public static PropertyItem[] GetExifProperties(System.IO.Stream stream)
//		{
//			using (Image image = Image.FromStream(stream,
//			                                      /* useEmbeddedColorManagement = */ true,
//			                                      /* validateImageData = */ false))
//				return image.PropertyItems;
//		}
		
		public System.Drawing.Image getImage()
		{
			if (_img != null) {
				return _img;
			}
			try {
				// Create a request for the URL.
				WebRequest request = WebRequest.Create(this.url);
				request.Credentials = CredentialCache.DefaultCredentials;
				
				// If required by the server, set the credentials.
				// Get the response.
				using (HttpWebResponse response = (HttpWebResponse)request.GetResponse ()){
					// Display the status.
					// Get the stream containing content returned by the server.
					using (System.IO.Stream content = response.GetResponseStream()){
						_img = System.Drawing.Image.FromStream(content);
					}
				}
				return _img;
			}
			catch (System.ArgumentException e) {
				if (!this.url.ToString().EndsWith("/s640x480")) {
					WebRequest request = WebRequest.Create(this.url.ToString() + "/");
					request.Credentials = CredentialCache.DefaultCredentials;
					using (HttpWebResponse response = (HttpWebResponse) request.GetResponse ()) {}
					this.url = new Uri(this.url.ToString() + "/s640x480");
					return this.getImage();
				}
				System.Diagnostics.Debug.WriteLine("Bad URL: " + this.url.ToString());
			}
			catch (System.Net.WebException e) {
				/* 404 errors go here */
			}
			return null;
		}
		
		
		public string getFilename() {
			return getFilename(0);
		}
		public string getFilename(int prefix) {
			string filename = "";;
			Image i = this.getImage();
			if (i == null) {
				return "";
			}
			if (prefix > 0) {
				filename = this.id.ToString().PadLeft(prefix,'0') + "_";
			}
			if (props["filename"] != null) {
				filename = filename + props["filename"];
				return filename;
			}
			filename = filename + this.md5.ToString();
			ImageFormat format = i.RawFormat;
			if (format == ImageFormat.Gif || this.format.Equals("image/gif")) {
				filename = filename + ".gif";
			}
			else if (format == ImageFormat.Bmp || this.format.Equals("image/bmp")) {
				filename = filename + ".bmp";
			}
			else if (format == ImageFormat.Emf) {
				filename = filename + ".emf";
			}
			else if (format == ImageFormat.Icon || this.format.Equals("image/ico")) {
				filename = filename + ".ico";
			}
			else if (format == ImageFormat.Jpeg  || this.format.Equals("image/jpg")  || this.format.Equals("image/jpeg")) {
				filename = filename + ".jpg";
			}
			else if (format == ImageFormat.Png  || this.format.Equals("image/png")) {
				filename = filename + ".png";
			}
			else if (format == ImageFormat.Tiff  || this.format.Equals("image/tiff")) {
				filename = filename + ".tiff";
			}
			else if (format == ImageFormat.Wmf) {
				filename = filename + ".wmf";
			}
			else {
				string[] parts = this.format.Split(new char[] {'/'});
				filename = filename + "." + parts[1];
			}
			return filename;
			
		}
		public void cleanImage() { _img = null; }
	}
}
