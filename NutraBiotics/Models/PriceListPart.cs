namespace NutraBiotics.Models
{
    using SQLite.Net.Attributes;

	public class PriceListPart
	{
		[PrimaryKey]
		public int PriceListPartId { get; set; }

		public int PriceListId { get; set; }  //Clave foranea de pricelist

		public string ListCode { get; set; }

		public int PartId { get; set; } //clave foranea de part

		public string PartNum { get; set; }

		public string PartDescription { get; set; }

		public decimal BasePrice { get; set; }

        public int PriceRange
        {
            get 
            {
                if (BasePrice < 10000) return 1;
				if (BasePrice < 30000) return 2;
				if (BasePrice < 50000) return 3;
				if (BasePrice < 100000) return 4;
                return 5;
			}
        }

        public override int GetHashCode()
        {
            return PriceListPartId;
        }
	}
}