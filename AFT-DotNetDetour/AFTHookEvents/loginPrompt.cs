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
        [HookMethod("WPFLauncher.Manager.Login.anh")]
        private bool e()
        {
            AFTClient.send("[AFT]Injection successful");
            
            CustomLoadingWindow.f(delegate{qw.n("{ AFT }:注入成功！");}, "加载中...");
            AFTClient.send("AFT Loading succeeded");
            return e_Original();
        }
        [OriginalMethod]
        private bool e_Original() => false;

    }
}
