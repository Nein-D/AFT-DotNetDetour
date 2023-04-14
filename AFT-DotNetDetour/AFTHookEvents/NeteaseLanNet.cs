using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows;
using AFT_DotNetDetour.Hook;
using Mcl.Core.Network;
using Mcl.Core.Network.Interface;
using WPFLauncher.Common;
using WPFLauncher.Manager;
using WPFLauncher.Manager.Game.Pipeline;
using WPFLauncher.Model;
using WPFLauncher.Network.Protocol;
using WPFLauncher.Util;

namespace AFT_DotNetDetour.AFTHookEvents
{
	public class NeteaseLanNet : IMethodHook
	{

		[HookMethod("WPFLauncher.Network.xu", null, null)]
		public void a(ushort fkk, Action<byte[]> fkl)
		{
			try
			{
				this.a_Original(fkk, fkl);
				AFTClient.send("Network.xu a :" + fkk.ToString(), fkl.Method.ToString() + " " + fkl.Target.ToString());
			}
			catch (Exception)
			{
			}
		}


		[OriginalMethod]
		public void a_Original(ushort fkk, Action<byte[]> fkl)
		{
		}

		[HookMethod("WPFLauncher.Network.xu", null, null)]
		public void b(ushort fkk, byte[] fkl)
		{
			try
			{
				bool flag = fkk == 19;
				if (flag)
				{
					while (!Tool.checkUnSafeMod())
					{
						Tool.deleteUnSafeMod();
						Thread.Sleep(100);
					}
					AFTClient.send("client_Pcyc_check");
				}
				else
				{
					ushort[] array = blokArray;
					ushort[] array2 = array;
					int i = 0;
					while (i < array2.Length)
					{
						int num = (int)array2[i];
						bool flag2 = num == (int)fkk;
						if (flag2)
						{
							int num2 = num;
							int num3 = num2;
							if (num3 != 261)
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
			}
			catch (Exception)
			{
			}
		}

		[HookMethod("WPFLauncher.Network.Protocol.abh", null, null)]
		public static NetRequestAsyncHandle f(string hpk, string hpl, Action<INetResponse> hpm = null, abe hpn = abe.a, string hpo = null)

		{
			return null;
		}


		[OriginalMethod]
		public void b_Original(ushort fkk, byte[] fkl)
		{
		}

		[HookMethod("WPFLauncher.Manager.alu", null, null)]
		public static List<string> n()
		{
			return arr;
		}

		private static List<string> arr = new List<string>
		{
			"Minecraft"
		};
        [HookMethod("WPFLauncher.util.se", null, null)]
        public static MessageBoxResult h(Window fkz, string fka, bool fkb, out bool fkc, string fkd, string fke = "确定", string fkf = "取消")
		{
			if (fka == ass<aml>.Instance.a("COMPONENT_UPDATE/GAME_UPDATE_MSG") && fkd == ass<aml>.Instance.a("COMPONENT_UPDATE/AUTO_LAUNCH"))
			{
				fkc = true;
				return MessageBoxResult.OK;
			}
			return p_Original(fkz,fka, fkb, out fkc, fkd, fke, fkf);

        }
		[OriginalMethod]
        private static MessageBoxResult p_Original(Window fkz, string fka, bool fkb, out bool fkc, string fkd, string fke = "确定", string fkf = "取消")
        {
			fkc = false;
			return MessageBoxResult.None;
			
        }

        [HookMethod("WPFLauncher.Manager.Game.Pipeline.aqe",null,null)]
        private void a(GameM mky, BaseWindow mkz)
		{
			if (se.r("检测到当前版本（网易版本）游戏客户端（基岩版）有更新，是否跳过更新？", string.Empty, "跳过", "更新", "") == MessageBoxResult.Yes)
			{
				typeof(apy).GetMethod("gl").Invoke(this, new object[] { LTaskOpcode.NEXT });
			}
			else
			{
                a_Original(mky, mkz);
            }				
		}

        private void a_Original(GameM mky, BaseWindow mkz)
        { 
        }

        private static ushort[] blokArray = new ushort[]
		{
			19
		};
	}

}