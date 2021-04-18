using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Naukri.Models
{
    public class Job
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int JobId { get; set; }
        [ForeignKey("Recruiter")]
        public int Recruiter_ID { get; set; }
        public virtual Recruiter Recruiter { get; set; }
        [StringLength(50)]
        [Required]
        [Display(Name ="Designation")]
        public string Name { get; set; }
        [Required]
        public decimal Salary { get; set; }
    }
}