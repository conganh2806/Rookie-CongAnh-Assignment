namespace ECommerce.Application.DTOs.Common
{
    public class MinioSettings
    {
        public string BucketName { get; set; } = string.Empty;
        public string Endpoint { get; set; } = string.Empty;
        public string AccessKey { get; set; } = string.Empty;
        public string SecretKey { get; set; } = string.Empty;
        public int ExpiredHoursPresignUrl { get; set; }
    }
}
