//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebPizza_18.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Order_Detail
    {
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public byte Quantity { get; set; }
        public int UnitPrice { get; set; }
        public string Discount { get; set; }
        public int Total { get; set; }
    }
}
