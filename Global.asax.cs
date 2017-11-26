using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Timers;

namespace PizaStore
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();

            //Schedule Email
            // Code that runs on application startup
            System.Timers.Timer myTimer = new System.Timers.Timer();
            // Set the Interval to 5 seconds (5000 milliseconds).
            myTimer.Interval = 5000;
            myTimer.AutoReset = true;
            myTimer.Elapsed += new ElapsedEventHandler(myTimer_Elapsed);
            myTimer.Enabled = true;
        }

        //Schedule Email 
        public void myTimer_Elapsed(object source, System.Timers.ElapsedEventArgs e)
        {
            ScheduleMail objScheduleMail = new ScheduleMail();

            //this line will send a email every 5 seconds
            //objScheduleMail.SendScheduleMail();
        }

        void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown

        }

        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs

        }

        void Session_Start(object sender, EventArgs e)
        {
            // Code that runs when a new session is started

        }

        void Session_End(object sender, EventArgs e)
        {
            // Code that runs when a session ends. 
            // Note: The Session_End event is raised only when the sessionstate mode
            // is set to InProc in the Web.config file. If session mode is set to StateServer 
            // or SQLServer, the event is not raised.

        }
    }

    public class ScheduleMail
    {
        public void SendScheduleMail()
        {
            PizaStore.Models.MailNotifyInfo mailNotifyInfo = new PizaStore.Models.MailNotifyInfo();
            mailNotifyInfo.EmailAddress = "ytt10ytt10@gmail.com";
            mailNotifyInfo.Subject = "auto test subject";
            mailNotifyInfo.TextBody = "auto this is a test";

            NotificationHandler.Instance.AppendNotification(mailNotifyInfo);
        }
    }
}