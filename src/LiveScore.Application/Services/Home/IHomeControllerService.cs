using LiveScoreEs.ViewModels.Home;
using System;

namespace LiveScoreES.Application.Services.Home
{
	public interface IHomeControllerService
	{
		void DispatchCommand(String matchId, String eventName);
		MatchViewModel GetCurrentState(String matchId);
	}
}