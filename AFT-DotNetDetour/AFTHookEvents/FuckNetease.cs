using AFT_DotNetDetour.Hook;
using System;
using Mcl.Core.Network;
using Mcl.Core.Network.Interface;
using WPFLauncher.Network.Protocol;
using WPFLauncher.Model;
using System.Linq;
using System.Net;
using System.Diagnostics;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using WPFLauncher.Manager.Configuration.CppConfigure;

namespace AFT_DotNetDetour.AFTHookEvents
{
    internal class FuckNetease : IMethodHook
    {

        //Anti Anti cheating


        [HookMethod("WPFLauncher.Manager.amg", null, null)]
        private static void p()
        {
			AFTClient.send("AcSdkInit");
			return;
		}


		[HookMethod("WPFLauncher.Manager.amg", null, null)]
		public void E(GameM kew)
		{
			if (kew != null)
			{
				if (kew.Type == GType.NET_GAME || kew.Type == GType.SERVER_GAME || kew.Type == GType.ONLINE_LOBBY_GAME)
				{
                    //拦截反作弊线程合集启动
                    AFTClient.send("Intercept Anti Cheating Thread Collection startup");
                    kew.Status = GStatus.PLAYING;
				}
				else
				{
					e_Original(kew);
				}
			}
		}

   

        [HookMethod("WPFLauncher.Manager.alp")]
        public void E(string knn, Dictionary<string, string> kno)
        {
           
            WPFLauncher.bw.h("client_check", null);//client_check
            AFTClient.send("Return Client_Check 'Null' Success");

        }


        [HookMethod("WPFLauncher.Manager.alp")]
        public static Process[] O(string krd)

        {
            AFTClient.send("F GetProcessesByName Success");
            return null;
          

        }
     
        [HookMethod("WPFLauncher.Manager.amg", null, null)]
        public Tuple<string, IntPtr> o(Process kfp, GameM kfq, string kfr = null)
        {
            return null;
        }

        
        [HookMethod("WPFLauncher.Network.yv", null, null)]
        public void a(ushort flo, Action<byte[]> flp)
        {
            try
            {
                this.a_Original(flo, flp);
                AFTClient.send("Network.yv.a" + flo.ToString(), flp.Method.ToString() + " " + flp.Target.ToString());
            }
            catch (Exception)
            {
            }
        }

        [HookMethod("WPFLauncher.Network.yv", null, null)]
        public void b(ushort fkk, byte[] fkl)
        {
            try
            {
                bool flag = fkk == 19;
                ushort[] array = FuckNetease.blokArray;
                ushort[] array2 = array;
                int i = 0;
                while (i < array2.Length)
                {
                    int num = (int)array2[i];
                    bool flag2 = num == (int)fkk;
                    bool flag3 = flag2;
                    if (flag3)
                    {
                        int num2 = num;
                        int num3 = num2;
                        bool flag4 = num3 != 261;
                        if (flag4)
                        {
                            return;
                        }
                        return;
                    }
                    else
                    {
                        i++;
                    }
                }
                this.b_Original(fkk, fkl);
            }
            catch (Exception)
            {
            }
        }




        [HookMethod("WPFLauncher.Network.Protocol.abc")]
        //public static void d(string ghk, object ghl, uint ghm = 0U)
        public static void d(string heg, object heh, uint hei = 0U)
        {
			return;
		}

		[HookMethod("WPFLauncher.Model.CompGameM")]
		public void f(byte[] hoq)
		{
			return;
		}

		[HookMethod("WPFLauncher.Model.CompGameM")]
		public void i(byte[] hot)
		{
			return;
		}


		[OriginalMethod]
		public void e_Original(GameM kew)
		{
		}

		//数据处理



		[HookMethod("WPFLauncher.Network.Protocol.abh")]
        public static NetRequestAsyncHandle f(string hpk, string hpl, Action<INetResponse> hpm = null, abe hpn = abe.a, string hpo = null)
        {
			return null;
		}


        [HookMethod("WPFLauncher.Manager.Auth.ari", "b", null)]
        public void phy79ht43R6Motg8()
        {
        }
        [HookMethod("WPFLauncher.Manager.Auth.ari", null, null)]
        public static void e()
        {
        }
        // Token: 0x06000030 RID: 48 RVA: 0x00002794 File Offset: 0x00000994
        [HookMethod("WPFLauncher.Manager.Game.ape")]
        public void e(GameM mhb)
        {
        }

        [OriginalMethod]
        public string e_Original(string dpi, string dpj)
        {
            return null;
        }


        [HookMethod("WPFLauncher.Model.Auth.VapeDetect", null, null)]
		public void set_game_title(string value)
		{
		}

		[HookMethod("WPFLauncher.Model.Auth.VapeDetect", null, null)]
		public string get_cmd_line()
		{
			return "";
		}

		[HookMethod("WPFLauncher.Util.ra", "a", "DllCheck_Original")]
		public static Tuple<string, string> a(string etr)
		{
			return Tuple.Create<string, string>(string.Empty, string.Empty);
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002A30 File Offset: 0x00000C30
		[HookMethod("WPFLauncher.Util.ra", null, null)]
        public static string b(string ewm)
       {
			return string.Empty;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002A48 File Offset: 0x00000C48
		[HookMethod("WPFLauncher.Util.ra", "c", null)]
		public static string DllCheckC(Process ewp)
		{
			return string.Empty;
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002A60 File Offset: 0x00000C60
		[OriginalMethod]
		public static Tuple<string, string> DllCheck_Original(string eau)
		{
			return Tuple.Create<string, string>(string.Empty, string.Empty);
		}

        [OriginalMethod]
        public void a_Original(ushort fkk, Action<byte[]> fkl)
        {
        }
        [OriginalMethod]
        public void b_Original(ushort fkk, byte[] fkl)
        {
        }
        private static ushort[] blokArray = new ushort[]
{
            19
};
    }

}
