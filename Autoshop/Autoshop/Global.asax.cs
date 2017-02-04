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

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            Init_Data();
        }

        void Init_Data()
        {
            AutoshopManager am = new AutoshopManager();
            AutoshopGenerator ag = new AutoshopGenerator();
            ag.AddRule(new AutoshopGenerator.Rule()
            {
                Type = "Lada Granta",
                SubLabels = new string[][]{
                    new string []{"8 клапанов", "16 клапанов"},
                    new string []{"Седан", "LiftBack"},
                    new string []{"Норма", "Люкс"}
                }
            });

            фg.AddRule(new AutoshopGenerator.Rule()
            {
                Type = "Lada Vesta",
                SubLabels = new string[][]{
                    new string []{"8 клапанов", "16 клапанов"},
                    new string []{"База", "Турбированная"},
                    new string []{"Норма", "Sport"}
                }
            });

            for (int i = 0; i < 8; i++)
            {
                am.AddCar(ag.Generate());
            }
        }
    }
}
