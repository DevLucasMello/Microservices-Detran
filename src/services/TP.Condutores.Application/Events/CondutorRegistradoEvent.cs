using TP.Core.DomainObjects;
using TP.Core.Messages;

namespace TP.Condutores.Application.Events
{
    public class CondutorRegistradoEvent : Event
    {
        public Nome Nome { get; private set; }
        public string CPF { get; private set; }        
        public string Email { get; private set; }

        public CondutorRegistradoEvent(Nome nome, string cpf, string email)
        {
            Nome = nome;
            CPF = cpf;            
            Email = email;            
        }
    }
}
