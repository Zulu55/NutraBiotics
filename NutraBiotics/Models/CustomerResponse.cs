namespace NutraBiotics.Models
{
    using System.Collections.Generic;

	public class CustomerResponse
    {
		public int CustomerId { get; set; }

		public string Country { get; set; }

		public string State { get; set; }

		public string City { get; set; }

		public string Address { get; set; }

		public string PhoneNum { get; set; }

		public string Names { get; set; }

		public string LastNames { get; set; }

		public bool CreditHold { get; set; }

		public string TermsCode { get; set; }

		public string Terms { get; set; }

		public List<ShipToResponse> ShipTos { get; set; }
	}
}
