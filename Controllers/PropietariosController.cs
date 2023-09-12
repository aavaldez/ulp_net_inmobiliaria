using ulp_net_inmobiliaria.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace ulp_net_inmobiliaria.Controllers
{
	public class PropietariosController : Controller
	{
		private readonly RepositorioPropietario repo;
		public PropietariosController()
		{
			repo = new RepositorioPropietario();
		}

		// GET: PropietariosController
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
			RepositorioInmueble repoInmueble = new RepositorioInmueble();
			ViewBag.Inmuebles = repoInmueble.ObtenerTodosPropietario(id);
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
		public ActionResult Create(Propietario propietario)
		{
			try
			{
				var lista = repo.Alta(propietario);
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
			var entidad = repo.ObtenerPorId(id);
			return View(entidad);
		}

		[HttpPost]
		[Authorize]
		public ActionResult Update(int id, Propietario propietario)
		{
			Propietario p;
			try
			{
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
		[Authorize(Policy = "Administrador")]
		public ActionResult Delete(int id)
		{
			var entidad = repo.ObtenerPorId(id);
			return View(entidad);
		}

		[HttpPost]
		[Authorize(Policy = "Administrador")]
		public ActionResult Delete(int id, Propietario propietario)
		{
			try
			{
				repo.Baja(id);
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View(propietario);
			}
		}
	}
}