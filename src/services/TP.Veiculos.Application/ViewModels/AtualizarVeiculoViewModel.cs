using System.ComponentModel.DataAnnotations;

namespace TP.Veiculos.Application.ViewModels
{
    public class AtualizarVeiculoViewModel
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Id { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Placa { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Modelo { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Marca { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Cor { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public int AnoFabricacao { get; set; }
    }
}
