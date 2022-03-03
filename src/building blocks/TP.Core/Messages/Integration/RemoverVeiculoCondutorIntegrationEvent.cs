using System;

namespace TP.Core.Messages.Integration
{
    public class RemoverVeiculoCondutorIntegrationEvent : IntegrationEvent
    {        
        public Guid VeiculoId { get; private set; }
        public string Placa { get; private set; }

        public RemoverVeiculoCondutorIntegrationEvent(Guid veiculoId, string placa)
        {            
            VeiculoId = veiculoId;
            Placa = placa;
        }
    }
}