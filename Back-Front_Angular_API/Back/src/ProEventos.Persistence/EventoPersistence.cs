using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;
using ProEventos.Persistence.Contextos;
using ProEventos.Persistence.Contratos;
using System.Linq;
using System.Threading.Tasks;

namespace ProEventos.Persistence.EventoPersistence
{
    public class EventoPersistence : IEventoPersistence
    {
        private readonly ProEventosContext _context;

        public EventoPersistence(ProEventosContext context)
        {       
            _context = context;
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

        }


        public async Task<Evento> ObterEventoPorIdAsync(int idEvento, bool incluirPalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos.Include(e => e.Lotes)
                                                        .Include(e => e.RedesSociais);

            if (incluirPalestrantes)
                query = query.Include(pe => pe.PalestrantesEventos).ThenInclude(pe => pe.Palestrante);

            query = query.AsNoTracking().Where(e => e.Id == idEvento);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Evento[]> ObterTodosEventosAsync(bool incluirPalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos.Include(e => e.Lotes)
                                                        .Include(e => e.RedesSociais);

            if (incluirPalestrantes)
                query = query.Include(pe => pe.PalestrantesEventos).ThenInclude(pe => pe.Palestrante);

            query = query.AsNoTracking().OrderBy(e => e.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Evento[]> ObterTodosEventosPorTemaAsync(string tema, bool incluirPalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos.Include(e => e.Lotes)
                                                        .Include(e => e.RedesSociais);

            if (incluirPalestrantes)
                query = query.Include(pe => pe.PalestrantesEventos).ThenInclude(pe => pe.Palestrante);

            query = query.AsNoTracking().Where(e => e.Tema.ToLower().Contains(tema.ToLower())).OrderBy(e => e.Id);


            return await query.ToArrayAsync();
        }


    }
}
