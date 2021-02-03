using System;
using MessageLib.DTO;

namespace ClientApp.States
{
	public class InitialState : ClientStateBase<InitialMessage>
	{
		public override string Key
		{ get; set; } = "Init";

		public override void Start()
		{
			base.Start();

			Client.SendMessage(new InitialMessage()
			{
				Message = Id
			});
		}

		public InitialState( Client1 сlient) : base( сlient)
		{
		}
	}
}