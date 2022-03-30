using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Bogus;
using Bogus.DataSets;
using Bogus.Extensions.Brazil;
using Moq.AutoMock;
using TP.Condutores.Application.AutoMapper;
using TP.Condutores.Application.Commands;
using TP.Condutores.Application.Queries;
using TP.Condutores.Domain;
using TP.Core.DomainObjects;
using Xunit;

namespace TP.Condutores.Application.Tests.Fixtures.Tests
{
    [CollectionDefinition(nameof(CondutorAutoMockerCollection))]
    public class CondutorAutoMockerCollection : ICollectionFixture<CondutorTestsAutoMockerFixture>
    {
    }

    public class CondutorTestsAutoMockerFixture : IDisposable
    {       
        private CondutorCommandHandler _condutorHandler;
        private CondutorQueries _condutorQueries;
        public AutoMocker _mocker;
        private Mapper _mapper;
        private readonly string[] _cnh = new string[] 
        { 
            "05328871696", "37726058737", "86744899812", "07065714464", "23766815593",
            "03542869403", "92073201432", "09530740113", "06673574647", "95773315989",
            "77391335005", "14277930236", "04285235031", "69610661090", "05983890166",
            "69114496384", "71690466041", "90990066410", "60978638611", "23628944494" 
        };
        private readonly string[] _placa = new string[]
        {
            "MEM1715", "HZP8125", "MOO8311", "NCG7091", "NEZ0898",
            "MMR3848", "HOM1479", "LGJ0784", "KCR0141", "DBB6629",
            "NEQ7550", "LWA6466", "JYH2468", "HQB9639", "NEU8303",
            "HZH0625", "MVV4213", "MQC3694", "JRK0949", "IAP5315"
        };
        private int posCNH = 0;
        private int posPlaca = 0;

        public Condutor CondutorValido()
        {
            return GerarCondutores(1).FirstOrDefault();
        }

        public Condutor CondutorInvalido()
        {
            return GerarCondutorInvalido();
        }        

        public IEnumerable<Condutor> ObterCondutores()
        {
            var condutores = new List<Condutor>();

            condutores.AddRange(GerarCondutores(20).ToList());

            return condutores.AsEnumerable();
        }

        public Veiculo VeiculoValido()
        {
            return GerarVeiculos(1).FirstOrDefault();
        }

        public Veiculo VeiculoInvalido()
        {
            return GerarVeiculoInvalido();
        }

        public Veiculo VeiculoPlacaInvalida()
        {
            return GerarVeiculoPlacaInvalida();
        }

        public Condutor CondutorComVeiculo()
        {
            var condutor = GerarCondutores(1).FirstOrDefault();
            var veiculo = GerarVeiculos(1).FirstOrDefault();

            condutor.AdicionarVeiculo(condutor, veiculo);

            return condutor;
        }

        public IEnumerable<Veiculo> ObterVeiculos()
        {
            var veiculos = new List<Veiculo>();

            veiculos.AddRange(GerarVeiculos(20).ToList());

            return veiculos;
        }

        private IEnumerable<Condutor> GerarCondutores(int quantidade)
        {
            var genero = new Faker().PickRandom<Name.Gender>();

            var condutores = new Faker<Condutor>("pt_BR")
                .CustomInstantiator(f => new Condutor(
                    new Nome(f.Name.FirstName(genero), f.Name.LastName(genero)),
                    "",
                    "",
                    "",
                    "",
                    f.Date.Past(80, DateTime.Now.AddYears(-18))))

                .RuleFor(p => p.CPF, f => f.Person.Cpf())
                .RuleFor(p => p.Telefone, f => f.Person.Phone)
                .RuleFor(c => c.Email, (f, c) =>
                      f.Internet.Email(c.Nome.PrimeiroNome.ToLower(), c.Nome.UltimoNome.ToLower()))
                .RuleFor(p => p.CNH, f => GerarCNH());

            return condutores.Generate(quantidade);
        }

        private Condutor GerarCondutorInvalido()
        {
            var genero = new Faker().PickRandom<Name.Gender>();

            var condutor = new Faker<Condutor>("pt_BR")
                .CustomInstantiator(f => new Condutor(
                    new Nome("", ""), "", "", "", "", null));

            return condutor;
        }

        private IEnumerable<Veiculo> GerarVeiculos(int quantidade)
        {
            var veiculos = new Faker<Veiculo>("pt_BR")
                .CustomInstantiator(f => new Veiculo(
                    Guid.Empty,
                    "",
                    ""))
                .RuleFor(p => p.CondutorId, f => GeradorGuid())
                .RuleFor(p => p.VeiculoId, f => GeradorGuid().ToString())
                .RuleFor(p => p.Placa, f => GerarPlaca());

            return veiculos.Generate(quantidade);
        }

        private Veiculo GerarVeiculoInvalido()
        {           
            var veiculo = new Faker<Veiculo>("pt_BR")
                .CustomInstantiator(f => new Veiculo(
                    Guid.Empty,
                    Guid.Empty.ToString(),
                    ""));

            return veiculo;
        }

        private Veiculo GerarVeiculoPlacaInvalida()
        {
            var veiculo = new Faker<Veiculo>("pt_BR")
                .CustomInstantiator(f => new Veiculo(
                    Guid.Empty,
                    "",
                    ""))
                .RuleFor(p => p.CondutorId, f => GeradorGuid())
                .RuleFor(p => p.VeiculoId, f => GeradorGuid().ToString())
                .RuleFor(p => p.Placa, f => "XP8490T");

            return veiculo;
        }

        private string GerarCNH(string cnhNova = null)
        {
            string cnh;
            if (cnhNova is null)
            {
                cnh = _cnh[posCNH];
                posCNH++;
            }
            else
            {
                cnh = cnhNova;
            }

            return cnh;
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

        public CondutorCommandHandler ObterCondutorHandler()
        {
            _mocker = new AutoMocker();
            _condutorHandler = _mocker.CreateInstance<CondutorCommandHandler>();

            return _condutorHandler;
        }

        public CondutorQueries ObterCondutorQueries()
        {
            _mocker = new AutoMocker();
            _condutorQueries = _mocker.CreateInstance<CondutorQueries>();

            return _condutorQueries;
        }

        public Mapper ObterCondutorMapper()
        {
            var myProfile = new ExibirCondutorQuerieToViewModel();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            _mapper = new Mapper(configuration);

            return _mapper;
        }

        public void Dispose()
        {
        }
    }
}