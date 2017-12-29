using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Xml.Linq;

namespace WorkFlowPresentation
{
    public class Global : System.Web.HttpApplication
    {
        public log4net.ILog Logger = log4net.LogManager.GetLogger("asdfasdfasdfasdfa");


        protected void Application_Start(object sender, EventArgs e)
        {
            log4net.Config.XmlConfigurator.Configure();
            
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            Session["logger"] = Logger;
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {
            string errMsg = "";
            if (Server.GetLastError().InnerException != null)
            {
                errMsg = Server.GetLastError().InnerException.Message + "\n" + Session["EMPLID"];
                
            }
            Application["ErrorMessage"] = errMsg;

        }

        protected void Session_End(object sender, EventArgs e)
        {
            
        }

        protected void Application_End(object sender, EventArgs e)
        {
            log4net.LogManager.Shutdown();
        }
    }
}