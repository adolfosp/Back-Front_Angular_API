using System.Linq;
using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;
using ProEventos.Persistence.Contextos;
using ProEventos.Persistence.Contratos;
using System.Threading.Tasks;

namespace ProEventos.Persistence.EventoPersistence
{
    public class LotePersistence : ILotePersistence
    {
        private readonly ProEventosContext _context;

        public LotePersistence(ProEventosContext context)
        {       
            _context = context;
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

        }

        public async Task<Lote> ObterLoteByIdsAsync(int idEvento, int loteId)
        {
            IQueryable<Lote> query = _context.Lotes;

            query = query
                .AsNoTracking()
                .Where(lote => lote.CodigoEvento == idEvento && lote.Id == loteId);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Lote[]> ObterTodosLotesByEventoIdAsync(int codigoEvento)
        {
            IQueryable<Lote> query = _context.Lotes;

            query = query
                .AsNoTracking()
                .Where(lote => lote.CodigoEvento == codigoEvento);

            return await query.ToArrayAsync();
        }
    }
}
