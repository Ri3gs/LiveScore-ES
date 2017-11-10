using LiveScore.CommandStack.Events;
using LiveScore.CommandStack.Sagas;
using LiveScore.Framework;
using LiveScore.Infrastructure;

namespace LiveScore.Web
{
	public class BusConfig
	{
		public static void Initialize()
		{
			Bus.RegisterSaga<MatchStartedEvent, MatchSaga>();
			Bus.EventRepository = new EventRepository();
		}
	}
}