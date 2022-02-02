using System.ComponentModel.DataAnnotations;

namespace helmyStor.ViewModel
{
    public class RoleViewModel
    {
        [Required(ErrorMessage ="Please Enter Role Name")]
        [Display(Name = "Role Name")]
        public string RoleName { get; set; }

    }
}
