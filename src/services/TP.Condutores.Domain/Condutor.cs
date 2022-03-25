using System;
using System.Collections.Generic;
using TP.Core.DomainObjects;

namespace TP.Condutores.Domain
{
    public class Condutor : Entity, IAggregateRoot
    {
        public Nome Nome { get; private set; }
        public string CPF { get; private set; }
        public string Telefone { get; private set; }
        public string Email { get; private set; }
        public string CNH { get; private set; }
        public DateTime? DataNascimento { get; private set; }

        private readonly List<Veiculo> _veiculo;
        public IReadOnlyCollection<Veiculo> Veiculo => _veiculo;

        public Condutor(Nome nome, string cpf, string telefone, string email, string cnh, DateTime? dataNascimento)
        {
            Nome = nome;
            CPF = cpf;
            Telefone = telefone;
            Email = email;
            CNH = cnh;
            DataNascimento = dataNascimento;
            _veiculo ??= new List<Veiculo>();
        }

        // EF Rel.
        protected Condutor() { }
    }
}