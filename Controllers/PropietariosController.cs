using ulp_net_inmobiliaria.Models;
using Microsoft.AspNetCore.Mvc;

namespace ulp_net_inmobiliaria.Controllers
{
	public class PropietariosController : Controller
	{
		// GET: PropietariosController
		public ActionResult Index()
		{
			RepositorioPropietario repo = new RepositorioPropietario();
			var lista = repo.ObtenerTodos();
			return View(lista);
		}

		[HttpGet]
		public ActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Create(Propietario propietario)
		{
			try
			{
				RepositorioPropietario repo = new RepositorioPropietario();
				var lista = repo.Alta(propietario);
				return RedirectToAction("Index");
			}
			catch (System.Exception)
			{
				
				throw;
			}
		}

		[HttpGet]
		public ActionResult Update(int id)
		{
			RepositorioPropietario repo = new RepositorioPropietario();
			Propietario p = repo.ObtenerPorId(id);
			return View(p);
		}

		[HttpPost]
		public ActionResult Update(Propietario propietario)
		{
			try
			{
				RepositorioPropietario repo = new RepositorioPropietario();
				var lista = repo.Alta(propietario);
				return RedirectToAction("Index");
			}
			catch (System.Exception)
			{
				
				throw;
			}
		}
	}
}