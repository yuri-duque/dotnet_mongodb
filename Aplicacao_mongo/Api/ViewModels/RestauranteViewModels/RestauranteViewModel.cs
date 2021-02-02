using Api.ViewModels.EnderecoViewModels;
using Domain.Entities;
using Domain.Enums;

namespace Api.ViewModels.RestauranteViewModels
{
    public class RestauranteViewModel
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public int Cozinha { get; set; }
        public EnderecoViewModel Endereco { get; set; }

        public RestauranteViewModel() { }

        public RestauranteViewModel(Restaurante restaurante)
        {
            Id = restaurante.Id;
            Nome = restaurante.Nome;
            Cozinha = (int)restaurante.Cozinha;
            Endereco = new EnderecoViewModel(restaurante.Endereco);
        }
    }

    public static class RestauranteViewModelExtensao
    {
        public static Restaurante ConverterParaDominio(this RestauranteViewModel viewModel)
        {
            var cozinha = ECozinhaHelper.ConverterDeInteiro(viewModel.Cozinha);

            var restaurante = new Restaurante(viewModel.Id, viewModel.Nome, cozinha);
            var endereco = viewModel.Endereco.ConverterParaDominio();
            restaurante.SetEndereco(endereco);

            return restaurante;
        }
    }
}
