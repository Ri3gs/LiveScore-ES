using System;

namespace LiveScore.QueryStack
{
	public class LiveMatch
	{
		public LiveMatch()
		{
			Id = "";
			State = MatchState.ToBePlayed;
			IsBallInPlay = false;
			CurrentPeriod = 0;
			Team1 = "Home";
			Team2 = "Visitors";
		}

		public String Id { get; set; }
		public String Team1 { get; set; }
		public String Team2 { get; set; }
		public int TotalGoals1 { get; set; }
		public int TotalGoals2 { get; set; }
		public Boolean IsBallInPlay { get; set; }
		public Int32 CurrentPeriod { get; set; }
		public Int32 TimeInPeriod { get; set; }
		public MatchState State { get; set; }
	}
}