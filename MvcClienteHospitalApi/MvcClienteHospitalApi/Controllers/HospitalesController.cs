using Microsoft.AspNetCore.Mvc;
using MvcClienteHospitalApi.Models;
using MvcClienteHospitalApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcClienteHospitalApi.Controllers
{
    public class HospitalesController : Controller
    {
        private ServiceHospital service;

        public HospitalesController(ServiceHospital service) {

            this.service = service;
        }

        public async Task<IActionResult> Index()
        {
            List<Hospital> hospitales = await this.service.GetHospitalesAsync();

            return View(hospitales);
        }
    }
}
