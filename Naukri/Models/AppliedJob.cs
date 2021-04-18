using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Naukri.Models
{
    public class AppliedJob
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("Job")]
        public int Job_Id { get; set; }
        public virtual Job Job { get; set; }

        [ForeignKey("Candidate")]
        public int Candiate_Id { get; set; }
        public virtual Candidate Candidate { get; set; }
    }
}