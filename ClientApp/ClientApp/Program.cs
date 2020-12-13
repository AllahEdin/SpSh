using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;

namespace ClientApp
{
	public enum State
	{
		WaitForCommand,
		WaitForUserId
	}

	class Program
	{
		private static State _state = State.WaitForCommand;

		static void Main(string[] args)
		{
			var server =
				Task.Run(Server1.Start);

			string ip =
				"109.124.230.186";
				//Console.ReadLine();

			var client =
				Task.Run(async () =>
				{
					await Task.Delay(TimeSpan.FromSeconds(1));
					Client1.Connect(ip, "shit");
				});

			var client2 =
				Task.Run(async () =>
				{
					await Task.Delay(TimeSpan.FromSeconds(1));
					Client1.Connect(ip, "shit2");
				});


			while (true)
			{
				var command =
					Console.ReadLine();
				
				ProcessCommand(command);
			}
		}


		private static void ProcessCommand(string command)
		{
			switch (_state)
			{
				case State.WaitForCommand:

					switch (command)
					{
						case "send":
						{
							_state = State.WaitForUserId;
							break;
						}
					}

					break;
				case State.WaitForUserId:

					var userId =
						Convert.ToInt32(command);

					Console.WriteLine("user id:");

					Server1.SentToDirectClient(userId, "chlen");
					
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}


			

		}

	}


// State object for receiving data from remote device.  
}
