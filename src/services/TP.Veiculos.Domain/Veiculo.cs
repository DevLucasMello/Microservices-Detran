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

        private readonly List<Condutor> _condutor;
        public IReadOnlyCollection<Condutor> Condutor => _condutor;

        public Veiculo(string placa, string modelo, string marca, string cor, int anoFabricacao)
        {
            Placa = placa;
            Modelo = modelo;
            Marca = marca;
            Cor = cor;
            AnoFabricacao = anoFabricacao;
            _condutor ??= new List<Condutor>();
        }

        // EF Rel.
        protected Veiculo() { }

        public Veiculo AdicionarCondutor(Veiculo veiculo, string idCondutor, string cpf)
        {
            var condutor = new Condutor(veiculo.Id, idCondutor, cpf);

            veiculo._condutor.Add(condutor);

            return veiculo;
        }
    }
}
