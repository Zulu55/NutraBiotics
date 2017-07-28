namespace NutraBiotics.Models
{
    using SQLite.Net.Attributes;
    using SQLiteNetExtensions.Attributes;

    public class OrderDetail
	{
		[PrimaryKey, AutoIncrement]
		public int SalesOrderDetaliId { get; set; }

		public int SalesOrderHeaderId { get; set; }

		public int PartId { get; set; }

		public int OrderLine { get; set; }

		public string PartNum { get; set; }

		public double OrderQty { get; set; }

		public decimal UnitPrice { get; set; }

		public decimal TaxAmt { get; set; }

		public double Discount { get; set; }

		[ManyToOne]
		public OrderHeader OrderHeader { get; set; }

		public decimal Value
		{
			get
			{
				return UnitPrice * (decimal)OrderQty * (decimal)(1 - Discount);
			}
		}

        public override int GetHashCode()
        {
            return SalesOrderDetaliId;
        }
	}
}