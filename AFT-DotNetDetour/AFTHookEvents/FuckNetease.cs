using AFT_DotNetDetour.Hook;
using System;
using Mcl.Core.Network;
using Mcl.Core.Network.Interface;
using WPFLauncher.Network.Protocol;
using WPFLauncher.Model;

using System.Threading;
using System.Diagnostics;
using System.Collections.Generic;

namespace AFT_DotNetDetour.AFTHookEvents
{
    internal class FuckNetease : IMethodHook
    {

		//Anti Anti cheating



		[HookMethod("WPFLauncher.Manager.alc", null, null)]
		private void q()
		{
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002EE0 File Offset: 0x000010E0
		[HookMethod("WPFLauncher.Manager.alc", null, null)]
		public void e(GameM kew)
		{
			bool flag = kew != null;
			if (flag)
			{
				bool flag2 = kew.Type == GType.NET_GAME || kew.Type == GType.SERVER_GAME || kew.Type == GType.ONLINE_LOBBY_GAME;
				if (flag2)
				{
					kew.Status = GStatus.PLAYING;
				}
				else
				{
					this.e_Original(kew);
				}
			}
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002F31 File Offset: 0x00001131
		[HookMethod("WPFLauncher.Network.Protocol.aaa", null, null)]
		public static void d(string ghk, object ghl, uint ghm = 0U)
		{
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002F36 File Offset: 0x00001136
		[HookMethod("WPFLauncher.Model.CompGameM", null, null)]
		public void f(byte[] hoq)
		{
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002F3B File Offset: 0x0000113B
		[HookMethod("WPFLauncher.Model.CompGameM", null, null)]
		public void i(byte[] hot)
		{
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002F40 File Offset: 0x00001140
		[OriginalMethod]
		public void e_Original(GameM kew)
		{
		}

		//日志拦截

		[HookMethod("WPFLauncher.Network.Protocol.aaf", null, null)]
		public static NetRequestAsyncHandle f(string gsq, string gsr, Action<INetResponse> gss = null, aac gst = aac.a, string gsu = null)
		{
			return null;
		}


		[HookMethod("WPFLauncher.Manager.Auth.aqu", null, null)]
		public void g(string lue)
		{
		}

		//bypass textcheck

		[HookMethod("WPFLauncher.bb", null, null)]
		public static bool c(string st)
		{
			AFTClient.send("[]Bpyass TextCheck：" + st);
			return true;

		}

		//////////////////////////////////////////////////////////////////
		[HookMethod("WPFLauncher.Manager.Auth.aqw", "b", null)]
		public void phy79ht43R6Motg8()
		{
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000029DF File Offset: 0x00000BDF
		[HookMethod("WPFLauncher.Manager.Game.aos", "e", null)]
		public void H5xshufF95yro9Ln(GameM lma)
		{
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000029E2 File Offset: 0x00000BE2
		[HookMethod("WPFLauncher.Manager.Auth.aqw", null, null)]
		public static void n()
		{
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000029E5 File Offset: 0x00000BE5
		[HookMethod("WPFLauncher.Manager.Auth.aqw", "o", null)]
		public static void o1111()
		{
		}

		// Token: 0x06000038 RID: 56 RVA: 0x000029E8 File Offset: 0x00000BE8
		[HookMethod("WPFLauncher.Manager.Auth.aqw", null, null)]
		public static void p()
		{
		}



		// Token: 0x0600003A RID: 58 RVA: 0x000029EE File Offset: 0x00000BEE
		[HookMethod("WPFLauncher.Manager.Auth.aqw", null, null)]
		public static void e()
		{
		}

		// Token: 0x0600003B RID: 59 RVA: 0x000029F1 File Offset: 0x00000BF1
		[HookMethod("WPFLauncher.Model.Auth.VapeDetect", null, null)]
		public void set_game_title(string value)
		{
		}

		// Token: 0x0600003C RID: 60 RVA: 0x000029F4 File Offset: 0x00000BF4
		[HookMethod("WPFLauncher.Model.Auth.VapeDetect", null, null)]
		public string get_cmd_line()
		{
			return "";
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002A0C File Offset: 0x00000C0C
		[HookMethod("WPFLauncher.Util.pz", "a", "DllCheck_Original")]
		public static Tuple<string, string> a(string eas)
		{
			return Tuple.Create<string, string>(string.Empty, string.Empty);
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002A30 File Offset: 0x00000C30
		[HookMethod("WPFLauncher.Util.py", null, null)]
		public static string b(string eas)
		{
			return string.Empty;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002A48 File Offset: 0x00000C48
		[HookMethod("WPFLauncher.Util.pz", "c", null)]
		public static string DllCheckC(Process eau)
		{
			return string.Empty;
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002A60 File Offset: 0x00000C60
		[OriginalMethod]
		public static Tuple<string, string> DllCheck_Original(string eau)
		{
			return Tuple.Create<string, string>(string.Empty, string.Empty);
		}


		// Token: 0x06000044 RID: 68 RVA: 0x00002A8A File Offset: 0x00000C8A
		[HookMethod("WPFLauncher.Manager.akl", "e", null)]
		public void kkkk(string jrm, Dictionary<string, string> jrn)
		{
		}
	}

}
