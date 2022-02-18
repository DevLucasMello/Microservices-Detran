using AutoMapper;
using System;
using TP.Condutores.Application.Commands;
using TP.Condutores.Application.ViewModels;
using TP.Core.DomainObjects;

namespace TP.Condutores.Application.AutoMapper
{
    public class AdicionarCondutorCommandToViewModel : Profile
    {
        public AdicionarCondutorCommandToViewModel()
        {
            CreateMap<AdicionarCondutorCommand, AdicionarCondutorViewModel>()
                .ForMember(n => n.PrimeiroNome, c => c.MapFrom(c => c.Nome.PrimeiroNome))
                .ForMember(n => n.UltimoNome, c => c.MapFrom(c => c.Nome.UltimoNome));
        }
    }

    public class ViewModelToAdicionarCondutorCommand : Profile
    {
        public ViewModelToAdicionarCondutorCommand()
        {
            CreateMap<AdicionarCondutorViewModel, AdicionarCondutorCommand>()
                .ConstructUsing(c => new AdicionarCondutorCommand(new Nome(c.PrimeiroNome, c.UltimoNome), c.CPF, c.Telefone, c.Email, c.CNH, Convert.ToDateTime(c.DataNascimento)));
        }
    }
}