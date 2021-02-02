using Domain.Enums;
using Domain.Entities;
using Domain.ValueObjects;

namespace Api.ViewModels.RestauranteViewModels
{
    public class RestauranteInclusaoViewModel
    {
        public string Nome { get; set; }
        public int Cozinha { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Cidade { get; set; }
        public string UF { get; set; }
        public string CEP { get; set; }
    }

    public static class RestauranteInclusaoViewModelExtensao
    {
        public static Restaurante ConverterParaDominio(this RestauranteInclusaoViewModel viewModel)
        {
            var cozinha = ECozinhaHelper.ConverterDeInteiro(viewModel.Cozinha);

            var restaurante = new Restaurante(viewModel.Nome, cozinha);
            var endereco = new Endereco(viewModel.UF, viewModel.Cidade, viewModel.CEP, viewModel.Logradouro, viewModel.Numero);
            restaurante.SetEndereco(endereco);

            return restaurante;
        }
    }
}
