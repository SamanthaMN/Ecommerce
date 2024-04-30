using Ecommerce_back.DTO;
using Ecommerce_back.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Ecommerce_back.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class ClientesController : Controller
	{
		private readonly ILogger<ClientesController> _logger;
		private readonly ClienteService _clientesService;

		public ClientesController(ILogger<ClientesController> logger)
		{
			_logger = logger;
			_clientesService = new ClienteService();
		}

		[HttpPost]
		[Route("Cadastrar")]
		public async Task<ActionResult<string>> PostCadastrarCliente(Cliente cliente)
		{
			try
			{
				await _clientesService.CadastrarCliente(cliente);
                return Ok();
			}
			catch (Exception ex)
			{
				//Cadastrar log
				return BadRequest(ex.Message);
			}
		}

		[HttpGet]
		[Route("Listar")]
		public async Task<ActionResult<Paginacao<Cliente>>> GetListarClientes(	string? ordernacao, 
																				int total, 
																				int pagina, 
																				int limite, 
																				string? campoOrdenacao,
																				string? filtroNomeRazaoSocial,
                                                                                string? filtroEmail,
                                                                                string? filtroTelefone,
                                                                                string? filtroDataCadastro,
                                                                                string? filtroBloqueado)
		{
			try
			{
				Paginacao<Cliente> paginacao = new Paginacao<Cliente>()
				{
					Resultados = new List<Cliente>(),
					Total = total,
					Limite = limite,
					Pagina = pagina,
					Ordenacao = string.IsNullOrEmpty(ordernacao)? "asc": ordernacao,
					OrdenacaoCampo = string.IsNullOrEmpty(campoOrdenacao) ? "id": campoOrdenacao
                };

				var retorno = await _clientesService.ListagemClientes(	paginacao, 
																		filtroNomeRazaoSocial,
                                                                        filtroEmail,
                                                                        filtroTelefone,
                                                                        filtroDataCadastro,
                                                                        filtroBloqueado);

				return Ok(retorno);
			}
			catch (Exception ex)
			{
				//Cadastrar log
				return BadRequest(ex.Message);
			}
		}

	}
}
