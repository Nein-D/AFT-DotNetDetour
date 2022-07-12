using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Mcl.Core.Utils;
using WPFLauncher.Manager;
using WPFLauncher.Model;
using WPFLauncher.Model.Game;
using WPFLauncher.Util;
using AFT_DotNetDetour.Hook;
using AFT_DotNetDetour.Properties;
using AFTDotNetDetour.AFTDotNetDetourEP;

namespace AFT_DotNetDetour.AFTHookEvents
{
    // Token: 0x02000013 RID: 19
    internal class GameProcesses : IMethodHook
	{
		// Token: 0x06000045 RID: 69 RVA: 0x00003140 File Offset: 0x00001340
		[HookMethod("System.Diagnostics.Process", null, null)]
		public static Process[] GetProcesses()
		{
			Process[] processes_Original = GameProcesses.GetProcesses_Original();
			List<Process> list = new List<Process>();
			string[] array = new string[]
			{
				"dllhost",
				"IAStorDataMgrSvc",
				"fontdrvhost",
				"WmiPrvSE",
				"svchost",
				"crss",
				"SecurityHealthService",
				"spoolsv",
				"dwm",
				"ctfmon",
				"conhost"
			};
			for (int i = 0; i <= processes_Original.Length; i++)
			{
				foreach (string value in array)
				{
					bool flag = processes_Original[i].ProcessName.Equals(value);
					if (flag)
					{
						list.Add(processes_Original[i]);
					}
				}
			}
			return list.ToArray();
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00003222 File Offset: 0x00001422
		[HookMethod("WPFLauncher.Manager.alc", null, null)]
		public Tuple<string, IntPtr> p(Process kfp, GameM kfq, string kfr = null)
		{
			return null;
		}

		[HookMethod("WPFLauncher.Util.se", null, null)]
		public static akp a(string euf, string eug, EventHandler euh, akn eui, string euj = null, bool euk = false, Action<string> eul = null)
		{

			string fileName = Path.GetFileName(euf);
			string a = fileName;
			akp result;
			if (!(a == "java.exe"))
			{
				if (!(a == "javaw.exe"))
				{
					result = GameProcesses.a_Original(euf, eug, euh, eui, euj, euk, eul);
				}
				else
				{
					try
					{
						bool flag = MessageBox.Show("是否注入CL?", string.Empty, MessageBoxButtons.YesNo) == DialogResult.Yes;
						if (flag)
						{
							File.WriteAllBytes(RegistryHelper.GetValue("DownloadPath") + "\\Game\\.minecraft\\mods\\" + Guid.NewGuid().ToString("X") + "█.jar", Resources.CL8);
						}
						
	
						foreach (FileInfo fileInfo in new DirectoryInfo(RegistryHelper.GetValue("DownloadPath") + "\\Game\\.minecraft\\mods\\").GetFiles())
						{
							bool flag5 = !fileInfo.Name.EndsWith("@3@0.jar") && fileInfo.Name.EndsWith(".jar") && !fileInfo.Name.Contains("█");
							if (flag5)
							{
								fileInfo.Delete();
							}
						}

						akp launchProces = GameProcesses.a_Original(euf, eug, euh, eui, euj, euk, eul);
						new Thread(delegate()
						{
							while (!launchProces.HasExited)
							{
								foreach (ProcessModule64 processModule in ProcessModules.getWOW64Modules(launchProces.ProcessId))
								{
									bool flag6 = processModule.ModuleName == "api-ms-win-crt-utility-l1-1-1.dll";
									if (flag6)
									{
										byte[] buffer = new byte[]{121,118,54,54,118,103,65,65,65,68,81,65,69,103,69,65,66,109,104,108,98,72,66,108,99,103,99,65,65,81,69,65,69,71,112,104,100,109,69,118,98,71,70,117,90,121,57,80,89,109,112,108,89,51,81,72,65,65,77,66,65,65,116,111,90,87,120,119,90,88,73,117,97,109,70,50,89,81,69,65,66,106,120,112,98,109,108,48,80,103,69,65,65,121,103,112,86,103,119,65,66,103,65,72,67,103,65,69,65,65,103,66,65,65,82,48,97,71,108,122,65,81,65,73,84,71,104,108,98,72,66,108,99,106,115,66,65,65,112,48,90,88,78,48,84,50,74,113,90,87,78,48,65,81,65,86,75,69,120,113,89,88,90,104,76,50,120,104,98,109,99,118,84,50,74,113,90,87,78,48,79,121,108,87,65,81,65,69,81,50,57,107,90,81,69,65,69,107,120,118,89,50,70,115,86,109,70,121,97,87,70,105,98,71,86,85,89,87,74,115,90,81,69,65,68,48,120,112,98,109,86,79,100,87,49,105,90,88,74,85,89,87,74,115,90,81,69,65,67,108,78,118,100,88,74,106,90,85,90,112,98,71,85,65,73,81,65,67,65,65,81,65,65,65,65,65,65,65,73,65,65,81,65,71,65,65,99,65,65,81,65,79,65,65,65,65,76,119,65,66,65,65,69,65,65,65,65,70,75,114,99,65,67,98,69,65,65,65,65,67,65,65,56,65,65,65,65,77,65,65,69,65,65,65,65,70,65,65,111,65,67,119,65,65,65,66,65,65,65,65,65,71,65,65,69,65,65,65,65,66,65,81,107,65,68,65,65,78,65,65,65,65,65,81,65,82,65,65,65,65,65,103,65,70 };
										
										launchProces.writeBytes((ulong)(processModule.BaseAddress + (long)GameProcesses.Dec_("60EA0")), buffer, 1672UL);
										launchProces.writeBytes((ulong)(processModule.BaseAddress + (long)GameProcesses.Dec_("606E0")), buffer, 1524UL);
										launchProces.writeBytes((ulong)(processModule.BaseAddress + (long)GameProcesses.Dec_("569E0")), buffer, 2036UL);
										launchProces.writeBytes((ulong)(processModule.BaseAddress + (long)GameProcesses.Dec_("61530")), buffer, 6952UL);
										launchProces.writeBytes((ulong)(processModule.BaseAddress + (long)GameProcesses.Dec_("63060")), buffer, 6444UL);
										launchProces.writeBytes((ulong)(processModule.BaseAddress + (long)GameProcesses.Dec_("65190")), buffer, 1524UL);
										launchProces.writeBytes((ulong)(processModule.BaseAddress + (long)GameProcesses.Dec_("5B730")), buffer, 1520UL);
										launchProces.writeBytes((ulong)(processModule.BaseAddress + (long)GameProcesses.Dec_("64990")), buffer, 2036UL);
										launchProces.writeBytes((ulong)(processModule.BaseAddress + (long)GameProcesses.Dec_("5C570")), buffer, 6448UL);
										launchProces.writeBytes((ulong)(processModule.BaseAddress + (long)GameProcesses.Dec_("5E340")), buffer, 8784UL);
									}
								}
							}
						})
						{
							IsBackground = true
						}.Start();
						result = launchProces;
					}
					catch
					{
						result = null;
					}
				}
			}
			else
			{
				result = null;
			}
			return result;
		}

		public static int Dec_(string Hex)
		{
			return Convert.ToInt32(Hex, 16);
		}

		public static akp HookEvents;

		// Token: 0x06000049 RID: 73 RVA: 0x00003534 File Offset: 0x00001734
		[OriginalMethod]
		public static Process[] GetProcesses_Original()
		{
			return null;
		}

		[HookMethod("WPFLauncher.Network.Protocol.aaa", null, null)]
		public static void d(string gej, object gek, uint gel = 0U)
		{
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00003537 File Offset: 0x00001737
		[OriginalMethod]
		public static akp a_Original(string euf, string eug, EventHandler euh, akn eui, string euj = null, bool euk = false, Action<string> eul = null)
		{
			return null;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x0000353A File Offset: 0x0000173A
		[OriginalMethod]
		private void a_Original(GameM lsy)
		{
		}
	}
}
