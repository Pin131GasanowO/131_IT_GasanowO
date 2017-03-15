using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HardwareStore.Models.Catalog.SimpleGenerator
{
    /// <summary>
    /// Служебная оболочка для правил генерации с расчетом количества сгенерированных объектов.
    /// </summary>
    public class RuleContainer
    {
        /// <summary>
        /// Защищенное хранилище сгенерированных объектов.
        /// </summary>
        protected Dictionary<string, Product> Store { get; private set; }

        /// <summary>
        /// Объект правила генерации служебного контейнера.
        /// </summary>
        public Rule Rule { get; private set; }

        /// <summary>
        /// Счетчик количества сгенерированных объектов.
        /// !: Можно напрямую делать запросы к Store, но данная реализация позволит переопределять данное свойство в дочерних классах.
        /// </summary>
        public int Counter
        {
            get
            {
                // Пробрасываем количество из хранилища.
                return Store.Count;
            }
        }

        /// <summary>
        /// Доступна ли генерация объектов с помощью данного свойства.
        /// </summary>
        public bool HasLimit
        {
            get
            {
                // Генерация объектов возможна только если не достигнут предел возможных комбинаций
                return Counter >= Rule.Combinations;
            }
        }

        public RuleContainer(Rule rule)
        {
            // Создаем объект хранилища
            Store = new Dictionary<string, Product>();
            Rule = rule;
        }

        /// <summary>
        /// Сгенерировать объект
        /// </summary>
        /// <param name="seed">Семя генерации</param>
        /// <returns>Объект товара</returns>
        public virtual Product GenerateProduct(int seed)
        {
            // Делаем проверку, если достигнут придел генерации, то возвращаем null
            if (HasLimit) return null;

            Product product;

            // Запускаем постфиксный цикл для поиска уникального имени товара.
            //////////////////////////////////////////////////////////////////
            //                                                              //
            // !: Данный подход являеться очень расточительным, т.к.        //
            // !: с приближением к лимиту правила количество сурогатных     //
            // !: объектов увеличиваеться (объекты создваваемые впустую).   //
            //                                                              //
            // !: В реальных проектах, при генерации необходимо создавать   //
            // !: средства сбора статистики. (Рассмотрим далее)             //
            //                                                              //
            //////////////////////////////////////////////////////////////////
            do
            {
                // Генерируем сам объект с помощью вложенного правила, из свойства Rule
                product = Rule.CreateProduct(seed);
                // Создаем название объекта
                product.Label = Rule.CreateLabel(product, seed);
            } while (Store.ContainsKey(product.Label));

            Store[product.Label] = product;

            return product;
        }
    }
}