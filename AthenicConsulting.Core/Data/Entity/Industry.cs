using AthenicConsulting.Core.Contracts;

namespace AthenicConsulting.Core.Data.Entity
{
    public class Industry : BaseEntity
    {
        public string Name { get; set; }
        public IndustryType Type { get; set; }
    }
}
