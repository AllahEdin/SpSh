using System;

namespace ClientServer.Contracts
{
	public class ClientMessage
	{
		public string Id { get; set; }
		public string Message { get; set; }
	}

	public class ServerMessage
	{
		public string Message { get; set; }
	}
}
