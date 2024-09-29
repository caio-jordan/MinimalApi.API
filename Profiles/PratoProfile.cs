using AutoMapper;
using MinimalApi.API.Entities;
using MinimalApi.API.Models;

namespace MinimalApi.API.Profiles
{
    public class PratoProfile : Profile
    {
        public PratoProfile()
        {
            CreateMap<Prato, PratoDTO>().ReverseMap();
            CreateMap<Prato, PratoParaCriacaoDTO>().ReverseMap();
            CreateMap<Prato, PratoParaAtualizacaoDTO>().ReverseMap();
            CreateMap<Ingrediente, IngredienteDTO>().ForMember(
                d => d.PratoId,                
                o => o.MapFrom(s=>s.Pratos.First().Id)                
            );
        }
    }
}