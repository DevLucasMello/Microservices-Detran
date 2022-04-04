using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Bogus;
using Bogus.Extensions.Brazil;
using Moq;
using Moq.AutoMock;
using TP.Veiculos.Application.AutoMapper;
using TP.Veiculos.Application.Commands;
using TP.Veiculos.Application.Queries;
using TP.Veiculos.Domain;
using Xunit;

namespace TP.Veiculos.Application.Tests.Fixtures
{
    [CollectionDefinition(nameof(VeiculoAutoMockerCollection))]
    public class VeiculoAutoMockerCollection : ICollectionFixture<VeiculoTestsAutoMockerFixture>
    {
    }
    public class VeiculoTestsAutoMockerFixture : IDisposable
    {
        private VeiculoCommandHandler _veiculoHandler;
        private VeiculoQueries _veiculoQueries;
        public AutoMocker _mocker;
        private Mapper _mapper;
        public Mock<IVeiculoRepository> _veiculoRepository;

        private readonly string[] _placa = new string[]
        {
            "MEM1715", "HZP8125", "MOO8311", "NCG7091", "NEZ0898",
            "MMR3848", "HOM1479", "LGJ0784", "KCR0141", "DBB6629",
            "NEQ7550", "LWA6466", "JYH2468", "HQB9639", "NEU8303",
            "HZH0625", "MVV4213", "MQC3694", "JRK0949", "IAP5315"
        };

        private int posPlaca = 0;
        public string _idCondutor;
        public string _cpf;

        public Veiculo VeiculoComCondutor()
        {
            var condutor = GerarCondutores(1).FirstOrDefault();
            var veiculo = GerarVeiculos(1).FirstOrDefault();

            veiculo = veiculo.AdicionarCondutor(veiculo, condutor.CondutorId, condutor.CPF);

            _idCondutor = condutor.CondutorId;
            _cpf = condutor.CPF;

            return veiculo;
        }

        public Condutor CondutorValido()
        {
            return GerarCondutores(1).FirstOrDefault();
        }

        public Condutor CondutorInvalido()
        {
            return GerarCondutorInvalido();
        }

        public Veiculo VeiculoValido()
        {
            return GerarVeiculos(1).FirstOrDefault();
        }

        public Veiculo VeiculoInvalido()
        {
            return GerarVeiculoInvalido();
        }

        public IEnumerable<Condutor> ObterCondutores()
        {
            var condutores = new List<Condutor>();

            condutores.AddRange(GerarCondutores(20).ToList());

            return condutores.AsEnumerable();
        }

        public IEnumerable<Veiculo> ObterVeiculos()
        {
            var veiculos = new List<Veiculo>();

            veiculos.AddRange(GerarVeiculos(20).ToList());

            return veiculos;
        }

        private IEnumerable<Condutor> GerarCondutores(int quantidade)
        {
            var condutores = new Faker<Condutor>("pt_BR")
                .CustomInstantiator(f => new Condutor(Guid.Empty, GeradorGuid().ToString(), ""))
                .RuleFor(p => p.CPF, f => f.Person.Cpf());

            return condutores.Generate(quantidade);
        }

        private Condutor GerarCondutorInvalido()
        {
            var condutor = new Faker<Condutor>("pt_BR")
                .CustomInstantiator(f => new Condutor(Guid.Empty, Guid.Empty.ToString(), ""));

            return condutor.Generate();
        }

        private IEnumerable<Veiculo> GerarVeiculos(int quantidade)
        {
            var veiculos = new Faker<Veiculo>("pt_BR")
                    .CustomInstantiator(f => new Veiculo(
                        "",
                        "",
                        "",
                        "",
                        0))
                    .RuleFor(p => p.Placa, f => GerarPlaca())
                    .RuleFor(p => p.Modelo, f => f.Vehicle.Model())
                    .RuleFor(p => p.Marca, f => f.Vehicle.Manufacturer())
                    .RuleFor(p => p.Cor, f => f.Commerce.Color())
                    .RuleFor(p => p.AnoFabricacao, f => f.Random.Number(1985, 2015));

            posPlaca = 0;

            return veiculos.Generate(quantidade);
        }

        private Veiculo GerarVeiculoInvalido()
        {
            var veiculo = new Faker<Veiculo>("pt_BR")
                .CustomInstantiator(f => new Veiculo(
                    "", "", "", "", default)
                { Id = Guid.Empty });

            _idCondutor = Guid.Empty.ToString();
            _cpf = "";

            return veiculo;
        }

        public Veiculo VeiculoPlacaInvalida()
        {
            return GerarVeiculoPlacaInvalida();
        }

        private Veiculo GerarVeiculoPlacaInvalida()
        {
            var veiculo = new Faker<Veiculo>("pt_BR")
                    .CustomInstantiator(f => new Veiculo(
                        "",
                        "",
                        "",
                        "",
                        0))
                    .RuleFor(p => p.Placa, f => "XP8490T")
                    .RuleFor(p => p.Modelo, f => f.Vehicle.Model())
                    .RuleFor(p => p.Marca, f => f.Vehicle.Manufacturer())
                    .RuleFor(p => p.Cor, f => f.Commerce.Color())
                    .RuleFor(p => p.AnoFabricacao, f => f.Random.Number(1985, 2015));

            _idCondutor = GeradorGuid().ToString();

            Faker _faker = new Faker("pt_BR");

            _cpf = _faker.Person.Cpf();

            return veiculo;
        }

        private string GerarPlaca(string placaNova = null)
        {
            string placa;
            if (placaNova is null)
            {
                placa = _placa[posPlaca];
                posPlaca++;
            }
            else
            {
                placa = placaNova;
            }

            return placa;
        }

        private Guid GeradorGuid()
        {
            return Guid.NewGuid();
        }

        public VeiculoCommandHandler ObterVeiculoHandler()
        {
            _mocker = new AutoMocker();
            _veiculoHandler = _mocker.CreateInstance<VeiculoCommandHandler>();

            return _veiculoHandler;
        }

        public VeiculoQueries ObterVeiculoQueries()
        {
            _veiculoRepository = new Mock<IVeiculoRepository>();
            _veiculoQueries = new VeiculoQueries(_veiculoRepository.Object, ObterCondutorMapper());

            return _veiculoQueries;
        }

        public Mapper ObterCondutorMapper()
        {
            var myProfile = new ExibirVeiculoQuerieToViewModel();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            _mapper = new Mapper(configuration);

            return _mapper;
        }

        public void Dispose()
        {

        }
    }
}
