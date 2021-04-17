using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ClientServer.Contracts;
using Newtonsoft.Json;

namespace ClientServer.Server
{
	public static class WorldServer
	{
		public static event Action<string> Output; 

		const int port = 8888; // порт для прослушивания подключений

		private static ConcurrentDictionary <string, TcpClient> _clients =
				new ConcurrentDictionary<string, TcpClient>();

		private static object _locker = new object();

		public static void StartServer()
		{
			TcpListener server = null;
			try
			{
				IPEndPoint ep =
					new IPEndPoint(IPAddress.Any, port);
				server = new TcpListener(ep);

				// запуск слушателя
				server.Start();

				while (true)
				{
					Output?.Invoke("Ожидание подключений... ");

					lock (_locker)
					{
						// получаем входящее подключение
						TcpClient client = server.AcceptTcpClient();

						Task.Run(() => ClientConnected(client));
					}
				}
			}
			catch (Exception e)
			{
				Output?.Invoke(e.Message);
			}
			finally
			{
				if (server != null)
					server.Stop();
			}
		}

		private static void ClientConnected(TcpClient client)
		{
			Output?.Invoke("Подключен клиент. Выполнение запроса...");
			// получаем сетевой поток для чтения и записи
			NetworkStream stream = client.GetStream();


			byte[] data = new byte[256];
			StringBuilder response = new StringBuilder();

			do
			{
				int bytes = stream.Read(data, 0, data.Length);
				response.Append(Encoding.UTF8.GetString(data, 0, bytes));
			}
			while (stream.DataAvailable);

			var resp =
				JsonConvert.DeserializeObject<ClientMessage>(response.ToString());

			ReaderWriterLockSlim rwl =
				new ReaderWriterLockSlim();

			lock(_locker)
			{
				_clients.AddOrUpdate(resp?.Id ?? "Error", client, (s, tcpClient) => throw new InvalidOperationException());
				Task.Run(() => ListenToClient(stream));
			}
		}

		private static async Task SendMessage(NetworkStream stream, string msg)
		{
			// преобразуем сообщение в массив байтов
			byte[] data = Encoding.UTF8.GetBytes(msg);

			// отправка сообщения
			await stream.WriteAsync(data, 0, data.Length);
			Output?.Invoke($"Отправлено сообщение: {msg}");
		}

		private static async Task ListenToClient(NetworkStream stream)
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
					JsonConvert.DeserializeObject<ClientMessage>(response.ToString());
			}
		}
	}
}
