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
		public ActionResult Details(int id)
		{
			RepositorioInquilino repo = new RepositorioInquilino();
			var entidad = repo.ObtenerPorId(id);
			return View(entidad);
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
		public ActionResult Edit(int id)
		{
			RepositorioInquilino repo = new RepositorioInquilino();
			Inquilino p = repo.ObtenerPorId(id);
			return View(p);
		}

		[HttpPost]
		public ActionResult Update(int id, Inquilino inquilino)
		{
			Inquilino i;
			try
			{
				RepositorioInquilino repo = new RepositorioInquilino();
				i = repo.ObtenerPorId(id);
				i.Nombre = inquilino.Nombre;
				i.Apellido = inquilino.Apellido;
				i.Dni = inquilino.Dni;
				i.Email = inquilino.Email;
				i.Telefono = inquilino.Telefono;
				repo.Modificacion(inquilino);
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
			RepositorioInquilino repo = new RepositorioInquilino();
			var entidad = repo.ObtenerPorId(id);
			return View(entidad);
		}

		[HttpPost]
		public ActionResult Delete(int id, IFormCollection collection)
		{
			try
			{
				RepositorioInquilino repo = new RepositorioInquilino();
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