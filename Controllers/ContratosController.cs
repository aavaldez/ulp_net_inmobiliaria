using ulp_net_inmobiliaria.Models;
using Microsoft.AspNetCore.Mvc;

namespace ulp_net_inmobiliaria.Controllers
{
	public class ContratosController : Controller
	{
		// GET: ContratosController
		public ActionResult Index()
		{
			RepositorioContrato repo = new RepositorioContrato();
			var lista = repo.ObtenerTodos();
			return View(lista);
		}

		[HttpGet]
		public ActionResult Details(int id)
		{
			RepositorioContrato repo = new RepositorioContrato();
			var entidad = repo.ObtenerPorId(id);
			return View(entidad);
		}

		[HttpGet]
		public ActionResult Create()
		{
			RepositorioInquilino repoInquilino = new RepositorioInquilino();
			ViewBag.Inquilinos = repoInquilino.ObtenerTodos();
			RepositorioInmueble repoInmueble = new RepositorioInmueble();
			ViewBag.Inmuebles = repoInmueble.ObtenerTodos();
			return View();
		}

		[HttpPost]
		public ActionResult Create(Contrato contrato)
		{
			try
			{
				RepositorioContrato repo = new RepositorioContrato();
				var lista = repo.Alta(contrato);
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
			RepositorioContrato repo = new RepositorioContrato();
			RepositorioInquilino repoInquilino = new RepositorioInquilino();
			ViewBag.Inquilinos = repoInquilino.ObtenerTodos();
			RepositorioInmueble repoInmueble = new RepositorioInmueble();
			ViewBag.Inmuebles = repoInmueble.ObtenerTodos();
			Contrato? c = repo.ObtenerPorId(id);
			return View(c);
		}

		[HttpPost]
		public ActionResult Update(int id, Contrato contrato)
		{
			Contrato? c = null;
			try
			{
				RepositorioContrato repo = new RepositorioContrato();
				c = repo.ObtenerPorId(id);
				c.InmuebleId = contrato.InmuebleId;
				c.InquilinoId = contrato.InquilinoId;
				c.Desde = contrato.Desde;
				c.Hasta = contrato.Hasta;
				c.Valor = contrato.Valor;
				repo.Modificacion(c);
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
			RepositorioContrato repo = new RepositorioContrato();
			var entidad = repo.ObtenerPorId(id);
			return View(entidad);
		}

		[HttpPost]
		public ActionResult Delete(int id, IFormCollection collection)
		{
			try
			{
				RepositorioContrato repo = new RepositorioContrato();
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