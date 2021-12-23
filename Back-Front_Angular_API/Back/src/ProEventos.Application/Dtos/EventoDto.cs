using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProEventos.Application.Dtos
{
    public class EventoDto
    {
        public int Id { get; set; }
        public string Local { get; set; }
        public DateTime? DataEvento { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório."),
            StringLength(50,MinimumLength = 3,ErrorMessage = "Intervalo permitido de 3 a 50 caracteres")]
        public string Tema { get; set; }

        [Display(Name = "Quantidade de pessoas")]
        [Range(1,120000, ErrorMessage = "{0} não pode ser menor que 1 e maior que 120.000")]
        public int QuantidadePessoas { get; set; }
        public string ImagemURL { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório"),
            Phone(ErrorMessage = "O campo {0} está como número inválido")]
        public string Telefone { get; set; }

        [Display(Name = "E-mail"),
            Required(ErrorMessage = "O campo {0} é obrigatório"),
            EmailAddress(ErrorMessage = "O campo {0} não é o campo válido.")]
        public string Email { get; set; }
        public IEnumerable<LoteDto> Lotes { get; set; }
        public IEnumerable<RedeSocialDto> RedesSociais { get; set; }
        public IEnumerable<PalestranteDto> PalestrantesEventos { get; set; }
    }
}
