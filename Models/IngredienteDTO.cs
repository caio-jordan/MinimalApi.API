namespace MinimalApi.API.Models
{
    public class IngredienteDTO
    {
        public int Id { get; set; }
        public required string Nome { get; set; }
        public int PratoId { get; set; }
    }
}