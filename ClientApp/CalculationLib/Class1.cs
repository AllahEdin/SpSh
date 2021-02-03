using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace CalculationLib
{
	public abstract class ParserBase
	{
		protected SourseBase _sourse;

		public ParserBase(SourseBase s)
		{
			_sourse = s;
		}

		public abstract CalcResultBase Calculate();
	}

	public abstract class SourseBase
	{
		public abstract string GetString(long length);
	}

	public abstract class CalcResultBase
	{
		public CalcResultBase(object o)
		{
			Result = o;
		}

		public object Result { get; }
	}



	#region inputstr

	public class FileSource : SourseBase
	{
		private string _path;
		private Stream _stream;
		private int _curPos = 0;

		public FileSource(string path)
		{
			_path = path;

			_stream =
				File.OpenRead(path);
		}

		public override string GetString(long length)
		{
			if (length < 0)
			{
				length = _stream.Length;
			}

			byte[] byteArray =
				new byte[length];

			_stream.Read(byteArray, 0, Convert.ToInt32(length));

			var result = Encoding.UTF8.GetString(byteArray);

			return result;
		}
	}

	public class TableParser : ParserBase
	{
		

		public TableParser(SourseBase s) : base(s)
		{
		}

		public override CalcResultBase Calculate()
		{
			var str =
				_sourse.GetString(-1);

			Regex regex =
				new Regex("\\w*<tr>.*</tr>" , RegexOptions.IgnoreCase);

			var res =
				regex.Matches(str);

			string resStr = "";

			foreach (Match match in res)
			{
				Console.WriteLine(match.Value);

				resStr +=
					$"{match.Value} AAAA blya est gi {Environment.NewLine}";
			}

			return new CalcResultString(resStr);
		}
	}

	public class CalcResultString : CalcResultBase
	{
		public CalcResultString(string str) : base(str)
		{
		}

		public string ResultString =>
			Result.ToString();
	}

	#endregion


}
