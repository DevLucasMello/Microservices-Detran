using System;
using System.Collections.Generic;
using TP.Core.DomainObjects;

namespace TP.Veiculos.Domain
{
    public class Veiculo : Entity, IAggregateRoot
    {
        public string Placa { get; private set; }
        public string Modelo { get; private set; }
        public string Marca { get; private set; }
        public string Cor { get; private set; }
        public int AnoFabricacao { get; private set; }

        private readonly List<CondutorVeiculo> _condutor;
        public IReadOnlyCollection<CondutorVeiculo> Condutor => _condutor;

        public Veiculo(string placa, string modelo, string marca, string cor, int anoFabricacao)
        {
            Placa = placa;
            Modelo = modelo;
            Marca = marca;
            Cor = cor;
            AnoFabricacao = anoFabricacao;
            _condutor ??= new List<CondutorVeiculo>();
        }

        // EF Rel.
        protected Veiculo() { }

        public void AdicionarCondutor(Guid veiculoId, Guid condutorId, string cpf)
        {
            var condutor = new CondutorVeiculo(veiculoId, cpf)
            {
                Id = condutorId
            };

            _condutor.Add(condutor);
        }

        public void RemoverCondutor(CondutorVeiculo condutor, Veiculo veiculo)
        {
            veiculo._condutor.ForEach(v =>
            {
                if (v.Id == condutor.Id && v.VeiculoId == condutor.VeiculoId) _condutor.Remove(v);
            });
        }
    }
}
