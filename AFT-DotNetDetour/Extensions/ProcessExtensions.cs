using System;
using System.Runtime.InteropServices;
using WPFLauncher.Manager;

namespace DotNetDetour.Extensions
{
	// Token: 0x02000017 RID: 23
	public static class ProcessExtensions
	{
		// Token: 0x06000054 RID: 84
		[DllImport("kernel32.dll")]
		public static extern int OpenProcess(int a_, int Handle, int dwProcessId);

		// Token: 0x06000055 RID: 85
		[DllImport("kernel32.dll")]
		private static extern bool CloseHandle(int hObject);

		// Token: 0x06000056 RID: 86
		[DllImport("ntdll.dll")]
		private static extern int ZwWow64WriteVirtualMemory64(int hProcess, ulong pMemAddress, byte[] Buffer, ulong nSize, out ulong nReturnSize);

		// Token: 0x06000057 RID: 87
		[DllImport("ntdll.dll")]
		private static extern int ZwWow64ReadVirtualMemory64(int hProcess, ulong pMemAddress, byte[] Buffer, ulong nSize, out ulong nReturnSize);

		// Token: 0x06000058 RID: 88 RVA: 0x000035BC File Offset: 0x000017BC
		public static int writeBytes(this alt process, ulong address, byte[] buffer, ulong size)
		{
			int num = ProcessExtensions.OpenProcess(2035711, 0, process.ProcessId);
			ulong num2;
			int result = ProcessExtensions.ZwWow64WriteVirtualMemory64(num, address, buffer, size, out num2);
			ProcessExtensions.CloseHandle(num);
			return result;
		}

		// Token: 0x06000059 RID: 89 RVA: 0x000035F4 File Offset: 0x000017F4
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
}
