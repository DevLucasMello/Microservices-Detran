using AutoMapper;

namespace TP.Veiculos.Application.AutoMapper
{
    public class AtualizarVeiculoCommandToViewModel : Profile
    {
        public AtualizarVeiculoCommandToViewModel()
        {
            //CreateMap<AdicionarVeiculoCommand, AdicionarVeiculoViewModel>();
        }
    }

    public class ViewModelToAtualizarVeiculoCommand : Profile
    {
        public ViewModelToAtualizarVeiculoCommand()
        {
            //CreateMap<AdicionarVeiculoViewModel, AdicionarVeiculoCommand>()
            //    .ConstructUsing(c => new AdicionarVeiculoCommand(Guid.Parse(c.Id), c.CPF, c.Telefone, c.Email, c.CNH, Convert.ToDateTime(c.DataNascimento)));
        }
    }
}
