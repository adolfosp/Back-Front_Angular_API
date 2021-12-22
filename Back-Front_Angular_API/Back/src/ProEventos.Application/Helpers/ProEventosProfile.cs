using AutoMapper;
using ProEventos.Application.Dtos;
using ProEventos.Domain;

namespace ProEventos.Application.Helpers
{
    public class ProEventosProfile : Profile
    {
        public ProEventosProfile()
        {
            //reverse.map faz a conversao inversa, eventoDto para evento
            CreateMap<Evento, EventoDto>().ReverseMap();

        }
    }
}
