using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Http;
using HardwareStore.Models.Catalog;
using HardwareStore.Models.Catalog.SimpleGenerator;

namespace HardwareStore
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            Catalog_Init();
        }

        /// <summary>
        /// Инициализировать предметную область.
        /// </summary>
        private void Catalog_Init()
        {
            // Создаем объект генератора
            Generator generator = new Generator(new CatalogManager());

            generator.AddRule(new EnumerableRule()
            {
                PriceStep = 100,
                BaseName = "Цемент",
                Values = new string[][] { 
                    new string[] {"40-M", "45-M", "50-M"},
                    new string[] {"Влагостойкий", "Термостойкий"},
                    new string[] {"С примесью кремня", "С примесью кварца"},
                }
            });

            generator.AddRule(new EnumerableRule()
            {
                PriceStep = 100,
                BaseName = "Гипс",
                Values = new string[][] { 
                    new string[] {"Волокнистый", "Зернистый"},
                    new string[] {"Акриловый", "Высокопрочный", "Полимерный"},
                    new string[] {"Строительный", "Скульптурный"},
                }
            });

            generator.AddRule(new EnumerableRule()
            {
                PriceStep = 100,
                BaseName = "Гвозди",
                Values = new string[][] { 
                    new string[] {"100", "150", "200"},
                    new string[] {"Кровельные", "Стандарт"},
                    new string[] {"Обычная шляпа", "Декоративная шляпа"},
                }
            });

            generator.AddRule(new EnumerableRule()
            {
                PriceStep = 100,
                BaseName = "Доска",
                Values = new string[][] { 
                    new string[] {"Обрезная", "Односторонне-обрезная", "Необрезная"},
                    new string[] {"Дощатый горбыль", "Горбыль", "Дощатый обапол", "Обапол", "Горбыльный обапол"},
                    new string[] {"Строганая", "Двухсторонне строганая", "Односторонне строганая", "Строганая"},
                }
            });

            generator.AddRule(new EnumerableRule()
            {
                PriceStep = 100,
                BaseName = "Арматура",
                Values = new string[][] { 
                    new string[] {"Стержневая", "Проволочная", "Каркасная сетка"},
                    new string[] {"Свариваемея", "Стойкая к корозийному растрескиванию", "Несвариваемая", "Не стойкая к корозийному растрескиванию"},
                    new string[] {"Кольцевой профиль", "Серповидный профиль", "Смешанный профиль", "Гладкая"},
                }
            });
            // Генерируем 100 записей
            generator.Process(6);
        }
    }
}