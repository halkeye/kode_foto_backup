using System;
using System.Collections;
using System.Xml;

namespace KodeFotoBackup
{
	/// <summary>
	/// Summary description for Gallery.
	/// </summary>
	[ Serializable( ) ]
	public class Gallery 
	{
		/*
		 * <Gal id='1' [incoming='1']>  
		 *	<Name>Top Level Gallery 1</Name>  
		 *	<Sec>255</Sec>
		 *	<Date>2004-01-01 01:01:01</Date>
		 *  <TimeUpdate>1096989775</TimeUpdate>
		 *  <URL>http://www.fb.com/bob/gallery/0000201g</URL>
		 *  <GalMembers>
		 *   <GalMember id='1' />  
		 *   <GalMember id='2' />
		 *  </GalMembers>
		 *  <ChildGals>
		 */
		public int _id;
		public string _name;
		public int _sec;
		public int _level;
		public DateTime _date;
		public long _timestamp;
		public string _url;
		public ArrayList subGalleries;
		public ArrayList pictures;

		public Gallery() { }

		public Gallery(XmlNode node)
		{
			init(node,0);
		}
		public Gallery(XmlNode node, int level)
		{
			init(node,level);
		}

		public int Id
		{
			get { return _id; }
			set { _id=value; }
		}
		public int Level
		{
			get { return _level; }
			set { _level=value; }
		}
		public string Name
		{
			get { return _name; }
			set { _name=value; }
		}
		public string Url
		{
			get { return _url; }
			set { _url=value; }
		}
		
		public void init(XmlNode node, int level) 
		{
			_level = level;
			subGalleries = new ArrayList();
			pictures = new ArrayList();
			_timestamp = 0;


			if (node == null) 
				return;
			XmlAttribute att = node.Attributes["id"];
			if (att != null) 
				_id = Convert.ToInt16(att.Value);

			for (int i = 0; i < node.ChildNodes.Count; i++) 
			{
				XmlNode item = node.ChildNodes[i];
/*				if (item.InnerText.Length <= 0) 
					continue;*/
				
				if (item.Name.ToLower() == "galid") 
					_id = Convert.ToInt16(item.InnerText);
				else if (item.Name.ToLower() == "name" || item.Name.ToLower() == "galname") 
					_name = item.InnerText;
				else if (item.Name.ToLower() == "sec") 
					_sec = Convert.ToInt16(item.InnerText);
				else if (item.Name.ToLower() == "date") 
				{
					try 
					{
						_date = Convert.ToDateTime(item.InnerText);
					}
					catch (Exception e) 
					{
						Console.WriteLine("Failed to Convert Date. Reason: " + e.ToString());
					}
				}
				else if (item.Name.ToLower() == "timeupdate") 
					_timestamp = Convert.ToInt32(item.InnerText);
				else if (item.Name.ToLower() == "url" || item.Name.ToLower() == "galurl") 
					_url = item.InnerText;
				else if (item.Name.ToLower() == "childgals") 
					for (int x = 0; x < item.ChildNodes.Count; x++) 
						subGalleries.Add(new Gallery(item.ChildNodes[x], level+1));
				else if (item.Name.ToLower() == "galmembers") {
					for (int x = 0; x < item.ChildNodes.Count; x++) {
						pictures.Add(Convert.ToInt32(item.ChildNodes[x].Attributes[0].Value));
					}
				}

			}
		}
		// System.Xml.XPath
		public override string ToString() 
		{
			string name = "";
			if (_level > 0) 
			{
				for (int x = 0; x < _level; x++) 
					name += "-";
				name += " ";
			}
			name += this._name;
			return name;
		}
	}
}
