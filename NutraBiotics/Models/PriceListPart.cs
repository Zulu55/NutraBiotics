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

        public override int GetHashCode()
        {
            return PriceListPartId;
        }
	}
}