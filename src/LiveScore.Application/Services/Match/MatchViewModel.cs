namespace LiveScore.Application.Services.Match
{
	public class MatchViewModel
	{
		public MatchViewModel(CommandStack.Match m)
		{
			CurrentMatch = m;
		}
		public CommandStack.Match CurrentMatch { get; }
	}
}