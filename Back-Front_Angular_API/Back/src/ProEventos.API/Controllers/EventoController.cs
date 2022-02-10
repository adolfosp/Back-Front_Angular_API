using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProEventos.Application.Contratos;
using ProEventos.Application.Dtos;
using ProEventos.Domain;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ProEventos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventoController : ControllerBase
    {

        private readonly IEventoService _service;
        private readonly IWebHostEnvironment _hostEnvironment;

        public EventoController(IEventoService service, IWebHostEnvironment hostEnvironment)
        { 
            _service = service;
            _hostEnvironment = hostEnvironment;
        }
        

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

        [HttpPost("upload-image/{eventoId}")]
        public async Task<IActionResult> InserirImagem(int eventoId)
        {
            try
            {
                var evento = await _service.ObterEventoPorIdAsync(eventoId, true);

                if (evento is null) return NoContent();

                var file = Request.Form.Files[0];

                if(file.Length > 0)
                {
                    DeletarImagem(evento.ImagemURL);
                    evento.ImagemURL = await SaveImagem(file);
                }

                var eventoRetorno = await _service.AtualizarEvento(eventoId, evento);

                return Ok(eventoRetorno);
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
                var evento = await _service.ObterEventoPorIdAsync(codigoEvento);
                if (evento is null) return NoContent();

                if (await _service.DeletarEvento(codigoEvento))
                {
                    DeletarImagem(evento.ImagemURL);
                    return Ok(new { mensagem = "Evento deletado com sucesso!" });

                }
                   

                return BadRequest("Não foi possível deletar o Evento!");

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Não foi possível excluir o EVENTO! Mensagem: {ex.Message}");
            }
        }

        [NonAction]
        public async Task<string> SaveImagem(IFormFile imagemFile)
        {

            string imagemNome = new String(Path.GetFileNameWithoutExtension(imagemFile.FileName).Take(10).ToArray()).Replace(' ','-');

            imagemNome = $"{imagemNome}{DateTime.UtcNow.ToString("yyyyMMssfff")}{Path.GetExtension(imagemFile.FileName)}";

            var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, @"Resources/Imagens", imagemNome);

            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await imagemFile.CopyToAsync(fileStream);
            }
                return "";
        }

        //Não será um endpoint
        [NonAction]
        public void DeletarImagem(string imagem)
        {
            var caminhoImagem = Path.Combine(_hostEnvironment.ContentRootPath, @"Resources/Imagens", imagem);

            if (System.IO.File.Exists(imagem))
                System.IO.File.Delete(imagem);
        }
    }
}
