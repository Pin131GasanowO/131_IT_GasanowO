using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Autoshop.Models.Common
{
    public class Keygen<TKey>
    {
        public TKey Id { get; set; }
    }

    public class Keygen : Keygen<int?> { }
}