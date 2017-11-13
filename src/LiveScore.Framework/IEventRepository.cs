using System;
using System.Collections.Generic;

namespace LiveScore.Framework
{
	public interface IEventRepository
	{
		void Save(DomainEvent domainEvent);
		void UndoLastAction(String id);
		IList<DomainEvent> GetEventStreamFor(String id);
		void Empty(String id);
	}
}