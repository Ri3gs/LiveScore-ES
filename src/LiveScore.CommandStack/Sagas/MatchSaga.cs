using LiveScore.CommandStack.Commands;
using LiveScore.CommandStack.Events;
using LiveScore.Framework;

namespace LiveScore.CommandStack.Sagas
{
	public class MatchSaga : SagaBase<MatchData>,
		IStartWithMessage<MatchStartedEvent>,
		ICanHandleMessage<PeriodStartedEvent>,
		ICanHandleMessage<HomeScoredGoalEvent>,
		ICanHandleMessage<VisitorsScoredGoalEvent>,
		ICanHandleMessage<PeriodEndedEvent>,
		ICanHandleMessage<EndMatchCommand>
	{
		private readonly IEventRepository _repo;

		public MatchSaga(IEventRepository repo)
		{
			_repo = repo;
		}

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
		}

		public void Handle(HomeScoredGoalEvent message)
		{
			_repo.Save(message);
		}

		public void Handle(VisitorsScoredGoalEvent message)
		{
			_repo.Save(message);
		}

		public void Handle(PeriodEndedEvent message)
		{
			_repo.Save(message);
		}

		public void Handle(EndMatchCommand message)
		{

		}
	}
}