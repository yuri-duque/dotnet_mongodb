namespace Api.ViewModels.Endereco
{
    public class EnderecoViewModel
    {
        public string Logradouro { get; }
        public string Numero { get; }
        public string Cidade { get; }
        public string UF { get; }
        public string CEP { get; }

        public EnderecoViewModel(Domain.Entities.Endereco endereco)
        {
            Logradouro = endereco.Logradouro;
            Numero = endereco.Numero;
            Cidade = endereco.Cidade;
            UF = endereco.UF;
            CEP = endereco.CEP;
        }
    }

    public static class EnderecoViewModelExtensao
    {
        public static Domain.Entities.Endereco ConverterParaDominio(this EnderecoViewModel viewModel)
        {
            var endereco = new Domain.Entities.Endereco(viewModel.UF, viewModel.Cidade, viewModel.CEP, viewModel.Logradouro, viewModel.Numero);

            return endereco;
        }
    }
}
