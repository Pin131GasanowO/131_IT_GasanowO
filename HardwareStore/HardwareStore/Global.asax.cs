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
                BaseName = "Cement",
                Values = new string[][] { 
                    new string[] {"40-M", "45-M", "50-M"},
                    new string[] {"Быстро застывающий", "Долго застывающий"},
                    new string[] {"С примесью кремня", "С примесью кварца"},
                }
            });

            // Генерируем 100 записей
            generator.Process(6);
        }
    }
}