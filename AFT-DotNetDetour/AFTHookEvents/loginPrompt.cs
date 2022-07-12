using System;
using System.IO;
using DotNetDetour;
using WPFLauncher;
using WPFLauncher.Util;
using WPFLauncher.View.UI;
using System.Windows.Forms;
using AFT_DotNetDetour.Hook;

namespace AFT_DotNetDetour.AFTHookEvents
{
    public class Demo : IMethodHook
    {
        [HookMethod("WPFLauncher.Manager.Login.ano")]
        private bool e()
        {
            AFTClient.send("[AFT]Injection successful");
            
            CustomLoadingWindow.f(delegate{rd.n("{ AFT }:载入成功-盒子版本:1.8.19");}, "加载中...");
            AFTClient.send("AFT Loading succeeded");
            return e_Original();
        }
        [OriginalMethod]
        private bool e_Original() => false;

    }
}
