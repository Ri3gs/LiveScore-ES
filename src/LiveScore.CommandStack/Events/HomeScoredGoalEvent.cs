using System;
using LiveScore.Framework;

namespace LiveScore.CommandStack.Events
{
	public class HomeScoredGoalEvent : DomainEvent
	{
		public String MatchId { get; }

		public HomeScoredGoalEvent(String matchId) : base("Goal1")
		{
			MatchId = matchId;

			// Command class has SagaId property used to find saga the message relates to. 
			// (Simpler schema than more general ConfigureHowToFindSaga method in NServiceBus)
			SagaId = matchId;
		}
	}
}