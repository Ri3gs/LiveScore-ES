using LiveScoreEs.Backend.ReadModel.Dto;
using System;

namespace LiveScoreES.Application.Services.Live
{
	public interface ILiveControllerService
	{
		LiveMatch GetLiveMatch(String matchId);
	}
}