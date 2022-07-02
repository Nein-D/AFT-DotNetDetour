using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Win32;
using Newtonsoft.Json;
using WPFLauncher.Manager.LanGame;

namespace AFT_DotNetDetour
{
	public class Tool
	{
		private static string HKEY_BASE = "SOFTWARE\\Netease\\MCLauncher";

		private static Config config = new Config("SystemInfo.ini");

		private static string mainPath = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);

		public static string randomStr(int len, string[] arr = null)
		{
			if (arr == null || arr.Length <= 1)
			{
				arr = new string[16]
				{
					"a", "b", "c", "d", "e", "f", "0", "1", "2", "3",
					"4", "5", "6", "7", "8", "9"
				};
			}
			string text = "";
			for (int i = 0; i < len; i++)
			{
				text += arr[new Random(new Random(Guid.NewGuid().GetHashCode()).Next(0, 100)).Next(arr.Length - 1)];
			}
			return text;
		}



		public static string randomMac(string source = null)
		{
			string text = "";
			int num = 12;
			if (source != null)
			{
				num = source.Length;
			}
			for (int i = 1; i <= num; i++)
			{
				text = ((i % 2 != 0) ? (text + randomStr(1)) : (i switch
				{
					12 => text + randomStr(1, new string[15]
					{
						"0", "1", "2", "3", "4", "5", "6", "7", "8", "9",
						"A", "B", "C", "D", "E"
					}),
					2 => text + randomStr(1, new string[8] { "0", "2", "4", "6", "8", "A", "C", "E" }),
					_ => text + randomStr(1),
				}));
			}
			return text.ToUpper();
		}

		public static byte[] Base64Encrypt(byte[] bytes)
		{
			bytes = Encoding.UTF8.GetBytes(Convert.ToBase64String(bytes));
			return bytes;
		}

		public static bool isGoodTitle(string title)
		{
			if (title != null && title.Length != 0)
			{
				string[] array = new string[67]
				{
					"ggl", "交流", "client", "360", "mysql", "google", "集", "visual", "idea", "liquid",
					"script", "java", "c#", "sharp", "sense", "aquavit", "bounce", "挂", "mix", "zero",
					"dnspy", "破解", "crack", "sigma", "jello", "flux", "remix", "lunar", "hax", "hacker",
					"minecraft", "godie", "管理员", "x64dbg", "x32dbg", "语言", "命令", "system32", "程序", "桌面",
					"图片", "弊", "端", "bili", "运行", "power", "远程", "连接", "server", "jbyte",
					"gui", "ida", "typora", "studio", "windows", "linux", "admin", "直播", "推流", "录制",
					"live", "收藏", "everything", "installer", "记事本", "XruiDD", "demarcia"
				};
				title = title.ToLower();
				string[] array2 = array;
				string[] array3 = array2;
				int num = 0;
				while (true)
				{
					if (num < array3.Length)
					{
						string text = array3[num];
						if (title.Contains(text.ToLower()))
						{
							break;
						}
						num++;
						continue;
					}
					return true;
				}
				return false;
			}
			return true;
		}

		public static bool isGoodName(string name)
		{
			if (name != null && name.Length != 0)
			{
				name = name.ToLower();
				if (isSystemProcess(name))
				{
					return true;
				}
				if (!name.Equals("java") && !name.Equals("e") && !name.Equals("qq") && !name.Equals("tim") && !name.StartsWith("lsp") && !name.StartsWith("dsb"))
				{
					if (!name.StartsWith("obs") && !name.StartsWith("huya"))
					{
						if (!name.EndsWith(".dat") && !name.EndsWith(".tmp") && !name.EndsWith(".safe"))
						{
							string[] array = new string[35]
							{
								"cheat", "调试", "inject", "hacker", "edit", "dnspy", "xmind", "wechat", "chrome", "installer",
								"SecureFolders", "Locker", "Hide", "sstap", "feiq", "v2ray", "studio", "presentermodulemonitor", "presentermodule", "ServiceHub",
								"PerfWatson2", "HipsTray", "DeskTopShare", "wallpaper", "mysql", "usysdiag", "sqlwriter", "jusched", "HipsDaemon", "PresentationFontCache",
								"破解", "盒子", "鼠标", "连点", "工具"
							};
							string[] array2 = array;
							int num = 0;
							while (true)
							{
								if (num < array2.Length)
								{
									string text = array2[num];
									if (name.Contains(text.ToLower()))
									{
										break;
									}
									num++;
									continue;
								}
								return true;
							}
							return false;
						}
						return false;
					}
					return false;
				}
				return false;
			}
			return false;
		}

