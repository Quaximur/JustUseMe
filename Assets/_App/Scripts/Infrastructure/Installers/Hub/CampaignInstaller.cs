using System;
using Cysharp.Threading.Tasks;
using DevKit.Solutions;
using Reflex.Core;
using SparksOfTheAbyss.Campaign;
using SparksOfTheAbyss.Data.Campaign;

using Lifetime = Reflex.Enums.Lifetime;
using Resolution = Reflex.Enums.Resolution;

namespace SparksOfTheAbyss.Infrastructure.Installers
{
    /// <summary>
    /// Dependencies: <see cref="CampaignLocalizationKey"/>
    /// </summary>
    public class CampaignInstaller : ModuleInstaller
    {
        public override void Install(ContainerBuilder builder)
        {
            builder.RegisterType(typeof(LinearCampaignUnlocker), Lifetime.Singleton, Resolution.Lazy);

            builder.RegisterType(typeof(CampaignModelFactory),
                new Type[] { typeof(CampaignModelFactory), typeof(IFactory<CampaignModel>) },
                Lifetime.Singleton, Resolution.Lazy); 

            builder.RegisterType(typeof(CampaignDataModelFactory),
                new Type[] { typeof(CampaignDataModelFactory), typeof(IFactory<CampaignDataModelFactory>) },
                Lifetime.Singleton, Resolution.Lazy);

            builder.RegisterType(typeof(CampaignDataLoader), Lifetime.Transient, Resolution.Lazy);
        }

        public override UniTask OnInitialize()
        {
            return UniTask.CompletedTask;
        }
    }
}