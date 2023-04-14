using System.IO;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using WPFLauncher.Manager;

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace AFTDotNetDetour.AFTDotNetDetourEP
{
	//processmodules

	internal class ProcessModules
	{
		// Token: 0x0600006D RID: 109 RVA: 0x00003AF4 File Offset: 0x00001CF4
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

	//processmodule64

	// Token: 0x0200001B RID: 27
	public class ProcessModule64
	{
		// Token: 0x06000067 RID: 103 RVA: 0x00003A64 File Offset: 0x00001C64
		public ProcessModule64(LDR_DATA_TABLE_ENTRY64 dllInfo, string dllpath)
		{
			try
			{
				this.FileName = dllpath;
				this.BaseAddress = (long)dllInfo.DllBase;
				this.ModuleMemorySize = (long)dllInfo.SizeOfImage;
				this.EntryPointAddress = (long)dllInfo.EntryPoint;
				this.ModuleName = Path.GetFileName(dllpath);
			}
			catch
			{
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000068 RID: 104 RVA: 0x00003ACC File Offset: 0x00001CCC
		public string ModuleName { get; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000069 RID: 105 RVA: 0x00003AD4 File Offset: 0x00001CD4
		public string FileName { get; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600006A RID: 106 RVA: 0x00003ADC File Offset: 0x00001CDC
		public long BaseAddress { get; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600006B RID: 107 RVA: 0x00003AE4 File Offset: 0x00001CE4
		public long ModuleMemorySize { get; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600006C RID: 108 RVA: 0x00003AEC File Offset: 0x00001CEC
		public long EntryPointAddress { get; }
	}

	//processextensions

	public static class ProcessExtensions
	{
		// Token: 0x06000058 RID: 88
		[DllImport("kernel32.dll")]
		public static extern int OpenProcess(int a_, int Handle, int dwProcessId);

		// Token: 0x06000059 RID: 89
		[DllImport("kernel32.dll")]
		private static extern bool CloseHandle(int hObject);

		// Token: 0x0600005A RID: 90
		[DllImport("ntdll.dll")]
		private static extern int ZwWow64WriteVirtualMemory64(int hProcess, ulong pMemAddress, byte[] Buffer, ulong nSize, out ulong nReturnSize);

		// Token: 0x0600005B RID: 91
		[DllImport("ntdll.dll")]
		private static extern int ZwWow64ReadVirtualMemory64(int hProcess, ulong pMemAddress, byte[] Buffer, ulong nSize, out ulong nReturnSize);

		// Token: 0x0600005C RID: 92 RVA: 0x000039E8 File Offset: 0x00001BE8
		public static int writeBytes(this alt process, ulong address, byte[] buffer, ulong size)
		{
			int num = ProcessExtensions.OpenProcess(2035711, 0, process.ProcessId);
			ulong num2;
			int result = ProcessExtensions.ZwWow64WriteVirtualMemory64(num, address, buffer, size, out num2);
			ProcessExtensions.CloseHandle(num);
			return result;
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00003A20 File Offset: 0x00001C20
		public static byte[] readBytes(this alt process, ulong address, ulong size)
		{
			byte[] array = new byte[size];
			int num = ProcessExtensions.OpenProcess(2035711, 0, process.ProcessId);
			ulong num3;
			int num2 = ProcessExtensions.ZwWow64ReadVirtualMemory64(num, address, array, size, out num3);
			ProcessExtensions.CloseHandle(num);
			return array;
		}
	}

	//dlltook

	public static class DLLTool
	{
		// Token: 0x0600005E RID: 94
		[DllImport("kernel32.dll")]
		public static extern int OpenProcess(uint dwDesiredAccess, bool bInheritHandle, uint dwProcessId);

		// Token: 0x0600005F RID: 95
		[DllImport("kernel32.dll")]
		public static extern bool IsWow64Process(int hProcess, out bool Wow64Process);

		// Token: 0x06000060 RID: 96
		[DllImport("ntdll.dll")]
		public static extern int NtWow64QueryInformationProcess64(int hProcess, uint ProcessInfoclass, out PROCESS_BASIC_INFORMATION64 pBuffer, uint nSize, out uint nReturnSize);

		// Token: 0x06000061 RID: 97
		[DllImport("kernel32.dll")]
		public static extern bool CloseHandle(int hObject);

		// Token: 0x06000062 RID: 98
		[DllImport("ntdll.dll")]
		public unsafe static extern int NtWow64ReadVirtualMemory64(int hProcess, ulong pMemAddress, int* pBufferPtr, ulong nSize, out ulong nReturnSize);

		// Token: 0x06000063 RID: 99
		[DllImport("ntdll.dll")]
		public static extern int NtWow64ReadVirtualMemory64(int hProcess, ulong pMemAddress, out LDR_DATA_TABLE_ENTRY64 pBufferPtr, ulong nSize, out ulong nReturnSize);

		// Token: 0x06000064 RID: 100
		[DllImport("ntdll.dll")]
		public static extern int NtWow64ReadVirtualMemory64(int hProcess, ulong pMemAddress, out ulong pBufferPtr, ulong nSize, out ulong nReturnSize);

		// Token: 0x06000065 RID: 101
		[DllImport("ntdll.dll")]
		public static extern int NtWow64ReadVirtualMemory64(int hProcess, ulong pMemAddress, out LIST_ENTRY64 pBufferPtr, ulong nSize, out ulong nReturnSize);

		// Token: 0x06000066 RID: 102
		[DllImport("kernel32.dll", EntryPoint = "RtlMoveMemory")]
		public static extern int BinToLDR_DATA_TABLE_ENTRY64(LDR_DATA_TABLE_ENTRY64 lpDestination, byte[] lpSource, int Length);
	}

	//LDR_DATA_TABLE_ENTRY64

	public struct LDR_DATA_TABLE_ENTRY64
	{
		// Token: 0x04000033 RID: 51
		public LIST_ENTRY64 InLoadOrderLinks;

		// Token: 0x04000034 RID: 52
		public LIST_ENTRY64 InMemoryOrderLinks;

		// Token: 0x04000035 RID: 53
		public LIST_ENTRY64 InInitializationOrderLinks;

		// Token: 0x04000036 RID: 54
		public ulong DllBase;

		// Token: 0x04000037 RID: 55
		public ulong EntryPoint;

		// Token: 0x04000038 RID: 56
		public uint SizeOfImage;

		// Token: 0x04000039 RID: 57
		public UNICODE_STRING64 FullDllName;

		// Token: 0x0400003A RID: 58
		public UNICODE_STRING64 BaseDllName;

		// Token: 0x0400003B RID: 59
		public uint Flags;

		// Token: 0x0400003C RID: 60
		public ushort LoadCount;

		// Token: 0x0400003D RID: 61
		public ushort TlsIndex;

		// Token: 0x0400003E RID: 62
		public LIST_ENTRY64 HashLinks;

		// Token: 0x0400003F RID: 63
		public uint CheckSum;

		// Token: 0x04000040 RID: 64
		public ulong LoadedImports;

		// Token: 0x04000041 RID: 65
		public ulong EntryPointActivationContext;

		// Token: 0x04000042 RID: 66
		public ulong PatchInformation;
	}

	//LIST_ENTRY64

	public struct LIST_ENTRY64
	{
		// Token: 0x04000031 RID: 49
		public ulong Flink;

		// Token: 0x04000032 RID: 50
		public ulong Blink;
	}

	//PROCESS_BASIC_INFORMATION64

	public struct PROCESS_BASIC_INFORMATION64
	{
		// Token: 0x04000043 RID: 67
		public uint NTSTATUS;

		// Token: 0x04000044 RID: 68
		public uint Reserved0;

		// Token: 0x04000045 RID: 69
		public ulong PebBaseAddress;

		// Token: 0x04000046 RID: 70
		public ulong AffinityMask;

		// Token: 0x04000047 RID: 71
		public uint BasePriority;

		// Token: 0x04000048 RID: 72
		public uint Reserved1;

		// Token: 0x04000049 RID: 73
		public ulong UniqueProcessId;

		// Token: 0x0400004A RID: 74
		public ulong InheritedFromUniqueProcessId;
	}

	//UNICODE_STRING64

	public struct UNICODE_STRING64
	{
		// Token: 0x0400004B RID: 75
		public ushort Length;

		// Token: 0x0400004C RID: 76
		public ushort MaximumLength;

		// Token: 0x0400004D RID: 77
		public ulong Buffer;
	}




}
