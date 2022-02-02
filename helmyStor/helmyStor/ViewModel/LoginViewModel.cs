using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace helmyStor.ViewModel
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="Please Enter Your Email")]
        [Display(Name ="Email Address")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage ="PLease Enter Your Password")]
        [DataType(DataType.Password)]
        [Display(Name ="Password")]
        public string Password { get; set; }
        [Display(Name ="Remember Me ?")]
        public bool RememberMe { get; set; } 
    }
}
