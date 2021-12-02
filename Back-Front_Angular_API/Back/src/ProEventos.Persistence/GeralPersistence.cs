using ProEventos.Persistence.Contextos;
using ProEventos.Persistence.Contratos;
using System.Threading.Tasks;

namespace ProEventos.Persistence.GeralPersistence
{
    public class GeralPersistence : IGeralPersistence
    {
        private readonly ProEventosContext _context;

        public GeralPersistence(ProEventosContext context)
            => _context = context;


        public void Adicionar<T>(T entidade) where T : class
        {
            _context.Add(entidade);
        }

        public void Alterar<T>(T entidade) where T : class
        {
            _context.Update(entidade);
        }

        public void Deletar<T>(T entidade) where T : class
        {
            _context.Remove(entidade);
        }

        public void DeletarTodos<T>(T[] entidade) where T : class
        {
            _context.RemoveRange(entidade);
        }

        public async Task<bool> SalvarAnteracoesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

    }
}
