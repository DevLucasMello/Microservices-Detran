using System;
using System.Collections.Generic;
using TP.Core.DomainObjects;

namespace TP.Veiculos.Domain
{
    public class CondutorVeiculo : Entity
    {
        public Guid VeiculoId { get; private set; }
        public string CPF { get; private set; }

        private readonly List<Veiculo> _veiculo;
        public IReadOnlyCollection<Veiculo> Veiculo => _veiculo;

        public CondutorVeiculo(Guid veiculoId, string cpf)
        {
            VeiculoId = veiculoId;
            CPF = cpf;
            _veiculo ??= new List<Veiculo>();
        }

        // EF Rel.
        protected CondutorVeiculo() { }
    }
}
