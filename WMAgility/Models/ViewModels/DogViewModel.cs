using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WMAgility.Models.ViewModels
{
    public class DogViewModel
    {
        public Dog Dog { get; set; }

        //IEnumerable<SelectListItem> UserSelectList { get; set; }
    }
}
