namespace NutraBiotics.Models
{
    using SQLite.Net.Attributes;

	public class PriceList
	{
		[PrimaryKey]
		public int PriceListId { get; set; }

        public string ListCode { get; set; }

        public string ListDescription { get; set; }

		public bool Active { get; set; }

        public override int GetHashCode()
        {
            return PriceListId;
        }
	}
}