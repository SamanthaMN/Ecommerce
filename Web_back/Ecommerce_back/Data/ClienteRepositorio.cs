using Ecommerce_back.DTO;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
using System.Data;

namespace Ecommerce_back.Data
{
	public sealed class ClienteRepositorio
	{
		private readonly string _connection;

		public ClienteRepositorio() //IConfiguration configuration
		{
			_connection = "server=localhost;user=root;database=EcommerceTeste;port=3306;password=root";//Configuration.GetConnectionString("ConnectionStrings");
		}

		public async Task InsertCliente(Cliente cliente)
		{
			try
			{
				string retornoInsercaoCliente = string.Empty;

				using (var conexao = new MySqlConnection(_connection))
				{
					conexao.Open();

					using (var comando = new MySqlCommand("Ecommerce_ClienteInserir", conexao))
					{
						comando.CommandType = CommandType.StoredProcedure;

						comando.Parameters.AddWithValue("p_NomeRazaoSocial", cliente.NomeRazaoSocial);
						comando.Parameters.AddWithValue("p_Email", cliente.Email);
						comando.Parameters.AddWithValue("p_Telefone", cliente.Telefone);
						comando.Parameters.AddWithValue("p_DataCadastro", cliente.DataCadastro);

						comando.Parameters.AddWithValue("p_TipoPessoa", cliente.TipoPessoa);
						comando.Parameters.AddWithValue("p_CpfCnpj", cliente.CpfCnpj);
						comando.Parameters.AddWithValue("p_InscricaoEstadual", cliente.InscricaoEstadual);
						comando.Parameters.AddWithValue("p_Isento_InscricaoEstadual", cliente.Isento_InscricaoEstadual);

						comando.Parameters.AddWithValue("p_IdGenero", cliente.IdGenero);
						comando.Parameters.AddWithValue("p_DataNascimento", cliente.DataNascimento);

						comando.Parameters.AddWithValue("p_Bloqueado", cliente.Bloqueado);
						comando.Parameters.AddWithValue("p_Senha", cliente.Senha);

						await comando.ExecuteNonQueryAsync();

					}
				}
			}
			catch (Exception ex)
			{
				throw new Exception($"Erro em operação do banco de dados: {ex.Message}", ex);
			}
		}

		public async Task<Paginacao<Cliente>> ListCliente(Paginacao<Cliente> paginacao,
                                                                string? filtroNomeRazaoSocial,
                                                                string? filtroEmail,
                                                                string? filtroTelefone,
                                                                string? filtroDataCadastro,
                                                                string? filtroBloqueado)
        {
			try
			{
				Paginacao<Cliente> ListagemCliente = paginacao;

				using (var conexao = new MySqlConnection(_connection))
				{
					conexao.Open();

					using (var comando = new MySqlCommand("Ecommerce_ClienteListar", conexao))
					{
						comando.CommandType = CommandType.StoredProcedure;

						comando.Parameters.AddWithValue("p_Limite", paginacao.Limite);
						comando.Parameters.AddWithValue("p_Pagina", paginacao.Pagina);
						comando.Parameters.AddWithValue("p_Ordenacao", paginacao.Ordenacao);
                        comando.Parameters.AddWithValue("p_OrdenacaoCampo", paginacao.OrdenacaoCampo);
                        comando.Parameters.AddWithValue("p_FiltroNomeRazaoSocial", filtroNomeRazaoSocial);
                        comando.Parameters.AddWithValue("p_FiltroEmail", filtroEmail);
                        comando.Parameters.AddWithValue("p_FiltroTelefone", filtroTelefone);
                        comando.Parameters.AddWithValue("p_FiltroDataCadastro", filtroDataCadastro);
                        comando.Parameters.AddWithValue("p_FiltroBloqueado", filtroBloqueado);
                        comando.Parameters.Add("p_Total", MySqlDbType.Int64).Direction = ParameterDirection.Output;

						var retorno = await comando.ExecuteReaderAsync();

						while (retorno.Read())
						{
							var cliente = new Cliente()
							{
								NomeRazaoSocial = retorno["NomeRazaoSocial"].ToString(),
								Email = retorno["Email"].ToString(),
								Telefone = (int) retorno["Telefone"],
								DataCadastro = (DateTime) retorno["DataCadastro"],
								Bloqueado = (bool) retorno["Bloqueado"]
							};

							ListagemCliente.Resultados.Add(cliente);
						}
						ListagemCliente.Total = Convert.ToInt32(comando.Parameters["p_Total"].Value);

                    }
				}
				return ListagemCliente;
			}
			catch (Exception ex)
			{
				throw new Exception($"Erro em operação do banco de dados: {ex.Message}", ex);
			}
		}
	}
}
