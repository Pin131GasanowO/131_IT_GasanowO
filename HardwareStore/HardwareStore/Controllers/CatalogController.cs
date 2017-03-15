using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HardwareStore.Models.Catalog;

namespace HardwareStore.Controllers
{
    /// <summary>
    /// Базовый контроллер витрины.
    /// </summary>
    public class CatalogController : Controller
    {
        /// <summary>
        /// Менеджер каталога товаров (используеться для обработки команд хранения данных каталога).
        /// </summary>
        protected CatalogManager CatalogManager { get; set; }

        /// <summary>
        /// Конструктор по умолчанию.
        /// </summary>
        public CatalogController()
        {
            // Создаем экземпляр менеджера каталога (будет создаваться автоматически при создании контроллера). 
            CatalogManager = new CatalogManager();
        }

        /// <summary>
        /// Метод действия списка товаров.
        /// </summary>
        public ActionResult Index()
        {
            // Передаем список товаров, извлекая из CatalogManager
            return View(CatalogManager.GetProducts());
        }
    }
}