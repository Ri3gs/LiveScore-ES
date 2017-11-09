using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using LiveScore.Application.Services.Match;

namespace LiveScore.Web.Controllers
{
	public class MatchController : Controller
	{
		private readonly IMatchControllerService _service;

		public MatchController(IMatchControllerService service)
		{
			_service = service;
		}

		public IActionResult Index(String id)
		{
			id = id ?? "WP001";
			var model = _service.GetCurrentState(id);
			return View(model);
		}

		public IActionResult Action(String id)
		{
			var eventName = MakeSenseOfWhatTheUserDid(Request);

			// Perform action here
			_service.DispatchCommand(id, eventName);

			return RedirectToAction("index", new { id = id });
		}

		private String MakeSenseOfWhatTheUserDid(HttpRequest request)
		{
			if (request.Form.ContainsKey("btnStart")) return "Start";

			if (request.Form.ContainsKey("btnStart")) return "End";

			if (request.Form.ContainsKey("btnNewPeriod")) return "NewPeriod";

			if (request.Form.ContainsKey("btnEndPeriod")) return "EndPeriod";

			if (request.Form.ContainsKey("btnGoal1")) return "Goal1";

			if (request.Form.ContainsKey("btnGoal2")) return "Goal2";

			if (request.Form.ContainsKey("btnUndo")) return "Undo";

			if (request.Form.ContainsKey("btnZap")) return "Zap";

			throw new InvalidOperationException("What just user did makes no sense!");
		}
	}
}