using System;
using AFT_DotNetDetour.Hook;


namespace AFT_DotNetDetour.AFTHookEvents
{

	internal class Virtual : IMethodHook
	{

		[HookMethod("WPFLauncher.Manager.Log.Util.amx", null, null)]
		public static string b()
		{
			string text = "";
			string result;
			try
			{
				if (text == null || text.Length != 12)
				{
					text = Virtual.getMac(Virtual.b_Original());
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

		[HookMethod("WPFLauncher.Manager.ake", "f", null)]
		public static string DiskId()
		{
			string text = "";
			string result;
			try
			{
				if (text == null || text.Length != 8)
				{
					text = Virtual.getDiskCode();
				}
				AFTClient.send("DiskId", text);
				result = text;
			}
			catch
			{
				result = Virtual.f_Original();
			}
			return result;
		}

		[OriginalMethod]
		public static string f_Original()
		{
			return null;
		}


		[HookMethod("WPFLauncher.Manager.ake", null, null)]
		public static string d(string jmh)
		{
			string text = "";
			string result;
			try
			{
				if (text == null || text.Length != 16)
				{
					text = Virtual.getCPUID();
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
				result = Virtual.d_Original(jmh);
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
				text = Virtual.randomStr(8, null).ToUpper();
			}
			return text;
		}


		public static string getCPUID()
		{
			string text = null;
			if (text == null || text.Length != 16)
			{
				text = Virtual.randomStr(16, null).ToUpper();
			}
			return text;
		}


		public static string getMac(string source)
		{
			string text = null;
			if (text == null || text.Length != 12)
			{
				text = Virtual.randomMac(source).ToUpper();
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
					text2 = text + Virtual.randomStr(1, null);
				}
				else
				{
					string text3;
					if (i != 2)
					{
						if (i != 12)
						{
							text3 = text + Virtual.randomStr(1, null);
						}
						else
						{
							text3 = text + Virtual.randomStr(1, new string[]
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
						text3 = text + Virtual.randomStr(1, new string[]
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
