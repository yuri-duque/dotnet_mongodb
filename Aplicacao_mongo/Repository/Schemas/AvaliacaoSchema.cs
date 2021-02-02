
using Domain.ValueObjects;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Infra.Schemas
{
    public class AvaliacaoSchema
    {
        
        public ObjectId Id { get; set; }// Usado ObjectId nesse cado pois esse Id nunca irá para o dominior

        [BsonRepresentation(BsonType.ObjectId)]
        public string RestauranteId { get; set; }
        public int Estrelas { get; set; }
        public string Comentario { get; set; }

        public AvaliacaoSchema(Avaliacao avaliacao, string restauranteId)
        {
            Estrelas = avaliacao.Estrelas;
            Comentario = avaliacao.Comentario;
            RestauranteId = restauranteId;
        }
    }

    public static class AvaliacaoSchemaExtensao
    {
        public static Avaliacao ConverterParaDominio(this AvaliacaoSchema document)
        {
            var avaliacao = new Avaliacao(document.Estrelas, document.Comentario);

            return avaliacao;
        }
    }
}
