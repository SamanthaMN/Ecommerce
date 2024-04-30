namespace Ecommerce_back.DTO
{
	public class Cliente
	{
		#region InformacoesPricipais
		public string NomeRazaoSocial { get; set; } //Obrigatório
		public string Email { get; set; } //Obrigatório
		public long Telefone { get; set; } //Obrigatório
		public DateTime DataCadastro { get; set; } //Obrigatório devido a não ser informado pelo usuario

		#endregion InformacoesPricipais

		#region InformacoesPessoais
		public int TipoPessoa { get; set; } //Obrigatório
		public string CpfCnpj { get; set; } //Obrigatório
		public long InscricaoEstadual { get; set; } //Obrigatório
		public bool Isento_InscricaoEstadual { get; set; }

		#endregion InformacoesPessoais

		#region InformacoesPessoaFisica

		public int IdGenero { get; set; } //Obrigatório
		public DateTime DataNascimento { get; set; } //Obrigatório

		#endregion InformacoesPessoaFisica

		#region Acesso

		public bool Bloqueado { get; set; }
		public string Senha { get; set; } //Obrigatório

		#endregion Acesso
	}
}
