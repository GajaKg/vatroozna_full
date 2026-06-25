
namespace VatroApi.V1.Dto.Control
{
    public class ControlDto
    {
        public int Id { get; set; }
        public string Subject { get; set; } = String.Empty;
        public string Duration { get; set; } = String.Empty;
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public DateTime NextCheck { get; set; }
        public string? Note { get; set; } = String.Empty;
        public bool Archive { get; set; } = false;

        // public ClientDto Client = null!;
    }
}