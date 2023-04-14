using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace DotNetDetour.Extensions
{
	// Token: 0x02000019 RID: 25
	internal class ProcessModules
	{
		// Token: 0x06000060 RID: 96 RVA: 0x000036C8 File Offset: 0x000018C8
		public unsafe static ProcessModule64[] getWOW64Modules(int PID)
		{
			uint num = 0U;
			ulong num2 = 0UL;
			ulong num3 = 0UL;
            _ = (new byte[512]);
			PROCESS_BASIC_INFORMATION64 process_BASIC_INFORMATION = default(PROCESS_BASIC_INFORMATION64);
			LIST_ENTRY64 list_ENTRY = default(LIST_ENTRY64);
			LDR_DATA_TABLE_ENTRY64 ldr_DATA_TABLE_ENTRY = default(LDR_DATA_TABLE_ENTRY64);
			List<ProcessModule64> list = new List<ProcessModule64>();
			int num4 = DLLTool.OpenProcess(1040U, false, (uint)PID);
			bool flag = num4 == 0;
			ProcessModule64[] result;
			if (flag)
			{
				result = new ProcessModule64[0];
			}
			else
			{
				bool flag2 = DLLTool.NtWow64QueryInformationProcess64(num4, 0U, out process_BASIC_INFORMATION, 48U, out num) < 0;
				if (flag2)
				{
					DLLTool.CloseHandle(num4);
					result = new ProcessModule64[0];
				}
				else
				{
					bool flag3 = process_BASIC_INFORMATION.PebBaseAddress == 0UL;
					if (flag3)
					{
						DLLTool.CloseHandle(num4);
						result = new ProcessModule64[0];
					}
					else
					{
						bool flag4 = DLLTool.NtWow64ReadVirtualMemory64(num4, process_BASIC_INFORMATION.PebBaseAddress + 24UL, out num3, 8UL, out num2) < 0;
						if (flag4)
						{
							DLLTool.CloseHandle(num4);
							result = new ProcessModule64[0];
						}
						else
						{
							bool flag5 = DLLTool.NtWow64ReadVirtualMemory64(num4, num3 + 16UL, out list_ENTRY, 16UL, out num2) < 0;
							if (flag5)
							{
								DLLTool.CloseHandle(num4);
								result = new ProcessModule64[0];
							}
							else
							{
								bool flag6 = DLLTool.NtWow64ReadVirtualMemory64(num4, list_ENTRY.Flink, out ldr_DATA_TABLE_ENTRY, (ulong)((long)Marshal.SizeOf(ldr_DATA_TABLE_ENTRY)), out num2) < 0;
								if (flag6)
								{
									DLLTool.CloseHandle(num4);
									result = new ProcessModule64[0];
								}
								else
								{
									while (ldr_DATA_TABLE_ENTRY.InLoadOrderLinks.Flink != list_ENTRY.Flink)
									{
										try
										{
											IntPtr intPtr = Marshal.AllocHGlobal((int)ldr_DATA_TABLE_ENTRY.FullDllName.Length);
											bool flag7 = DLLTool.NtWow64ReadVirtualMemory64(num4, ldr_DATA_TABLE_ENTRY.FullDllName.Buffer, (int*)((void*)intPtr), (ulong)ldr_DATA_TABLE_ENTRY.FullDllName.Length, out num2) >= 0;
											if (flag7)
											{
												ProcessModule64 item = new ProcessModule64(ldr_DATA_TABLE_ENTRY, Marshal.PtrToStringUni(intPtr, (int)(ldr_DATA_TABLE_ENTRY.FullDllName.Length / 2)));
												Marshal.FreeHGlobal(intPtr);
												list.Add(item);
											}
										}
										catch
										{
										}
										bool flag8 = DLLTool.NtWow64ReadVirtualMemory64(num4, ldr_DATA_TABLE_ENTRY.InLoadOrderLinks.Flink, out ldr_DATA_TABLE_ENTRY, (ulong)((long)Marshal.SizeOf(ldr_DATA_TABLE_ENTRY)), out num2) < 0;
										if (flag8)
										{
											break;
										}
									}
									DLLTool.CloseHandle(num4);
									result = list.ToArray();
								}
							}
						}
					}
				}
			}
			return result;
		}
	}
}
