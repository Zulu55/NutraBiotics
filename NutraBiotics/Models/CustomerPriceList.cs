namespace NutraBiotics.Models
{
    using SQLite.Net.Attributes;

    public class CustomerPriceList
	{
		[PrimaryKey]
		public int CustomerPriceListId { get; set; }

		public int PriceListId { get; set; }

		public int CustomerId { get; set; }

        public override int GetHashCode()
        {
            return CustomerPriceListId;
        }
	}
}
