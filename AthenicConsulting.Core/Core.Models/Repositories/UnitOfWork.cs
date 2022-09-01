using AthenicConsulting.Core.Core.Interfaces;
using AthenicConsulting.Core.Core.Interfaces.Repositories;
using AthenicConsulting.Core.Data.Contexts;

namespace AthenicConsulting.Core.Core.Models
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AthenicConsultingContext _athenicConsulting;

        public UnitOfWork(AthenicConsultingContext athenicConsulting,
            IBrandRepository brandRepository, 
            ICampaignRepository campaignRepository, 
            IIndustryRepository industryRepository, 
            ILeadRepository leadRepository)
        {
            _athenicConsulting = athenicConsulting;
            BrandRepo = brandRepository;
            CampaignRepo = campaignRepository;
            IndustryRepository = industryRepository;
            LeadRepository = leadRepository;

        }

        public IBrandRepository BrandRepo { get; }

        public ICampaignRepository CampaignRepo { get; }

        public IIndustryRepository IndustryRepository { get; }
        public ILeadRepository LeadRepository { get; }



        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public bool SaveChanges()
        {
            return _athenicConsulting.SaveChanges() > 0;
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _athenicConsulting?.Dispose();
            }
        }
    }
}
