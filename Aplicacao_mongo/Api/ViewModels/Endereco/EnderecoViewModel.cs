namespace Api.ViewModels.Endereco
{
    public class EnderecoViewModel
    {
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Cidade { get; set; }
        public string UF { get; set; }
        public string CEP { get; set; }

        public EnderecoViewModel() { }

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
