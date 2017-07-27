namespace NutraBiotics.Models
{
    using SQLite.Net.Attributes;

	public class OrderDetail
	{
		[PrimaryKey]
		public int SalesOrderDetaliId { get; set; }

		public int SalesOrderHeaderId { get; set; }

		public int PriceListPartId { get; set; }

		public int PartId { get; set; }

		public int OrderLine { get; set; }

		public string PartNum { get; set; }

		public decimal OrderQty { get; set; }

		public decimal UnitPrice { get; set; }

		public decimal TaxAmt { get; set; }

        public override int GetHashCode()
        {
            return SalesOrderDetaliId;
        }
	}
}