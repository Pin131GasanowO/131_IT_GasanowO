using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autoshop.Models.Common;

namespace Autoshop.Models.Autoshop
{
    public class Car : Keygen
    {

        public string Label { get; set; }

        public decimal Price { get; set; }

        public decimal Balance { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }
    }
}