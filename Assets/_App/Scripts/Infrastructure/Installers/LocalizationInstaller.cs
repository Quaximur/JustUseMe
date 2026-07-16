using Cysharp.Threading.Tasks;
using R3;
using Reflex.Core;
using SparksOfTheAbyss.Shared.Localization;
using SparksOfTheAbyss.Shared.SerializedRefs;
using UnityEngine;

using Lifetime = Reflex.Enums.Lifetime;
using Resolution = Reflex.Enums.Resolution;

namespace SparksOfTheAbyss.Infrastructure.Installers
{
    public class LocalizationInstaller : ModuleInstaller
    {
        [SerializeReference, RealizationSelector(typeof(SerializedLocalizationKey))]
        private SerializedLocalizationKey _localizationKey;

        public override void Install(ContainerBuilder builder)
        {
            builder.RegisterValue(_localizationKey, new System.Type[] { _localizationKey.RegistrationType });
        }

        public override async UniTask OnInitialize()
        {
            var localozationLoader = (ILocalizationLoader)Container.Resolve(_localizationKey.RegistrationType);
            localozationLoader.Init();
            localozationLoader.AddTo(this);

            await localozationLoader.LoadTablesAsync();
        }
    }
}