namespace NutraBiotics.Models
{
    using System;
	using System.Collections.Generic;

	public class SynOrderHeaderRequest
    {
		public int UserId { get; set; }

		public int CustomerId { get; set; }

		public bool CreditHold { get; set; }

		public DateTime Date { get; set; }

		public string TermsCode { get; set; }

		public int ShipToId { get; set; }

		public int ContactId { get; set; }

		public string SalesCategory { get; set; }

		public string Observations { get; set; }

		public List<SynOrderDetailRequest> OrderDetails { get; set; }
	}
}
