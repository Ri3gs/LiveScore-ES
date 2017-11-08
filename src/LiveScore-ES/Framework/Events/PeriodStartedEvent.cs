using System;

namespace LiveScoreEs.Framework.Events
{
	public class PeriodStartedEvent : DomainEvent
	{
		public String MatchId { get; }

		public PeriodStartedEvent(String matchId) : base("NewPeriod")
		{
			MatchId = matchId;

			// Command class has SagaId property used to find saga the message relates to. 
			// (Simpler schema than more general ConfigureHowToFindSaga method in NServiceBus)
			SagaId = matchId;
		}
	}
}