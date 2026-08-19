using DevKit.UI.MVVM;
using Reflex.Core;
using Cysharp.Threading.Tasks;
using DevKit.Utils;

using Lifetime = Reflex.Enums.Lifetime;
using Resolution = Reflex.Enums.Resolution;

namespace JustUseMe.Infrastructure.Installers
{
    public class RootUICleanupInstaller : ModuleInstaller
    {
        public override void Install(ContainerBuilder builder)
        {
        }

        public override UniTask OnInitialize()
        {
            var rootUIBinder = Container.Resolve<IRootUIBinder>();
            rootUIBinder.ClearViews();

            FLogger.Log<RootUICleanupInstaller>("Root UI cleaned");

            return UniTask.CompletedTask;
        }
    }
}
