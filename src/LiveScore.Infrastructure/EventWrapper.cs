using LiveScore.Framework;

namespace LiveScore.Infrastructure
{
	public class EventWrapper
	{
		public EventWrapper(DomainEvent theEvent)
		{
			TheEvent = theEvent;
		}
		public DomainEvent TheEvent { get; }
	}
}