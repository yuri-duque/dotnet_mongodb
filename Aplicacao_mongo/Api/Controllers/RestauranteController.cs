using Api.Controllers.ViewModels.Restaurante;
using Domain.Entities;
using Domain.Enums;
using Infra.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RestauranteController : ControllerBase
    {
        private readonly RestauranteRepository _restauranteRepository;

        public RestauranteController(RestauranteRepository restauranteRepository)
        {
            _restauranteRepository = restauranteRepository;
        }

        /// <summary>
        /// Insere um restaurante
        /// </summary>
        /// <param name="restauranteInclusao"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult IncluirRestaurante([FromBody] RestauranteInclusaoViewModel restauranteInclusao)
        {
            var cozinha = ECozinhaHelper.ConverterDeInteiro(restauranteInclusao.Cozinha);

            var restaurante = new Restaurante(restauranteInclusao.Nome, cozinha);
            var endereco = new Endereco(restauranteInclusao.UF, restauranteInclusao.Cidade, restauranteInclusao.CEP, restauranteInclusao.Logradouro, restauranteInclusao.Numero);
            restaurante.SetEndereco(endereco);

            if (!restaurante.Validar())
            {
                // Erro para identificar erro de sintaxe ou erro de validaçoes
                return BadRequest(new { erros = restaurante.ValidationResult.Errors.Select(x => x.ErrorMessage) });
            }

            _restauranteRepository.Inserir(restaurante);

            return Ok(new { data = "Restaurante inserido com sucesso"});
        }
    }
}
