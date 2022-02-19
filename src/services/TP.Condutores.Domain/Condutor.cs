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

        public void AdicionarVeiculo(Guid veiculoId, Guid condutorId, string placa)
        {
            var pl = new VeiculoCondutor(condutorId, placa)
            {
                Id = veiculoId
            };

            _veiculo.Add(pl);
        }

        public void RemoverVeiculo(VeiculoCondutor veiculo, Condutor condutor)
        {
            condutor._veiculo.ForEach(v => 
            {
                if (v.Id == veiculo.Id && v.CondutorId == veiculo.CondutorId) _veiculo.Remove(v);
            });
        }
    }
}