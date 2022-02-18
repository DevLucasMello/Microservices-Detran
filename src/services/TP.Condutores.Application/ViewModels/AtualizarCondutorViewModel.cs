using System.ComponentModel.DataAnnotations;

namespace TP.Condutores.Application.ViewModels
{
    public class AtualizarCondutorViewModel
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Id { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string PrimeiroNome { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string UltimoNome { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string CPF { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Telefone { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Email { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string CNH { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string DataNascimento { get; set; }
    }
}