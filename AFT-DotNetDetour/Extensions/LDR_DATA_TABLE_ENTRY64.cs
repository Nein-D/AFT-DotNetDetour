using System;

namespace DotNetDetour.Extensions
{
	// Token: 0x02000015 RID: 21
	public struct LDR_DATA_TABLE_ENTRY64
	{
		// Token: 0x0400002F RID: 47
		public LIST_ENTRY64 InLoadOrderLinks;

		// Token: 0x04000030 RID: 48
		public LIST_ENTRY64 InMemoryOrderLinks;

		// Token: 0x04000031 RID: 49
		public LIST_ENTRY64 InInitializationOrderLinks;

		// Token: 0x04000032 RID: 50
		public ulong DllBase;

		// Token: 0x04000033 RID: 51
		public ulong EntryPoint;

		// Token: 0x04000034 RID: 52
		public uint SizeOfImage;

		// Token: 0x04000035 RID: 53
		public UNICODE_STRING64 FullDllName;

		// Token: 0x04000036 RID: 54
		public UNICODE_STRING64 BaseDllName;

		// Token: 0x04000037 RID: 55
		public uint Flags;

		// Token: 0x04000038 RID: 56
		public ushort LoadCount;

		// Token: 0x04000039 RID: 57
		public ushort TlsIndex;

		// Token: 0x0400003A RID: 58
		public LIST_ENTRY64 HashLinks;

		// Token: 0x0400003B RID: 59
		public uint CheckSum;

		// Token: 0x0400003C RID: 60
		public ulong LoadedImports;

		// Token: 0x0400003D RID: 61
		public ulong EntryPointActivationContext;

		// Token: 0x0400003E RID: 62
		public ulong PatchInformation;
	}
}
