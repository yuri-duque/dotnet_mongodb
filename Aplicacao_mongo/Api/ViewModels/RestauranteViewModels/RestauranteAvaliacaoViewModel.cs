using Domain.Entities;

namespace Api.ViewModels.RestauranteViewModels
{
    public class RestauranteAvaliacaoViewModel
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public int Cozinha { get; set; }
        public string Cidade { get; set; }
        public double Estrelas { get; set; }

        public RestauranteAvaliacaoViewModel(Restaurante restaurante, double estrelas)
        {
            Id = restaurante.Id;
            Nome = restaurante.Nome;
            Cozinha = (int) restaurante.Cozinha;
            Cidade = restaurante.Endereco.Cidade;
            Estrelas = estrelas;
        }
    }
}
