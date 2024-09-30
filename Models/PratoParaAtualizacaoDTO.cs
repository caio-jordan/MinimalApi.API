using System.ComponentModel.DataAnnotations;

namespace MinimalApi.API.Models
{
    public class PratoParaAtualizacaoDTO
    {
        [Required]
        [StringLength(100, MinimumLength =3)]
        public required string Nome { get; set; }        
    }
}