using Domain.Entities;
using Domain.Enums;
using Infra.Schemas;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            // converte a dominio restaurante para a entidade do banco mongo restauranteSchema
            var document = new RestauranteSchema(restaurante);

            _restaurantes.InsertOne(document);
        }

        public async Task<IEnumerable<Restaurante>> ObterTodosComFiltro()
        {
            var restaurantes = new List<Restaurante>();

            var filter = Builders<RestauranteSchema>.Filter.Empty;
            await _restaurantes.Find(filter).ForEachAsync(x =>
            {
                // converte a entidade do banco mongo restauranteSchema para o dominio restaurante para 
                var restaurante = x.ConverterParaDominio();

                restaurantes.Add(restaurante);
            });

            return restaurantes;
        }

        public async Task<IEnumerable<Restaurante>> ObterTodos()
        {
            var restaurantes = new List<Restaurante>();

            await _restaurantes.AsQueryable().ForEachAsync(x =>
            {
                // converte a entidade do banco mongo restauranteSchema para o dominio restaurante para 
                var restaurante = x.ConverterParaDominio();

                restaurantes.Add(restaurante);
            });

            return restaurantes;
        }

        public Restaurante ObterPorId(string id)
        {
            try
            {
                var document = _restaurantes.AsQueryable().FirstOrDefault(x => x.Id == id);

                if (document is null)
                    return null;

                return document.ConverterParaDominio();
            }
            catch
            {
                return null;
            }
        }

        public bool AlterarCompleto(Restaurante restaurante)
        {
            var document = new RestauranteSchema(restaurante, restaurante.Id);

            var resultado = _restaurantes.ReplaceOne(x => x.Id == document.Id, document);

            return resultado.ModifiedCount > 0;
        }

        public bool AlterarCozinha(string id, ECozinha cozinha)
        {
            var atualizacao = Builders<RestauranteSchema>.Update.Set(x => x.Cozinha, cozinha);

            var resultado = _restaurantes.UpdateOne(x => x.Id == id, atualizacao);

            return resultado.ModifiedCount > 0;
        }
    }
}
