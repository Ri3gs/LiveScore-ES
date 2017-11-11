using System;

namespace LiveScore.Framework
{
	public class DomainEvent : Message
	{
		public DateTime TimeStamp { get; }

		public DomainEvent()
		{
			TimeStamp = DateTime.Now;
		}
	}

}