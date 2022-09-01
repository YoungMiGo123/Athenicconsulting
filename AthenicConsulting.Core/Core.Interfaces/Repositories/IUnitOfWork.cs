using AthenicConsulting.Core.Core.Interfaces.Repositories;

namespace AthenicConsulting.Core.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        public IBrandRepository BrandRepo { get; }
        public ICampaignRepository CampaignRepo { get; }
        public IIndustryRepository IndustryRepository { get; }
        public ILeadRepository LeadRepository { get; }
        bool SaveChanges();
    }
}
