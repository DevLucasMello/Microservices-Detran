using System;
using System.Collections.Generic;
using TP.Core.DomainObjects;

namespace TP.Condutores.Domain
{
    public class VeiculoCondutor : Entity
    {
        public Guid CondutorId { get; private set; }
        public string Placa { get; private set; }

        private readonly List<Condutor> _condutor;
        public IReadOnlyCollection<Condutor> Condutor => _condutor;

        public VeiculoCondutor(Guid condutorId, string placa)
        {
            CondutorId = condutorId;
            Placa = placa;
            _condutor ??= new List<Condutor>();
        }

        // EF Rel.
        protected VeiculoCondutor() { }

        public void AtualizarPlaca(string placa)
        {
            Placa = placa;
        }
    }
}