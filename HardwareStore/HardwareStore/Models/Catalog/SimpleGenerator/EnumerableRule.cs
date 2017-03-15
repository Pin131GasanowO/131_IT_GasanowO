using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HardwareStore.Models.Catalog.SimpleGenerator
{
    public class EnumerableRule : Rule
    {
        private Random Rand = new Random();

        /// <summary>
        /// Минимальный генерируемый баланс.
        /// </summary>
        public decimal MinBalance { get; private set; }

        /// <summary>
        /// Максимальный генерируемый баланс.
        /// </summary>
        public decimal MaxBalance { get; private set; }

        /// <summary>
        /// Минимальная генерируемая цена.
        /// </summary>
        public decimal MinPrice { get; private set; }

        /// <summary>
        /// Максимальная генерируемая цена.
        /// </summary>
        public decimal MaxPrice { get; private set; }

        /// <summary>
        /// Шаг изменения цены.
        /// </summary>
        public int PriceStep { get; set; }

        public EnumerableRule()
        {
            // устанавливаем интервал для цен
            MinPrice = 1;
            MaxPrice = 10000;
            PriceStep = 1;

            // устанавливаем интервал баланса
            MinBalance = 1;
            MaxBalance = 10000;
        }

        /// <summary>
        /// Установить интервал цен.
        /// </summary>
        /// <param name="min">Минимальное значение</param>
        /// <param name="max">Максимальное значение</param>
        /// <returns>Объект правила</returns>
        public EnumerableRule SetPriceIntervel(decimal min, decimal max)
        {
            // Проверяем оба числа, чтобы гарантированно записывать минимальные и максимальные значения
            MinPrice = Math.Min(min, max);
            MaxPrice = Math.Max(min, max);
            return this;
        }

        /// <summary>
        /// Генерация цены
        /// </summary>
        /// <param name="seed">Семя генерации</param>
        /// <returns>Сгенерируемая цена</returns>
        protected decimal GeneratePrice(int seed)
        {
            decimal dirty = (seed % (MaxPrice - MinPrice)) + MinPrice;
            decimal minCounter = Math.Floor(dirty / PriceStep);
            return minCounter * PriceStep;
        }

        /// <summary>
        /// Генерация баланса
        /// </summary>
        /// <param name="seed">Семя генерации</param>
        /// <returns>Сгенерированный баланс</returns>
        protected decimal GenerateBalance(int seed)
        {
            return Math.Abs((seed % (MaxBalance - MinBalance)) + MinBalance);
        }

        /// <summary>
        /// Создать название товара.
        /// </summary>
        /// <param name="product">Объект товара</param>
        /// <param name="seed">Семя генерации</param>
        /// <returns>Строка названия товара</returns>
        public override string CreateLabel(Product product, int seed)
        {
            //Список частей названия
            List<string> parts = new List<string>();

            // Добавляем базовую часть название, если она указана
            if (BaseName != null && BaseName != String.Empty)
            {
                parts.Add(BaseName);
            }

            if (Values != null)
            {
                int i = 1;
                // для каждой линии вариации выдергиваем случаиной значение
                foreach (IEnumerable<string> varValues in Values)
                {
                    parts.Add(varValues.Skip((seed / i) % varValues.Count()).Take(1).FirstOrDefault());
                    i += Rand.Next(1, 100);
                }
            }

            return String.Concat(parts.Select(a => a + " ")).Trim();
        }

        /// <summary>
        /// Создать товар по семени
        /// </summary>
        /// <param name="seed">Семя генерации</param>
        /// <returns>Объект товара</returns>
        public override Product CreateProduct(int seed)
        {
            return new Product()
            {
                Price = GeneratePrice(seed),
                Balance = GenerateBalance(seed)
            };
        }

        /// <summary>
        /// Коичество возможных комбинаций для правила.
        /// </summary>
        public override int Combinations
        {
            get
            {
                return _valuesAmount;
            }
        }

        /// <summary>
        /// Базовая часть названия генерируемого товара.
        /// </summary>
        public string BaseName { get; set; }



        /// <summary>
        /// Наборы генерируемых значений.
        /// </summary>
        public IEnumerable<IEnumerable<string>> Values
        {
            get
            {
                return _values;
            }
            set
            {
                _values = value;
                _valuesAmount = 1;

                // Сичтаем количество вариаций. Не самый оптимальный способ, 
                // т.к. есть узкий момент нулевой инициализации (не базовая часть, ни вариации не указанны).
                foreach (IEnumerable<string> values in Values)
                {
                    _valuesAmount *= values.Count();
                }
            }
        }
        private int _valuesAmount = 0;
        private IEnumerable<IEnumerable<string>> _values;
    }
}