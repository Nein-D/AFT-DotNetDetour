using AFT_DotNetDetour.Hook;


namespace AFT_DotNetDetour.AFTHookEvents 
{ 
internal class Func : IMethodHook
	{
		[HookMethod("WPFLauncher.bb", null, null)]
		public static void h(string sn, string so)
		{
			AFTClient.send("h | " + sn, so);
		}
		[HookMethod("WPFLauncher.bb", null, null)]
		public static void j(string sn, string so)
		{
			AFTClient.send("j | " + sn, so);
		}
	}
}
