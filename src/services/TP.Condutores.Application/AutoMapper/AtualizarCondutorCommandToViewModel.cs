using AutoMapper;
using System;
using TP.Condutores.Application.Commands;
using TP.Condutores.Application.ViewModels;
using TP.Core.DomainObjects;

namespace TP.Condutores.Application.AutoMapper
{
    public class AtualizarCondutorCommandToViewModel : Profile
    {
        public AtualizarCondutorCommandToViewModel()
        {
            CreateMap<AtualizarCondutorCommand, AtualizarCondutorViewModel>()
                .ForMember(n => n.PrimeiroNome, c => c.MapFrom(c => c.Nome.PrimeiroNome))
                .ForMember(n => n.UltimoNome, c => c.MapFrom(c => c.Nome.UltimoNome));
        }
    }

    public class ViewModelToAtualizarCondutorCommand : Profile
    {
        public ViewModelToAtualizarCondutorCommand()
        {
            CreateMap<AtualizarCondutorViewModel, AtualizarCondutorCommand>()
                .ConstructUsing(c => new AtualizarCondutorCommand(Guid.Parse(c.Id), new Nome(c.PrimeiroNome, c.UltimoNome), c.CPF, c.Telefone, c.Email, c.CNH, Convert.ToDateTime(c.DataNascimento)));
        }
    }
}
