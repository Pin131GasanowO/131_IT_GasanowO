using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Http;
using ComputerShop.Models.Catalog;

namespace ComputerShop
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            Init_Data();
        }

        void Init_Data()
        {
            CatalogManager cm = new CatalogManager();
            CatalogGenerator cg = new CatalogGenerator();
            cg.AddRule(new GypsumRule()
            {
                Type = "Гипс",
                StartDate = DateTime.Now.AddYears(1),
                Varieties = new string[] { "Карьерный", "Заводской" },
                DateMaxDaysRange = 365,
                SubLabels = new string[][]{
                    new string []{"Мраморный", "Каменный"},
                }
            });

            cg.AddRule(new CementRule()
            {
                StartDate = DateTime.Now.AddDays(10),
                Varieties = new string[] { "90%", "95%", "100%"},
                City = new string[] { "Черкеск", "Саратов", "Ставропль" },
                DateMaxDaysRange = 750,
                SubLabels = new string[][]{
                }
            });

            for (int i = 0; i < 16; i++)
            {
                cm.AddArticle(cg.Generate());
            }
            var t = 1;
        }
    }
}