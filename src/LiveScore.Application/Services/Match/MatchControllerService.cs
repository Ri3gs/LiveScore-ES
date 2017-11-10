using System;
using System.Linq;
using LiveScore.CommandStack;
using LiveScore.CommandStack.Events;
using LiveScore.Framework;
using LiveScore.Infrastructure;
using LiveScore.QueryStack;

namespace LiveScore.Application.Services.Match
{
	public class MatchControllerService : IMatchControllerService
	{
		private readonly IEventRepository _eventRepository;
		private readonly WaterpoloContext _dbContext;

		//TODO: for the time being just use poor mans injection
		public MatchControllerService(WaterpoloContext dbContext) : this(new EventRepository(), dbContext)
		{
		}

		public MatchControllerService(IEventRepository eventRepository, WaterpoloContext dbContext)
		{
			_eventRepository = eventRepository;
			_dbContext = dbContext;
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
				case "Goal1":
					domainEvent = new HomeScoredGoalEvent(matchId);
					break;
			}

			Bus.Send(domainEvent);

			//if (eventName == "Undo")
			//    repo.UndoLastAction(matchId);
			//else
			//    repo.RecordEvent(matchId, eventName);
			//repo.Commit();

			// Update snapshot for live scoring
			UpdateSnapshots(matchId);
		}

		public MatchViewModel GetCurrentState(String matchId)
		{
			var events = _eventRepository.GetEventStreamFor(matchId);
			var matchInfo = EventHelper.PlayEvents(matchId, events.ToList());
			return new MatchViewModel(matchInfo);
		}

		private void UpdateSnapshots(String matchId)
		{
			var events = _eventRepository.GetEventStreamFor(matchId);
			var matchInfo = EventHelper.PlayEvents(matchId, events.ToList());

			var lm = (from m in _dbContext.Matches where m.Id == matchId select m).FirstOrDefault();
			if (lm == null)
			{
				var liveMatch = new LiveMatch
				{
					Id = matchId,
					Team1 = matchInfo.Team1,
					Team2 = matchInfo.Team2,
					State = (QueryStack.MatchState)matchInfo.State,
					IsBallInPlay = matchInfo.IsBallInPlay,
					TotalGoals1 = matchInfo.CurrentScore.TotalGoals1,
					TotalGoals2 = matchInfo.CurrentScore.TotalGoals2,
					CurrentPeriod = matchInfo.CurrentPeriod,
					TimeInPeriod = 0
				};
				_dbContext.Matches.Add(liveMatch);
			}
			else
			{
				lm.State = (QueryStack.MatchState)matchInfo.State;
				lm.IsBallInPlay = matchInfo.IsBallInPlay;
				lm.TotalGoals1 = matchInfo.CurrentScore.TotalGoals1;
				lm.TotalGoals2 = matchInfo.CurrentScore.TotalGoals2;
				lm.CurrentPeriod = matchInfo.CurrentPeriod;
				lm.TimeInPeriod = 0;
			}
			_dbContext.SaveChanges();
		}

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
