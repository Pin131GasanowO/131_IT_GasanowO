using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Autoshop.Models.Total
{
    public class Keygen<Tkey>
    {
        public TKey Id { get; set; }
    }
    public class Keygen : Keygen<int?> { }
}