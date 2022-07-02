using AFT_DotNetDetour.Hook;
using System;
using Mcl.Core.Network;
using Mcl.Core.Network.Interface;
using WPFLauncher.Network.Protocol;
using WPFLauncher.Model;

namespace AFT_DotNetDetour.AFTHookEvents
{
    internal class AFTHookEvents : IMethodHook
    {
		//关闭CG动画
		public class fuck_CG : IMethodHook
		{
			[HookMethod("WPFLauncher.Manager.Configuration.ans", null, null)]
			private static bool get_PlayCG()
			{
				AFTClient.send("[]Bypass CG animation");
				return false;
			}
		}

		//Anti Anti cheating

		[HookMethod("WPFLauncher.Network.Protocol.zt", null, null)]
		public static void d(string ggh, object ggi, uint ggj = 0U)
		{
		}

		[HookMethod("WPFLauncher.Model.CompGameM", null, null)]
		public void f(byte[] hnj)
		{
		}

		[HookMethod("WPFLauncher.Model.CompGameM", null, null)]
		public void i(byte[] hnm)
		{
		}

		[HookMethod("WPFLauncher.Manager.aiy", null, null)]
		public void l(string jii = "No Exception Description\r\n", int jij = 0)
		{
		}

		[HookMethod("WPFLauncher.Manager.akv", null, null)]
		public void e(GameM kcy)
		{
			bool flag = kcy != null;
			if (flag)
			{
				bool flag2 = kcy.Type == GType.NET_GAME || kcy.Type == GType.SERVER_GAME || kcy.Type == GType.ONLINE_LOBBY_GAME;
				if (flag2)
				{
					kcy.Status = GStatus.PLAYING;
				}
				else
				{
					this.e_Original(kcy);
				}
			}
		}

		[OriginalMethod]
		public void e_Original(GameM kbs)
		{
		}

		//日志拦截

		[HookMethod("WPFLauncher.Network.Protocol.zy", null, null)]
		public static NetRequestAsyncHandle f(string grg, string grh, Action<INetResponse> gri = null, zv grj = zv.a, string grk = null)
		{
			return null;
		}

	
		[HookMethod("WPFLauncher.Manager.Auth.aqm", null, null)]
		public void g(string lsa)
		{
		}

		//bypass textcheck

		[HookMethod("WPFLauncher.bb", null, null)]
		public static bool c(string sy)
		{
			AFTClient.send("[]Bpyass TextCheck：" + sy);
			return true;
		}

		[HookMethod("WPFLauncher.Manager.aku", null, null)]
		private static void o()
		{
			AFTClient.send("[]SDKinit");
		}


	}

}
