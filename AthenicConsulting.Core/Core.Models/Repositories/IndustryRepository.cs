using AthenicConsulting.Core.Core.Interfaces.Repositories;
using AthenicConsulting.Core.Data.Contexts;
using AthenicConsulting.Core.Data.Entity;

namespace AthenicConsulting.Core.Core.Models.Repositories
{
    public class IndustryRepository : GenericRepository<Industry>, IIndustryRepository
    {
        public IndustryRepository(AthenicConsultingContext context) : base(context)
        {

        }
    }
}
