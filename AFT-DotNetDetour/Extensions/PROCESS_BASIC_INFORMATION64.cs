using System;

namespace DotNetDetour.Extensions
{
	// Token: 0x0200001A RID: 26
	public struct PROCESS_BASIC_INFORMATION64
	{
		// Token: 0x04000046 RID: 70
		public uint NTSTATUS;

		// Token: 0x04000047 RID: 71
		public uint Reserved0;

		// Token: 0x04000048 RID: 72
		public ulong PebBaseAddress;

		// Token: 0x04000049 RID: 73
		public ulong AffinityMask;

		// Token: 0x0400004A RID: 74
		public uint BasePriority;

		// Token: 0x0400004B RID: 75
		public uint Reserved1;

		// Token: 0x0400004C RID: 76
		public ulong UniqueProcessId;

		// Token: 0x0400004D RID: 77
		public ulong InheritedFromUniqueProcessId;
	}
}
