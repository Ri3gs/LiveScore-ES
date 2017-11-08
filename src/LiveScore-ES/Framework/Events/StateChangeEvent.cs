namespace LiveScoreEs.Framework.Events
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