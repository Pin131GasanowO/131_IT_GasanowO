using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComputerShop.Models.Common
{
    public class KeyGen<TKey>
    {
        public TKey Id { get; set; }
    }
    public class KeyGen : KeyGen<int?> { }
}