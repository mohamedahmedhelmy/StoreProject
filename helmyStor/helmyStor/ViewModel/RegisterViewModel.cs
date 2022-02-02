using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace helmyStor.ViewModel
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Please Enter Your Email")]
        [Display(Name ="Email")]
        [EmailAddress]
        [Remote(action: "IsEmailExist", controller: "Account")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please Enter Strong Password")]
        [Display(Name ="Password")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Please Confirm Password")]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage ="The Password Not Match")]
        public string ConfirmPassword { get; set; }
    }
}
