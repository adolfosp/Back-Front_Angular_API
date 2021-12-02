using ProEventos.Application.Contratos;
using ProEventos.Domain;
using ProEventos.Persistence.Contratos;
using System;
using System.Threading.Tasks;

namespace ProEventos.Application
{
    public class EventoService : IEventoService
    {
        private readonly IGeralPersistence _geralPersistence;
        private readonly IEventoPersistence _eventoPersistence;

        public EventoService(IGeralPersistence geralPersistence, IEventoPersistence eventoPersistence)
        {
            _geralPersistence = geralPersistence;
            _eventoPersistence = eventoPersistence;
        }

        public async Task<Evento> AdicionarEventos(Evento model)
        {
            try
            {
                _geralPersistence.Adicionar<Evento>(model);

                if (await _geralPersistence.SalvarAnteracoesAsync())
                    return await _eventoPersistence.ObterEventoPorIdAsync(model.Id, false);

                return null;

            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao tentar adicionar o EVENTO! Mensagem: {ex.Message}");
            }
        }

        public async Task<Evento> AtualizarEvento(int eventoId, Evento model)
        {
            try
            {
                var evento = await _eventoPersistence.ObterEventoPorIdAsync(eventoId, false);

                if (evento is null) return null;

                model.Id = evento.Id;

                _geralPersistence.Alterar(model);

                if (await _geralPersistence.SalvarAnteracoesAsync())
                    return await _eventoPersistence.ObterEventoPorIdAsync(model.Id, false);

                return null;
            }
            catch (Exception ex)
            {

                throw new Exception($"Erro ao tentar atualizar o EVENTO! Mensagem: {ex.Message}");

            }
        }

        public async Task<bool> DeletarEvento(int eventoId)
        {
            try
            {
                var evento = await _eventoPersistence.ObterEventoPorIdAsync(eventoId, false);

                if (evento is null) throw new Exception("Evento não foi encontrado!");

                _geralPersistence.Deletar(evento);

                return (await _geralPersistence.SalvarAnteracoesAsync());

            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao tentar atualizar o EVENTO! Mensagem: {ex.Message}");
            }
        }

        public async Task<Evento> ObterEventoPorIdAsync(int idEvento, bool incluirPalestrantes = false)
        {
            try
            {
                var evento = await _eventoPersistence.ObterEventoPorIdAsync(idEvento, incluirPalestrantes);

                if (evento is null) return null;

                return evento;

            }
            catch (Exception ex)
            {
                throw new Exception($"Não foi possível obter o Evento! Mensagem: {ex.Message}");
            }
        }

        public async Task<Evento[]> ObterTodosEventosAsync(bool incluirPalestrantes = false)
        {

            try
            {
                var eventos = await _eventoPersistence.ObterTodosEventosAsync(incluirPalestrantes);

                if (eventos is null) return null;

                return eventos;

            }
            catch (Exception ex)
            {
                throw new Exception($"Não foi possível obter todos os Eventos! Mensagem: {ex.Message}");
            }
        }

        public async Task<Evento[]> ObterTodosEventosPorTemaAsync(string tema, bool incluirPalestrantes = false)
        {
            try
            {
                var eventos = await _eventoPersistence.ObterTodosEventosPorTemaAsync(tema, incluirPalestrantes);

                if (eventos is null) return null;

                return eventos;

            }
            catch (Exception ex)
            {
                throw new Exception($"Não foi possível obter os Eventos por tema! Mensagem: {ex.Message}");
            }
        }
    }
}
