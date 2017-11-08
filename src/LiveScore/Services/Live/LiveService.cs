using System;
using System.Linq;
using LiveScoreEs.Backend.DAL;
using LiveScoreEs.Backend.ReadModel.Dto;

namespace LiveScoreEs.Services.Live
{
	public class LiveService : ILiveService
	{
		private readonly WaterpoloContext _dbContext;

		public LiveService(WaterpoloContext dbContext)
		{
			_dbContext = dbContext;
		}

		public LiveMatch GetLiveMatch(String matchId)
		{
			var lm = (from m in _dbContext.Matches where m.Id == matchId select m).FirstOrDefault();
			if (lm == null)
				return new LiveMatch() { Id = matchId };
			return lm;
		}

	}
}