using AutoMapper;

namespace TP.Veiculos.Application.AutoMapper
{
    public class AdicionarVeiculoCommandToViewModel : Profile
    {
        public AdicionarVeiculoCommandToViewModel()
        {
            //CreateMap<AdicionarVeiculoCommand, AdicionarVeiculoViewModel>();
        }
    }

    public class ViewModelToAdicionarVaiculoCommand : Profile
    {
        public ViewModelToAdicionarVaiculoCommand()
        {
            //CreateMap<AdicionarVeiculoViewModel, AdicionarVeiculoCommand>()
            //    .ConstructUsing(c => new AdicionarVeiculoCommand(c.CPF, c.Telefone, c.Email, c.CNH, Convert.ToDateTime(c.DataNascimento)));
        }
    }
}
