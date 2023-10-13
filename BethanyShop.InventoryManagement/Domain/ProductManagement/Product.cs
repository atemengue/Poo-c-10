using System;
using System.Text;
using BethanyShop.InventoryManagement.Domain.General;

namespace BethanyShop.InventoryManagement.Domain.ProductManagement
{
    public partial class Product
    {
        private int id;
        private string name = string.Empty;
        private string? description;

        private int maxItemsInStock = 0;

        //private UnitType unitType;
        //private int amountInStock = 0;
        //private bool isBelowStockThreshold = false;

        public Product(int id): this(id, string.Empty) {}

        public Product(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public Product(int id, string name, string? Description, Price price, UnitType unitType, int maxAmountInStock)
        {
            Id = id;
            Name = name;
            Description = description;
            UnitType = unitType;
            Price = price;

            maxItemsInStock = maxAmountInStock;

            UpdateLowStock();
        }

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

        public Price Price { get; set; }

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

        public void IncreaseStock(int amount)
        {
            int newStock = AmoutInStock + amount;

            if (newStock <= maxItemsInStock)
            {
                AmoutInStock += amount;
            } else
            {
                AmoutInStock = maxItemsInStock;
                Log($"{CreateSimpleProductRepresentation} stock overflow. {newStock - AmoutInStock} item(s) ordered that couldn't be stored");
            }

            if (AmoutInStock > StockThresold)
            {
                IsBelowStockThreshold = false;
            }
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


    }
}
