namespace Footstep.Infrastructure.Settings
{
    public class S3Settings
    {
        public string Region { get; init; } = string.Empty;
        public string BucketName { get; init; } = string.Empty;
        public string AccessKey { get; init; } = string.Empty;
        public string SecretKey { get; init; } = string.Empty;
    }
}
