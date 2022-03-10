using System.Collections.Generic;

namespace TP.Bff.Detran.Models.Veiculo
{
    public class VeiculoDTO
    {
        public string Id { get; set; }
        public string Placa { get; set; }
        public string Modelo { get; set; }
        public string Marca { get; set; }
        public string Cor { get; set; }
        public int AnoFabricacao { get; set; }
        public List<CondutorVeiculoDTO> Condutores { get; set; }
    }    

    public class CondutorVeiculoDTO
    {
        public string VeiculoId { get; set; }
        public string CondutorId { get; set; }
        public string CPF { get; set; }
    }
}
