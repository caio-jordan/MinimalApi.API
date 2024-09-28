using Microsoft.EntityFrameworkCore;
using MinimalApi.API.Entities;

namespace MinimalApi.API.DbContexts
{
    public class PratoDbContext(DbContextOptions<PratoDbContext> options) : DbContext(options)
    {
        public DbSet<Prato> Pratos { get; set; } = null!;
        private DbSet<Ingrediente> Ingredientes { get; set;} = null!;

         protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        _ = modelBuilder.Entity<Ingrediente>().HasData(
        new { Id = 1, Nome = "Carne de Vaca" },
        new { Id = 2, Nome = "Cebola" },
        new { Id = 3, Nome = "Cerveja Escura" },
        new { Id = 4, Nome = "Fatia de Pão Integral" },
        new { Id = 5, Nome = "Mostarda" },
        new { Id = 6, Nome = "Chicória" },
        new { Id = 7, Nome = "Maionese" },
        new { Id = 8, Nome = "Vários Temperos" },
        new { Id = 9, Nome = "Mexilhões" },
        new { Id = 10, Nome = "Aipo" },
        new { Id = 11, Nome = "Batatas Fritas" },
        new { Id = 12, Nome = "Tomate" },
        new { Id = 13, Nome = "Extrato de Tomate" },
        new { Id = 14, Nome = "Folha de Louro" },
        new { Id = 15, Nome = "Cenoura" },
        new { Id = 16, Nome = "Alho" },
        new { Id = 17, Nome = "Vinho Tinto" },
        new { Id = 18, Nome = "Leite de Coco" },
        new { Id = 19, Nome = "Gengibre" },
        new { Id = 20, Nome = "Pimenta Malagueta" },
        new { Id = 21, Nome = "Tamarindo" },
        new { Id = 22, Nome = "Peixe Firme" },
        new { Id = 23, Nome = "Pasta de Gengibre e Alho" },
        new { Id = 24, Nome = "Garam Masala" });

        _ = modelBuilder.Entity<Prato>().HasData(
            new { Id = 1, Nome = "Ensopado Flamengo de Carne de Vaca com Chicória" },
            new { Id = 2, Nome = "Mexilhões com Batatas Fritas" },
            new { Id = 3, Nome = "Ragu alla Bolognese" },
            new { Id = 4, Nome = "Rendang" },
            new { Id = 5, Nome = "Masala de Peixe" });

        _ = modelBuilder
            .Entity<Prato>()
            .HasMany(d => d.Ingredientes)
            .WithMany(i => i.Pratos)
            .UsingEntity(e => e.HasData(
                new { PratosId = 1, IngredientesId = 1 },
                new { PratosId = 1, IngredientesId = 2 },
                new { PratosId = 1, IngredientesId = 3 },
                new { PratosId = 1, IngredientesId = 4 },
                new { PratosId = 1, IngredientesId = 5 },
                new { PratosId = 1, IngredientesId = 6 },
                new { PratosId = 1, IngredientesId = 7 },
                new { PratosId = 1, IngredientesId = 8 },
                new { PratosId = 1, IngredientesId = 14 },
                new { PratosId = 2, IngredientesId = 9 },
                new { PratosId = 2, IngredientesId = 19 },
                new { PratosId = 2, IngredientesId = 11 },
                new { PratosId = 2, IngredientesId = 12 },
                new { PratosId = 2, IngredientesId = 13 },
                new { PratosId = 2, IngredientesId = 2 },
                new { PratosId = 2, IngredientesId = 21 },
                new { PratosId = 2, IngredientesId = 8 },
                new { PratosId = 3, IngredientesId = 1 },
                new { PratosId = 3, IngredientesId = 12 },
                new { PratosId = 3, IngredientesId = 17 },
                new { PratosId = 3, IngredientesId = 14 },
                new { PratosId = 3, IngredientesId = 2 },
                new { PratosId = 3, IngredientesId = 16 },
                new { PratosId = 3, IngredientesId = 23 },
                new { PratosId = 3, IngredientesId = 8 },
                new { PratosId = 4, IngredientesId = 1 },
                new { PratosId = 4, IngredientesId = 18 },
                new { PratosId = 4, IngredientesId = 16 },
                new { PratosId = 4, IngredientesId = 20 },
                new { PratosId = 4, IngredientesId = 22 },
                new { PratosId = 4, IngredientesId = 2 },
                new { PratosId = 4, IngredientesId = 21 },
                new { PratosId = 4, IngredientesId = 8 },
                new { PratosId = 5, IngredientesId = 24 },
                new { PratosId = 5, IngredientesId = 10 },
                new { PratosId = 5, IngredientesId = 23 },
                new { PratosId = 5, IngredientesId = 2 },
                new { PratosId = 5, IngredientesId = 12 },
                new { PratosId = 5, IngredientesId = 18 },
                new { PratosId = 5, IngredientesId = 14 },
                new { PratosId = 5, IngredientesId = 20 },
                new { PratosId = 5, IngredientesId = 13 }
            ));

        base.OnModelCreating(modelBuilder);
    }
       
    }
}