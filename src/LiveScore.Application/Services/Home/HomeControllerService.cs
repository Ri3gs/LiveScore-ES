using LiveScoreEs.Backend.DAL;
using LiveScoreEs.Backend.Services;
using LiveScoreEs.Framework;
using LiveScoreEs.Framework.Events;
using LiveScoreEs.ViewModels.Home;
using System;
using System.Linq;

namespace LiveScoreES.Application.Services.Home
{
	public class HomeControllerService : IHomeControllerService
	{
		private readonly IEventRepository _eventRepository;

		//TODO: for the time being just use poor mans injection
		public HomeControllerService() : this(new EventRepository())
		{
		}

		public HomeControllerService(IEventRepository eventRepository)
		{
			_eventRepository = eventRepository;
		}

		public void DispatchCommand(String matchId, String eventName)
		{
			//// Log event unless it is UNDO
			//var repo = new EventRepository();

			//if (eventName == "Zap")
			//{
			//    repo.Empty(matchId);
			//    ZapSnapshots(matchId);
			//    return;
			//}

			//TODO: this is a dirty hack
			DomainEvent domainEvent = default(DomainEvent);

			switch (eventName)
			{
				case "Start":
					domainEvent = new MatchStartedEvent(matchId);
					break;
				case "NewPeriod":
					domainEvent = new PeriodStartedEvent(matchId);
					break;
			}

			Bus.Send(domainEvent);

			//if (eventName == "Undo")
			//    repo.UndoLastAction(matchId);
			//else
			//    repo.RecordEvent(matchId, eventName);
			//repo.Commit();

			// Update snapshot for live scoring
			// UpdateSnapshots(matchId);
		}

		public MatchViewModel GetCurrentState(String matchId)
		{
			var events = _eventRepository.GetEventStreamFor(matchId);
			var matchInfo = EventHelper.PlayEvents(matchId, events.ToList());
			return new MatchViewModel(matchInfo);
		}

		//private void UpdateSnapshots(String matchId)
		//{
		//	var events = _eventRepository.GetEventStreamFor(matchId);
		//	var matchInfo = EventHelper.PlayEvents(matchId, events.ToList());

		//	using (var db = new WaterpoloContext())
		//	{
		//		var lm = (from m in db.Matches where m.Id == matchId select m).FirstOrDefault();
		//		if (lm == null)
		//		{
		//			var liveMatch = new LiveMatch
		//			{
		//				Id = matchId,
		//				Team1 = matchInfo.Team1,
		//				Team2 = matchInfo.Team2,
		//				State = matchInfo.State,
		//				IsBallInPlay = matchInfo.IsBallInPlay,
		//				CurrentScore = matchInfo.CurrentScore,
		//				CurrentPeriod = matchInfo.CurrentPeriod,
		//				TimeInPeriod = 0
		//			};
		//			db.Matches.Add(liveMatch);
		//		}
		//		else
		//		{
		//			lm.State = matchInfo.State;
		//			lm.IsBallInPlay = matchInfo.IsBallInPlay;
		//			lm.CurrentScore = matchInfo.CurrentScore;
		//			lm.CurrentPeriod = matchInfo.CurrentPeriod;
		//			lm.TimeInPeriod = 0;
		//		}
		//		db.SaveChanges();
		//	}
		//}

		//private void ZapSnapshots(String matchId)
		//{
		//	using (var db = new WaterpoloContext())
		//	{
		//		var lm = (from m in db.Matches where m.Id == matchId select m).FirstOrDefault();
		//		if (lm != null)
		//		{
		//			db.Matches.Remove(lm);
		//		}
		//		db.SaveChanges();
		//	}
		//}
	}
}
