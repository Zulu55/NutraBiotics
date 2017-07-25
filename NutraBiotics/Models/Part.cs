namespace NutraBiotics.Models
{
    using SQLite.Net.Attributes;

	public class Part
	{
		[PrimaryKey]
		public int PartId { get; set; }

		public string PartNum { get; set; }

		public string PartDescription { get; set; }

		public string Picture { get; set; }

        public override int GetHashCode()
        {
            return PartId;  
        }
	}
}