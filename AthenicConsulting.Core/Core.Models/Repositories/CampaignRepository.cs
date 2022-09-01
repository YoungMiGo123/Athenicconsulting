using AthenicConsulting.Core.Core.Interfaces;
using AthenicConsulting.Core.Data.Contexts;

namespace AthenicConsulting.Core.Core.Models.Repositories
{
    public class CampaignRepository : GenericRepository<Campaign>, ICampaignRepository
    {
        public CampaignRepository(AthenicConsultingContext context) : base(context)
        {

        }
    }
}
