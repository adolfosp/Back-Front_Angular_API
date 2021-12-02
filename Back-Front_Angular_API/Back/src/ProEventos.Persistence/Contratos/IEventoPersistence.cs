using ProEventos.Domain;
using System.Threading.Tasks;

namespace ProEventos.Persistence.Contratos
{
    public interface IEventoPersistence
    {

        Task<Evento[]> ObterTodosEventosPorTemaAsync(string tema, bool incluirPalestrantes = false);
        Task<Evento[]> ObterTodosEventosAsync(bool incluirPalestrantes = false);
        Task<Evento> ObterEventoPorIdAsync(int idEvento, bool incluirPalestrantes = false);


    }
}
