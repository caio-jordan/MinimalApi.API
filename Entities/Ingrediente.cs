using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace MinimalApi.API.Entities
{
    public class Ingrediente
    {
        [Key]
        public int Id { get; set;}

        [Required]
        [MaxLength(200)]
        public required string Nome { get; set;}

        public ICollection<Prato> Pratos { get; set; } = new List<Prato>();

        public Ingrediente()
        {            
            
        }
        [SetsRequiredMembers]
        public Ingrediente(int id, string nome){
            Id = id;
            Nome = nome;
        }
    }
}