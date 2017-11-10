using System;
using System.Collections.Generic;
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

				var name = e.EventName;
				switch (name)
				{
					case "Start":
						match.Start();
						break;
					case "End":
						match.Finish();
						break;
					case "NewPeriod":
						match.StartPeriod();
						break;
					case "ENDPERIOD":
						match.EndPeriod();
						break;
					case "Goal1":
						match.Goal(TeamId.Home);
						break;
					case "GOAL2":
						match.Goal(TeamId.Visitors);
						break;
				}
			}

            return match;
        }
    }
}