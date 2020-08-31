using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WMAgility.Models
{
    public class Practice
    {
        [Key]
        public int PracticeID { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Date { get; set; }

        [Display(Name = "Skill")]
        public Skill Skill { get; set; }

        [Display(Name = "Rounds")]
        public int Rounds { get; set; }

        [Display(Name = "Fails")]
        public int Fails { get; set; }

        [Display(Name = "Notes")]
        public string Notes { get; set; }

        public int CanineId { get; set; }
        [ForeignKey("CanineId")]
        public virtual Dog Dog { get; set; }

        [NotMapped]
        public virtual ICollection<Practice> Practices { get; set; }
    }
}