		public static bool isSystemProcess(string name)
		{
			string[] array = new string[11]
			{
				"dllhost", "IAStorDataMgrSvc", "fontdrvhost", "WmiPrvSE", "svchost", "crss", "SecurityHealthService", "spoolsv", "dwm", "ctfmon",
				"conhost"
			};
			string[] array2 = array;
			int num = 0;
			while (true)
			{
				if (num < array2.Length)
				{
					string value = array2[num];
					if (name.Equals(value))
					{
						break;
					}
					num++;
					continue;
				}
				return false;
			}
			return true;
		}

		public static string getMainPath()
		{
			if (mainPath != null && File.Exists(mainPath))
			{
				return mainPath;
			}
			return Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
		}

		public static string getMac(string source)
		{
			string text = config.get("mac");
			if (text == null || text.Length != 12)
			{
				text = randomMac(source).ToUpper();
				if (text.Length > 12)
				{
					text = text.Substring(0, 12);
				}
				config.set("mac", text);
			}
			return text;
		}

		public static string getLocalIP(string source)
		{
			string text = config.get("ip");
			if (text == null || text.Length < 7)
			{
				text = randomIP(source).ToUpper();
				if (text.Length > 15)
				{
					text = text.Substring(0, 15);
				}
				config.set("ip", text);
			}
			return text;
		}

		public static string randomIP(string source)
		{
			Random random = new Random();
			string text = "";
			for (int i = 0; i <= 3; i++)
			{
				string text2 = random.Next(0, 255).ToString();
				text = ((i >= 3) ? (text + text2.ToString()) : (text + (text2 + ".").ToString()));
			}
			if (Regex.IsMatch(text, "^((25[0-5]|2[0-4]\\d|((1\\d{2})|([1-9]?\\d)))\\.){3}(25[0-5]|2[0-4]\\d|((1\\d{2})|([1-9]?\\d)))$"))
			{
				return text;
			}
			return "";
		}

		public static string getDiskCode()
		{
			string text = config.get("disk");
			if (text == null || text.Length != 8)
			{
				text = randomStr(8).ToUpper();
				config.set("disk", text);
			}
			return text;
		}

		public static string getCPUID()
		{
			string text = config.get("cpuid");
			if (text == null || text.Length != 16)
			{
				text = randomStr(16).ToUpper();
				config.set("cpuid", text);
			}
			return text;
		}

		public static string getGamePath()
		{
			try
			{
				return (string)Registry.CurrentUser.OpenSubKey(HKEY_BASE).GetValue("DownloadPath");
			}
			catch (Exception)
			{
				return "C:\\MCLDownload";
			}
		}

		public static bool createMK(string source, string dest)
		{
			if (Directory.Exists(dest) || File.Exists(dest))
			{
				deleteMK(dest);
			}
			string text = "";
			text = ((!Directory.Exists(dest)) ? ("mklink \"" + dest + "\" \"" + source + "\"") : ("mklink /d \"" + dest + "\" \"" + source + "\""));
			AFTClient.send(runCMD(text));
			return false;
		}

		public static bool deleteMK(string dest)
		{
			runCMD("rmdir \"" + dest + "\"");
			try
			{
				if (Directory.Exists(dest))
				{
					if (Directory.Exists(dest))
					{
						Directory.Delete(dest);
					}
					return !Directory.Exists(dest);
				}
				if (File.Exists(dest))
				{
					if (File.Exists(dest))
					{
						File.Delete(dest);
					}
					return !File.Exists(dest);
				}
			}
			catch
			{
			}
			return true;
		}

