using System;
using System.Management.Instrumentation;

namespace ServerApp
{
	class Program
	{
		static void Main(string[] args)
		{
			//TCPServer server =
			//	new TCPServer();
			//server.Start();

			//Console.ReadKey();

			AsynchronousSocketListener.StartListening();

		}
	}
}
