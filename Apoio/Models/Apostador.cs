using System.ComponentModel.DataAnnotations;

namespace Apoio.Models
{
    public class Apostador
    {
        public int Id { get; set; }
        public string  Nome { get; set; }
        public string Email { get; set; }
        public decimal? Saldo { get; set; }


    }
}
