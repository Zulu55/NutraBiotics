namespace NutraBiotics.Models
{
	using SQLite.Net.Attributes;

	public class ShipTo
    {
        [PrimaryKey]
		public int ShipToId { get; set; }
		
        public int CustomerId { get; set; }
		
        public string ShipToName { get; set; }
		
        public string Country { get; set; }
		
        public string State { get; set; }
		
        public string City { get; set; }
		
        public string Address { get; set; }
		
        public string PhoneNum { get; set; }
		
        public string Email { get; set; }
		
        public override int GetHashCode()
        {
            return ShipToId;
        }
    }
}
