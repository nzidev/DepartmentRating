using System;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using DepartmentRating.Models;
using DepartmentRating.DataBase;
using DepartmentRating.Repositories;
using DepartmentRating.Models.Repositories;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace DepartmentRating.Controllers
{
    public class HomeController : Controller
    {
        private readonly AdminRepository adminRepository;
        private readonly UserRepository userRepository;
        private readonly OffenceRepository offenceRepository;
        private readonly MainRepository mainRepository;
        

        public HomeController(AdminRepository adminRepository, 
                                UserRepository userRepository, 
                                OffenceRepository offenceRepository, 
                                MainRepository mainRepository)
        {
            this.adminRepository = adminRepository;
            this.userRepository = userRepository;
            this.offenceRepository = offenceRepository;
            this.mainRepository = mainRepository;
            
            
        }
        public IActionResult Index()
        {
            var model = new ViewModel();
            model.Admins = adminRepository.GetAll();
            model.Users = userRepository.GetAll();
            model.Offences = offenceRepository.GetAll();
            model.Mains = mainRepository.GetAll();
            
            model.isAdmin = adminRepository.IsUserAdmin(User.Identity.Name);
            //model.isAdmin = adminRepository.IsUserAdmin("DPC\\n7701-00-057");
            return View(model);
        }

        

        [HttpPost]
        public IActionResult AdminsAdd(string name)
        {
            Admin model = new Admin();
            if(name != null)
            {
                model.Name = name;
                model.Owner = User.Identity.Name;
                model.Date = DateTime.Now;
                adminRepository.SaveAdmin(model);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }



        [HttpPost]
        public IActionResult AdminDelete(int id)
        {
            adminRepository.Delete(new Admin { AdminId = id});
            return RedirectToAction("Index");

        }

        [HttpPost]
        public IActionResult OffenceDelete(int id)
        {
            offenceRepository.Delete(new Offence { OffenceId = id });
            return RedirectToAction("Index");

        }

        [HttpPost]
        public IActionResult OffenceAdd(string cost, string name)
        {
            Offence model = new Offence();
            if (cost.All(char.IsDigit) && name != "")
            {
                model.Name = name;
                model.Cost = Int32.Parse(cost);
                model.Owner = User.Identity.Name;
                model.CreateDate = DateTime.Now;
                offenceRepository.SaveOffence(model);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult ResetMain()
        {
            userRepository.ResetEveryoneRating();
            mainRepository.DeleteEverything();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult MainAdd(string dropDownUsers, string dropDownOffences, string description)
        {
            Main model = new Main();
            if (dropDownUsers != null && dropDownOffences != null)
            {
                model.UserId = Int32.Parse(dropDownUsers);
                model.OffenceId = Int32.Parse(dropDownOffences);
                model.Description = description;
                model.Owner = User.Identity.Name; 
                model.Date = DateTime.Now;

                
                userRepository.ChangeRating(userRepository.GetById(model.UserId), offenceRepository.GetById(model.OffenceId));
                mainRepository.SaveMain(model);


                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Excel()
        {
            var stream = new MemoryStream();
            //using (var package = new ExcelPackage(stream))
            //{
            //    var workSheet = package.Workbook.Worksheets.Add("Sheet1");
            //    workSheet.Cells.LoadFromCollection(list, true);
            //    package.Save();
            //}

            return RedirectToAction("Index");
        }

            public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }



        public IActionResult AdminsAdd()
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
