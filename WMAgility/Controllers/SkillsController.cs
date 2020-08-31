using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WMAgility.Data;
using WMAgility.Models;
using WMAgility.Models.ViewModels;

namespace WMAgility.Controllers
{
    public class SkillsController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public SkillsController(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }


        public IActionResult Index()
        {
            IEnumerable<Skill> objList = _db.Skills;

            foreach (var obj in objList)
            {
                obj.Dog = _db.Dogs.FirstOrDefault(u => u.Id == obj.CanineId);
            };
            return View(objList);
        }

        //GET - UpSkill
        public IActionResult UpSkill(int? id)
        {

            SkillViewModel skillViewModel = new SkillViewModel()
            {
                Skill = new Skill(),
                DogSelectList = _db.Dogs.Select(i => new SelectListItem
                {
                    Text = i.DogName,
                    Value = i.Id.ToString()
                })
            };

            if (id == null)
            {
                //for create new skill
                return View(skillViewModel);
            }

            else
            {
                skillViewModel.Skill = _db.Skills.Find(id);
                if (skillViewModel.Skill == null)
                {
                    return NotFound();
                }
                return View(skillViewModel);
            }
        }

        //POST - UpDog
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpSkill(SkillViewModel skillViewModel)
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                string webRootPath = _webHostEnvironment.WebRootPath;

                if (skillViewModel.Skill.Id == 0)
                {
                    //creating
                    string upload = webRootPath + WebConstants.ImagePath;
                    string fileName = Guid.NewGuid().ToString();
                    string extension = Path.GetExtension(files[0].FileName);

                    using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }

                    skillViewModel.Skill.Image = fileName + extension;

                    _db.Skills.Add(skillViewModel.Skill);
                }

                else
                {
                    //updating
                    var objFromDb = _db.Skills.AsNoTracking().FirstOrDefault(u => u.Id == skillViewModel.Skill.Id);

                    if (files.Count > 0)
                    {
                        string upload = webRootPath + WebConstants.ImagePath;
                        string fileName = Guid.NewGuid().ToString();
                        string extension = Path.GetExtension(files[0].FileName);

                        var oldFile = Path.Combine(upload, objFromDb.Image);

                        if (System.IO.File.Exists(oldFile))
                        {
                            System.IO.File.Delete(oldFile);
                        }

                        using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                        {
                            files[0].CopyTo(fileStream);
                        }

                        skillViewModel.Skill.Image = fileName + extension;
                    }
                    else
                    {
                        skillViewModel.Skill.Image = objFromDb.Image;
                    }
                    _db.Skills.Update(skillViewModel.Skill);
                }

                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            skillViewModel.DogSelectList = _db.Dogs.Select(i => new SelectListItem
            {
                Text = i.DogName,
                Value = i.Id.ToString()
            });
            return View(skillViewModel);
        }

        //GET - EDIT
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var obj = _db.Skills.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        //POST - EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Skill obj)
        {
            if (ModelState.IsValid)
            {
                _db.Skills.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //GET - DELETE
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var obj = _db.Skills.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        //POST - DELETE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int? id)
        {
            var obj = _db.Skills.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            _db.Skills.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}
