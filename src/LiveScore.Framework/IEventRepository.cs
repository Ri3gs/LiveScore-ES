using System;
using System.Collections.Generic;

namespace LiveScore.Framework
{
	public interface IEventRepository
	{
		IEventRepository Save(DomainEvent domainEvent);
		void RecordEvent(String id, String eventName);
		void BeginHistory(String matchId);
		void UndoLastAction(String id);
		IList<DomainEvent> GetEventStreamFor(String id);
		void Commit();
		void Empty(String id);
	}
}