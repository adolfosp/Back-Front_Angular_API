using System.Threading.Tasks;

namespace ProEventos.Persistence.Contratos
{
    public interface IGeralPersistence
    {

        void Adicionar<T>(T entidade) where T : class;
        void Alterar<T>(T entidade) where T : class;
        void Deletar<T>(T entidade) where T : class;
        void DeletarTodos<T>(T[] entidade) where T : class;
        Task<bool> SalvarAnteracoesAsync();

    }
}
