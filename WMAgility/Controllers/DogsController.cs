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
    public class DogsController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public DogsController(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            IEnumerable<Dog> objList = _db.Dogs;

            return View(objList);
        }

        //GET - UpDog
        public IActionResult UpDog(int? id)                 //what's up dog?
        {

            DogViewModel dogViewModel = new DogViewModel()
            {
                Dog = new Dog()
            };
            
            if (id == null)
            {
                //for create new dog
                return View(dogViewModel);
            }

            else
            {
                dogViewModel.Dog = _db.Dogs.Find(id);
                if (dogViewModel.Dog == null)
                {
                    return NotFound();
                }
                return View(dogViewModel);
            }
        }

        //POST - UpDog
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpDog(DogViewModel dogViewModel)
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                string webRootPath = _webHostEnvironment.WebRootPath;

                if (dogViewModel.Dog.Id == 0)
                {
                    //creating
                    string upload = webRootPath + WebConstants.ImagePath;
                    string fileName = Guid.NewGuid().ToString();
                    string extension = Path.GetExtension(files[0].FileName);

                    using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }

                    dogViewModel.Dog.Image = fileName + extension;

                    _db.Dogs.Add(dogViewModel.Dog);
                }

                else
                {
                    //updating
                    var objFromDb = _db.Dogs.AsNoTracking().FirstOrDefault(u => u.Id == dogViewModel.Dog.Id);

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

                        dogViewModel.Dog.Image = fileName + extension;
                    }
                    else
                    {
                        dogViewModel.Dog.Image = objFromDb.Image;
                    }
                    _db.Dogs.Update(dogViewModel.Dog);
                }

                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            return View(dogViewModel);
        }


        //GET - DELETE
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Dog dog = _db.Dogs.FirstOrDefault(u => u.Id == id);

            if (dog == null)
            {
                return NotFound();
            }

            return View(dog);
        }

        //POST - DELETE
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int? id)
        {
            var obj = _db.Dogs.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            string upload = _webHostEnvironment.WebRootPath + WebConstants.ImagePath;

            var oldFile = Path.Combine(upload, obj.Image);

            if (System.IO.File.Exists(oldFile))
            {
                System.IO.File.Delete(oldFile);
            }

            _db.Dogs.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");

        }


    }
}
