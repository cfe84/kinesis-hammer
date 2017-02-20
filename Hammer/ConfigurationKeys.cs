namespace Hammer
{
	public static class ConfigurationKeys
	{
		public static string Region { get; } = "AwsRegion";
		public static string AccessKeyId { get; } = "AwsAccessKeyId";
		public static string AccessKeySecret { get; } = "AwsAccessKeySecret";
		public static string KinesisStreamName { get; } = "KinesisStreamName";
		public static string FirehoseStreamName { get; } = "FirehoseStreamName";
	}
}
