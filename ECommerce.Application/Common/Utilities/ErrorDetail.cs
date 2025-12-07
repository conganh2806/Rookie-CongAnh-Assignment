using System.Text.Json;

namespace ECommerce.Common.Utilities
{
    public class ErrorDetails
    {
        public int StatusCode { get; set; }
        public string Message { get; set; } = default!;
        public List<ValidationError> ValidationErrors { get; set; } = new List<ValidationError>();

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
