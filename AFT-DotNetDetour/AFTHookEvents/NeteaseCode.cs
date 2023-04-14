using System;
using System.Net;
using AFT_DotNetDetour.Hook;
using System.Linq;


namespace AFT_DotNetDetour.AFTHookEvents
{

	internal class NeteaseCode : IMethodHook
	{
		static string GetRandomIp()
		{
			var Random = new Random();
			var ipBuffer = new byte[4];
			Random.NextBytes(ipBuffer);
			return new IPAddress(ipBuffer).ToString();
		}

		static string GetRandomStr(int strLen)
		{
			var Random = new Random();
			return new string(Enumerable.Repeat("ABCDEFGHIJKLMNOPQRST1234567890", strLen)
				.Select(s => s[Random.Next(s.Length)]).ToArray());
		}

		public class fuck_CG : IMethodHook
		{
            [HookMethod("WPFLauncher.Manager.Configuration.arn", null, null)]
            private static bool get_PlayCG()
            {
				AFTClient.send("get_PlayCG ");
				return false;
			}
		}


	[HookMethod("WPFLauncher.Manager.Log.Util.ant", null, null)]
		public static string b()
		{
			string result = GetRandomStr(12);
			AFTClient.send("MAC_ADDRESS ：", result);
			AFTClient.send("替换MAC成功 ");

            return result;
		}

		[HookMethod("WPFLauncher.bw")]
		public static string an()
		{
			string result = string.Concat(new string[]
			{
				"netdns=",
				GetRandomIp(),
				" gw=",
				GetRandomIp(),
				" gwdns=correct"
			});

			AFTClient.send("Dns:", result);

			return result;
		}

		[HookMethod("WPFLauncher.Manager.als")]
		private string d(string jrg)
		{
			string result = GetRandomStr(16) + jrg;

			AFTClient.send("Cpuid", result);
            //Get Processorid
            return result;
		}

		[HookMethod("WPFLauncher.Manager.als")]
		public string f()
		{
			string result = GetRandomStr(8);
			AFTClient.send("DiskId :", result);
            //Get VolumeSerialNumber
            return result;
		}

		[OriginalMethod]
		public static string f_Original()
		{
			return null;
		}

		[HookMethod("WPFLauncher.Manager.alp")]
		public string g()
		{
			string result = GetRandomIp();

			AFTClient.send("IP:", result);

			return result;
		}
	}
}
