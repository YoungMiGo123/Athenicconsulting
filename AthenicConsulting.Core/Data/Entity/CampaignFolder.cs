namespace AthenicConsulting.Core.Data.Entity
{
    public class CampaignFolder
    {
        public string? BrandName { get; set; }
        public int BrandId { get; set; }
        public CampaignType CampaignType { get; set; }
        public int CampaignId { get; set; }
        public List<string>? Images { get;set; }
    }
}
