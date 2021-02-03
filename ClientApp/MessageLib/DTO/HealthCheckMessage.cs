namespace MessageLib.DTO
{
	public class HealthCheckMessage : MessageBase
	{
		public override string Key { get; } = "HealthCheck";
		public override string Message { get; set; }
	}
}