using System;
using Cysharp.Threading.Tasks;
using DevKit.Solutions;
using DevKit.UI.MVVM;
using Reflex.Core;
using SparksOfTheAbyss.Campaign;
using SparksOfTheAbyss.Data.Campaign;
using SparksOfTheAbyss.Shared.Localization;
using SparksOfTheAbyss.UI.MVVM;
using UnityEngine;
using Lifetime = Reflex.Enums.Lifetime;
using Resolution = Reflex.Enums.Resolution;

namespace SparksOfTheAbyss.Infrastructure.Installers
{
    public class CampaignInstaller : ModuleInstaller
    {
        [SerializeField] private CampaignView _campaignViewPrefab;
        [SerializeField] private SerializableLocalizationLoader _campaignLocalizationLoader;

        public override void Install(ContainerBuilder builder)
        {
            builder.RegisterType(typeof(LinearCampaignUnlocker), Lifetime.Singleton, Resolution.Lazy);

            builder.RegisterType(typeof(CampaignModelFactory),
                new Type[] { typeof(CampaignModelFactory), typeof(IFactory<CampaignModel>) },
                Lifetime.Singleton, Resolution.Lazy);

            builder.RegisterType(typeof(CampaignDataModelFactory),
                new Type[] { typeof(CampaignDataModelFactory), typeof(IFactory<CampaignDataModel>) },
                Lifetime.Singleton, Resolution.Lazy);

            builder.RegisterType(typeof(CampaignDataLoader), Lifetime.Transient, Resolution.Lazy);

            builder.RegisterMVVM<CampaignView, CampaignViewModel, CampaignBinder>(_campaignViewPrefab,
                viewModelLifetime: Lifetime.Transient);

            builder.RegisterValue(new SettingsLocalizationLoaderKey(_campaignLocalizationLoader));
        }

        public override UniTask OnInitialize()
        {
            // var campaignBinder = Container.Resolve<IViewBinder<CampaignViewModel>>();
            // campaignBinder.Open();

            return UniTask.CompletedTask;
        }
    }
}