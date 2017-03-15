using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HardwareStore.Models.Common;

namespace HardwareStore.Models.Catalog
{
    /// <summary>
    /// Базовый тип товаров в системе
    /// </summary>
    public class Product : GenericKey
    {
        /// <summary>
        /// Название товара, для отбражения в интерфейсе
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// Остаток товара на складе
        /// </summary>
        public decimal Balance { get; set; }

        /// <summary>
        /// Цена товара
        /// </summary>
        public decimal Price { get; set; }
    }
}