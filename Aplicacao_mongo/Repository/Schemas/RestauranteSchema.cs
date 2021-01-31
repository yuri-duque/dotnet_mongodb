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

        public RestauranteSchema(Restaurante restaurante, string id)
        {
            Id = id;
            Nome = restaurante.Nome;
            Cozinha = restaurante.Cozinha;
            Endereco = new EnderecoSchema(restaurante.Endereco);
        }
    }

    public static class RestauranteSchemaExtensao
    {
        public static Restaurante ConverterParaDominio(this RestauranteSchema document)
        {
            var restaurante = new Restaurante(document.Id, document.Nome, document.Cozinha);
            var endereco = document.Endereco.ConverterParaDominio();

            restaurante.SetEndereco(endereco);

            return restaurante;

        }
    }
}