		public static string runCMD(string cmdline, bool waiteForExit = true)
		{
			string result = "";
			Process process = new Process();
			ProcessStartInfo processStartInfo = new ProcessStartInfo();
			processStartInfo.FileName = "cmd.exe";
			processStartInfo.Arguments = "/c " + cmdline;
			processStartInfo.UseShellExecute = false;
			processStartInfo.RedirectStandardInput = false;
			processStartInfo.RedirectStandardOutput = true;
			processStartInfo.CreateNoWindow = true;
			process.StartInfo = processStartInfo;
			try
			{
				process.Start();
				if (waiteForExit)
				{
					process.WaitForExit();
				}
				result = process.StandardOutput.ReadToEnd();
				return result;
			}
			catch
			{
				return result;
			}
			finally
			{
				process.Kill();
				process.Close();
			}
		}

		public static string toString()
		{
			return "";
		}

		public static string getProcessCmd(int pid)
		{
			string result = "";
			string cmdline = "wmic process where ProcessId=" + pid + " get commandline";
			try
			{
				result = runCMD(cmdline);
				return result;
			}
			catch (Exception ex)
			{
				AFTClient.send(ex.ToString());
				return result;
			}
		}

		public static bool isNormalClient(int pid)
		{
			if (getProcessCmd(pid).Contains("-DlauncherControlPort="))
			{
				return true;
			}
			return false;
		}

		public static bool copyFile(string source, string target)
		{
			if (!File.Exists(source))
			{
				return false;
			}
			try
			{
				if (deleteFile(target))
				{
					File.Copy(source, target);
				}
			}
			catch
			{
			}
			return false;
		}

		public static bool deleteFile(string source)
		{
			if (!File.Exists(source))
			{
				return true;
			}
			try
			{
				File.Delete(source);
			}
			catch
			{
			}
			return File.Exists(source);
		}

		public static bool deleteUnSafeMod()
		{
			string path = getGamePath() + "\\Game\\.minecraft\\mods";
			try
			{
				if (Directory.Exists(path))
				{
					FileInfo[] files = new DirectoryInfo(path).GetFiles();
					FileInfo[] array = files;
					foreach (FileInfo fileInfo in array)
					{
						string name = fileInfo.Name;
						if (!name.Contains("@") && name.EndsWith(".jar"))
						{
							try
							{
								fileInfo.Delete();
								AFTClient.send("clean unsafe mod", name);
							}
							catch
							{
							}
						}
					}
				}
			}
			catch
			{
				return false;
			}
			return true;
		}

		public static bool checkUnSafeMod()
		{
			string path = getGamePath() + "\\Game\\.minecraft\\mods";
			try
			{
				if (Directory.Exists(path))
				{
					FileInfo[] files = new DirectoryInfo(path).GetFiles();
					FileInfo[] array = files;
					foreach (FileInfo fileInfo in array)
					{
						try
						{
							string name = fileInfo.Name;
							if (!name.Contains("@") && name.EndsWith(".jar"))
							{
								return false;
							}
						}
						catch
						{
						}
					}
				}
			}
			catch
			{
				return true;
			}
			return true;
		}

