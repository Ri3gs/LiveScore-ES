using LiveScore.Framework;

namespace LiveScore.CommandStack.Events
{
	public class PeriodEndedEvent : DomainEvent
	{
		public string MatchId { get; }

		public PeriodEndedEvent(string matchId)
		{
			MatchId = matchId;
			SagaId = matchId;
		}
	}
}