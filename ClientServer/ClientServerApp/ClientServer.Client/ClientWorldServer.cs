using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using ClientServer.Contracts;
using Newtonsoft.Json;

namespace ClientServer.Client
{
	public class WorldServerClient
	{
		public static event Action<string> Output;

		private static object _locker = new object();

		private const int port = 8888;
		private const string server = "127.0.0.1";

		public static void ConnectClient()
		{
			try
			{
				TcpClient client = new TcpClient();
				client.Connect(server, port);

				lock (_locker)
				{

					NetworkStream stream = client.GetStream();

					var task =
						SendMessage(stream, JsonConvert.SerializeObject(new ClientMessage
						{
							Id = Guid.NewGuid().ToString(),
							Message = "kek",
						})).GetAwaiter();

					task.OnCompleted(() => Task.Run(() => ListenToServer(stream)));
				}
			}
			catch (SocketException e)
			{
				Output?.Invoke($"SocketException: {e}");
			}
			catch (Exception e)
			{
				Output?.Invoke($"Exception: {e.Message}");
			}

			Output?.Invoke("Запрос завершен...");
		}

		private static async Task SendMessage(NetworkStream stream, string msg)
		{
			// преобразуем сообщение в массив байтов
			byte[] data = Encoding.UTF8.GetBytes(msg);

			// отправка сообщения
			await stream.WriteAsync(data, 0, data.Length);
			Output?.Invoke($"Отправлено сообщение: {msg}");
		}


		private static async Task ListenToServer(NetworkStream stream)
		{

			while (true)
			{
				byte[] data = new byte[256];
				StringBuilder response = new StringBuilder();

				do
				{
					int bytes = stream.Read(data, 0, data.Length);
					response.Append(Encoding.UTF8.GetString(data, 0, bytes));
				}
				while (stream.DataAvailable);

				var resp =
					JsonConvert.DeserializeObject<ServerMessage>(response.ToString());
			}
		}
	}
}
