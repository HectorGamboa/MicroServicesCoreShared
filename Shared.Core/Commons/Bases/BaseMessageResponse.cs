namespace Shared.Core.Commons.Bases
{
    public class BaseMessageResponse
    {
        public bool IsSuccess { get; set; }
        public string? Title { get; set; } = "Operación exitosa"; // Titulo: "Operación exitosa
        public string? Message { get; set; }
        public int StatusCode { get; set; } = 200; // Código de estado HTTP: 200
    }
}
