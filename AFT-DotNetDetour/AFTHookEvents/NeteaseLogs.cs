using AFT_DotNetDetour.Hook;


namespace AFT_DotNetDetour.AFTHookEvents 
{
	internal class Func : IMethodHook
	{
		[HookMethod("WPFLauncher.bc", null, null)]
		public static void h(string ti, string tj)
		{
			AFTClient.send("h | " + ti, tj);
		}
		[HookMethod("WPFLauncher.bc", null, null)]
		public static void j(string tm, string tn)
		{
			AFTClient.send("j | " + tm, tn);
		}
		[HookMethod("WPFLauncher.bc", null, null)]
		public static void i(string tk, string tl)
        {
        }

	}
}
