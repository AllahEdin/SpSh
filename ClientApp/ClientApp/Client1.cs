using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using ClientApp.States;
using MessageLib.DTO;
using Newtonsoft.Json;

namespace ClientApp
{
	public delegate void EmptyDelegate();

	public class Client1
	{
		private int _id = 0;

		//private StateManager _stateManager;

		private BackgroundWorker listenerWorker =
			new BackgroundWorker();

		private TcpClient _client;

		private NetworkStream _stream;

		public Queue<string> Messages { get; set; } =
			new Queue<string>();

		public event EmptyDelegate OnGotMessage;

		public void Connect(string server)
		{
			try
			{
				Int32 port = 1010;
				_client = new TcpClient(server, port);
				_stream = _client.GetStream();

				Task.Run(ReceiveMessage);

				//_stateManager =
				//	new StateManager();

				var initState =
					new InitialState( this);

				var healthCheck =
					new HealthCheckState( this);

				initState.NextState =
					healthCheck;

				healthCheck.NextState =
					healthCheck;

				//_stateManager.AddToQueue(initState);

				initState.Start();

			}
			catch (ArgumentNullException e)
			{
				Console.WriteLine("ArgumentNullException: {0}", e);
			}
			catch (SocketException e)
			{
				Console.WriteLine("SocketException: {0}", e);
			}

			Console.WriteLine("\n Press Enter to continue...");
		}

		public void SendMessage(MessageBase message)
		{
			var sendData = JsonConvert.SerializeObject(message);
			Byte[] data = System.Text.Encoding.ASCII.GetBytes(sendData);
			_stream.Write(data, 0, data.Length);
			Console.WriteLine("Client sent: {0}", sendData);
		}

		public void ReceiveMessage()
		{
			Byte[] data = new Byte[256];

			int i;
				while ((i = _stream.Read(data, 0, data.Length)) != 0)
				{
					//Int32 bytes = _stream.Read(data, 0, data.Length);
					string dataStr = System.Text.Encoding.ASCII.GetString(data, 0, i);
					Console.WriteLine("Client received: {0}", dataStr);
					Messages.Enqueue(dataStr);
					OnGotMessage?.Invoke();
				}
			
		}
    }
}