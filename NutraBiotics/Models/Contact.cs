namespace NutraBiotics.Models
{
    using SQLite.Net.Attributes;

	public class Contact
    {
        [PrimaryKey]
		public int ContactId { get; set; }
		
        public int ShipToId { get; set; }
		
        public string Name { get; set; }
		
        public string Address1 { get; set; }
		
        public string PhoneNum { get; set; }
		
        public string Email { get; set; }
		
        public string EMailAddress { get; set; }

        public override int GetHashCode()
        {
            return ContactId;
        }
    }
}
