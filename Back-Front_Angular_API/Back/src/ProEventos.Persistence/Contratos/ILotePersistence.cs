using ProEventos.Domain;
using System.Threading.Tasks;

namespace ProEventos.Persistence.Contratos
{
    public interface ILotePersistence
    {
        Task<Lote[]> ObterTodosLotesByEventoIdAsync(int codigoEvento);
        Task<Lote> ObterLoteByIdsAsync(int idEvento, int loteId);

    }
}
