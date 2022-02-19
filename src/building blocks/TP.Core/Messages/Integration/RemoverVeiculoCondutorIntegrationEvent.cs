using System;

namespace TP.Core.Messages.Integration
{
    public class RemoverVeiculoCondutorIntegrationEvent : IntegrationEvent
    {
        public Guid CondutorId { get; private set; }

        public RemoverVeiculoCondutorIntegrationEvent(Guid condutorId)
        {
            CondutorId = condutorId;
            // Passar todas as Informações para adicionar Veículo na API de Veículos
        }
    }
}