using System.ComponentModel.DataAnnotations;

namespace AthenicConsulting.Core.Core.Interfaces.Repositories
{
    public enum RoleFlag
    {
        [Display(Name = "Basic")]
        Basic = 0,
        [Display(Name = "Admin")]
        Admin = 1,
        [Display(Name = "Editor")]
        Editor = 2,
        [Display(Name = "None")]
        None = 3,
    }
}
