using Microsoft.AspNetCore.Mvc;
using MvcCacheRedisProductos.Models;
using MvcCacheRedisProductos.Repositories;
using MvcCacheRedisProductos.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCacheRedisProductos.Controllers
{
    public class ProductoController : Controller
    {
        private RepositoryProductos repo;
        private ServiceCacheRedis service;


        public ProductoController(RepositoryProductos repo, ServiceCacheRedis service) {

            this.service = service;
            this.repo = repo;
        }

        public IActionResult Favoritos() {

            List<Producto> favoritos = this.service.GetFavoriteProducts();

            if (favoritos == null)
            {

                return View();
            }
            else { 
            
                return View(favoritos);
            }
        }

        public IActionResult SeleccionarFavorito(int id) {

            //BUSCAMOS EL PRODUCTO A ALMACENAR DENTRO DE XML

            Producto favorito = this.repo.FindProducto(id);

            //ALMACENAMOS EL PRODUCTO EN EL SERVICIO CACHE
            this.service.AddProduct(favorito);

            ViewData["MENSAJE"] = "Producto " + favorito.Nombre + " almacenado en cache";

            return RedirectToAction("Details", new { id = id });
        }

        public IActionResult EliminarFavorito(int id) {

            this.service.DeleteFavoriteProduct(id);

            return RedirectToAction("favoritos");
        }

        public IActionResult Index()
        {
            List<Producto> productos = this.repo.GetProductos();

            return View(productos);
        }

        public IActionResult Details(int id) {

            Producto producto = this.repo.FindProducto(id);

            return View(producto);
        }
    }
}
