using System;
using System.Collections.Generic;

namespace LiveScore.Framework
{
	public interface IEventRepository
	{
		void Save(DomainEvent domainEvent);
		void UndoLastAction(String id);
		IReadOnlyCollection<DomainEvent> GetEventStreamFor(String id);
		void Empty(String id);
	}
}