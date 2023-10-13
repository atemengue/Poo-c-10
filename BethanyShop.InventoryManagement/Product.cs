using System;

namespace BethanyShop.InventoryManagement
{
    public class Product
    {
        private int id;
        private string name = string.Empty;
        private string? description;

        private int maxItemsInStock = 0;

        //private UnitType unitType;
        //private int amountInStock = 0;
        //private bool isBelowStockThreshold = false;

        public int Id
        {
            get { return id;  }
            set { id = value; }
        }

        public string Name
        {
            get { return name; }
            set
            {
                name = value.Length > 50 ? value[..50] : value
;            }
        }

        public string? Description
        {
            get { return description; }
            set
            {
                if (value == null)
                {
                    description = string.Empty;
                } else {
                    description = value.Length > 250 ? value[..250] : value;
                }
            }
        }

        public UnitType UnitType { get; set; }
        public int AmoutInStock { get; private set; }
        public bool IsBelowStockThreshold { get; private set; }

        public void UseProduct(int items)
        {
            if (items <= AmoutInStock)
            {
                //use the items
                AmoutInStock -= items;

                UpdateLowStock();

                Log($"Amount in stock updated. Now {AmoutInStock} items in stock.");
            }
            else
            {
                Log($"Not enough items on stock for {CreateSimpleProductRepresentation()}. {AmoutInStock} available but {items} requested.");
            }
        }

        public void IncreaseStock()
        {
            AmoutInStock++;
        }

        private void DecreaseStock(int items, string reason)
        {
            if (items <= AmoutInStock)
            {
                //decrease the stock with the specified number items
                AmoutInStock -= items;
            }
            else
            {
                AmoutInStock = 0;
            }

            UpdateLowStock();

            Log(reason);
        }

        private void UpdateLowStock()
        {
            if (AmoutInStock < 10)//for now a fixed value
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

    }
}
