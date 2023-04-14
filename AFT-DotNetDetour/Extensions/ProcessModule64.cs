using System;
using System.IO;

namespace DotNetDetour.Extensions
{
	// Token: 0x02000018 RID: 24
	public class ProcessModule64
	{
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00003636 File Offset: 0x00001836
		public string ModuleName { get; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600005B RID: 91 RVA: 0x0000363E File Offset: 0x0000183E
		public string FileName { get; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00003646 File Offset: 0x00001846
		public long BaseAddress { get; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600005D RID: 93 RVA: 0x0000364E File Offset: 0x0000184E
		public long ModuleMemorySize { get; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00003656 File Offset: 0x00001856
		public long EntryPointAddress { get; }

		// Token: 0x0600005F RID: 95 RVA: 0x00003660 File Offset: 0x00001860
		public ProcessModule64(LDR_DATA_TABLE_ENTRY64 dllInfo, string dllpath)
		{
			try
			{
				this.FileName = dllpath;
				this.BaseAddress = (long)dllInfo.DllBase;
				this.ModuleMemorySize = (long)(ulong)dllInfo.SizeOfImage;
				this.EntryPointAddress = (long)dllInfo.EntryPoint;
				this.ModuleName = Path.GetFileName(dllpath);
			}
			catch
			{
			}
		}
	}
}
