using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Mcl.Core.Utils;
using WPFLauncher.Common;
using WPFLauncher.Manager;
using WPFLauncher.Util;
using AFT_DotNetDetour.Hook;
using AFTDotNetDetour.AFTDotNetDetourEP;
using AFT_DotNetDetour.Properties;

namespace AFT_DotNetDetour.AFTHookEvents
{



	internal class GameProcessces : IMethodHook
	{
		// Token: 0x0600004E RID: 78 RVA: 0x0000355C File Offset: 0x0000175C
		public static string GetMiddleStr(string oldStr, string preStr, string nextStr)
		{
			string text = oldStr.Substring(oldStr.IndexOf(preStr) + preStr.Length);
			return text.Substring(0, text.IndexOf(nextStr));
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00003594 File Offset: 0x00001794
		[HookMethod("System.Diagnostics.Process", null, null)]
		public static Process[] GetProcesses()
		{
			Process[] processes_Original = GameProcessces.GetProcesses_Original();
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

	
		[OriginalMethod]
		public static Process[] GetProcesses_Original()
		{
			return null;
		}


		[HookMethod("WPFLauncher.Manager.akj", null, null)]
		public static List<string> n()
		{
			return null;
		}


		[HookMethod("WPFLauncher.Util.rx", null, null)]
		public static aki a(string processPath, string processArgs, EventHandler onGameExit, akg gameType, string workingDirectory = null, bool isListenLogs = false, Action<string> onLogsOutPut = null)
	
		{
			string middleStr = GameProcessces.GetMiddleStr(processArgs, "-Druntime_path=\"", "\"");
			foreach (string path in Directory.GetFiles(middleStr))
			{
				File.Delete(path);
			}

			File.WriteAllBytes(RegistryHelper.GetValue("DownloadPath") + "\\Game\\.minecraft\\mods\\" + Guid.NewGuid().ToString("X") + "█.jar", Resources.CL8);
			AFTClient.send("[]Executing write CL8 instruction");

	
			bool flag5 = MessageBox.Show("是否删除非必要模组?", string.Empty, MessageBoxButtons.YesNo) == DialogResult.Yes;
			if (flag5)
			{
				foreach (FileInfo fileInfo in new DirectoryInfo(RegistryHelper.GetValue("DownloadPath") + "\\Game\\.minecraft\\mods\\").GetFiles())
				{
					bool flag6 = !fileInfo.Name.EndsWith("@3@0.jar") && fileInfo.Name.EndsWith(".jar") && !fileInfo.Name.Contains("█");
					if (flag6)
					{
						fileInfo.Delete();
						AFTClient.send("[] Module deleted successfully");
					}
				}
			}
			GameProcessces.Process = GameProcessces.a_Original(processPath, processArgs, delegate (object A_0, EventArgs A_1)
			//a_Original(processPath, processArgs, onGameExit, gameType, workingDirectory, isListenLogs, onLogsOutPut);
			{
				onGameExit(A_0, A_1);
			}, gameType, workingDirectory, true, delegate (string LogsStr)
			{
				bool flag7 = string.IsNullOrEmpty(LogsStr);
				if (flag7)
				{
				}
			});
			new Thread(delegate ()
			{
				while (!GameProcessces.Process.HasExited)
				{
					foreach (ProcessModule64 processModule in ProcessModules.getWOW64Modules(GameProcessces.Process.ProcessId))
					{
						bool flag7 = processModule.ModuleName == "api-ms-win-crt-utility-l1-1-1.dll";
						if (flag7)
						{
							GameProcessces.Process.writeBytes((ulong)(processModule.BaseAddress + 191664L), new byte[]
							{
								176,
								16,
								195,
								36,
								16
							}, 5UL);
							GameProcessces.Process.writeBytes((ulong)(processModule.BaseAddress + 187616L), new byte[]
							{
								176,
								1,
								195,
								36,
								16
							}, 5UL);
							while (Convert.ToBase64String(GameProcessces.Process.readBytes((ulong)(processModule.BaseAddress + 21552L), 5UL)) != "SIlcJAg=")
							{
								Application.DoEvents();
							}
							GameProcessces.Process.writeBytes((ulong)(processModule.BaseAddress + 21552L), new byte[]
							{
								195
							}, 1UL);
							GameProcessces.Process.writeBytes((ulong)(processModule.BaseAddress + 184448L), new byte[]
							{
								176,
								16,
								195
							}, 3UL);
						}
					}
				}
			})
			{
				IsBackground = true
			}.Start();
			return GameProcessces.Process;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x000039BC File Offset: 0x00001BBC
		[OriginalMethod]
		public static aki a_Original(string esk, string esl, EventHandler esm, akg esn, string eso = null, bool esp = false, Action<string> esq = null)
		{
			return null;
		}

		// Token: 0x04000030 RID: 48
		public static aki Process = null;


		
	}
}







