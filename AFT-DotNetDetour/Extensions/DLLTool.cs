using System;
using System.Runtime.InteropServices;

namespace DotNetDetour.Extensions
{
	// Token: 0x02000014 RID: 20
	public static class DLLTool
	{
		// Token: 0x0600004B RID: 75
		[DllImport("kernel32.dll")]
		public static extern int OpenProcess(uint dwDesiredAccess, bool bInheritHandle, uint dwProcessId);

		// Token: 0x0600004C RID: 76
		[DllImport("kernel32.dll")]
		public static extern bool IsWow64Process(int hProcess, out bool Wow64Process);

		// Token: 0x0600004D RID: 77
		[DllImport("ntdll.dll")]
		public static extern int NtWow64QueryInformationProcess64(int hProcess, uint ProcessInfoclass, out PROCESS_BASIC_INFORMATION64 pBuffer, uint nSize, out uint nReturnSize);

		// Token: 0x0600004E RID: 78
		[DllImport("kernel32.dll")]
		public static extern bool CloseHandle(int hObject);

		// Token: 0x0600004F RID: 79
		[DllImport("ntdll.dll")]
		public unsafe static extern int NtWow64ReadVirtualMemory64(int hProcess, ulong pMemAddress, int* pBufferPtr, ulong nSize, out ulong nReturnSize);

		// Token: 0x06000050 RID: 80
		[DllImport("ntdll.dll")]
		public static extern int NtWow64ReadVirtualMemory64(int hProcess, ulong pMemAddress, out LDR_DATA_TABLE_ENTRY64 pBufferPtr, ulong nSize, out ulong nReturnSize);

		// Token: 0x06000051 RID: 81
		[DllImport("ntdll.dll")]
		public static extern int NtWow64ReadVirtualMemory64(int hProcess, ulong pMemAddress, out ulong pBufferPtr, ulong nSize, out ulong nReturnSize);

		// Token: 0x06000052 RID: 82
		[DllImport("ntdll.dll")]
		public static extern int NtWow64ReadVirtualMemory64(int hProcess, ulong pMemAddress, out LIST_ENTRY64 pBufferPtr, ulong nSize, out ulong nReturnSize);

		// Token: 0x06000053 RID: 83
		[DllImport("kernel32.dll", EntryPoint = "RtlMoveMemory")]
		public static extern int BinToLDR_DATA_TABLE_ENTRY64(LDR_DATA_TABLE_ENTRY64 lpDestination, byte[] lpSource, int Length);
	}
}
