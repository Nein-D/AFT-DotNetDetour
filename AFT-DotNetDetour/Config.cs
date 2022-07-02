using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AFT_DotNetDetour
{
	public class Config
	{
		public Dictionary<string, string> configData;

		private string fullFileName;

		public Config(string _fileName)
		{
			configData = new Dictionary<string, string>();
			fullFileName = Tool.getMainPath() + "\\" + _fileName;
			if (!File.Exists(Tool.getMainPath() + "\\" + _fileName))
			{
				new StreamWriter(File.Create(Tool.getMainPath() + "\\" + _fileName), Encoding.UTF8).Close();
			}
			StreamReader streamReader = new StreamReader(Tool.getMainPath() + "\\" + _fileName, Encoding.UTF8);
			int num = 0;
			string text;
			while ((text = streamReader.ReadLine()) != null)
			{
				if (text.StartsWith(";") || string.IsNullOrEmpty(text))
				{
					Dictionary<string, string> dictionary = configData;
					dictionary.Add(";" + num++, text);
					continue;
				}
				string[] array = text.Split('=');
				if (array.Length >= 2)
				{
					configData.Add(array[0], array[1]);
					continue;
				}
				Dictionary<string, string> dictionary2 = configData;
				dictionary2.Add(";" + num++, text);
			}
			streamReader.Close();
		}

		public string get(string key)
		{
			if (configData.Count <= 0)
			{
				return null;
			}
			if (configData.ContainsKey(key))
			{
				return configData[key].ToString();
			}
			return null;
		}

		public void set(string key, string value)
		{
			if (configData.ContainsKey(key))
			{
				configData[key] = value;
			}
			else
			{
				configData.Add(key, value);
			}
			save();
		}

		public void save()
		{
			StreamWriter streamWriter = new StreamWriter(fullFileName, append: false, Encoding.UTF8);
			IDictionaryEnumerator dictionaryEnumerator = configData.GetEnumerator();
			while (dictionaryEnumerator.MoveNext())
			{
				if (dictionaryEnumerator.Key.ToString().StartsWith(";"))
				{
					streamWriter.WriteLine(dictionaryEnumerator.Value);
				}
				else
				{
					streamWriter.WriteLine(dictionaryEnumerator.Key?.ToString() + "=" + dictionaryEnumerator.Value);
				}
			}
			streamWriter.Close();
		}

		public static string toString()
		{
			return "";
		}
	}
}
