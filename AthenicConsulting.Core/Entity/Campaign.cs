using AthenicConsulting.Core.Contracts;
using System.ComponentModel.DataAnnotations.Schema;

namespace AthenicConsulting.Core
{
    public class Campaign : BaseEntity
    {

        public string Name { get; set; }
        public CampaignType CampaignType { get; set; }
        public string ImgFolderPath { get; set; }
        [ForeignKey("BrandId")]
        public int BrandId { get; set; }
        [ForeignKey("BrandId")]
        public virtual Brand Brand { get; set; }

    }
}
