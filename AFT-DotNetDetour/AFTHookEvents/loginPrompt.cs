using WPFLauncher.Util;
using WPFLauncher.View.UI;
using AFT_DotNetDetour.Hook;

namespace AFT_DotNetDetour.AFTHookEvents
{
    public class loginPrompt : IMethodHook
    {
        [HookMethod("WPFLauncher.Manager.Login.aod")]
        private bool e()
        {
            AFTClient.send("[AFT]Injection successful");

            CustomLoadingWindow.f(delegate { sg.n("{ AFT }: 载入成功 #1.9.8.3.19102"); }, "注入中，请稍后...");
            AFTClient.send("AFT Loading succeeded-#");
            return e_Original();
        }
        [OriginalMethod]
        private bool e_Original() => false;

    }
}
