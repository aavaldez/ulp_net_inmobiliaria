using ulp_net_inmobiliaria.Models;
using Microsoft.AspNetCore.Mvc;

namespace ulp_net_inmobiliaria.Controllers
{
	public class PagosController : Controller
	{
		// GET: PagosController
		public ActionResult Index(int contratoId)
		{
			RepositorioContrato repoContrato = new RepositorioContrato();
			ViewBag.Contrato = repoContrato.ObtenerPorId(contratoId);
			RepositorioPago repo = new RepositorioPago();
			var lista = repo.ObtenerTodos();
			return View(lista);
		}

		[HttpGet]
		public ActionResult Details(int id)
		{
			RepositorioPago repo = new RepositorioPago();
			var entidad = repo.ObtenerPorId(id);
			return View(entidad);
		}

		[HttpGet]
		public ActionResult Create(int contratoId)
		{
			RepositorioContrato repoContrato = new RepositorioContrato();
			ViewBag.Contrato = repoContrato.ObtenerPorId(contratoId);
			Pago p = new Pago();
			return View(p);
		}

		[HttpPost]
		public ActionResult Create(Pago pago)
		{
			try
			{
				RepositorioPago repo = new RepositorioPago();
				var lista = repo.Alta(pago);
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
			RepositorioPago repo = new RepositorioPago();
			RepositorioContrato repoContrato = new RepositorioContrato();
			ViewBag.Contratos = repoContrato.ObtenerTodos();
			Pago p = repo.ObtenerPorId(id);
			return View(p);
		}

		[HttpPost]
		public ActionResult Update(int id, Pago pago)
		{
			Pago p;
			try
			{
				RepositorioPago repo = new RepositorioPago();
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
		public ActionResult Delete(int id)
		{
			RepositorioPago repo = new RepositorioPago();
			var entidad = repo.ObtenerPorId(id);
			return View(entidad);
		}

		[HttpPost]
		public ActionResult Delete(int id, IFormCollection collection)
		{
			try
			{
				RepositorioPago repo = new RepositorioPago();
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