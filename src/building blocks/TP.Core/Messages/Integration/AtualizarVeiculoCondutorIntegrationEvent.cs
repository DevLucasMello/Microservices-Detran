using System;

namespace TP.Core.Messages.Integration
{
    public class AtualizarVeiculoCondutorIntegrationEvent : IntegrationEvent
    {
        public Guid VeiculoId { get; private set; }
        public Guid CondutorId { get; private set; }
        public string Placa { get; private set; }

        public AtualizarVeiculoCondutorIntegrationEvent(Guid veiculoId, Guid condutorId, string placa)
        {
            VeiculoId = veiculoId;
            CondutorId = condutorId;
            Placa = placa;
        }
    }
}
