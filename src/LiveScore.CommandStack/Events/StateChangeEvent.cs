using LiveScore.Framework;

namespace LiveScore.CommandStack.Events
{
	public class StateChangeEvent : DomainEvent
	{
		private string id;

		public StateChangeEvent(string id, string eventName) : base(eventName)
		{
			this.id = id;
		}
	}
}