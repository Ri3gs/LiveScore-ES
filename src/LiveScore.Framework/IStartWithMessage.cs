﻿namespace LiveScore.Framework
{
	public interface IStartWithMessage<in T> where T : Message
	{
		void Handle(T message);
		string SagaId { get; set; }
	}
}