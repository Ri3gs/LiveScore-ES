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
		    String buffer = "Start";
		    //buffer = request.["btnStart"];
		    //if (buffer != null) return "Start";

		    //buffer = request.Params["btnEnd"];
		    //if (buffer != null) return "End";

		    //buffer = request.Params["btnNewPeriod"];
		    //if (buffer != null) return "NewPeriod";

		    //buffer = request.Params["btnEndPeriod"];
		    //if (buffer != null) return "EndPeriod";

		    //buffer = request.Params["btnGoal1"];
		    //if (buffer != null) return "Goal1";

		    //buffer = request.Params["btnGoal2"];
		    //if (buffer != null) return "Goal2";

		    //buffer = request.Params["btnUndo"];
		    //if (buffer != null) return "Undo";

		    //buffer = request.Params["btnZap"];
		    //if (buffer != null) return "Zap";

		    return buffer;
	    }
	}
}