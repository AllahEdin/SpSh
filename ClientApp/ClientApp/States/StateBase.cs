using System;
using System.Data.SqlClient;
using MessageLib.DTO;
using Newtonsoft.Json;

namespace ClientApp.States
{
	public abstract class StateBase
	{
		public abstract string Key { get; set; }

		public abstract void Start();
	}

	public abstract class ClientStateBase<TAwaitingMessage> : StateBase
	where TAwaitingMessage : MessageBase
	{
		protected readonly Client1 Client;
		protected string Id = Guid.NewGuid().ToString();

		public StateBase NextState { get; set; }

		public ClientStateBase( Client1 сlient) : base()
		{
			Client = сlient;
		}

		public override void Start()
		{
			Client.OnGotMessage += OnGotMessage;
		}

		private void OnGotMessage()
		{
			try
			{
				var msg =
					Client.Messages.Dequeue();

				var received =
					JsonConvert.DeserializeObject<TAwaitingMessage>(msg);

				Console.WriteLine(received.Message);
			}
			catch (Exception e)
			{
				if(OnGotWrongMessage())
					return;
			}

			OnGotRightMessage();

		}

		protected virtual void OnGotRightMessage()
		{
			Client.OnGotMessage -= OnGotMessage;

			NextState?.Start();
		}


		protected virtual bool OnGotWrongMessage()
		{
			return true;
		}
		
		
	}
}