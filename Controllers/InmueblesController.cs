using ulp_net_inmobiliaria.Models;
using Microsoft.AspNetCore.Mvc;

namespace ulp_net_inmobiliaria.Controllers
{
	public class InmueblesController : Controller
	{
		// GET: InmueblesController
		public ActionResult Index()
		{
			RepositorioInmueble repo = new RepositorioInmueble();
			var lista = repo.ObtenerTodos();
			return View(lista);
		}

		[HttpGet]
		public ActionResult Details(int id)
		{
			RepositorioInmueble repo = new RepositorioInmueble();
			var entidad = repo.ObtenerPorId(id);
			return View(entidad);
		}

		[HttpGet]
		public ActionResult Create()
		{
			RepositorioPropietario repoPropietario = new RepositorioPropietario();
			ViewBag.Propietarios = repoPropietario.ObtenerTodos();
			ViewBag.Tipos = Inmueble.ObtenerTipos();
			return View();
		}

		[HttpPost]
		public ActionResult Create(Inmueble inmueble)
		{
			try
			{
				RepositorioInmueble repo = new RepositorioInmueble();
				var lista = repo.Alta(inmueble);
				return RedirectToAction("Index");
			}
			catch (System.Exception)
			{
				
				throw;
			}
		}

		[HttpGet]
		public ActionResult Edit(int id)
		{
			RepositorioInmueble repo = new RepositorioInmueble();
			RepositorioPropietario repoPropietario = new RepositorioPropietario();
			ViewBag.Propietarios = repoPropietario.ObtenerTodos();
			Inmueble i = repo.ObtenerPorId(id);
			return View(i);
		}

		[HttpPost]
		public ActionResult Update(int id, Inmueble inmueble)
		{
			Inmueble i;
			try
			{
				RepositorioInmueble repo = new RepositorioInmueble();
				i = repo.ObtenerPorId(id);
				i.Direccion = inmueble.Direccion;
				i.Ambientes = inmueble.Ambientes;
				i.Superficie = inmueble.Superficie;
				i.Latitud = inmueble.Latitud;
				i.Longitud = inmueble.Longitud;
				repo.Modificacion(inmueble);
				return RedirectToAction("Index");
			}
			catch (System.Exception)
			{
				throw;
			}
		}
	
		[HttpGet]
		public ActionResult Delete(int id)
		{
			RepositorioInmueble repo = new RepositorioInmueble();
			var entidad = repo.ObtenerPorId(id);
			return View(entidad);
		}

		[HttpPost]
		public ActionResult Delete(int id, IFormCollection collection)
		{
			try
			{
				RepositorioInmueble repo = new RepositorioInmueble();
				var lista = repo.Baja(id);
				return RedirectToAction("Index");
			}
			catch (System.Exception)
			{
				throw;
			}
		}
	}
}