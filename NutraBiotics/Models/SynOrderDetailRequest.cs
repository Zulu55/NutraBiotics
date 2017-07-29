namespace NutraBiotics.Models
{
    public class SynOrderDetailRequest
    {
		public int PartId { get; set; }

		public int PriceListPartId { get; set; }

		public int OrderLine { get; set; }

		public string PartNum { get; set; }

		public double OrderQty { get; set; }

		public decimal UnitPrice { get; set; }

		public decimal TaxAmt { get; set; }

		public double Discount { get; set; }
	}
}
