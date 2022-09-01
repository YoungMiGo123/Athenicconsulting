using System.ComponentModel.DataAnnotations;

namespace AthenicConsulting.Core.Contracts
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool Deactivatated { get; set; }
        public DateTime DeactivatedDate { get; set; }
    }
}
