using System;
using System.Collections.Generic;
using System.Linq;
using LiveScore.CommandStack;
using LiveScore.Framework;
using Raven.Client;
using Raven.Client.Document;

namespace LiveScore.Infrastructure
{
	public class EventRepository : IEventRepository
	{
		private readonly IDocumentStore _documentStore;

		public EventRepository()
		{
			//TODO: this is a hack
			_documentStore = new DocumentStore
			{
				Url = "http://localhost:9666",
				DefaultDatabase = "LiveScore"
			}.Initialize();
		}

		public void Save(DomainEvent domainEvent)
		{
			using (var session = _documentStore.OpenSession())
			{
				MatchHistory history = session.Load<MatchHistory>(domainEvent.SagaId);
				if (history == null)
				{
					history = new MatchHistory(domainEvent.SagaId);
					session.Store(history);
				}

				var eventWrapper = new EventWrapper(domainEvent);
				session.Store(eventWrapper);
				string key = session.Advanced.GetDocumentId(eventWrapper);
				history.Records.Add(key);

				session.SaveChanges();
			}
		}

		public void UndoLastAction(String matchId)
		{
			using (var session = _documentStore.OpenSession())
			{
				MatchHistory matchHistory = session.Load<MatchHistory>(matchId);
				if (matchHistory == null)
				{
					return;
				}

				EventWrapper lastEvent = session.Load<EventWrapper>(matchHistory.Records).LastOrDefault();
				if (lastEvent == null)
				{
					return;
				}

				string key = session.Advanced.GetDocumentId(lastEvent);
				session.Delete(lastEvent);
				matchHistory.Records.Remove(key);

				session.SaveChanges();
			}
		}

		public IReadOnlyCollection<DomainEvent> GetEventStreamFor(String id)
		{
			using (var session = _documentStore.OpenSession())
			{
				var history = session.Load<MatchHistory>(id);
				if (history == null)
					return new List<DomainEvent>().AsReadOnly();

				IList<string> keys = history.Records;

				if (keys.Any(k => k == null))
					return new List<DomainEvent>().AsReadOnly();

				var list = session.Load<EventWrapper>(keys);
				return list.Select(eventWrapper => eventWrapper.TheEvent).ToList().AsReadOnly();
			}
		}

		public void Empty(String id)
		{
			using (var session = _documentStore.OpenSession())
			{
				MatchHistory history = session.Load<MatchHistory>(id);

				if (history == null)
				{
					return;
				}

				EventWrapper[] events = session.Load<EventWrapper>(history.Records);
				foreach (var @event in events)
				{
					if (@event != null)
					{
						session.Delete(@event);
					}
				}

				session.Delete(history);
				session.SaveChanges();
			}
		}
	}
}