using LiveScore.Framework;

namespace LiveScore.CommandStack.Events
{
	public class VisitorsScoredGoalEvent : DomainEvent
	{
		public string MatchId { get; }

		public VisitorsScoredGoalEvent(string matchId)
		{
			MatchId = matchId;
			SagaId = matchId;
		}
	}
}