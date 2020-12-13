using System;
using System.ComponentModel;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace ClientApp
{
	public class Client1
	{
		private int _id = 0;

		private BackgroundWorker listenerWorker =
			new BackgroundWorker();

		private TcpClient _client;

		public void Connect(string server)
		{
			try
			{
				// Create a TcpClient.
				// Note, for this client to work you need to have a TcpServer
				// connected to the same address as specified by the server, port
				// combination.
				Int32 port = 1010;
				TcpClient client = new TcpClient(server, port);



				

			

				// Receive the TcpServer.response.

				// Buffer to store the response bytes.
				data = new Byte[256];

				// String to store the response ASCII representation.
				String responseData = String.Empty;

				// Read the first batch of the TcpServer response bytes.
				Int32 bytes = stream.Read(data, 0, data.Length);
				responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);

				_id = Convert.ToInt32(responseData);

				Console.WriteLine("Received: {0}", responseData);

				// Close everything.
				stream.Close();
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
			Console.Read();
		}

		private void SendMessage(string message)
		{
			Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);
			NetworkStream stream = client.GetStream();
			stream.Write(data, 0, data.Length);

			Console.WriteLine("Sent: {0}", message);
		}

		private async Task ReceiveMessage()
		{

		}
    }
}