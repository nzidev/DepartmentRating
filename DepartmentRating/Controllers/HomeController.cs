using System;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

using System.IO;
using Microsoft.AspNetCore.Http;
using DepartmentRatingDAL.Models.Repositories;

using DepartmentRatingDAL.Models.DataBase;

namespace DepartmentRating.Controllers
{
    public class HomeController : Controller
    {
        private readonly AdminRepository adminRepository;
        private readonly UserRepository userRepository;
        private readonly OffenceRepository offenceRepository;
        private readonly MainRepository mainRepository;
        private readonly EventLogRepository eventLogRepository;

        public HomeController(AdminRepository adminRepository, 
                                UserRepository userRepository, 
                                OffenceRepository offenceRepository, 
                                MainRepository mainRepository,
                                EventLogRepository eventLogRepository)
        {
            this.adminRepository = adminRepository;
            this.userRepository = userRepository;
            this.offenceRepository = offenceRepository;
            this.mainRepository = mainRepository;
            this.eventLogRepository = eventLogRepository;

            
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
                model.Date = DateTime.UtcNow;
                adminRepository.SaveAdmin(model);
                AddEventLog("Admin",String.Format("Add admin - {0}",name));
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        private void AddEventLog(string table, string message)
        {
            DepartmentRatingDAL.Models.DataBase.EventLog model = new DepartmentRatingDAL.Models.DataBase.EventLog();
            model.Date = DateTime.UtcNow;
            model.Description = message;
            model.Owner = User.Identity.Name;
            model.Table = table;
            eventLogRepository.SaveEvent(model);
        }


        [HttpPost]
        public IActionResult AdminDelete(int id)
        {
            string name = adminRepository.GetById(id).Name;

            //adminRepository.Delete(new Admin { AdminId = id});
            adminRepository.Delete(id);
            AddEventLog("Admin", String.Format("Delete admin - {0}", name));
            return RedirectToAction("Index");

        }

        [HttpPost]
        public IActionResult OffenceDelete(int id)
        {
            //AddEventLog("Offence", String.Format("Delete Offence - {0}", offenceRepository.GetById(id).Name));
            offenceRepository.Delete(id);
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
                model.CreateDate = DateTime.UtcNow;
                offenceRepository.SaveOffence(model);
                AddEventLog("Offence", String.Format("Add Offence - {0}", name));
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult ResetMain()
        {
            AddEventLog("Main", String.Format("Reset Main"));
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
                model.Date = DateTime.UtcNow;

                
                userRepository.ChangeRating(userRepository.GetById(model.UserId), offenceRepository.GetById(model.OffenceId));


                mainRepository.SaveMain(model);
                AddEventLog("Main", String.Format("Add Main - {0} : {1}. {2}", userRepository.GetById(model.UserId).Account, offenceRepository.GetById(model.OffenceId).Name, description));

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Excel()
        {
            //var stream = new MemoryStream();
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

    }
}