		public static byte[] objectsToBytes(params object[] ktb)
		{
			if (ktb == null)
			{
				return null;
			}
			byte[] array = new byte[0];
			foreach (object obj in ktb)
			{
				byte[] array2 = new byte[0];
				Type type = obj.GetType();
				switch (Type.GetTypeCode(type))
				{
					case TypeCode.Object:
						if (type == typeof(byte[]))
						{
							array2 = (byte[])obj;
						}
						else if (type == typeof(List<uint>))
						{
							List<byte> list = new List<byte>();
							byte[] bytes = BitConverter.GetBytes((ushort)(((List<uint>)obj).Count * 4));
							list.AddRange(array2);
							list.AddRange(bytes);
							foreach (uint item in obj as List<uint>)
							{
								list.AddRange(BitConverter.GetBytes(item));
							}
							array2 = list.ToArray();
						}
						else if (type == typeof(List<ulong>))
						{
							List<byte> list2 = new List<byte>();
							byte[] bytes2 = BitConverter.GetBytes((ushort)(((List<ulong>)obj).Count * 8));
							list2.AddRange(array2);
							list2.AddRange(bytes2);
							foreach (ulong item2 in obj as List<ulong>)
							{
								list2.AddRange(BitConverter.GetBytes(item2));
							}
							array2 = list2.ToArray();
						}
						else if (type == typeof(List<long>))
						{
							List<byte> list3 = new List<byte>();
							byte[] bytes3 = BitConverter.GetBytes((ushort)(((List<long>)obj).Count * 8));
							list3.AddRange(array2);
							list3.AddRange(bytes3);
							foreach (long item3 in obj as List<long>)
							{
								list3.AddRange(BitConverter.GetBytes(item3));
							}
							array2 = list3.ToArray();
						}
						else if (type == typeof(GameDescription))
						{
							string text = JsonConvert.SerializeObject(obj);
							array2 = objectsToBytes(text);
						}
						break;
					case TypeCode.Boolean:
						array2 = BitConverter.GetBytes((bool)obj);
						break;
					case TypeCode.Byte:
						array2 = new byte[1] { (byte)obj };
						break;
					case TypeCode.Int16:
						array2 = BitConverter.GetBytes((short)obj);
						break;
					case TypeCode.UInt16:
						array2 = BitConverter.GetBytes((ushort)obj);
						break;
					case TypeCode.Int32:
						array2 = BitConverter.GetBytes((int)obj);
						break;
					case TypeCode.UInt32:
						array2 = BitConverter.GetBytes((uint)obj);
						break;
					case TypeCode.Int64:
						array2 = BitConverter.GetBytes((long)obj);
						break;
					case TypeCode.Double:
						array2 = BitConverter.GetBytes((double)obj);
						break;
					case TypeCode.String:
						array2 = Encoding.UTF8.GetBytes((string)obj);
						array2 = objectsToBytes((ushort)array2.Length, array2);
						break;
				}
				array = array.Concat(array2).ToArray();
			}
			return array;
		}

		public static string GetMD5(string sDataIn)
		{
			MD5CryptoServiceProvider mD5CryptoServiceProvider = new MD5CryptoServiceProvider();
			byte[] bytes = Encoding.UTF8.GetBytes(sDataIn);
			byte[] array = mD5CryptoServiceProvider.ComputeHash(bytes);
			mD5CryptoServiceProvider.Clear();
			string text = "";
			for (int i = 0; i < array.Length; i++)
			{
				text += array[i].ToString("X").PadLeft(2, '0');
			}
			return text.ToLower();
		}

		public static string encode(string str, string key)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(str);
			byte[] bytes2 = Encoding.UTF8.GetBytes(GetMD5(key));
			string text = (bytes[0] ^ bytes2[0 % bytes2.Length]).ToString() ?? "";
			int num = 1;
			for (int i = 1; i < bytes.Length; i++)
			{
				if (num == bytes2.Length)
				{
					num = 0;
				}
				string text2 = text;
				text = text2 + " " + (bytes[i] ^ bytes2[num++]);
			}
			bytes = Encoding.UTF8.GetBytes(text);
			return Convert.ToBase64String(bytes);
		}

		public static string decode(string str, string key)
		{
			byte[] array = Convert.FromBase64String(str);
			byte[] bytes = Encoding.UTF8.GetBytes(GetMD5(key));
			string[] array2 = Convert.ToString(array).Split(' ');
			array = new byte[array2.Length];
			int num = 0;
			for (int i = 0; i < array2.Length; i++)
			{
				if (num == bytes.Length)
				{
					num = 0;
				}
				int num2 = Convert.ToInt32(array2[i]);
				array[i] = (byte)(num2 ^ bytes[num++]);
			}
			return Convert.ToString(array);
		}


	}
}
