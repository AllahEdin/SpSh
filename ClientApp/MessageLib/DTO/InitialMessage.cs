namespace MessageLib.DTO
{
	public class InitialMessage : MessageBase
	{
		public override string Key { get; } = "Init";
		public override string Message { get; set; }
	}
}