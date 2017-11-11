using System;
using System.Collections.Generic;
using LiveScore.CommandStack.Events;
using LiveScore.Framework;

namespace LiveScore.CommandStack
{
	public class EventHelper
	{
		public static Match PlayEvents(String id, IList<DomainEvent> events)
		{
			var match = new Match(id);

			foreach (var e in events)
			{
				if (e == null)
					return match;

				switch (e)
				{
					case MatchStartedEvent msg:
						match.Start();
						break;
					case MatchEndedEvent msg:
						match.Finish();
						break;
					case PeriodStartedEvent msg:
						match.StartPeriod();
						break;
					case PeriodEndedEvent msg:
						match.EndPeriod();
						break;
					case HomeScoredGoalEvent msg:
						match.Goal(TeamId.Home);
						break;
					case VisitorsScoredGoalEvent msg:
						match.Goal(TeamId.Visitors);
						break;
				}
			}

			return match;
		}
	}
}