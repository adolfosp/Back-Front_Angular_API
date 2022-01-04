using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProEventos.Application.Contratos;
using ProEventos.Application.Dtos;
using System;
using System.Threading.Tasks;

namespace ProEventos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LotesController : ControllerBase
    {

        private readonly ILoteService _service;

        public LotesController(ILoteService service)
            => _service = service;



        [HttpGet("{id}")]
        public async Task<IActionResult> Obter(int id)
        {
            try
            {
                var lotes = await _service.ObterLotesPorEventoIdAsync(id);

                if (lotes is null) return NoContent();

                return Ok(lotes);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Não foi possível obter os lotes! Mensagem: {ex.Message}");
            }
        }

        [HttpPut("{codigoEvento}")]
        public async Task<IActionResult> SaveLotes(int codigoEvento, LoteDto[] models)
        {
            try
            {
                var lotes = await _service.SaveLote(codigoEvento, models);

                if (lotes is null) return BadRequest("Não foi possível atualizar os lotes!");

                return Ok(lotes);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Não foi possível atualizar o Lotes! Mensagem: {ex.Message}");
            }
        }

        [HttpDelete("{codigoEvento}/{loteId}")]
        public async Task<IActionResult> Deletar(int codigoEvento, int loteId)
        {
            try
            {
                var lote = await _service.ObterLoteByIdsAsync(codigoEvento, loteId);

                if (await _service.DeletarLote(lote.CodigoEvento, lote.Id))
                    return Ok(new {mensagem = "Lote deletado com sucesso!" });

                return BadRequest("Não foi possível deletar o Lote!");

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Não foi possível excluir o Lote! Mensagem: {ex.Message}");
            }
        }
    }
}
