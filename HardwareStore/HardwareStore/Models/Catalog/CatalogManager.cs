using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HardwareStore.Models.Catalog
{
    public class CatalogManager
    {
        /// <summary>
        /// Хранилище данных каталога.
        /// </summary>
        private static Dictionary<int, Product> _store = new Dictionary<int, Product>();

        /// <summary>
        /// Статический конструктор по умолчанию. 
        /// Используеться для первоначальной генерации объектов.
        /// </summary>
        static CatalogManager()
        {

        }

        /// <summary>
        /// Последний назначеный идентификатор.
        /// </summary>
        private static int _lastStoreId = 0;

        /// <summary>
        /// Извлечь товар из хранилища.
        /// </summary>
        /// <param name="id">Идентификатор товара</param>
        /// <returns>Объект товара</returns>
        public Product GetProduct(int id)
        {
            // Проверяем, есть ли товар с заданным идентификатором
            if (_store.ContainsKey(id)) return _store[id];
            // Если товар с указанным идентификатором отсутствует, то возвращаем null
            return null;
        }

        /// <summary>
        /// Извлеч все товары.
        /// </summary>
        /// <returns>Перечисление товаров</returns>
        public IEnumerable<Product> GetProducts()
        {
            // Возвращаем все товары, предварительно преобразовав перечисление в массив насильно, чтобы не вызвать отложенное выполнение.
            // Сортируем по идентификатору
            return _store.Select(a => a.Value).OrderBy(a => a.Id).ToArray();
        }


        /// <summary>
        /// Извлечь определенное количество товаров.
        /// </summary>
        /// <param name="limit">Количество товаров</param>
        /// <returns>Перечисление товаров</returns>
        public IEnumerable<Product> GetProducts(int limit)
        {
            // Обратите внимание, для извлечения мы используем переопределение GetProducts() без параметров.
            // Таким образом меняя логику извлечения всех записей в одном месте, она поменяеться везде.

            ////////////////////////////////////////////////////////////////////////////////////////////
            //                                                                                        //
            // !: Данный способ не самый действенный при работе с БД на прямую с помощью ADO.NET.     // 
            // !: Но с Entity Framework и хранилищами в ОЗУ работает хорошо.                          //
            //                                                                                        //
            ////////////////////////////////////////////////////////////////////////////////////////////

            return GetProducts().Take(limit);
        }


        /// <summary>
        /// Извлечь определенное количество товаров с пропуском.
        /// </summary>
        /// <param name="limit">Количество товаров</param>
        /// <param name="skip">Пропускаемые товары</param>
        /// <returns>Перечисление товаров</returns>
        public IEnumerable<Product> GetProducts(int limit, int skip)
        {
            // Аналогично с предыдущим методом
            return GetProducts(limit).Skip(skip);
        }

        /// <summary>
        /// Добавить товар в хранилище.
        /// </summary>
        /// <param name="product">Обхект товара</param>
        public void SaveProduct(Product product)
        {
            // Назначем идентификатор, только если он не задан
            if (product.Id == null)
            {
                // Т.к. идентификатор задаеться вручную, нам необходимо всегда проверять уникальность назначеного идентификатора. 
                // Для этого запускаем цикл с постфиксной проверкой идентификатора

                do
                {
                    product.Id = ++_lastStoreId;
                    // Цикл повторяеться, пока заданный ключ находиться в хранилище.
                    // Не забываем приведение к (int), т.к. по умолчанию ключь int? (Nullable<int>).
                } while (_store.ContainsKey((int)product.Id));
            }

            // Не забываем приводить к (int)
            _store[(int)product.Id] = product;
        }

        /// <summary>
        /// Удалить товар из хранилища.
        /// </summary>
        /// <param name="id">Идентификатор товара</param>
        public void DeleteProduct(int id)
        {
            _store.Remove(id);
        }
    }
}