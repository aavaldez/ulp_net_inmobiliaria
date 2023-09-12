using ulp_net_inmobiliaria.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace ulp_net_inmobiliaria.Controllers
{
	public class InquilinosController : Controller
	{
		private readonly RepositorioInquilino repo;
		public InquilinosController()
		{
			repo = new RepositorioInquilino();
		}

		// GET: InquilinosController
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
			return View(entidad);
		}

		[HttpGet]
		[Authorize]
		public ActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[Authorize]
		public ActionResult Create(Inquilino inquilino)
		{
			try
			{
				var lista = repo.Alta(inquilino);
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
			Inquilino p = repo.ObtenerPorId(id);
			return View(p);
		}

		[HttpPost]
		[Authorize]
		public ActionResult Update(int id, Inquilino inquilino)
		{
			Inquilino i;
			try
			{
				i = repo.ObtenerPorId(id);
				i.Nombre = inquilino.Nombre;
				i.Apellido = inquilino.Apellido;
				i.Dni = inquilino.Dni;
				i.Email = inquilino.Email;
				i.Telefono = inquilino.Telefono;
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
			RepositorioInquilino repo = new RepositorioInquilino();
			var entidad = repo.ObtenerPorId(id);
			return View(entidad);
		}

		[HttpPost]
		[Authorize(Policy = "Administrador")]
		public ActionResult Delete(int id, Inquilino inquilino)
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