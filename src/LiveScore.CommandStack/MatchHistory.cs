using System;
using System.Collections.Generic;

namespace LiveScore.CommandStack
{
	public class MatchHistory
	{
		public static MatchHistory New(String id)
		{
			return new MatchHistory(id);
		}

		public MatchHistory(String id)
		{
			Id = id;
			Records = new List<String>();
		}

		public String Id { get; }
		public IList<String> Records { get; }
	}
}