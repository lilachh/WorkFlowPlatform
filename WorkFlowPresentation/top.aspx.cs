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
using WorkFlowEngine.BLL;

namespace WorkFlowPresentation
{
    public partial class top : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    int emplID = Convert.ToInt32(Session["EMPLID"].ToString());
                    Employee employee = new Employee(emplID);
                    lblName.Text = (string)employee.GetName();
                }
                catch (Exception ex)
                {
                    ((log4net.ILog)Session["logger"]).Error(Session["EMPLID"] + "  " + ex.Message);
                    throw ex;
                }                
            }
        }
    }
}
