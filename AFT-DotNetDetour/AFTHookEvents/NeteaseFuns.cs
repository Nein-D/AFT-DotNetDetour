using AFT_DotNetDetour.Hook;
using System.Linq;
using System.Net;
using System;
using WPFLauncher.Model;
using System.Collections.Generic;

namespace AFT_DotNetDetour.AFTHookEvents 
{
	internal class NeteaseFuns : IMethodHook
	{


		[HookMethod("WPFLauncher.bw", null, null)]
		public static void h(string ti, string tj)
		{
			AFTClient.send("h | " + ti, tj);
		}
		[HookMethod("WPFLauncher.bw", null, null)]
		public static void j(string tm, string tn)
		{
			AFTClient.send("j | " + tm, tn);
		}
		[HookMethod("WPFLauncher.bw", null, null)]
		public static void i(string tk, string tl)
        {
        }

	}
}
