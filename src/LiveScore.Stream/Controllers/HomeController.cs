using System.Diagnostics;
using LiveScore.Stream.Models;
using Microsoft.AspNetCore.Mvc;

namespace LiveScore.Stream.Controllers
{
	public class HomeController : Controller
	{
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
