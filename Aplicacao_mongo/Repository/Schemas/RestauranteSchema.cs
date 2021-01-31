using Domain.Entities;
using Domain.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Infra.Schemas
{
    public class RestauranteSchema
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Nome { get; set; }
        public ECozinha Cozinha { get; set; }
        public EnderecoSchema Endereco { get; set; }

        public RestauranteSchema() { }

        public RestauranteSchema(Restaurante restaurante)
        {
            Nome = restaurante.Nome;
            Cozinha = restaurante.Cozinha;
            Endereco = new EnderecoSchema(restaurante.Endereco);
        }

        public Restaurante ConvertToRestaurante()
        {
            var restaurante = new Restaurante(Id, Nome, Cozinha);
            var endereco = Endereco.ConvertToEndereco();

            restaurante.SetEndereco(endereco);

            return restaurante;

        }
    }
}
