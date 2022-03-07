using System.Collections.Generic;
using TP.Core.DomainObjects;

namespace TP.Condutores.Domain
{
    public class Veiculo : Entity
    {
        public string IdVeiculo { get; private set; }
        public string Placa { get; private set; }

        private readonly List<Condutor> _condutor;
        public IReadOnlyCollection<Condutor> Condutor => _condutor;

        public Veiculo(string idVeiculo, string placa)
        {
            IdVeiculo = idVeiculo;
            Placa = placa;
            _condutor ??= new List<Condutor>();
        }

        // EF Rel.
        protected Veiculo() { }
    }
}