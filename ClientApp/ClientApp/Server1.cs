using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using MessageLib.DTO;
using Newtonsoft.Json;

namespace ClientApp
{

	public class ClientData
	{
		public TcpClient Client { get; set; }
	}

	public static class Server1
	{
		private static object locker = new object();

		private static int _num;

		public static void Start()
		{

			TcpListener server = null;
			try
			{
				// Set the TcpListener on port 13000.
				Int32 port = 1010;
				IPAddress localAddr = IPAddress.Parse("127.0.0.1");

				IPHostEntry ipHost = Dns.GetHostEntry("127.0.0.1");
				var ipv4 = ipHost.AddressList.ToList().Find((e) => e.AddressFamily == AddressFamily.InterNetwork);
				Console.WriteLine(ipv4?.ToString() ?? "null");

				// TcpListener server = new TcpListener(port);
				server = new TcpListener(IPAddress.Any, port);

				// Start listening for client requests.
				server.Start();

				while (true) // Add your exit flag here
				{
					TcpClient client = server.AcceptTcpClient();
					ThreadPool.QueueUserWorkItem(ThreadProc, client);
				}

			}
			catch (SocketException e)
			{
				Console.WriteLine("SocketException: {0}", e);
			}
			finally
			{
				// Stop listening for new clients.
				server?.Stop();
			}

			Console.WriteLine("\nHit enter to continue...");
		}

		private static void ThreadProc(object obj)
		{
			var client = (TcpClient)obj;

			var clientListener =
				new ClientListener(client, _num);

			lock (locker)
			{
				_num++;
			}
		}

		public static void SentToDirectClient(int clientId, string msg)
		{
			//if (list.TryGetValue(clientId, out var client))
			//{
			//	NetworkStream stream = client.Client.GetStream();

			//	var byteMsg =
			//		System.Text.Encoding.ASCII.GetBytes(msg);
			//	stream.Write(byteMsg, 0, msg.Length);
			//	Console.WriteLine("Sent: {0} only for user {1}", msg, clientId);
			//}
		}
	}


	internal class ClientListener
	{
		private readonly TcpClient _client;
		private readonly int _num;

		public ClientListener(TcpClient client,
			int num)
		{
			_client = client;
			_num = num;

			Task.Factory.StartNew(() => { Start(); });
		}

		private void Start()
		{
			// Buffer for reading data
			Byte[] bytes = new Byte[256];
			String data = null;


			data = null;

			// Get a stream object for reading and writing
			NetworkStream stream = _client.GetStream();

			int i;

			// Loop to receive all the data sent by the client.
			while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
			{
				// Translate data bytes to a ASCII string.
				data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
				Console.WriteLine("Server received: {0}", data);

				// Process the data sent by the client.
				data = data.ToUpper();

				byte[] msg = System.Text.Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(new InitialMessage()
				{
					Message = _num.ToString()
				}));

				// Send back a response.
				stream.Write(msg, 0, msg.Length);
				Console.WriteLine("Server sent: {0}", data);
			}
		}
	}
}
