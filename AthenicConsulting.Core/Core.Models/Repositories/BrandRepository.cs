using AthenicConsulting.Core.Core.Interfaces;
using AthenicConsulting.Core.Data.Contexts;

namespace AthenicConsulting.Core.Core.Models.Repositories
{
    public class BrandRepository : GenericRepository<Brand>, IBrandRepository
    {
        public BrandRepository(AthenicConsultingContext context) : base(context)
        {

        }
    }
}
