using ulp_net_inmobiliaria.Models;
using Microsoft.AspNetCore.Mvc;

namespace ulp_net_inmobiliaria.Controllers
{
	public class InquilinosController : Controller
	{
		// GET: InquilinosController
		public ActionResult Index()
		{
			RepositorioInquilino repo = new RepositorioInquilino();
			var lista = repo.ObtenerTodos();
			return View(lista);
		}

		[HttpGet]
		public ActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Create(Inquilino inquilino)
		{
			try
			{
				RepositorioInquilino repo = new RepositorioInquilino();
				var lista = repo.Alta(inquilino);
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
			RepositorioInquilino repo = new RepositorioInquilino();
			Inquilino p = repo.ObtenerPorId(id);
			return View(p);
		}

		[HttpPost]
		public ActionResult Update(Inquilino inquilino)
		{
			try
			{
				RepositorioInquilino repo = new RepositorioInquilino();
				var lista = repo.Alta(inquilino);
				return RedirectToAction("Index");
			}
			catch (System.Exception)
			{
				
				throw;
			}
		}
	}
}