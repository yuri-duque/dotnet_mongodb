namespace Domain.Entities
{
    public class Endereco
    {
        public Endereco(string logradouro, string numero, string cidade, string uf, string cep)
        {
            Logradouro = logradouro;
            Numero = numero;
            Cidade = cidade;
            UF = uf;
            CEP = cep;
        }

        public string Logradouro { get; }
        public string Numero { get; }
        public string Cidade { get; }
        public string UF { get; }
        public string CEP { get; }
    }
}