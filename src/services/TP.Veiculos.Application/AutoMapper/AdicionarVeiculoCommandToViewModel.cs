using AutoMapper;
using System;
using TP.Veiculos.Application.Commands;
using TP.Veiculos.Application.ViewModels;

namespace TP.Veiculos.Application.AutoMapper
{
    public class AdicionarVeiculoCommandToViewModel : Profile
    {
        public AdicionarVeiculoCommandToViewModel()
        {
            CreateMap<AdicionarVeiculoCommand, AdicionarVeiculoViewModel>();
        }
    }

    public class ViewModelToAdicionarVaiculoCommand : Profile
    {
        public ViewModelToAdicionarVaiculoCommand()
        {
            CreateMap<AdicionarVeiculoViewModel, AdicionarVeiculoCommand>()
                .ConstructUsing(c => new AdicionarVeiculoCommand(Guid.Parse(c.CondutorId), c.Placa, c.Modelo, c.Marca, c.Cor, c.AnoFabricacao, c.CPF));
        }
    }
}