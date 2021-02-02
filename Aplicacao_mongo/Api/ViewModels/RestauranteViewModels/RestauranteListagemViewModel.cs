using Domain.Entities;

namespace Api.Controllers
{
    public class RestauranteListagemViewModel
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public int Cozinha { get; set; }
        public string Cidade { get; set; }

        public RestauranteListagemViewModel(Restaurante restaurante)
        {
            Id = restaurante.Id;
            Nome = restaurante.Nome;
            Cozinha = (int) restaurante.Cozinha;
            Cidade = restaurante.Endereco?.Cidade;
        } 
    }
}