﻿using System;
using System.Collections.Generic;
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

		public MatchControllerService(IEventRepository eventRepository, WaterpoloContext dbContext)
		{
			_eventRepository = eventRepository;
			_dbContext = dbContext;
		}

		public void DispatchCommand(string matchId, string eventName)
		{
			if (eventName == "Zap")
			{
				_eventRepository.Empty(matchId);
				ZapSnapshots(matchId);
				return;
			}

			if (eventName == "Undo")
			{
				_eventRepository.UndoLastAction(matchId);
				UpdateSnapshots(matchId);
				return;
			}

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
				case "Goal2":
					domainEvent = new VisitorsScoredGoalEvent(matchId);
					break;
				case "EndPeriod":
					domainEvent = new PeriodEndedEvent(matchId);
					break;
				case "End":
					domainEvent = new MatchEndedEvent(matchId);
					break;
			}

			Bus.Send(domainEvent);
			UpdateSnapshots(matchId);
		}

		public MatchViewModel GetCurrentState(String matchId)
		{
			IReadOnlyCollection<DomainEvent> events = _eventRepository.GetEventStreamFor(matchId);
			CommandStack.Match matchInfo = EventHelper.PlayEvents(matchId, events.ToList());
			return new MatchViewModel(matchInfo);
		}

		private void UpdateSnapshots(String matchId)
		{
			IReadOnlyCollection<DomainEvent> events = _eventRepository.GetEventStreamFor(matchId);
			CommandStack.Match matchInfo = EventHelper.PlayEvents(matchId, events.ToList());

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

		private void ZapSnapshots(String matchId)
		{
			var lm = (from m in _dbContext.Matches where m.Id == matchId select m).FirstOrDefault();
			if (lm != null)
			{
				_dbContext.Matches.Remove(lm);
			}
			_dbContext.SaveChanges();
		}
	}
}
