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

using DotNetDetour.Extensions;


namespace AFT_DotNetDetour.AFTHookEvents
{
    // Token: 0x02000013 RID: 19
    internal class GameProcesses : IMethodHook
	{
		public static string GetMiddleStr(string oldStr, string preStr, string nextStr)
		{
			string text = oldStr.Substring(oldStr.IndexOf(preStr) + preStr.Length);
			return text.Substring(0, text.IndexOf(nextStr));
		}
		// Token: 0x06000045 RID: 69 RVA: 0x00003140 File Offset: 0x00001340
		[HookMethod("System.Diagnostics.Process", null, null)]
		public static Process[] GetProcesses()
		{
			Process[] processes_Original = GameProcesses.GetProcesses_Original();
			List<Process> list = new List<Process>();
			string[] array = new string[11]
			{
				"dllhost", "IAStorDataMgrSvc", "fontdrvhost", "WmiPrvSE", "svchost", "crss", "SecurityHealthService", "spoolsv", "dwm", "ctfmon",
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





        [HookMethod("WPFLauncher.Manager.amg", null, null)]
        public Tuple<string, IntPtr> o(Process kfp, GameM kfq, string kfr = null)
        {
            return null;
        }
        //	public Tuple<string, IntPtr> o(Process kfp, GameM kfq, string kfr = null)



        [HookMethod("WPFLauncher.Util.tf", null, null)]
		public static alt a(string euf, string eug, EventHandler euh, alr eui, string euj = null, bool euk = false, Action<string> eul = null)
       
        {

            string fileName = Path.GetFileName(euf);
			string a = fileName;
			if (!(a == "java.exe"))
			{
				if (!(a == "javaw.exe"))
				{
					return GameProcesses.a_Original(euf, eug, euh, eui, euj, euk, eul);
				}
				else
				{
					try
					{

						bool flag = MessageBox.Show("#是否注入CL?(请选否)", string.Empty, MessageBoxButtons.YesNo) == DialogResult.Yes;
						if (flag)
						{
							File.WriteAllBytes(RegistryHelper.GetValue("DownloadPath") + "\\Game\\.minecraft\\mods\\" + Guid.NewGuid().ToString("X") + "я█.jar", Resources.CL8);
						}
						
	
						foreach (FileInfo fileInfo in new DirectoryInfo(RegistryHelper.GetValue("DownloadPath") + "\\Game\\.minecraft\\mods\\").GetFiles())
						{
							bool flag5 = !fileInfo.Name.EndsWith("@3@0.jar") && fileInfo.Name.EndsWith(".jar") && !fileInfo.Name.Contains("я█");
							if (flag5)
							{
								fileInfo.Delete();
							}
						}

						var launchProces = a_Original(euf, eug, euh, eui, euj, euk, eul);


						new Thread(delegate ()
						{

                           // while (!launchProces.HasExited)
                           //{
                               // foreach (ProcessModule64 tagModule in ProcessModules.getWOW64Modules(launchProces.ProcessId))
                             //   {
                              //      if (tagModule.ModuleName == "api-ms-win-crt-utility-l1-1-1.dll")
                               //     {
                                //        Thread.CurrentThread.Abort();
                               //     }
                              //  }
                        //    }


                            while (!((Process)launchProces).HasExited)
                            {
                                ProcessModule64[] wow64Modules = ProcessModules.getWOW64Modules(launchProces.ProcessId);
                                foreach (ProcessModule64 processModule in wow64Modules)
                                {
                                    bool flag8 = processModule.ModuleName == "api-ms-win-crt-utility-l1-1-1.dll";
                                    if (flag8)
                                    {
                                        while (Convert.ToBase64String(launchProces.readBytes((ulong)(processModule.BaseAddress + 21552), 5)).ToUpper() != "SIlcJAg=")
										{
										}
										launchProces.writeBytes((ulong)(processModule.BaseAddress + 21552), new byte[] { 195 }, 1);

									}
								}
                            }

                         //   do
							//{
							//	launchProces.Refresh();
							//} while (launchProces.ProcessId == 0);
							
							AFTClient.send("Initialize memory anti sealing |", launchProces.ProcessId.ToString());
						})
						{
							IsBackground = true
						}.Start();
						return launchProces;
					}
					catch (Exception thisException)
					{
						AFTClient.send(thisException.ToString());
						return null;
					}
				}
			}
			return null;
		}

		public static int Dec_(string Hex)
		{
			return Convert.ToInt32(Hex, 16);
		}

		public static alt HookEvents;


		[OriginalMethod]
		public static Process[] GetProcesses_Original()
		{
			return null;
		}




		[OriginalMethod]
		public static alt a_Original(string euf, string eug, EventHandler euh, alr eui, string euj = null, bool euk = false, Action<string> eul = null)
		{
			return null;
		}



		[OriginalMethod]
		private void a_Original(GameM lsy)
		{
			return;
		}
	}
}
