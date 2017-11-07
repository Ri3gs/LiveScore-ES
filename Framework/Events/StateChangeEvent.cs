using LiveScoreEs.Framework;

namespace LiveScoreEs.Framework.Events
{
	public class StateChangeEvent : DomainEvent
	{
		private string id;
		private string eventName;

		public StateChangeEvent(string id, string eventName)
		{
			this.id = id;
			this.eventName = eventName;
		}
	}
}