using ProEventos.Application.Dtos;
using System.Threading.Tasks;

namespace ProEventos.Application.Contratos
{
    public interface ILoteService
    {

        Task<LoteDto[]> SaveLote(int eventoId, LoteDto[] model);
        Task<bool> DeletarLote(int eventoId, int loteId);

        Task<LoteDto[]> ObterLotesPorEventoIdAsync(int eventoId);
        Task<LoteDto> ObterLoteByIdsAsync(int eventoId, int loteId);
        Task AdicionarLote(int eventoId, LoteDto model);
    }
}
