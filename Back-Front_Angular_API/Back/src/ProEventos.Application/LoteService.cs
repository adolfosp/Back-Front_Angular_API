using AutoMapper;
using ProEventos.Application.Contratos;
using ProEventos.Application.Dtos;
using ProEventos.Persistence.Contratos;
using System;
using System.Threading.Tasks;

namespace ProEventos.Application
{
    public class LoteService : ILoteService
    {
        private readonly IGeralPersistence _geralPersistence;
        private readonly ILotePersistence _lotePersistence;
        private readonly IMapper _mapper;


        public LoteService(IGeralPersistence geralPersistence, ILotePersistence lotePersistence, IMapper mapper)
        {
            _geralPersistence = geralPersistence;
            _lotePersistence = lotePersistence;
            _mapper = mapper;
        }

        public async Task<bool> DeletarLote(int eventoId, int loteId)
        {

            try
            {
                var lote = await _lotePersistence.ObterLoteByIdsAsync(eventoId, loteId);

                if (lote is null) throw new Exception("Lote não foi encontrado!");

                _geralPersistence.Deletar(lote);

                return (await _geralPersistence.SalvarAnteracoesAsync());

            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao tentar deletar o LOTE! Mensagem: {ex.Message}");
            }

        }

        public async Task<LoteDto> ObterLoteByIdsAsync(int eventoId, int loteId)
        {
            try
            {
                var lote = await _lotePersistence.ObterLoteByIdsAsync(eventoId, loteId);

                if (lote is null) return null;

                var resultadoMapeado = _mapper.Map<LoteDto>(lote);

                return resultadoMapeado;

            }
            catch (Exception ex)
            {
                throw new Exception($"Não foi possível obter o Lote! Mensagem: {ex.Message}");
            }
        }

        public async Task<LoteDto[]> ObterLotesPorEventoIdAsync(int eventoId)
        {
            try
            {
                var lotes = await _lotePersistence.ObterTodosLotesByEventoIdAsync(eventoId);

                if (lotes is null) return null;

                var resultadosMapeado = _mapper.Map<LoteDto[]>(lotes);

                return resultadosMapeado;

            }
            catch (Exception ex)
            {
                throw new Exception($"Não foi possível obter todos os Lotes! Mensagem: {ex.Message}");
            }
        }

        public Task<LoteDto> SaveLote(int eventoId, LoteDto[] model)
        {
            throw new NotImplementedException();
        }
    }
}
