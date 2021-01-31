using Domain.Enums;

namespace Api.Controllers.ViewModels.Restaurante
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
        public static Domain.Entities.Restaurante ConverterParaDominio(this RestauranteInclusaoViewModel viewModel)
        {
            var cozinha = ECozinhaHelper.ConverterDeInteiro(viewModel.Cozinha);

            var restaurante = new Domain.Entities.Restaurante(viewModel.Nome, cozinha);
            var endereco = new Domain.Entities.Endereco(viewModel.UF, viewModel.Cidade, viewModel.CEP, viewModel.Logradouro, viewModel.Numero);
            restaurante.SetEndereco(endereco);

            return restaurante;
        }
    }
}
