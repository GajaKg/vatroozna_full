using Microsoft.VisualBasic;
using VatroApi.V1.Models;

namespace VatroApi.V1.Entities
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public string? City { get; set; } = String.Empty;
        public string? Address { get; set; } = String.Empty;
        public string? Email { get; set; } = String.Empty;
        public string? Phone { get; set; } = String.Empty;
        public string? Phone2 { get; set; } = String.Empty;
        public string? Note { get; set; } = String.Empty;
        public bool Referent { get; set; } = false;
        public bool Archived { get; set; } = false;

        public ICollection<Control> Controls = [];
    }
}