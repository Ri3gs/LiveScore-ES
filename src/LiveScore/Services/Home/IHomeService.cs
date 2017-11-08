using LiveScoreEs.ViewModels.Home;
using System;

namespace LiveScoreEs.Services.Home
{
	public interface IHomeService
	{
		void DispatchCommand(String matchId, String eventName);
		MatchViewModel GetCurrentState(String matchId);
	}
}