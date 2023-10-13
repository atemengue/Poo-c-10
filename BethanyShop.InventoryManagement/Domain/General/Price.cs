using System;

namespace BethanyShop.InventoryManagement.Domain.General
{
	public class Price
	{
        //public Price(int v, Currency euro)
        //{
        //    ItemPrice = v;
        //    Currency = euro;
        //}

        public double ItemPrice { get; set; }

		public Currency Currency { get; set; }

        public override string ToString()
        {
            return $"{ItemPrice} {Currency}";
        }
    }
}

