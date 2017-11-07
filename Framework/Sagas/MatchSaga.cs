using LiveScoreEs.Backend.DAL;
using LiveScoreEs.Framework.Commands;
using LiveScoreEs.Framework.Events;

namespace LiveScoreEs.Framework.Sagas
{
	public class MatchSaga : SagaBase<MatchData>,
		IStartWithMessage<MatchStartedEvent>,
		ICanHandleMessage<PeriodStartedEvent>,
		ICanHandleMessage<EndMatchCommand>
	{
		private readonly EventRepository _repo = new EventRepository();

		public void Handle(MatchStartedEvent message)
		{
			// This code only serves the purpose of the RavenDB example here. 
			_repo.BeginHistory(message.MatchId);
			// Persist the event
			_repo.Save(message).Commit();

			// Set the ID of the saga
			SagaId = message.MatchId;
		}

		public void Handle(PeriodStartedEvent message)
		{
			_repo.Save(message);
			//_repo.RecordEvent(message.MatchId, "NewPeriod");
		}

		public void Handle(EndMatchCommand message)
		{

		}
	}
}