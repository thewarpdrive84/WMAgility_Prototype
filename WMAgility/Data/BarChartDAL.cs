using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WMAgility.Models;

namespace WMAgility.Data
{
    public class BarChartDAL
    {
        //NOT CURRENTLY IN USE
        public static List<BarChart> GetSkillChartList()
        {
            var list = new List<BarChart>();
            list.Add(new BarChart
            {
                SkillName = "SeeSaw",
                Day1 = 1,
                Day2 = 2,
                Day3 = 3,
                Day4 = 4,
                Day5 = 5,
                Day6 = 6,
                Day7 = 7
            });

            return list;
        }
    }
}
