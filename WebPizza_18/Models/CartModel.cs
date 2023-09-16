using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebPizza_18.Models
{
    public class CartModel
    {
        WebPizza18Entities db = new WebPizza18Entities();
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public decimal? UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal? Total { get { return UnitPrice * Quantity; } }
        public CartModel(int productID)
        {
            Product p = db.Products.FirstOrDefault(s => s.ProductID == productID);
            this.ProductID = p.ProductID;
            this.ProductName = p.ProductName;
            this.UnitPrice = p.UnitPrice;
            this.Quantity = 1;
        }
    }
}