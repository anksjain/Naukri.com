using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Naukri.Models
{
    public class Recruiter
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Email Address")]
        public string Email { get; set; }
        [StringLength(30)]
        [Required]
        [Display(Name = "Name")]
        public string FirstName { get; set; }

        [StringLength(30)]
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Password { get; set; }
        [StringLength(50)]
        [Required]
        [Display(Name = "Company Name")]
        public string Company { get; set; }
        [Required]
        [Display(Name = "Contact Number")]
        public long PhoneNumber { get; set; }

    }
}