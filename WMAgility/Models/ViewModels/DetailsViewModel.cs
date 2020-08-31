using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WMAgility.Models.ViewModels
{
    public class DetailsViewModel
    {
        public DetailsViewModel()
        {
            Skill = new Skill();
        }

        public Skill Skill { get; set; }
        public bool Exists { get; set; }
    }
}
