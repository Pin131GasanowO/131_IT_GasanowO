using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HardwareStore.Models.Catalog.SimpleGenerator
{
    /// <summary>
    /// Тип правила генерации объектов каталога
    /// </summary>
    public abstract class Rule
    {
        /// <summary>
        /// Созданить название продукта
        /// </summary>
        /// <param name="seed">Базовое число генерации (семя)</param>
        /// <returns>Название продукта</returns>
        public abstract string CreateLabel(Product product, int seed);

        /// <summary>
        /// Создать отбект по семени
        /// </summary>
        /// <param name="seed">Базовое число генерации (семя)</param>
        /// <returns>Оюъект продукта</returns>
        public abstract Product CreateProduct(int seed);

        /// <summary>
        /// Мексимальное допустимое количество комбинаций правила.
        /// </summary>
        public abstract int Combinations { get; }
    }
}