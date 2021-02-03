using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading;
using System.Threading.Tasks;
using CalculationLib;
using Newtonsoft.Json;

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
		private static bool _serverStarted = false;
		private static IPAddress _ip;


		static void Main(string[] args)
		{

			//var client2 =
			//	Task.Run(async () =>
			//	{
			//		await Task.Delay(TimeSpan.FromSeconds(1));
			//		var cl = new Client1();

			//		await Task.Delay(TimeSpan.FromSeconds(1));
			//		cl.Connect(ip);
			//	});

			SwitchIp();

			Console.WriteLine($"s - start server {Environment.NewLine} c - start client {Environment.NewLine} ip - reset ip");

			while (true)
			{
				var command =
					Console.ReadLine();

				if (command == "s")
				{
					if (!_serverStarted)
					{
						_serverStarted = true;

						var server =
							Task.Run(Server1.Start);

						//string _ip =
						//	//"192.168.1.102";
						//	"109.124.225.96";
						////Console.ReadLine();
					}
				}

				if (command == "ip")
				{
					SwitchIp();
				}
				if (command == "c")
				{
					if (string.IsNullOrEmpty(_ip?.ToString() ?? ""))
					{
						Console.WriteLine("Ip is empty");
					}
					else
					{
						var client =
							Task.Run(async () =>
							{
								var cl = new Client1();

								await Task.Delay(TimeSpan.FromSeconds(1));
								cl.Connect(_ip.ToString());
							});
					}
				}

				//ProcessCommand(command);
			}


			FileSource source =
				new FileSource("1.txt");

			TableParser parser =
				new TableParser(source);

			switch (parser.Calculate())
			{
				case CalcResultString calcResultString:
					//Console.WriteLine(calcResultString.ResultString);
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}



			/*
			System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)Thread.CurrentThread.CurrentCulture.Clone();
			customCulture.NumberFormat.NumberDecimalSeparator = ".";

			Thread.CurrentThread.CurrentCulture = customCulture;

			int maxX = Convert.ToInt32(Console.ReadLine());

			int maxY = Convert.ToInt32(Console.ReadLine());

			int count = Convert.ToInt32(Console.ReadLine());

			int pointCount = Convert.ToInt32(Console.ReadLine());

			Random r = new Random();

			List<Point> list =
				new List<Point>();

			for (int i = 0; i < count; i++)
			{
				Thread.Sleep(TimeSpan.FromSeconds(0.3f));
				var x = r.NextDouble() * maxX;
				var y = r.NextDouble() * maxY;

				list.Add(new Point(i)
				{
					X = x,
					Y = y,
				});
			}

			foreach (var point in list)
			{
				point.GetNextRanges(list.Where(t => t.Id != point.Id).ToArray());
			}

			File.Delete("1.txt");

			using (StreamWriter sw =
				new StreamWriter(File.OpenWrite("1.txt")))
			{
				string s = "";

				foreach (var point in list)
				{
					for (int i = 0; i < pointCount; i++)
					{
						var cur =
							point.GetInLowestRange(maxX, maxY);

						s += $"[{Math.Round(cur.X, 1)},{Math.Round(cur.Y, 1)}],";
					}
				}

				s = s.Remove(s.Length - 1);

				s = "[" + s + "]";

				var s2 = "[";

				foreach (var point in list)
				{
					for (int i = 0; i < pointCount; i++)
					{
						s2 += $"{point.Id},";
					}
				}

				s2 = s2.Remove(s2.Length - 1);

				s2 += "]";

				sw.Write(s);

				sw.WriteLine();

				sw.Write(s2);
			}
			*/

			var g = Guid.NewGuid().ToString();

			var res =
				JsonConvert.DeserializeObject<string>(g);

			Console.ReadKey();
		}

		private static void SwitchIp()
		{
			if (File.Exists("1.txt"))
			{
				using (var r = new StreamReader(File.OpenRead("1.txt")))
				{
					var a = r.ReadLine();

					if (IPAddress.TryParse(a, out var ip))
					{
						_ip = ip;

						Console.WriteLine($"Ip has successfully switched to {_ip.ToString()}");
					}
					else
					{
						Console.WriteLine($"Ip has incorrect format");
					}
				}
			}
			else
			{
				Console.WriteLine("Create 1.txt file w/ ip then enter \"ip\" command");
			}
		}


		private class Point
		{
			public int Id { get; }

			public double X { get; set; }

			public double Y { get; set; }

			public double[] Ranges { get; private set; }

			public Point(int id)
			{
				Id = id;
			}

			public void GetNextRanges(Point[] points)
			{
				Ranges = points.Select(t => Math.Sqrt(Math.Pow(X - t.X, 2) + Math.Pow(Y - t.Y, 2))).ToArray();
			}

			public Point GetInLowestRange(int maxY, int maxX)
			{
				Thread.Sleep(TimeSpan.FromSeconds(0.3f));
				Random r = new Random();

				var minRange =
					Ranges.Min()/2;

				var borders =
					new[]
					{
						X,
						Y,
						maxX - X,
						maxX - Y
					};

				minRange =
					minRange <= borders.Min()
						? minRange
						: borders.Min();

					var dx = r.NextDouble() * minRange;

				var a = r.Next(0, 360) * Math.PI / 360;
				
				var x =
					X - dx * Math.Sin(a);
				
				var y =
					Y + dx * Math.Cos(a);

				return new Point(-1)
				{
					X = x,
					Y = y,
				};
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
