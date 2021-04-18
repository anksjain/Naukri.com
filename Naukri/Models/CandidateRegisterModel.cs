using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Naukri.Models
{
    public class CandidateRegisterModel
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
        [DateMinimumAge(18, ErrorMessage = "User should be {1} years or Above")]
        [DisplayFormat(DataFormatString = "{0:DD MM YYYY}")]
        [Display(Name = "DOB")]
        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }
        [StringLength(50)]
        [Required]
        public string College { get; set; }
        [Required(ErrorMessage = "Enter Days Between 0 t0 60")]
        public int NoticePeriod { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Enter 10 Digit Mobile Number ")]

        public long PhoneNumber { get; set; }
        [Required]
        [RegularExpression("^(?=.*?[A-Za-z])(?=.*?[0-9]).{6,}$", ErrorMessage = "Password must contains numbers and alphabets")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Password Not Matched")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
    }
    public class DateMinimumAgeAttribute : ValidationAttribute
    {
        public int MinimumAge { get; }
        public DateMinimumAgeAttribute(int minimumAge)
        {
            MinimumAge = minimumAge;
            ErrorMessage = "{0} must be someone at least {1} years of age";
        }

        public override bool IsValid(object value)
        {
            DateTime date;
            if ((value != null && DateTime.TryParse(value.ToString(), out date)))
            {
                return date.AddYears(MinimumAge) < DateTime.Now;
            }

            return false;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(ErrorMessageString, name, MinimumAge);
        }


    }
}