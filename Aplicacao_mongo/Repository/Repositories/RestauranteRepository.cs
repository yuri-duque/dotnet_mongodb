using Domain.Entities;
using Domain.Enums;
using Domain.ValueObjects;
using Infra.Schemas;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infra.Repositories
{
    public class RestauranteRepository
    {
        IMongoCollection<RestauranteSchema> _restaurantes;
        IMongoCollection<AvaliacaoSchema> _avaliacoes;

        public RestauranteRepository(MongoDB mongoDB)
        {
            _restaurantes = mongoDB.DB.GetCollection<RestauranteSchema>("restaurante");
            _avaliacoes = mongoDB.DB.GetCollection<AvaliacaoSchema>("avaliacao");
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

        public IEnumerable<Restaurante> ObterPorNome(string nome)
        {
            var restaurantes = new List<Restaurante>();

            //var filter = new BsonDocument { { "nome", new BsonDocument { { "$refex", nome }, { "$options", "i" } } } };

            //_restaurantes.Find(filter)
            //    .ToList()
            //    .ForEach(x => restaurantes.Add(x.ConverterParaDominio()));

            _restaurantes.AsQueryable()
                .Where(x => x.Nome.ToLower().Contains(nome.ToLower()))
                .ToList()
                .ForEach(x => restaurantes.Add(x.ConverterParaDominio()));

            return restaurantes;
        }

        public void Avaliar(string restauranteId, Avaliacao avaliacao)
        {
            var document = new AvaliacaoSchema(avaliacao, restauranteId);

            _avaliacoes.InsertOne(document);
        }

        public async Task<Dictionary<Restaurante, double>> ObterTop3()
        {
            var retorno = new Dictionary<Restaurante, double>();

            // Pega as empresas com melhores medias de avaliação
            var top3 = _avaliacoes
                .Aggregate()
                .Group(a => a.RestauranteId, r => new { RestauranteId = r.Key, MediaEstrelas = r.Average(x => x.Estrelas) })
                .SortByDescending(x => x.MediaEstrelas)
                .Limit(3);

            // Formata o retorno para enviar a empresa com as avaliaçoes e a media
            await top3.ForEachAsync(x =>
            {
                var restaurante = ObterPorId(x.RestauranteId);

                _avaliacoes.AsQueryable()
                    .Where(a => a.RestauranteId == x.RestauranteId)
                    .ToList()
                    .ForEach(a => restaurante.InserirAvaliacao(a.ConverterParaDominio()));

                retorno.Add(restaurante, x.MediaEstrelas);
            });

            return retorno;
        }
    }
}
