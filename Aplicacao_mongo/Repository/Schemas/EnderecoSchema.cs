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

        public Endereco ConvertToEndereco()
        {
            var endereco = new Endereco(UF, Cidade, CEP, Logradouro, Numero);

            return endereco;
        }
    }
}
