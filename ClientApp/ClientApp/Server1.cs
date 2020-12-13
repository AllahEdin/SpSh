using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace ClientApp
{

	public class ClientData
	{
		public TcpClient Client { get; set; }
	}

	public static class Server1
	{
		private static Dictionary<int, ClientData> list =
			new Dictionary<int, ClientData>();

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

				// Buffer for reading data
				Byte[] bytes = new Byte[256];
				String data = null;

				// Enter the listening loop.
				while (true)
				{
					Console.WriteLine("Waiting for a connection... ");

					// Perform a blocking call to accept requests.
					// You could also use server.AcceptSocket() here.
					TcpClient client = server.AcceptTcpClient();
					Console.WriteLine("Connected!");

					data = null;

					// Get a stream object for reading and writing
					NetworkStream stream = client.GetStream();

					int i;

					// Loop to receive all the data sent by the client.
					while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
					{
						// Translate data bytes to a ASCII string.
						data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
						Console.WriteLine("Received: {0}", data);

						// Process the data sent by the client.
						data = data.ToUpper();

						int number = list.Count;

						list.Add(number, new ClientData()
						{
							Client = client,
						});

						byte[] msg = System.Text.Encoding.ASCII.GetBytes(number.ToString());

						// Send back a response.
						stream.Write(msg, 0, msg.Length);
						Console.WriteLine("Sent: {0}", data);
					}

					// Shutdown and end connection
					//client.Close();
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


		public static void SentToDirectClient(int clientId, string msg)
		{
			if (list.TryGetValue(clientId, out var client))
			{
				NetworkStream stream = client.Client.GetStream();

				var byteMsg =
					System.Text.Encoding.ASCII.GetBytes(msg);
				stream.Write(byteMsg, 0, msg.Length);
				Console.WriteLine("Sent: {0} only for user {1}", msg, clientId);
			}
		}
	}
}
