using System;

namespace LiveScoreEs.Framework
{
	public class DomainEvent : Message
	{
		public DateTime TimeStamp { get; }
		public string EventName { get; }

		public DomainEvent(string eventName)
		{
			EventName = eventName;
			TimeStamp = DateTime.Now;
		}
	}

}