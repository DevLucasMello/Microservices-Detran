using AutoMapper;
using System;
using TP.Veiculos.Application.Commands;
using TP.Veiculos.Application.ViewModels;

namespace TP.Veiculos.Application.AutoMapper
{
    public class AtualizarVeiculoCommandToViewModel : Profile
    {
        public AtualizarVeiculoCommandToViewModel()
        {
            CreateMap<AtualizarVeiculoCommand, AtualizarVeiculoViewModel>();
        }
    }

    public class ViewModelToAtualizarVeiculoCommand : Profile
    {
        public ViewModelToAtualizarVeiculoCommand()
        {
            CreateMap<AtualizarVeiculoViewModel, AtualizarVeiculoCommand>()
                .ConstructUsing(c => new AtualizarVeiculoCommand(Guid.Parse(c.Id), c.Placa, c.Modelo, c.Marca, c.Cor, c.AnoFabricacao));
        }
    }
}