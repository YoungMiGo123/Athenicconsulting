using AthenicConsulting.Core.Contracts;

namespace AthenicConsulting.Core.Entity
{
    public class Lead : BaseEntity
    {
        public string Email { get; set; }
        public bool Subscribed { get; set; }
    }
}
