using Domain.ValueObjects;

namespace Api.ViewModels.AvaliacaoViewModels
{
    public class AvaliacaoInclusaoViewModel
    {
        public int Estrelas { get; set; }
        public string Comentario { get; set; }
    }

    public static class EnderecoViewModelExtensao
    {
        public static Avaliacao ConverterParaDominio(this AvaliacaoInclusaoViewModel viewModel)
        {
            var avaliacao = new Avaliacao(viewModel.Estrelas, viewModel.Comentario);

            return avaliacao;
        }
    }
}
