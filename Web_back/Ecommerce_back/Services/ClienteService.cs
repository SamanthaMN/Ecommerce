using Ecommerce_back.Data;
using Ecommerce_back.DTO;

namespace Ecommerce_back.Services
{
	public class ClienteService
	{
		private readonly ClienteRepositorio _clienteRepositorio;

		public ClienteService()
		{
			_clienteRepositorio = new ClienteRepositorio();
		}

		public async Task CadastrarCliente(Cliente cliente)
		{
			try
			{
				if (!CamposValidos(cliente))
				{
					throw new Exception($"Validar os campos obrigatórios"); 
				}

				await _clienteRepositorio.InsertCliente(cliente);
			}
			catch (Exception ex)
			{
				throw new Exception($"ClienteCadastrar - {ex.Message}", ex);
			}
		}

		public async Task<Paginacao<Cliente>> ListagemClientes(Paginacao<Cliente> paginacao, 
                                                                string? filtroNomeRazaoSocial,
                                                                string? filtroEmail,
                                                                string? filtroTelefone,
                                                                string? filtroDataCadastro,
                                                                string? filtroBloqueado)
        {
			try
			{
				return await _clienteRepositorio.ListCliente(paginacao,
                                                             filtroNomeRazaoSocial,
                                                             filtroEmail,
                                                             filtroTelefone,
                                                             filtroDataCadastro,
                                                             filtroBloqueado);
            }
			catch (Exception ex)
			{
				throw new Exception($"ClienteListar - {ex.Message}", ex);
			}
		}


		private static bool CamposValidos(Cliente cliente)
		{
			bool validos;

			if (String.IsNullOrEmpty(cliente.NomeRazaoSocial) ||
				String.IsNullOrEmpty(cliente.Email) ||
				String.IsNullOrEmpty(cliente.CpfCnpj)||
				String.IsNullOrEmpty(cliente.Senha))
			{
				validos = false;
			}
			else if (cliente.Telefone <= 0 ||
					 cliente.TipoPessoa <=0 ||
					 cliente.InscricaoEstadual <= 0 ||
					 cliente.IdGenero <= 0)
			{
				validos = false;
			}
			else if (cliente.DataNascimento >= DateTime.Now)
			{
				validos = false;
			}
			else
				validos = true;

			return validos;
		}
	}
}
