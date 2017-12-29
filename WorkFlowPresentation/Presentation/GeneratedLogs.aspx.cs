using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WorkFlowEngine.BLL;
using System.Data.SqlClient;

namespace WorkFlowPresentation.Presentation
{
    public partial class GeneratedLogs : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {                
                try
                {
                    int systemID = Convert.ToInt32(Request.QueryString["SystemID"]);
                    string test = Request.QueryString["DocumentID"];
                    int documentID = Convert.ToInt32(Request.QueryString["DocumentID"]);
                    this.ApproverFlow.DataSource = WorkFlow.GetApproverFlow(systemID, documentID);
                    this.ApproverFlow.DataBind();
                }
                catch (SqlException ex)
                {
                    ((log4net.ILog)Session["logger"]).Error(Session["EMPLID"] + "  " + ex.Message);
                    throw ex;
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
