using System;

namespace LiveScore.Application.Services.Match
{
	public interface IMatchControllerService
	{
		void DispatchCommand(String matchId, String eventName);
		MatchViewModel GetCurrentState(String matchId);
	}
}