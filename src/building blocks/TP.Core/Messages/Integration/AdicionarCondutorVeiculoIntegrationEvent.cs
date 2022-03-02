using System;

namespace TP.Core.Messages.Integration
{
    public class AdicionarCondutorVeiculoIntegrationEvent : IntegrationEvent
    {
        public Guid CondutorId { get; private set; }
        public Guid VeiculoId { get; private set; }
        public string Placa { get; private set; }

        public AdicionarCondutorVeiculoIntegrationEvent(Guid condutorId, Guid veiculoId, string placa)
        {            
            CondutorId = condutorId;
            VeiculoId = veiculoId;
            Placa = placa;
        }
    }
}
