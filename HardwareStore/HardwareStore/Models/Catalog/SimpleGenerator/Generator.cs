using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HardwareStore.Models.Catalog.SimpleGenerator
{
    /// <summary>
    /// Генератор каталога товаров
    /// </summary>
    public class Generator
    {
        private Random Rand = new Random();

        /// <summary>
        /// Список правил генерации
        /// </summary>
        protected List<RuleContainer> Rules { get; private set; }

        /// <summary>
        /// Менеджер для которого осуществляеться генерация объектов
        /// </summary>
        protected CatalogManager CatalogManager { get; private set; }

        public Generator(CatalogManager catalogManager)
        {
            CatalogManager = catalogManager;
            Rules = new List<RuleContainer>();
        }

        public void Process(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                // Проверяем, что есть наличие активных контейнеров для генерации.
                // Обрываем генерацию, если контейнеры закончались
                if (Rules.Count == 0) break;

                // Семя генерации
                int seed = Rand.Next();

                // Рандомный контейнер правила
                RuleContainer container = Rules[seed % Rules.Count];

                // Генерируем объект с помощью контейнера
                Product product = container.GenerateProduct(seed);

                // Сохраняем в менеджер каталога
                CatalogManager.SaveProduct(product);

                // Если для текущего правила достигнут предела, то его удаляем
                if (container.HasLimit) Rules.Remove(container);
            }
        }

        /// <summary>
        /// Добавить правило к генератору.
        /// </summary>
        /// <param name="rule">Правило генерации.</param>
        public void AddRule(Rule rule)
        {
            Rules.Add(new RuleContainer(rule));
        }
    }
}