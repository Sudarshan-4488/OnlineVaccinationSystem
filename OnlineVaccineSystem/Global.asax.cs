using OnlineVaccineSystem.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace OnlineVaccineSystem
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //registr Admin User Default
          VaccineSystemEntities2 db = new VaccineSystemEntities2();
            if (!db.UserMasters.Any(x => x.EmailId == "Admin@admin.com"))
            {
                db.UserMasters.Add(new UserMaster
                {
                    EmailId = "Admin@admin.com",
                    Address = "Default",
                    Name = "Admin",
                    BirthDate = DateTime.Now,
                    CreatedDate=DateTime.Now,
                    IsAdmin=true,
                    IsDeleted=false,
                    MobileNo="9110010101",
                    ModifiedDate=DateTime.Now,
                    Password="1234",
                });
                db.SaveChanges();
            }
    }
    }
}
