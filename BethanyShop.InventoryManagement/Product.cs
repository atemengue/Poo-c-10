﻿using System;
using System.Text;

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

        public Product(int id): this(id, string.Empty) {}

        public Product(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public Product(int id, string name, string? Description, UnitType unitType, int maxAmountInStock)
        {
            Id = id;
            Name = name;
            Description = description;
            UnitType = unitType;

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

            if (AmoutInStock > 10)
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

        public string DisplayDetailsFull()
        {
            StringBuilder sb = new();
            //ToDo: add price here too
            sb.Append($"{Id} {Name} \n{Description}\n{AmoutInStock} item(s) in stock");

            if (IsBelowStockThreshold)
            {
                sb.Append("\n!!STOCK LOW!!");
            }

            return sb.ToString();

        }

        public string DisplayDetailsFull(string extraDetails)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append($"{Id} {Name} \n{Description}\n{AmoutInStock} item(s) in stock");

            sb.Append(extraDetails);

            if (IsBelowStockThreshold)
            {
                sb.Append("\n!!STOCK LOW!!");
            }

            return sb.ToString();
        }

    }
}
