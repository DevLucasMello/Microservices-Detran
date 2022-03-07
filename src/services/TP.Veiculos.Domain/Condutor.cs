using System;
using TP.Core.DomainObjects;
namespace TP.Veiculos.Domain
{
    public class Condutor : Entity
    {
        public Guid VeiculoId { get; private set; }
        public string IdCondutor { get; private set; }
        public string CPF { get; private set; }
        public Veiculo Veiculo { get; private set; }

        public Condutor(Guid idVeiculo, string idCondutor, string cpf)
        {
            VeiculoId = idVeiculo;
            IdCondutor = idCondutor;
            CPF = cpf;
        }

        // EF Rel.
        protected Condutor() { }
    }
}
