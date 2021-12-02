using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;
using ProEventos.Persistence;
using ProEventos.Persistence.Contextos;
using System.Linq;
using System.Threading.Tasks;

namespace ProEventos.PalestrantePersistence
{
    public class PalestrantePersistence : IPalestrantePersistence
    {
        private readonly ProEventosContext _context;

        public PalestrantePersistence(ProEventosContext context)
            => _context = context;


        public async Task<Palestrante[]> ObterTodosPalestrantesAsync(bool incluirEventos = false)
        {
            IQueryable<Palestrante> query = _context.Palestrantes.Include(e => e.RedesSociais);

            if (incluirEventos)
                query = query.Include(pe => pe.PalestrantesEventos).ThenInclude(pe => pe.Evento);

            query = query.AsNoTracking().OrderBy(e => e.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Palestrante[]> ObterTodosPalestrantesPorNomeAsync(string nome, bool incluirEventos = false)
        {
            IQueryable<Palestrante> query = _context.Palestrantes.Include(e => e.RedesSociais);


            if (incluirEventos)
                query = query.Include(pe => pe.PalestrantesEventos).ThenInclude(pe => pe.Evento);

            query = query.AsNoTracking().Where(e => e.Nome.Contains(nome)).OrderBy(e => e.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Palestrante> ObterPalestrantePorIdAsync(int idPalestrante, bool incluirEventos = false)
        {
            IQueryable<Palestrante> query = _context.Palestrantes.Include(e => e.RedesSociais);

            if (incluirEventos)
                query = query.Include(pe => pe.PalestrantesEventos).ThenInclude(pe => pe.Palestrante);

            query = query.AsNoTracking().Where(e => e.Id == idPalestrante);

            return await query.FirstOrDefaultAsync();
        }

    }
}
