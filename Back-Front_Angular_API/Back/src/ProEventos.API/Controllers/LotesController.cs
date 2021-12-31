using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProEventos.Application.Contratos;
using ProEventos.Application.Dtos;
using ProEventos.Domain;
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
                var eventos = await _service.ObterLotesById(true);

                if (eventos is null) return NoContent();

                return Ok(eventos);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Não foi possível obter os eventos! Mensagem: {ex.Message}");
            }
        }

        [HttpPut("{codigoEvento}")]
        public async Task<IActionResult> Atualizar(int codigoEvento, LoteDto[] models)
        {
            try
            {
                var evento = await _service.AtualizarEvento(codigoEvento, models);

                if (evento is null) return BadRequest("Não foi possível atualizar o Evento!");

                return Ok(evento);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Não foi possível atualizar o EVENTO! Mensagem: {ex.Message}");
            }
        }

        [HttpDelete("{codigoEvento}/{loteId}")]
        public async Task<IActionResult> Deletar(int codigoEvento, int loteId)
        {
            try
            {
                var evento = await _service.ObterLoteEventoById(codigoEvento, loteId);
                if (await _service.DeletarEvento(codigoEvento))
                    return Ok(new {mensagem = "Evento deletado com sucesso!" });

                return BadRequest("Não foi possível deletar o Evento!");

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Não foi possível excluir o EVENTO! Mensagem: {ex.Message}");
            }
        }
    }
}
