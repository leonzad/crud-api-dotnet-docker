using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("users")]
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Column("nome")]
        public required string Nome { get; set; }
        [Column("cpf")]
        public required string Cpf { get; set; }
        [Column("email")]
        [EmailAddress]
        public required string Email { get; set; }
        [Column("dt_nasc")]
        public DateTime DataNasc { get; set; }
    }
}