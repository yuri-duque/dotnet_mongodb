using Domain.Entities;

namespace Infra.Schemas
{
    public class EnderecoSchema
    {
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Cidade { get; set; }
        public string UF { get; set; }
        public string CEP { get; set; }

        public EnderecoSchema(Endereco endereco)
        {
            Logradouro = endereco.Logradouro;
            Numero = endereco.Numero;
            Cidade = endereco.Cidade;
            CEP = endereco.CEP;
            UF = endereco.UF;
        }
    }

    public static class EnderecoSchemaExtensao
    {
        public static Endereco ConverterParaDominio(this EnderecoSchema document)
        {
            var endereco = new Endereco(document.UF, document.Cidade, document.CEP, document.Logradouro, document.Numero);

            return endereco;
        }
    }
}
