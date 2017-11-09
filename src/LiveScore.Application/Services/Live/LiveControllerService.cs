using System;
using System.Linq;
using LiveScore.Infrastructure;
using LiveScore.QueryStack;

namespace LiveScore.Application.Services.Live
{
	public class LiveControllerService : ILiveControllerService
	{
		private readonly WaterpoloContext _dbContext;

		public LiveControllerService(WaterpoloContext dbContext)
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