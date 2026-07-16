using DevKit.Solutions;
using R3;

namespace ITCafe.Data.Campaign
{
    public class CampaignModelFactory : IFactory<CampaignModel>
    {
        public ReadOnlyReactiveProperty<CampaignModel> Current => _current;

        private readonly ReactiveProperty<CampaignModel> _current = new();
        private readonly CampaignState _campaignState;

        public CampaignModelFactory(CampaignState campaignState)
        {
            _campaignState = campaignState;
        }
        
        public CampaignModel Create()
        {
            var campaignModel = new CampaignModel(_campaignState);
            _current.Value = campaignModel;
            
            return campaignModel;
        }
    }
}