namespace NutraBiotics.Models
{
    using System.Collections.Generic;
	using SQLite.Net.Attributes;

	public class Customer
    {
        [PrimaryKey]
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
		
        public override int GetHashCode()
        {
            return CustomerId;
        }
    }
}
