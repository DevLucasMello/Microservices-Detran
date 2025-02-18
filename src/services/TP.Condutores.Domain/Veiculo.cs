﻿using System;
using TP.Core.DomainObjects;

namespace TP.Condutores.Domain
{
    public class Veiculo : Entity
    {
        public Guid CondutorId { get; private set; }
        public string VeiculoId { get; private set; }
        public string Placa { get; private set; }
        public Condutor Condutor { get; private set; }

        public Veiculo(Guid idCondutor, string idVeiculo, string placa)
        {
            CondutorId = idCondutor;
            VeiculoId = idVeiculo;
            Placa = placa;
        }

        // EF Rel.
        protected Veiculo() { }
    }
}