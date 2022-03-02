using AutoMapper;
using System;
using TP.Veiculos.Application.ViewModels;
using TP.Veiculos.Domain;

namespace TP.Veiculos.Application.AutoMapper
{
    public class ExibirVeiculoQuerieToViewModel : Profile
    {
        public ExibirVeiculoQuerieToViewModel()
        {
            CreateMap<Veiculo, ExibirVeiculoViewModel>()                
                .ForMember(n => n.Condutores, c => c.MapFrom(c => c.Condutor));

            CreateMap<CondutorVeiculo, ExibirVeiculoViewModel>();
        }
    }

    public class ViewModelToExibirVeiculoQuerie : Profile
    {
        public ViewModelToExibirVeiculoQuerie()
        {
            CreateMap<ExibirVeiculoViewModel, Veiculo>()
                .ConstructUsing(c => new Veiculo(c.Placa, c.Modelo, c.Marca, c.Cor, c.AnoFabricacao));

            CreateMap<CondutorVeiculoViewModel, CondutorVeiculo>()
                .ConstructUsing(c => new CondutorVeiculo(Guid.Parse(c.VeiculoId), c.CPF));
        }
    }
}
