using System;

namespace MessageLib.DTO
{
	[Serializable]
	public abstract class MessageBase
	{
		public abstract string Key { get;}

		public abstract string Message { get; set; }
	}
}
