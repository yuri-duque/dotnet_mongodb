using Api.ViewModels.AvaliacaoViewModels;
using Api.ViewModels.RestauranteViewModels;
using Domain.Enums;
using Infra.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

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
        /// Busca todos os restaurantes
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ObterRestaurantes()
        {
            var restaurantes = await _restauranteRepository.ObterTodos();

            var listagem = restaurantes.Select(x => new RestauranteListagemViewModel(x));

            return Ok(new { data = listagem });
        }

        /// <summary>
        /// Busca restaurantes pelo nome
        /// </summary>
        /// <param name="nome"></param>
        /// <returns></returns>
        [HttpGet("nome")]
        public ActionResult AlterarCozinha([FromQuery] string nome)
        {
            var restaurantes = _restauranteRepository.ObterPorNome(nome);

            var listagem = restaurantes.Select(x => new RestauranteListagemViewModel(x));

            return Ok(new { data = listagem });
        }

        [HttpGet("top3")]
        public async Task<ActionResult> ObterTop3Restaurantes()
        {
            var top3 = await _restauranteRepository.ObterTop3();

            var listagem = top3.Select(x => new RestauranteAvaliacaoViewModel(x.Key, x.Value));

            return Ok(new { data = listagem });
        }

        /// <summary>
        /// Busca o restaurante por ID
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult ObterRestaurantes(string id)
        {
            var restaurante = _restauranteRepository.ObterPorId(id);

            if (restaurante is null)
                return NotFound();

            var exibicao = new RestauranteViewModel(restaurante);

            return Ok(new { data = exibicao });
        }

        /// <summary>
        /// Insere um restaurante
        /// </summary>
        /// <param name="restauranteInclusao"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult IncluirRestaurante([FromBody] RestauranteInclusaoViewModel restauranteInclusao)
        {
            var restaurante = restauranteInclusao.ConverterParaDominio();

            if (!restaurante.Validar())
            {
                // Erro para identificar erro de sintaxe ou erro de validaçoes
                return BadRequest(new { erros = restaurante.ValidationResult.Errors.Select(x => x.ErrorMessage) });
            }

            _restauranteRepository.Inserir(restaurante);

            return Ok(new { data = "Restaurante inserido com sucesso" });
        }

        /// <summary>
        /// Altera todos os dados do restaurante
        /// </summary>
        /// <param name="restauranteAlteracao"></param>
        /// <returns></returns>
        [HttpPut]
        public ActionResult AlterarRestaurante([FromBody] RestauranteViewModel restauranteAlteracao)
        {
            var restaurante = _restauranteRepository.ObterPorId(restauranteAlteracao.Id);

            if (restaurante is null)
                return NotFound();

            restaurante = restauranteAlteracao.ConverterParaDominio();

            if (!restaurante.Validar())
            {
                return BadRequest(new { erros = restaurante.ValidationResult.Errors.Select(x => x.ErrorMessage) });
            }

            var resultado = _restauranteRepository.AlterarCompleto(restaurante);

            if (!resultado)
            {
                return BadRequest(new { erros = "Nenhum documento foi alterado" });
            }

            return Ok(new { data = "Restaurante alterado com sucesso" });
        }

        /// <summary>
        /// Altera somente a cozinha do restaurante
        /// </summary>
        /// <param name="id"></param>
        /// <param name="restauranteAlteracao"></param>
        /// <returns></returns>
        [HttpPatch("{id}")]
        public ActionResult AlterarCozinha(string id, [FromBody] RestauranteViewModel restauranteAlteracao)
        {
            var restaurante = _restauranteRepository.ObterPorId(id);

            if (restaurante is null)
                return NotFound();

            var cozinha = ECozinhaHelper.ConverterDeInteiro(restauranteAlteracao.Cozinha);

            var resultado = _restauranteRepository.AlterarCozinha(id, cozinha);

            if (!resultado)
            {
                return BadRequest(new { erros = "Nenhum documento foi alterado" });
            }

            return Ok(new { data = "Restaurante alterado com sucesso" });
        }

        /// <summary>
        /// Cria uma nova avaliação para o restaurante
        /// </summary>
        /// <param name="id"></param>
        /// <param name="restauranteAlteracao"></param>
        /// <returns></returns>
        [HttpPatch("{id}/avaliar")]
        public ActionResult AvaliarRestaurante(string id, [FromBody] AvaliacaoInclusaoViewModel avaliacaoInclusao)
        {
            var restaurante = _restauranteRepository.ObterPorId(id);

            if (restaurante is null)
                return NotFound();

            var avaliacao = avaliacaoInclusao.ConverterParaDominio();

            if (!avaliacao.Validar())
            {
                return BadRequest(new { erros = avaliacao.ValidationResult.Errors.Select(x => x.ErrorMessage) });
            }

            _restauranteRepository.Avaliar(id, avaliacao);

            return Ok(new { data = "Restaurante avaliado com sucesso" });
        }

        
    }
}
