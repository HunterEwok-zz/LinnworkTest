using System.Runtime.Serialization;

namespace APIService.Models
{
    public class ErrorMessage
    {
        public string Code { get; set; }
        public string Message { get; set; }
    }
}