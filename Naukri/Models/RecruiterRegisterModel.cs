using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Naukri.Models
{
    public class RecruiterRegisterModel
    {
        [StringLength(30)]
        [MinLength(4)]
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [StringLength(30)]
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }
        [StringLength(50)]
        [Required]
        public string Company { get; set; }
        [Required]
        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Enter 10 Digit Mobile Number")]

        public long PhoneNumber { get; set; }
        [Required]
        [RegularExpression("^(?=.*?[A-Za-z])(?=.*?[0-9]).{6,}$", ErrorMessage = "Password must contains numbers and alphabets")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Password Not Matched")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
    }
}