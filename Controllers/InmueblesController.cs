using ulp_net_inmobiliaria.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace ulp_net_inmobiliaria.Controllers
{
	public class InmueblesController : Controller
	{
		private readonly RepositorioInmueble repo;
		public InmueblesController()
		{
			repo = new RepositorioInmueble();
		}

		// GET: InmueblesController
		[Authorize]
		public ActionResult Index()
		{
			var lista = repo.ObtenerTodos();
			return View(lista);
		}

		[Authorize]
		public ActionResult Disponibles()
		{
			var lista = repo.ObtenerDisponibles();
			ViewBag.desde = false;
			ViewBag.hasta = false;
			return View("Index", lista);
		}

		[Authorize]
		public ActionResult DisponiblesFecha(DateTime desde, DateTime hasta)
		{
			if (desde == DateTime.MinValue && hasta == DateTime.MinValue)
			{
				TempData["Error"] = "Tiene que completar al menos una fecha.";
				return RedirectToAction("Index");
			}

			if (desde != DateTime.MinValue && hasta != DateTime.MinValue && desde > hasta)
			{
				TempData["Error"] = "La fecha inicial no puede ser mayor a la fecha final.";
				return RedirectToAction("Index");
			}

			var lista = repo.ObtenerDisponiblesFecha(desde, hasta);
			ViewBag.desde = desde;
			ViewBag.hasta = hasta;
			return View("Index", lista);
		}

		[HttpGet]
		[Authorize]
		public ActionResult Details(int id)
		{
			var entidad = repo.ObtenerPorId(id);
			RepositorioContrato repoContrato = new RepositorioContrato();
			ViewBag.Contratos = repoContrato.ObtenerTodosInmueble(id);
			return View(entidad);
		}

		[HttpGet]
		[Authorize]
		public ActionResult Create()
		{
			RepositorioPropietario repoPropietario = new RepositorioPropietario();
			ViewBag.Propietarios = repoPropietario.ObtenerTodos();
			ViewBag.Tipos = Inmueble.ObtenerTipos();
			ViewBag.Usos = Inmueble.ObtenerUsos();
			return View();
		}

		[HttpPost]
		[Authorize]
		public ActionResult Create(Inmueble inmueble)
		{
			try
			{
				var lista = repo.Alta(inmueble);
				return RedirectToAction("Index");
			}
			catch (System.Exception)
			{

				throw;
			}
		}

		[HttpGet]
		[Authorize]
		public ActionResult Edit(int id)
		{
			RepositorioPropietario repoPropietario = new RepositorioPropietario();
			ViewBag.Propietarios = repoPropietario.ObtenerTodos();
			ViewBag.Tipos = Inmueble.ObtenerTipos();
			ViewBag.Usos = Inmueble.ObtenerUsos();
			ViewBag.Estados = Inmueble.ObtenerEstados();
			Inmueble? i = repo.ObtenerPorId(id);
			return View(i);
		}

		[HttpPost]
		[Authorize]
		public ActionResult Update(int id, Inmueble inmueble)
		{
			Inmueble? i;
			try
			{
				i = repo.ObtenerPorId(id);
				i.Uso = inmueble.Uso;
				i.Tipo = inmueble.Tipo;
				i.Direccion = inmueble.Direccion;
				i.Ambientes = inmueble.Ambientes;
				i.Superficie = inmueble.Superficie;
				i.Latitud = inmueble.Latitud;
				i.Longitud = inmueble.Longitud;
				i.Valor = inmueble.Valor;
				i.Estado = inmueble.Estado;
				repo.Modificacion(i);
				return RedirectToAction("Index");
			}
			catch (System.Exception)
			{
				throw;
			}
		}

		[HttpGet]
		[Authorize(Policy = "Administrador")]
		public ActionResult Delete(int id)
		{
			var entidad = repo.ObtenerPorId(id);
			return View(entidad);
		}

		[HttpPost]
		[Authorize(Policy = "Administrador")]
		public ActionResult Delete(int id, Inmueble inmueble)
		{
			try
			{
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