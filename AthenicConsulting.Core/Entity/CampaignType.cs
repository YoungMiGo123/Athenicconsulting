using System.ComponentModel.DataAnnotations;

namespace AthenicConsulting.Core
{
    public enum CampaignType
    {
        [Display(Name = "Abandoned")]
        Abandoned = 0,
        [Display(Name = "Post Purchase")]
        PostPurchase = 1,
        [Display(Name = "Welcome Series")]
        Welcome = 2,
        [Display(Name = "Offer")]
        Offer = 3,
        [Display(Name = "Unengaged")]
        Unengaged = 4,
        [Display(Name = "Product Launch")]
        ProductLaunch = 5,
        [Display(Name = "Holidays")]
        Holidays = 6,
        [Display(Name = "Customer Win Back")]
        CustomerWinBack = 7,
        [Display(Name = "Other")]
        Other = 8,
        [Display(Name = "Back In Stock")]
        BackInStock = 9,
        [Display(Name = "Pop Up")]
        PopUp= 10,
    }
}
