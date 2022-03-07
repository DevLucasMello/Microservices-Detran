using System.Collections.Generic;

namespace TP.Veiculos.Application.ViewModels
{
    public class ExibirVeiculoViewModel
    {        
        public string Id { get; set; }
        public string Placa { get; set; }        
        public string Modelo { get; set; }        
        public string Marca { get; set; }        
        public string Cor { get; set; }        
        public int AnoFabricacao { get; set; }
        public List<CondutorVeiculoViewModel> Condutores { get; set; }
    }

    public class CondutorVeiculoViewModel
    {
        public string CondutorId { get; set; }
        public string CPF { get; set; }
    }
}
