using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheWorld.Models;
using TheWorld.Services;
using TheWorld.ViewModels;


namespace TheWorld.Controllers.Web
{
    public class AppController : Controller
    {

        private IMailService _mailService;
        private IConfigurationRoot _config;
        private WorldContext _context;

        public AppController(IMailService mailService, IConfigurationRoot config, WorldContext context)
        {
            _mailService = mailService;
            _config = config;
            _context = context;
        }

        
        public IActionResult Index()
        {
            var data = _context.Trips.ToList();

            return View(data);
        }

        public IActionResult Contact()
        {

         //   throw new InvalidOperationException("Błąd!");

            return View();
        }

        [HttpPost]
        public IActionResult Contact(ContactViewModel model)
        {

            if(model.Email.Contains("aol.com"))
            {
                ModelState.AddModelError("Email", "Nie wspieramy AOL.com");  //Bład wyświetli się przy email, jak zmienimy na "" to w validation-summary
            }


            if(ModelState.IsValid)
            {
                _mailService.SendEmail(_config["MailSettings:ToAddress"], model.Email, "From WorldApp", model.Message);

                ModelState.Clear();

                ViewBag.UserMessage = "Wiadomośc została wysłana";
            }


            return View();
        }

        public IActionResult About()
        {
            return View();
        }


    }
}
