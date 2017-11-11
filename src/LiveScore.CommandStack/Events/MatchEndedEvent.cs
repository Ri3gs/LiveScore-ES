using LiveScore.Framework;

namespace LiveScore.CommandStack.Events
{
	public class MatchEndedEvent : DomainEvent
	{
		public string MatchId { get; }

		public MatchEndedEvent(string matchId)
		{
			MatchId = matchId;
			SagaId = matchId;
		}
	}
}