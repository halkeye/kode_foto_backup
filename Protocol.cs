using System;
using System.Collections;
using System.Data;
using System.Net;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.XPath;
using System.ComponentModel;
using System.Security.Cryptography;

using System.Threading;

using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

using System.Drawing.Imaging;

namespace KodeFotoBackup
{
	/// <summary>
	/// Summary description for Protocol.
	/// </summary>
	public class Protocol
	{
		private string challenge;
		private string username;
		private string password;
		private string url;

		private string _message;
		private string _errorstr;
		private int    _errcode;
		private bool   _successful;

		public Protocol(string username, string password, string server)
		{
			challenge = "";
			_successful = true;
			this.username = username;
			this.password = password;
			url = "http://" + server + "/interface/simple";
			_errorstr = null;
			_errcode  = 0;
		}

		private WebRequest _setup_request()
		{
			WebRequest webReq = null;
			_successful = false;
			webReq = WebRequest.Create(new Uri(url));
			webReq.Method = "POST";
			webReq.ContentType = "application/x-www-form-urlencoded";
			HttpWebRequest httpReq = (HttpWebRequest) webReq;
			httpReq.UserAgent = "KodeFotoBilder";
			httpReq.Method = webReq.Method;
			httpReq.ContentType = webReq.ContentType;
			httpReq.ProtocolVersion = HttpVersion.Version11;
			httpReq.KeepAlive = true;
			webReq.Timeout = 100000;
			(webReq as HttpWebRequest).AllowWriteStreamBuffering = true;
			return webReq;
		}

		public XmlDocument _go_request(WebRequest webReq)
		{
			Stream streamResponse = null;
			StreamReader streamRead = null;
			XmlDocument doc = null;

			#if (DEBUG)
			{
				Console.WriteLine("Headers:");
				Console.WriteLine();
				for (int i = 0; i < webReq.Headers.Count; i++)
				{
					Console.WriteLine(webReq.Headers.Keys[i].ToString() + ": " + webReq.Headers[i].ToString());
				}

				Console.WriteLine();
			}
			#endif
			try
			{
				webReq.GetRequestStream().Close(); // Do the actual request, then close it
				streamResponse=webReq.GetResponse().GetResponseStream();
				streamRead = new StreamReader( streamResponse );
				doc = new XmlDocument();
				#if (DEBUG)
				Char[] readBuff = new Char[256];
				String outputData = "";
				int count = streamRead.Read( readBuff, 0, 256 );
				
				while (count > 0)
				{
					outputData += new String(readBuff, 0, count);
					count = streamRead.Read(readBuff, 0, 256);
				}
				Console.WriteLine(outputData);
				try
				{
					doc.LoadXml(outputData);
				}
				#else
				try
				{
					doc.Load(streamRead);
				}
				#endif
				catch (System.Xml.XmlException e)
				{
					Console.WriteLine("Failed to Load xml. Reason: " + e.ToString());
				}
			}
			catch (Exception e)
			{
				Console.WriteLine("Error: " + e.ToString());
			}
			finally
			{
				// Close the Stream Object.
				if (streamResponse != null)
					streamResponse.Close();
				if (streamRead != null)
					streamRead.Close();
			}

			_successful = true;
			if (doc != null)
			{
				XmlNodeList node = null;
				if ((node = doc.GetElementsByTagName("Error")) != null && node.Count > 0)
				{
					_successful = false;
					_errorstr = node[0].InnerXml;
					_errcode  = Int32.Parse(node[0].Attributes[0].Value);
				}
				if ((node = doc.GetElementsByTagName("Challenge")) != null && node.Count > 0)
				{
					challenge = node[0].InnerText;
				}
			}
			return doc;
		}

		public void _authify(WebRequest webReq)
		{
			MD5 service = System.Security.Cryptography.MD5.Create();
			byte[] b_password = Encoding.UTF8.GetBytes(password);
			string password_md5 = BitConverter.ToString(service.ComputeHash(b_password)).Replace("-", String.Empty).ToLower();

			byte[] b_challenege = Encoding.UTF8.GetBytes(challenge + password_md5 );
			byte[] b_challenge_md5 = service.ComputeHash(b_challenege);
			string auth = BitConverter.ToString(b_challenge_md5).Replace("-", String.Empty).ToLower();
			webReq.Headers.Add("X-FB-Auth", "crp:" + challenge + ":" + auth);
			webReq.Headers.Add("X-FB-GetChallenge", "1");
			// crp:challenge_string:MD5(challenge_string, MD5(user_password))
			challenge = ""; // so we can't reuse it.
		}

		public string GetChallenge()
		{
			WebRequest webReq = _setup_request();
			webReq.Headers.Add("X-FB-Mode", "GetChallenge");
			webReq.Headers.Add("X-FB-User", username);
			XmlDocument doc = _go_request(webReq);
			if (doc != null)
				challenge = doc.GetElementsByTagName("Challenge")[0].InnerXml;;
			return challenge;
		}

		public bool Login()
		{
			//return true;
			if (challenge.Length <= 0) GetChallenge();
			
			WebRequest webReq = _setup_request();
			_authify(webReq);
			webReq.Headers.Add("X-FB-User", username);
			//if (url.IndexOf("pics.livejournal.com") > 0)
			//	webReq.Headers.Add("X-FB-Mode", "Login");
			//else
			//webReq.Headers.Add("X-FB-Mode", "EmptyBogusLogin");
			webReq.Headers.Add("X-FB-Mode", "Login");
			webReq.Headers.Add("X-FB-Login.ClientVersion", "KodeFoto/Beta");
			XmlNodeList logindata = null;
			try
			{
				XmlDocument doc = _go_request(webReq);
				if (doc == null) return false;
				logindata = doc.GetElementsByTagName("LoginResponse");
				if (logindata == null) return false;
			}
			catch (Exception e)
			{
				Console.WriteLine("Error: " + e.Message);
				return true;
			}
			for(int i = 0; i < logindata.Count; i++)
			{
				XmlNode node = logindata.Item(i);
				if (node.Name.ToLower() == "message")
					_message = node.InnerXml;
				// Do something with quota
			}
			if (!IsSuccessful)
				return false;
			return true;
		}
		
		public ArrayList GetGalleries()
		{
			ArrayList galleries = new ArrayList();
			if (challenge.Length <= 0) GetChallenge();
			WebRequest webReq = _setup_request();
			_authify(webReq);
			webReq.Headers.Add("X-FB-Mode", "GetGalsTree");
			webReq.Headers.Add("X-FB-User", username);
			XmlDocument doc = _go_request(webReq);

			//<FBResponse>  <GetGalsResponse>  <RootGals>
			XmlNodeList gallery_node_list = doc.GetElementsByTagName("RootGals");
			if (gallery_node_list != null)
			{
				for(int i = 0; i < gallery_node_list.Count; i++) {
					for(int x = 0; x < gallery_node_list[i].ChildNodes.Count; x++) {
						//galleries.Add(new Gallery(gallery_node_list[i].ChildNodes[x]));
					}
				}
			}
			return galleries;
		}
		
		public bool IsSuccessful { get { return _successful; } }
		public string ErrorStr
		{
			set {}
			get { return _errorstr; }
		}
		
		public int ErrorCode
		{
			set {}
			get { return _errcode; }
		}

		public bool HasMessage()
		{
			return !(_message == null || _message.Length > 0);
		}

		public string Message
		{
			set { _message = value; }
			get { return _message; }
		}
	}
}
