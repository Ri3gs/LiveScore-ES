using LiveScoreEs.Backend.ReadModel;
using LiveScoreEs.Framework;
using System;
using System.Collections.Generic;

namespace LiveScoreEs.Backend.DAL
{
	public interface IEventRepository
	{
		EventRepository Save(DomainEvent domainEvent);
		void RecordEvent(String id, String eventName);
		MatchHistory BeginHistory(String matchId);
		MatchHistory GetHistory(String matchId);
		void UndoLastAction(String id);
		IList<DomainEvent> GetEventStreamFor(String id);
		void Commit();
		void Empty(String id);
	}
}