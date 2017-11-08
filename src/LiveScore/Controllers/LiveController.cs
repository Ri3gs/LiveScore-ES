using System;
using System.Web.Mvc;
using LiveScoreEs.Services.Live;

namespace LiveScoreEs.Controllers
{
	public class LiveController : Controller
	{
		private readonly ILiveService _service;

		//public LiveController() : this(new LiveService())
		//{
		//}
		public LiveController(ILiveService service)
		{
			_service = service;
		}
		public ActionResult Index(String id = "")
		{
			var model = _service.GetLiveMatch(id);
			return View("live", model);
		}

		public ActionResult Match(String id = "")
		{
			var model = _service.GetLiveMatch(id);
			return Json(model, JsonRequestBehavior.AllowGet);
		}
	}
}