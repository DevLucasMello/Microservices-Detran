using System.Collections.Generic;
using TP.Core.DomainObjects;
namespace TP.Veiculos.Domain
{
    public class Condutor : Entity
    {
        public string IdCondutor { get; private set; }
        public string CPF { get; private set; }

        private readonly List<Veiculo> _veiculo;
        public IReadOnlyCollection<Veiculo> Veiculo => _veiculo;

        public Condutor(string idCondutor, string cpf)
        {
            IdCondutor = idCondutor;
            CPF = cpf;
            _veiculo ??= new List<Veiculo>();
        }

        // EF Rel.
        protected Condutor() { }
    }
}
