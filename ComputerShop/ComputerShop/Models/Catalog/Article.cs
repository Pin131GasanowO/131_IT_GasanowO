using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ComputerShop.Models.Common;

namespace ComputerShop.Models.Catalog
{
    public class Article : KeyGen
    {
        public string Label { get; set; }
        public decimal Price { get; set; }
        public decimal Balance { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
    }
}