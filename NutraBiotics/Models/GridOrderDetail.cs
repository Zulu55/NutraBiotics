namespace NutraBiotics.Models
{
    public class GridOrderDetail
    {
		public string PartNum { get; set; }

		public string PartDescription { get; set; }

		public decimal BasePrice { get; set; }

		public double Quantity { get; set; }

		public double Discount { get; set; }

        public decimal Value 
        { 
            get 
            { 
                return BasePrice * (decimal)Quantity * (decimal)(1 - Discount); 
            }  
        }
	}
}
