﻿using System;
using AFT_DotNetDetour.Hook;


namespace AFT_DotNetDetour.AFTHookEvents
{

	internal class NeteaseCode : IMethodHook
	{

		public class fuck_CG : IMethodHook
		{
			[HookMethod("WPFLauncher.Manager.Configuration.anz", null, null)]
			private static bool get_PlayCG()
			{
				return false;
			}
		}


	[HookMethod("WPFLauncher.Manager.Log.Util.ane", null, null)]
		public static string b()
		{
			string text = "";
			string result;
			try
			{
				if (text == null || text.Length != 12)
				{
					text = NeteaseCode.getMac(NeteaseCode.b_Original());
				}
				AFTClient.send("MAC_ADDRESS", text);

				result = text;
			}
			catch
			{
				result = text;
			}
			return result;
		}


		[OriginalMethod]
		public static string b_Original()
		{
			return null;
		}

		[HookMethod("WPFLauncher.Manager.akl", "f", null)]
		public static string DiskId()
		{
			string text = "";
			string result;
			try
			{
				if (text == null || text.Length != 8)
				{
					text = NeteaseCode.getDiskCode();
				}
				AFTClient.send("DiskId", text);
				result = text;
			}
			catch
			{
				result = NeteaseCode.f_Original();
			}
			return result;
		}

		[OriginalMethod]
		public static string f_Original()
		{
			return null;
		}


		[HookMethod("WPFLauncher.Manager.akl", null, null)]
		public static string d(string jmh)
		{
			string text = "";
			string result;
			try
			{
				if (text == null || text.Length != 16)
				{
					text = NeteaseCode.getCPUID();
				}
				text += jmh;
				if (text.Length > 24)
				{
					text.Substring(0, 24);
					result = text;
				}
				else
				{
					AFTClient.send("CPUID", text);
					result = text;
				}
			}
			catch
			{
				result = NeteaseCode.d_Original(jmh);
			}
			return result;
		}


		[OriginalMethod]
		public static string d_Original(string jmh)
		{
			return null;
		}


		public static string getDiskCode()
		{
			string text = null;
			if (text == null || text.Length != 8)
			{
				text = NeteaseCode.randomStr(8, null).ToUpper();
			}
			return text;
		}


		public static string getCPUID()
		{
			string text = null;
			if (text == null || text.Length != 16)
			{
				text = NeteaseCode.randomStr(16, null).ToUpper();
			}
			return text;
		}


		public static string getMac(string source)
		{
			string text = null;
			if (text == null || text.Length != 12)
			{
				text = NeteaseCode.randomMac(source).ToUpper();
				if (text.Length > 12)
				{
					text = text.Substring(0, 12);
				}
			}
			return text;
		}


		public static string randomStr(int len, string[] arr = null)
		{
			if (arr == null || arr.Length <= 1)
			{
				arr = new string[]
				{
					"a",
					"b",
					"c",
					"d",
					"e",
					"f",
					"0",
					"1",
					"2",
					"3",
					"4",
					"5",
					"6",
					"7",
					"8",
					"9"
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
				string text2;
				if (i % 2 != 0)
				{
					text2 = text + NeteaseCode.randomStr(1, null);
				}
				else
				{
					string text3;
					if (i != 2)
					{
						if (i != 12)
						{
							text3 = text + NeteaseCode.randomStr(1, null);
						}
						else
						{
							text3 = text + NeteaseCode.randomStr(1, new string[]
							{
								"0",
								"1",
								"2",
								"3",
								"4",
								"5",
								"6",
								"7",
								"8",
								"9",
								"A",
								"B",
								"C",
								"D",
								"E"
							});
						}
					}
					else
					{
						text3 = text + NeteaseCode.randomStr(1, new string[]
						{
							"0",
							"2",
							"4",
							"6",
							"8",
							"A",
							"C",
							"E"
						});
					}
					text2 = text3;
				}
				text = text2;
			}
			return text.ToUpper();
		}
	}
}
