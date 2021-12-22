using AutoMapper;
using ProEventos.Application.Contratos;
using ProEventos.Application.Dtos;
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
        private readonly IMapper _mapper;


        public EventoService(IGeralPersistence geralPersistence, IEventoPersistence eventoPersistence, IMapper mapper)
        {
            _geralPersistence = geralPersistence;
            _eventoPersistence = eventoPersistence;
            _mapper = mapper;
        }

        public async Task<EventoDto> AdicionarEventos(EventoDto model)
        {
            try
            {

                var evento = _mapper.Map<Evento>(model);

                _geralPersistence.Adicionar<Evento>(evento);

                if (await _geralPersistence.SalvarAnteracoesAsync()) {

                    var retornoConsultaPorId = await _eventoPersistence.ObterEventoPorIdAsync(evento.Id, false);

                    return _mapper.Map<EventoDto>(retornoConsultaPorId);
                }
                  

                return null;

            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao tentar adicionar o EVENTO! Mensagem: {ex.Message}");
            }
        }

        public async Task<EventoDto> AtualizarEvento(int eventoId, EventoDto model)
        {
            try
            {
                var evento = await _eventoPersistence.ObterEventoPorIdAsync(eventoId, false);

                if (evento is null) return null;

                model.Id = evento.Id;

                _mapper.Map(model, evento);

                _geralPersistence.Alterar<Evento>(evento);

                if (await _geralPersistence.SalvarAnteracoesAsync())
                {
                    var retorno = await _eventoPersistence.ObterEventoPorIdAsync(evento.Id, false);

                    return _mapper.Map<EventoDto>(retorno);

                }

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

        public async Task<EventoDto> ObterEventoPorIdAsync(int idEvento, bool incluirPalestrantes = false)
        {
            try
            {
                var evento = await _eventoPersistence.ObterEventoPorIdAsync(idEvento, incluirPalestrantes);

                if (evento is null) return null;

                var resultadoMapeado = _mapper.Map<EventoDto>(evento);

                return resultadoMapeado;

            }
            catch (Exception ex)
            {
                throw new Exception($"Não foi possível obter o Evento! Mensagem: {ex.Message}");
            }
        }

        public async Task<EventoDto[]> ObterTodosEventosAsync(bool incluirPalestrantes = false)
        {

            try
            {
                var eventos = await _eventoPersistence.ObterTodosEventosAsync(incluirPalestrantes);

                if (eventos is null) return null;

                var resultadosMapeado = _mapper.Map<EventoDto[]>(eventos);

                return resultadosMapeado;

            }
            catch (Exception ex)
            {
                throw new Exception($"Não foi possível obter todos os Eventos! Mensagem: {ex.Message}");
            }
        }

        public async Task<EventoDto[]> ObterTodosEventosPorTemaAsync(string tema, bool incluirPalestrantes = false)
        {
            try
            {
                var eventos = await _eventoPersistence.ObterTodosEventosPorTemaAsync(tema, incluirPalestrantes);

                if (eventos is null) return null;

                var resultadosMapeado = _mapper.Map<EventoDto[]>(eventos);

                return resultadosMapeado;

            }
            catch (Exception ex)
            {
                throw new Exception($"Não foi possível obter os Eventos por tema! Mensagem: {ex.Message}");
            }
        }
    }
}
