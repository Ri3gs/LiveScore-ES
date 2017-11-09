using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LiveScore.Web.Models;

namespace LiveScore.Web.Controllers
{
	public class HomeController : Controller
	{
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
