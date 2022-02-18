using System;
using TP.Core.Messages;

namespace TP.Condutores.Application.Events
{
    public class CondutorAtualizadoEvent : Event
    {
        public Guid CondutorId { get; private set; }
        public string CPF { get; private set; }

        public CondutorAtualizadoEvent(Guid condutorId, string cpf)
        {
            CondutorId = condutorId;
            CPF = cpf;
        }
    }
}
