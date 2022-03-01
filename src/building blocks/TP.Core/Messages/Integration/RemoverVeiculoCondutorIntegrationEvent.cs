using System;

namespace TP.Core.Messages.Integration
{
    public class RemoverVeiculoCondutorIntegrationEvent : IntegrationEvent
    {
        public Guid CondutorId { get; private set; }
        public Guid VeiculoId { get; private set; }
        public string Placa { get; private set; }

        public RemoverVeiculoCondutorIntegrationEvent(Guid condutorId, Guid veiculoId, string placa)
        {
            CondutorId = condutorId;
            VeiculoId = veiculoId;
            Placa = placa;
            // Passar todas as Informações para adicionar Veículo na API de Veículos
        }
    }
}