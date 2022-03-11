using Microsoft.AspNetCore.Mvc;
using MvcApiManagement.Models;
using MvcApiManagement.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcApiManagement.Controllers
{
    public class DoctorController : Controller
    {
        private ServiceApiDoctores service;

        public DoctorController(ServiceApiDoctores service) {

            this.service = service;
        }

        public async Task<IActionResult> Index()
        {
            List<Doctor> doctores = await this.service.GetDoctoresAsync();
            return View(doctores);
        }
    }
}
