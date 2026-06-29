using System.ComponentModel.DataAnnotations;
using VatroApi.V1.Models;

namespace VatroApi.V1.Dto.Client
{
    public class PostClientDto
    {
        [Required]
        public string Name { get; set; } = String.Empty;
        [Required]
        public string? City { get; set; } = String.Empty;
        public string? Address { get; set; } = String.Empty;
        public string? Email { get; set; } = String.Empty;
        public string? Phone { get; set; } = String.Empty;
        public string? Phone2 { get; set; } = String.Empty;
        public string? Note { get; set; } = String.Empty;
        public bool Referent { get; set; } = false;
        public bool Archived { get; set; } = false;
        // public DateTime Date { get; set; } = DateTime.UtcNow;
    }
}