using LiveScore.Framework;

namespace LiveScore.CommandStack.Events
{
	public class StateChangeEvent : DomainEvent
	{
		private string id;

		public StateChangeEvent(string id)
		{
			this.id = id;
		}
	}
}