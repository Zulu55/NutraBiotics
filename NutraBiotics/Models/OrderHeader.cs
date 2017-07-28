namespace NutraBiotics.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using SQLite.Net.Attributes;
    using SQLiteNetExtensions.Attributes;

    public class OrderHeader
	{
		[PrimaryKey, AutoIncrement]
		public int SalesOrderHeaderId { get; set; }

		public int UserId { get; set; }

		public int CustomerId { get; set; }

		public bool CreditHold { get; set; }

		public DateTime Date { get; set; }

		public string TermsCode { get; set; }

		public int ShipToId { get; set; }

		public int ContactId { get; set; }

		public string SalesCategory { get; set; }

		public string Observations { get; set; }

		public bool IsSync { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
		public List<OrderDetail> OrderDetails { get; set; }

        public decimal Total
        {
            get 
            {
                if (OrderDetails == null)
                {
                    return 0;
                }

                return OrderDetails.Sum(od => od.Value);
            }
        }

		[ManyToOne]
		public Customer Customer { get; set; }

		public override int GetHashCode()
        {
            return SalesOrderHeaderId;
        }
	}
}