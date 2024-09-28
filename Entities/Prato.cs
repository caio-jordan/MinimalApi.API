using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace MinimalApi.API.Entities
{
    public class Prato
    {
        [Key]
        public int Id { get; set;}

        [Required]
        [MaxLength(200)]
        public required string Nome { get; set;}

        public ICollection<Ingrediente> Ingredientes { get; set; } = new List<Ingrediente>();

        public Prato()
        {            
            
        }
        [SetsRequiredMembers]
        public Prato(int id, string nome){
            Id = id;
            Nome = nome;
        }
    }
}