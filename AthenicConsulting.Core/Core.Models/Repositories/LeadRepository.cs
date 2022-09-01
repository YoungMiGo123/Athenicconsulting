using AthenicConsulting.Core.Core.Interfaces.Repositories;
using AthenicConsulting.Core.Data.Contexts;
using AthenicConsulting.Core.Entity;

namespace AthenicConsulting.Core.Core.Models.Repositories
{
    public class LeadRepository : GenericRepository<Lead>, ILeadRepository
    {
        public LeadRepository(AthenicConsultingContext context) : base(context)
        {
                
        }
    }
}
