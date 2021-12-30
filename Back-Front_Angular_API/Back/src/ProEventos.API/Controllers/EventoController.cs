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
    public class EventoController : ControllerBase
    {

        private readonly IEventoService _service;

        public EventoController(IEventoService service)
            => _service = service;



        [HttpGet]
        public async Task<IActionResult> Obter()
        {
            try
            {
                var eventos = await _service.ObterTodosEventosAsync(true);

                if (eventos is null) return NoContent();

                return Ok(eventos);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Não foi possível obter os eventos! Mensagem: {ex.Message}");
            }
        }

        // evento/1
        [HttpGet("{codigoEvento}")]
        public async Task<ActionResult<Evento>> ObterPorId(int codigoEvento)
        {
            try
            {
                var evento = await _service.ObterEventoPorIdAsync(codigoEvento, true);

                if (evento is null) return NoContent();

                return Ok(evento);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Não foi possível obter o EVENTO por id! Mensagem: {ex.Message}");
            }
        }


        [HttpGet("tema/{tema}")]
        public async Task<ActionResult<Evento>> ObterPorTema(string tema)
        {
            try
            {
                var eventos = await _service.ObterTodosEventosPorTemaAsync(tema, true);

                if (eventos is null) return NoContent();

                return Ok(eventos);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Não foi possível obter o EVENTO por tema! Mensagem: {ex.Message}");
            }
        }


        [HttpPost]
        public async Task<IActionResult> Inserir(EventoDto model)
        {
            try
            {
                var evento = await _service.AdicionarEventos(model);

                if (evento is null) return BadRequest("Não foi possível inserir o Evento!");

                return Ok(evento);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Não foi possível inserir o EVENTO! Mensagem: {ex.Message}");
            }
        }

        [HttpPut("{codigoEvento}")]
        public async Task<IActionResult> Atualizar(int codigoEvento, EventoDto model)
        {
            try
            {
                var evento = await _service.AtualizarEvento(codigoEvento, model);

                if (evento is null) return BadRequest("Não foi possível atualizar o Evento!");

                return Ok(evento);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Não foi possível atualizar o EVENTO! Mensagem: {ex.Message}");
            }
        }

        [HttpDelete("{codigoEvento}")]
        public async Task<IActionResult> Deletar(int codigoEvento)
        {
            try
            {
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
