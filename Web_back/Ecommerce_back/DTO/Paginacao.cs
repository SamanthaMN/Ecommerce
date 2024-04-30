namespace Ecommerce_back.DTO
{
	public class Paginacao<T>
	{
		public List<T> Resultados { get; set; }
		public int Total { get; set; }
		public int Limite { get; set; }
		public int Pagina { get; set; }
		public string? Ordenacao { get; set; }
		public string? OrdenacaoCampo { get; set; }
    }
}
