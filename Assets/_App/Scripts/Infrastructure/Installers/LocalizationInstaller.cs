using Cysharp.Threading.Tasks;
using R3;
using Reflex.Core;
using JustUseMe.Shared.Localization;
using JustUseMe.Shared.SerializedRefs;
using UnityEngine;

using Lifetime = Reflex.Enums.Lifetime;
using Resolution = Reflex.Enums.Resolution;

namespace JustUseMe.Infrastructure.Installers
{
    /// <summary>
    /// For dynamic localization registrations only.
    /// </summary>
    public class LocalizationInstaller : ModuleInstaller
    {
        [SerializeReference, RealizationSelector(typeof(SerializedLocalizationRef))]
        private SerializedLocalizationRef _localizationKey;

        public override void Install(ContainerBuilder builder)
        {
            builder.RegisterValue(_localizationKey, new System.Type[] { _localizationKey.RegistrationType });
        }

        public override async UniTask OnInitialize()
        {
            var localozationLoaderKey = (SerializedLocalizationRef)Container.Resolve(_localizationKey.RegistrationType);
            var localozationLoader = localozationLoaderKey.Value;
            localozationLoader.Init();
            localozationLoader.AddTo(this);

            await localozationLoader.LoadTablesAsync();
        }
    }
}