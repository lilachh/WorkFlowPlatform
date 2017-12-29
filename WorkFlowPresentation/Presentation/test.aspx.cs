using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Reflection;
using WorkFlowEngine.BLL;

namespace WorkFlowPresentation.Presentation
{

    public partial class test : System.Web.UI.Page
    {
        private Node1 node;
        private static string assemblyName = "WorkFlowEngine";
        private static string db = "Node1";
 

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string className=assemblyName+"."+db;
                node = (WorkFlowEngine.BLL.Node1)Assembly.Load(assemblyName).CreateInstance(className);
       
            }
        
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

        }
    }
}
