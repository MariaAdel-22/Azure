using Microsoft.AspNetCore.Mvc;
using MvcClienteCrudDoctores2022.Models;
using MvcClienteCrudDoctores2022.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcClienteCrudDoctores2022.Controllers
{
    public class DoctoresController : Controller
    {
        private ServiceApiDoctores service;

        public DoctoresController(ServiceApiDoctores service) {

            this.service = service;
        }

        public async Task<IActionResult> Index()
        {
            List<Doctor> doctores = await this.service.GetDoctoresAsync();

            return View(doctores);
        }


        public IActionResult Create() {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Doctor doctor) {

            await this.service.InsertarDoctor(doctor.IdDoctor, doctor.IdHospital, doctor.Apellido, doctor.Especialidad, doctor.Salario);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(string id) {

            Doctor doc = await this.service.FindDoctorAsync(id);

            return View(doc);
        }

        [HttpPut]
        public async Task<IActionResult> Edit(Doctor doctor) {

            await this.service.ModificarDoctor(doctor.IdDoctor,doctor.IdHospital,doctor.Apellido,doctor.Especialidad,doctor.Salario);

            return RedirectToAction("Index");
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string id) {

            await this.service.EliminarDoctor(id);

            return RedirectToAction("Index");
        }
    }
}
