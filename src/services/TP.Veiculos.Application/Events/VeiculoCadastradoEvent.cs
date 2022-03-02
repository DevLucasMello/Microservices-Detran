using System;
using TP.Core.Messages;

namespace TP.Veiculos.Application.Events
{
    public class VeiculoCadastradoEvent : Event
    {
        public Guid VeiculoId { get; private set; }
        public Guid CondutorId { get; private set; }
        public string Placa { get; private set; }

        public VeiculoCadastradoEvent(Guid veiculoId, Guid condutorId, string placa)
        {
            VeiculoId = veiculoId;
            CondutorId = condutorId;
            Placa = placa;
        }
    }
}
