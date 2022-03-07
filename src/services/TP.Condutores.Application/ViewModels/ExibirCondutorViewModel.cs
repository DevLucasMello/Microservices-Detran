using System.Collections.Generic;

namespace TP.Condutores.Application.ViewModels
{
    public class ExibirCondutorViewModel
    {        
        public string Id { get; set; }        
        public string PrimeiroNome { get; set; }        
        public string UltimoNome { get; set; }        
        public string CPF { get; set; }        
        public string Telefone { get; set; }       
        public string Email { get; set; }       
        public string CNH { get; set; }       
        public string DataNascimento { get; set; }
        public List<VeiculoCondutorViewModel> Veiculos { get; set; }
    }

    public class VeiculoCondutorViewModel
    {
        public string VeiculoId { get; set; }
        public string Placa { get; set; }
    }
}
