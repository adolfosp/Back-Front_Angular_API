using ProEventos.Application.Dtos;
using ProEventos.Domain;
using System.Threading.Tasks;

namespace ProEventos.Application.Contratos
{
    public interface IEventoService
    {
        Task<EventoDto> AdicionarEventos(EventoDto model);
        Task<EventoDto> AtualizarEvento(int eventoId, EventoDto model);
        Task<bool> DeletarEvento(int eventoId);

        Task<EventoDto[]> ObterTodosEventosPorTemaAsync(string tema, bool incluirPalestrantes = false);
        Task<EventoDto[]> ObterTodosEventosAsync(bool incluirPalestrantes = false);
        Task<EventoDto> ObterEventoPorIdAsync(int idEvento, bool incluirPalestrantes = false);

    }
}
