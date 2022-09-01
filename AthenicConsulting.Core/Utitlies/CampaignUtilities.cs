namespace AthenicConsulting.Core.Utitlies
{
    public static class CampaignUtilities
    {
        public static CampaignType GetCampaignType(string campaignType)
        {
            if (string.IsNullOrEmpty(campaignType)) return CampaignType.Other;
            var lowerCaseCampaignType = campaignType.Replace(" ", "").ToLower();
            if (lowerCaseCampaignType.Contains($"{CampaignType.Offer}".ToLower()))
            {
                return CampaignType.Offer;
            }
            if (lowerCaseCampaignType.Contains($"{CampaignType.Holidays}".ToLower()))
            {
                return CampaignType.Holidays;
            }
            if (lowerCaseCampaignType.Contains($"{CampaignType.Unengaged}".ToLower()))
            {
                return CampaignType.Unengaged;
            }
            if (lowerCaseCampaignType.Contains($"{CampaignType.PostPurchase}".ToLower()))
            {
                return CampaignType.PostPurchase;
            }
            if (lowerCaseCampaignType.Contains($"{CampaignType.Welcome}".ToLower()))
            {
                return CampaignType.Welcome;
            }
            if (lowerCaseCampaignType.Contains($"{CampaignType.CustomerWinBack}".ToLower()))
            {
                return CampaignType.CustomerWinBack;
            }
            if (lowerCaseCampaignType.Contains($"{CampaignType.ProductLaunch}".ToLower()))
            {
                return CampaignType.ProductLaunch;
            }
            if (lowerCaseCampaignType.Contains($"{CampaignType.Abandoned}".ToLower()))
            {
                return CampaignType.Abandoned;
            }
            return CampaignType.BackInStock;
        }
    }
}
