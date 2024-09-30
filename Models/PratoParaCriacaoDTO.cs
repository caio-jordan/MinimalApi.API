using System.ComponentModel.DataAnnotations;

namespace MinimalApi.API.Models
{
    public class PratoParaCriacaoDTO
    {        
        [Required]
        [StringLength(100, MinimumLength =3)]
        public required string Nome { get; set; }        
    }
}