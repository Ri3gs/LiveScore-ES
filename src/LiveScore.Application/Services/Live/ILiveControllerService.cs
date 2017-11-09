using System;
using LiveScore.QueryStack;

namespace LiveScore.Application.Services.Live
{
	public interface ILiveControllerService
	{
		LiveMatch GetLiveMatch(String matchId);
	}
}