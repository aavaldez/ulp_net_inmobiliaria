using ulp_net_inmobiliaria.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace ulp_net_inmobiliaria.Controllers
{
	public class ContratosController : Controller
	{
		private readonly RepositorioContrato repo;
		public ContratosController()
		{
			repo = new RepositorioContrato();
		}

		// GET: ContratosController
		[Authorize]
		public ActionResult Index()
		{
			var lista = repo.ObtenerTodos();
			return View(lista);
		}

		[HttpGet]
		[Authorize]
		public ActionResult Details(int id)
		{
			var entidad = repo.ObtenerPorId(id);
			RepositorioPago repoPago = new RepositorioPago();
			ViewBag.Pagos = repoPago.ObtenerTodosContrato(id);
			return View(entidad);
		}

		[HttpGet]
		[Authorize]
		public ActionResult Create()
		{
			RepositorioInquilino repoInquilino = new RepositorioInquilino();
			ViewBag.Inquilinos = repoInquilino.ObtenerTodos();
			RepositorioInmueble repoInmueble = new RepositorioInmueble();
			ViewBag.Inmuebles = repoInmueble.ObtenerTodos();
			return View();
		}

		[HttpPost]
		[Authorize]
		public ActionResult Create(Contrato contrato)
		{
			try
			{
				var lista = repo.Alta(contrato);
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
			RepositorioInquilino repoInquilino = new RepositorioInquilino();
			ViewBag.Inquilinos = repoInquilino.ObtenerTodos();
			RepositorioInmueble repoInmueble = new RepositorioInmueble();
			ViewBag.Inmuebles = repoInmueble.ObtenerTodos();
			Contrato? c = repo.ObtenerPorId(id);
			return View(c);
		}

		[HttpPost]
		[Authorize]
		public ActionResult Update(int id, Contrato contrato)
		{
			Contrato? c = null;
			try
			{
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
		[Authorize(Policy = "Administrador")]
		public ActionResult Delete(int id)
		{
			var entidad = repo.ObtenerPorId(id);
			return View(entidad);
		}

		[HttpPost]
		[Authorize(Policy = "Administrador")]
		public ActionResult Delete(int id, Contrato contrato)
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