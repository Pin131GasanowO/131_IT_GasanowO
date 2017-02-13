using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Http;
using Autoshop.Models.Autoshop;

namespace Autoshop
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Код, выполняемый при запуске приложения
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);            
        }

        void Init_Data()
        {
            AutoshopManager am = new AutoshopManager();
            AutoshopGenerator ag = new AutoshopGenerator();
            ag.AddRule(new SedanRule()
            {
                Type = "Lada Granta",
                StartDate = DateTime.Now.AddYears(1),
                Colors = new string[] { "Черный", "Белый", "Серый", "Серебристый" },
                DateMaxDaysRange = 365,
                SubLabels = new string[][]{
                    new string []{"Sport", "Standard", "Comfort"},
                }
            });

            for (int i = 0; i < 8; i++)
            {
                am.AddCar(ag.Generate());
            }
            var t = 1;
        }
    }
}