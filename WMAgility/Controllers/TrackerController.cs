using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WMAgility.Data;
using WMAgility.Models;
using WMAgility.Utility;

namespace WMAgility.Controllers
{
    public class TrackerController : Controller
    {
        private readonly ApplicationDbContext _db;

        public TrackerController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<Tracker> trackerList = new List<Tracker>();
            if (HttpContext.Session.Get<IEnumerable<Tracker>>(WebConstants.SessionTrack) != null
                && HttpContext.Session.Get<IEnumerable<Tracker>>(WebConstants.SessionTrack).Count() > 0)
            {
                //session exists
                trackerList = HttpContext.Session.Get<List<Tracker>>(WebConstants.SessionTrack);
            }
            List<int> skillTracked = trackerList.Select(i => i.SkillId).ToList();
            IEnumerable<Skill> skillList = _db.Skills.Where(u => skillTracked.Contains(u.Id));

            return View(skillList);
        }
    }
}
