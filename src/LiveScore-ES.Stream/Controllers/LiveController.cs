using LiveScoreEs.Services.Live;
using Microsoft.AspNetCore.Mvc;
using System;

namespace LiveScore_ES.Stream.Controllers
{
	public class LiveController : Controller
	{
		private readonly ILiveService _service;

		public LiveController(ILiveService service)
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