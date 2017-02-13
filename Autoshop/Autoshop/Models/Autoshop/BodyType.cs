using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Autoshop.Models.Autoshop
{
    public class BodyType : Car
    {
        public DateTime? WarrantyPeriod { get; set; }
        public int MaxWeight { get; set; }
    }

    public class Offroad : BodyType
    {
        public int EngineSize { get; set; }
        public string Color { get; set; }
        public string Valves { get; set; }
        public string GasTankSize { get; set; }
    }

    public class Sedan : BodyType
    {
        public int EngineSize { get; set; }
        public string Color { get; set; }
        public string Valves { get; set; }
        public string GasTankSize { get; set; }
    }
}