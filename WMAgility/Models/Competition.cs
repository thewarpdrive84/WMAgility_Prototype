using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WMAgility.Models
{        public enum Faults
        { KnockedBar, Refusal, Weave, Contact, WrongCourse, Eliminated, NA }

        public enum Placement
        { First, Second, Third, Fourth, Fifth, Sixth, Seventh, Eighth, Ninth, Tenth, NA }

        public enum Surface
        { Grass, Sand, Dirt, Artificial, Rubber, Other }

        public class Competition
        {
            [Key]
            public int CompID { get; set; }

            [Required(ErrorMessage = "The Competition Name field is required")]
            [Display(Name = "Competition Name:")]
            public string CompName { get; set; }

            [Required(ErrorMessage = "The Location field is required")]
            [Display(Name = "Location:")]
            public string Location { get; set; }

            [DataType(DataType.Date)]
            [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
            public DateTime Date { get; set; }

            [Display(Name = "Course Length:")]
            public string Length { get; set; }

            [Display(Name = "Faults:")]
            public Faults Faults { get; set; }

            [Display(Name = "Placement:")]
            public Placement Placement { get; set; }

            [Display(Name = "Surface:")]
            public Surface Surface { get; set; }

            [Display(Name = "Notes:")]
            public string Notes { get; set; }

        public int CanineId { get; set; }
        [ForeignKey("CanineId")]
        public virtual Dog Dog { get; set; }


        [NotMapped]
            public virtual ICollection<Competition> Competitions { get; set; }
        }
}
