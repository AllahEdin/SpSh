using System;
using System.Threading.Tasks;
using MessageLib.DTO;

namespace ClientApp.States
{
	public class HealthCheckState : ClientStateBase<HealthCheckMessage>
	{
		public override string Key { get; set; } =
			"HealthCheck";

		public override void Start()
		{
			base.Start();

			Task.Factory.StartNew(async () =>
			{
				await Task.Delay(TimeSpan.FromSeconds(3));

				Client.SendMessage(new HealthCheckMessage()
				{
					Message = "health check"
				});

			});
		}

		public HealthCheckState( Client1 сlient) : base(сlient)
		{
		}
	}

}