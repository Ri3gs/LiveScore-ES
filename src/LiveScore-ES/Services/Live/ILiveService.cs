using LiveScoreEs.Backend.ReadModel.Dto;
using System;

namespace LiveScoreEs.Services.Live
{
	public interface ILiveService
	{
		LiveMatch GetLiveMatch(String matchId);
	}
}