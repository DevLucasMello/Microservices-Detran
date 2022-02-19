using System;

namespace TP.Core.Messages.Integration
{
    public class AdicionarCondutorVeiculoIntegrationEvent : IntegrationEvent
    {
        public Guid CondutorId { get; private set; }

        public AdicionarCondutorVeiculoIntegrationEvent(Guid condutorId)
        {
            CondutorId = condutorId;
            // Passar todas as Informações para adicionar Veículo na API de Veículos
        }
    }
}
