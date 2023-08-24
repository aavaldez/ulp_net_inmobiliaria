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
		public ActionResult Details(int id)
		{
			RepositorioPropietario repo = new RepositorioPropietario();
			var entidad = repo.ObtenerPorId(id);
			return View(entidad);
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
		public ActionResult Edit(int id)
		{
			RepositorioPropietario repo = new RepositorioPropietario();
			Propietario p = repo.ObtenerPorId(id);
			return View(p);
		}

		[HttpPost]
		public ActionResult Update(int id, Propietario propietario)
		{
			Propietario p;
			try
			{
				RepositorioPropietario repo = new RepositorioPropietario();
				p = repo.ObtenerPorId(id);
				p.Nombre = propietario.Nombre;
				p.Apellido = propietario.Apellido;
				p.Dni = propietario.Dni;
				p.Email = propietario.Email;
				p.Telefono = propietario.Telefono;
				repo.Modificacion(p);
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
			RepositorioPropietario repo = new RepositorioPropietario();
			var entidad = repo.ObtenerPorId(id);
			return View(entidad);
		}

		[HttpPost]
		public ActionResult Delete(int id, IFormCollection collection)
		{
			try
			{
				RepositorioPropietario repo = new RepositorioPropietario();
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