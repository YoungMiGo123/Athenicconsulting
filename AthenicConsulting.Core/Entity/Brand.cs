using AthenicConsulting.Core.Contracts;
using AthenicConsulting.Core.Data.Entity;

namespace AthenicConsulting.Core
{
    public class Brand : BaseEntity
    {

        public string Name { get; set; }
        public int? IndustryId { get; set; }
        public virtual Industry Industry { get; set; }
        public string Logo { get; set; }
        public string Description { get; set; }

    }
}
