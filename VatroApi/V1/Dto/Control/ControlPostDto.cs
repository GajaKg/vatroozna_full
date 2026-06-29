
using System.ComponentModel.DataAnnotations;

namespace VatroApi.V1.Dto.Control
{
    public class ControlPostDto
    {
        public int ClientId { get; set; }
        [Required]
        public string Subject { get; set; } = String.Empty;
        [Required]
        public string Duration { get; set; } = String.Empty;
        // public DateTime Date { get; set; } = DateTime.UtcNow;
        [Required]
        public DateTime NextCheck { get; set; }
        public string? Note { get; set; } = String.Empty;
        public bool Archive { get; set; } = false;
    }
}