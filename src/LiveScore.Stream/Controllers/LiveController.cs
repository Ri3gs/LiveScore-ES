using System;
using Microsoft.AspNetCore.Mvc;
using LiveScore.Application.Services.Live;

namespace LiveScore.Stream.Controllers
{
	public class LiveController : Controller
	{
		private readonly ILiveControllerService _service;

		public LiveController(ILiveControllerService service)
		{
			_service = service;
		}

		public IActionResult Index(String id = "WP001")
		{
			var model = _service.GetLiveMatch(id);
			return View("live", model);
		}

		public IActionResult Match(String id = "")
		{
			var model = _service.GetLiveMatch(id);
			return Json(model);
		}
	}
}