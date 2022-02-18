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
        public DateTime DataNascimento { get; private set; }
        public Guid VeiculoId { get; private set; }

        private readonly List<VeiculoCondutor> _veiculo;
        public IReadOnlyCollection<VeiculoCondutor> Veiculo => _veiculo;

        public Condutor(Nome nome, string cpf, string telefone, string email, string cnh, DateTime dataNascimento)
        {
            Nome = nome;
            CPF = cpf;
            Telefone = telefone;
            Email = email;
            CNH = cnh;
            DataNascimento = dataNascimento;
            _veiculo ??= new List<VeiculoCondutor>();
        }

        // EF Rel.
        protected Condutor() { }

        public void AdicionarPlaca(string placa)
        {
            var pl = new VeiculoCondutor(placa);

            _veiculo.Add(pl);
        }

        public void AtualizarPlaca(Guid veiculoId, string placa)
        {
            _veiculo.ForEach(v => 
            {
                if (v.Id == veiculoId) v.AtualizarPlaca(placa);
            });
        }
    }
}
