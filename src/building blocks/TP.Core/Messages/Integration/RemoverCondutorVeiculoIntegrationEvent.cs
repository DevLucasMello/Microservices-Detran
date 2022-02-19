using System;

namespace TP.Core.Messages.Integration
{
    public class RemoverCondutorVeiculoIntegrationEvent : IntegrationEvent
    {
        public Guid VeiculoId { get; private set; }

        public RemoverCondutorVeiculoIntegrationEvent(Guid veiculoId)
        {
            VeiculoId = veiculoId;
        }
    }
}
