using ulp_net_inmobiliaria.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace ulp_net_inmobiliaria.Controllers
{
	public class PagosController : Controller
	{
		private readonly RepositorioPago repo;

		public PagosController()
		{
			repo = new RepositorioPago();
		}

		[Authorize]
		// GET: PagosController
		public ActionResult Index(int id = 0)
		{
			List<Pago> lista = new List<Pago>();
			if (id != 0)
			{
				RepositorioContrato repoContrato = new RepositorioContrato();
				ViewBag.Contrato = repoContrato.ObtenerPorId(id);
				lista = repo.ObtenerTodosContrato(id);
			}
			else
			{
				lista = repo.ObtenerTodos();
			}
			return View(lista);
		}

		[HttpGet]
		[Authorize]
		public ActionResult Details(int id)
		{
			var entidad = repo.ObtenerPorId(id);
			return View(entidad);
		}

		[HttpGet]
		[Authorize]
		public ActionResult Create(int id)
		{
			RepositorioContrato repoContrato = new RepositorioContrato();
			ViewBag.Contrato = repoContrato.ObtenerPorId(id);
			ViewBag.PagoUltimo = repo.ObtenerUltimoPago(id);
			Pago p = new Pago();
			return View(p);
		}

		[HttpPost]
		[Authorize]
		public ActionResult Create(Pago pago)
		{
			try
			{
				var lista = repo.Alta(pago);
				return RedirectToAction("Details", "Contratos", new { id = pago.ContratoId });
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
			RepositorioContrato repoContrato = new RepositorioContrato();
			ViewBag.Contratos = repoContrato.ObtenerTodos();
			Pago? p = repo.ObtenerPorId(id);
			return View(p);
		}

		[HttpPost]
		[Authorize]
		public ActionResult Update(int id, Pago pago)
		{
			Pago? p;
			try
			{
				p = repo.ObtenerPorId(id);
				p.Numero = pago.Numero;
				p.Fecha = pago.Fecha;
				p.Importe = pago.Importe;
				repo.Modificacion(pago);
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
		public ActionResult Delete(int id, Pago pago)
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