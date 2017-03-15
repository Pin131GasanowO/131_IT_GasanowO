using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HardwareStore.Models.Common
{
    /// <summary>
    /// Обобщенный тип данных модели
    /// </summary>
    /// <typeparam name="TKey">Тип ключа</typeparam>
    public class GenericKey<TKey>
    {
        /// <summary>
        /// Идентификатор объекта
        /// </summary>
        public TKey Id { get; set; }
    }

    /// <summary>
    /// Тип данных модели с целочисленным ключем
    /// </summary>
    public class GenericKey : GenericKey<int?> { }
}