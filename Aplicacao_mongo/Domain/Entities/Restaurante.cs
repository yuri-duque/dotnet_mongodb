using Domain.Enums;

namespace Domain.Entities
{
    public class Restaurante
    {
        public Restaurante(string id, string nome, ECozinha cozinha)
        {
            Id = id;
            Nome = nome;
            Cozinha = cozinha;
        }

        public string Id { get; }
        public string Nome { get; }
        public ECozinha Cozinha { get; }
        public Endereco Endereco { get; }
    }
}
