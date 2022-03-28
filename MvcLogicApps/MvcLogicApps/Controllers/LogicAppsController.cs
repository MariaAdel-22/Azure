using Microsoft.AspNetCore.Mvc;
using MvcLogicApps.Models;
using MvcLogicApps.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcLogicApps.Controllers
{
    public class LogicAppsController : Controller
    {
        private ServiceLogicApps service;

        public LogicAppsController(ServiceLogicApps service) {

            this.service = service;
        }

        public IActionResult SendMail()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendMail(EmailModel model) {

            await this.service.SendMail(model.Email, model.Subject, model.Body);

            ViewData["MENSAJE"] = "Logic Apps de Mail en proceso!!";

            return View();
        }
    }
}
