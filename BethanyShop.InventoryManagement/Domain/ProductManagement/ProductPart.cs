using System;
using System.Text;
using BethanyShop.InventoryManagement.Domain.General;

namespace BethanyShop.InventoryManagement.Domain.ProductManagement
{

	public partial class Product
	{
        public static int StockThresold = 5;

        public static void ChangeStockThreshold(int newStockThreshold)
        {
            if (newStockThreshold > 0)
            {
                StockThresold = newStockThreshold;
            }
        }
        public void UpdateLowStock()
        {
            if (AmountInStock < StockThresold)//for now a fixed value
            {
                IsBelowStockThreshold = true;
            }
        }

        private void Log(string message)
        {
            //this could be written to a file
            Console.WriteLine(message);
        }

        private string CreateSimpleProductRepresentation()
        {
            return $"Product {id} ({name})";
        }


        public string DisplayDetailsFull(string extraDetails)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append($"{Id} {Name} \n{Description}\n{Price}\n{AmountInStock} item(s) in stock");

            sb.Append(extraDetails);

            if (IsBelowStockThreshold)
            {
                sb.Append("\n!!STOCK LOW!!");
            }

            return sb.ToString();
        }

    }
}

