using ProEventos.Domain;
using System.Threading.Tasks;

namespace ProEventos.Application.Contratos
{
    public interface IEventoService
    {
        Task<Evento> AdicionarEventos(Evento model);
        Task<Evento> AtualizarEvento(int eventoId, Evento model);
        Task<bool> DeletarEvento(int eventoId);

        Task<Evento[]> ObterTodosEventosPorTemaAsync(string tema, bool incluirPalestrantes = false);
        Task<Evento[]> ObterTodosEventosAsync(bool incluirPalestrantes = false);
        Task<Evento> ObterEventoPorIdAsync(int idEvento, bool incluirPalestrantes = false);

    }
}
