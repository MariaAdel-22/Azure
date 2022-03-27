using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MvcAzureFiles.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MvcAzureFiles.Controllers
{
    public class AzureFilesController : Controller
    {
        private ServiceStorageFile service;

        public AzureFilesController(ServiceStorageFile service)
        {
            this.service = service;
        }

        public async Task<IActionResult> Index()
        {
            List<string> files = await this.service.GetFilesAsync();
            return View(files);
        }

        public async Task<IActionResult> ReadFile(string fileName)
        {
            string data = await this.service.ReadFileAsync(fileName);
            ViewData["CONTENT"] = data;
            return View();
        }

        public async Task<IActionResult> DeleteFile(string fileName)
        {
            await this.service.DeleteFileAsync(fileName);
            return RedirectToAction("Index");
        }

        public IActionResult UploadFile()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            string fileName = file.FileName;
            using (Stream stream = file.OpenReadStream())
            {
                await this.service.UploadFileAsync(fileName, stream);
            }
            ViewData["MENSAJE"] = "Fichero subido correctamente";
            return View();
        }

    }
}
