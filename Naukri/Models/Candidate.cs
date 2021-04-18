using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Naukri.Models
{
    public class Candidate
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Email Address")]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [StringLength(30)]
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [StringLength(30)]
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = "Date Of Birth")]
        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }
        [StringLength(50)]
        [Required]
        [Display(Name = "Where I Study")]
        public string  College { get; set; }
        [Required]
        [Display(Name = "Notice Period")]
        public int NoticePeriod { get; set; }
        [Required]
        [Display(Name = "Contact Number")]
        public long PhoneNumber { get; set; }
    }
}