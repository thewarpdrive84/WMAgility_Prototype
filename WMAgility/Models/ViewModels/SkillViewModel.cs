using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WMAgility.Models.ViewModels
{
    public class SkillViewModel
    {
        public Skill Skill { get; set; }
        public IEnumerable<SelectListItem> DogSelectList { get; set; }
    }
}
