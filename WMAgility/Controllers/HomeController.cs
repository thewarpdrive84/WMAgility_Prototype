using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WMAgility.Data;
using WMAgility.Models;
using WMAgility.Models.ViewModels;
using WMAgility.Utility;

namespace WMAgility.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }
        public IActionResult Index()
        {
            HomeViewModel homeViewModel = new HomeViewModel()
            {
                Skills = _db.Skills.Include(u => u.Dog),
                Dogs = _db.Dogs
            };
            return View(homeViewModel);
        }

        public IActionResult Details(int id)
        {
            List<Tracker> trackerList = new List<Tracker>();
            if (HttpContext.Session.Get<IEnumerable<Tracker>>(WebConstants.SessionTrack) != null
                && HttpContext.Session.Get<IEnumerable<Tracker>>(WebConstants.SessionTrack).Count() > 0)
            {
                trackerList = HttpContext.Session.Get<List<Tracker>>(WebConstants.SessionTrack);
            }

            DetailsViewModel DetailsViewModel = new DetailsViewModel()
            {
                Skill = _db.Skills.Include(u => u.Dog).Where(u => u.Id == id).FirstOrDefault(),
                Exists = false
            };

            foreach (var item in trackerList)
            {
                if (item.SkillId == id)
                {
                    DetailsViewModel.Exists = true;
                }
            }

            return View(DetailsViewModel);
        }

        [HttpPost, ActionName("Details")]
        public IActionResult DetailsPost(int id)
        {
            List<Tracker> trackerList = new List<Tracker>();
            if (HttpContext.Session.Get<IEnumerable<Tracker>>(WebConstants.SessionTrack) != null
                && HttpContext.Session.Get<IEnumerable<Tracker>>(WebConstants.SessionTrack).Count() > 0)
            {
                trackerList = HttpContext.Session.Get<List<Tracker>>(WebConstants.SessionTrack);
            }

            trackerList.Add(new Tracker { SkillId = id });
            HttpContext.Session.Set(WebConstants.SessionTrack, trackerList);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult RemoveFromTracker(int id)
        {
            List<Tracker> trackerList = new List<Tracker>();
            if (HttpContext.Session.Get<IEnumerable<Tracker>>(WebConstants.SessionTrack) != null
                && HttpContext.Session.Get<IEnumerable<Tracker>>(WebConstants.SessionTrack).Count() > 0)
            {
                trackerList = HttpContext.Session.Get<List<Tracker>>(WebConstants.SessionTrack);
            }

            var itemToRemove = trackerList.SingleOrDefault(r => r.SkillId == id);
            if (itemToRemove != null)
            {
                trackerList.Remove(itemToRemove);
            }

            HttpContext.Session.Set(WebConstants.SessionTrack, trackerList);
            return RedirectToAction(nameof(Index));

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
