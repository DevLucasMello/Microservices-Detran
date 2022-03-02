using System;

namespace TP.Core.Messages.Integration
{
    public class RemoverCondutorVeiculoIntegrationEvent : IntegrationEvent
    {
        public Guid VeiculoId { get; private set; }
        public Guid CondutorId { get; private set; }
        public string CPF { get; private set; }

        public RemoverCondutorVeiculoIntegrationEvent(Guid veiculoId, Guid condutorId, string cpf)
        {
            VeiculoId = veiculoId;
            CondutorId = condutorId;
            CPF = cpf;
        }
    }
}
