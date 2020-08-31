using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WMAgility.Models.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Dog> Dogs { get; set; }
        public IEnumerable<Skill> Skills { get; set; }
    }
}
