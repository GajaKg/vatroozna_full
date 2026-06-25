using VatroApi.V1.Entities;

namespace VatroApi.V1.Models
{
    public class Control
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public string Subject { get; set; } = String.Empty;
        public string Duration { get; set; } = String.Empty;
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public DateTime NextCheck { get; set; }
        public string? Note { get; set; } = String.Empty;
        public bool Archive { get; set; } = false;

        public Client Client { get; set; } = null!;
    }
}