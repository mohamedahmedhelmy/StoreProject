using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domains
{
    public partial class Customer
    {
        [Key]
        public int CustomerId { get; set; }
        [Required]
        [Display(Name = "Customer Name ")]
        public string CustomerName { get; set; }
        [Required]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Address")]
        public string Address { get; set; }

        public virtual ICollection<Product> Products{ get; set; }
    }
}
