using System.Collections.Generic;

namespace TP.Bff.Detran.Models.Condutor
{
    public class CondutorDTO
    {
        public string Id { get; set; }
        public string PrimeiroNome { get; set; }
        public string UltimoNome { get; set; }
        public string CPF { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string CNH { get; set; }
        public string DataNascimento { get; set; }
        public List<VeiculoCondutorDTO> Veiculos { get; set; }
    }

    public class VeiculoCondutorDTO
    {
        public string CondutorId { get; set; }
        public string VeiculoId { get; set; }
        public string Placa { get; set; }
    }
}
