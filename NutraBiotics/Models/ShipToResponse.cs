namespace NutraBiotics.Models
{
    using System.Collections.Generic;

	public class ShipToResponse
    {
		public int ShipToId { get; set; }

		public int CustomerId { get; set; }

		public string ShipToName { get; set; }

		public string Country { get; set; }

		public string State { get; set; }

		public string City { get; set; }

		public string Address { get; set; }

		public string PhoneNum { get; set; }

		public string Email { get; set; }

		public List<Contact> Contacts { get; set; }
	}
}
