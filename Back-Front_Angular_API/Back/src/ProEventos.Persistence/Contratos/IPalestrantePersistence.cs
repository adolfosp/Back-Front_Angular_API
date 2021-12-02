using ProEventos.Domain;
using System.Threading.Tasks;

namespace ProEventos.Persistence
{
    public interface IPalestrantePersistence
    {

        Task<Palestrante[]> ObterTodosPalestrantesPorNomeAsync(string nome, bool incluirEventos);
        Task<Palestrante[]> ObterTodosPalestrantesAsync(bool incluirEventos);
        Task<Palestrante> ObterPalestrantePorIdAsync(int idPalestrante, bool incluirEventos);


    }
}
