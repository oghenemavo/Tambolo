using System.Net;

namespace Tambolo.Dtos
{
    public class TamboloResponse
    {
        public HttpStatusCode Status { get; set; } = HttpStatusCode.OK;
        public bool IsSuccess { get; set; } = true;
        public List<string>? Message { get; set; }
        public object? Data { get; set; }
    }
}
