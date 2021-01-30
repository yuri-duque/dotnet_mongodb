using Domain.Entities;
using Infra.Schemas;
using MongoDB.Driver;

namespace Infra.Repositories
{
    public class RestauranteRepository
    {
        IMongoCollection<RestauranteSchema> _restaurantes;

        public RestauranteRepository(MongoDB mongoDB)
        {
            _restaurantes = mongoDB.DB.GetCollection<RestauranteSchema>("restaurante");
        }

        public void Inserir(Restaurante restaurante)
        {
            var document = new RestauranteSchema(restaurante);

            _restaurantes.InsertOne(document);
        }
    }
}
