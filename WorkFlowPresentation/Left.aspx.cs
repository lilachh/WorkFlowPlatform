using System;
using System.Data;
using System.Collections;
using System.Web;
using System.Web.UI;
using WorkFlowEngine.BLL;
using System.Data.SqlClient;

namespace WorkFlowPresentation
{
    public partial class Left : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string sqlQuery = "select -1 SystemID, 'All Systems' SystemName union select SystemID, SystemName from SystemType";
            try
            {                
                DataSet ds = SystemType.GetSystem(sqlQuery);

                dgvPendinglist.DataSource = ds.Tables[0];
                dgvPendinglist.DataBind();

                dgvQuerylist.DataSource = ds.Tables[0];
                dgvQuerylist.DataBind();

                sqlQuery = "select SystemID, SystemName, ApplyUrl from SystemType order by systemid";
                ds = SystemType.GetSystem(sqlQuery);
                dgvApplylist.DataSource = ds.Tables[0];
                dgvApplylist.DataBind();
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

        protected void lbExit_Click(object sender, EventArgs e)
        {
            log4net.ILog Logger = (log4net.ILog)Session["logger"];
            Logger.Info(Session["EMPLID".ToString()] + " logged off.");
            Session["EMPLID"] = null;
            
            Response.Write("<script>window.parent.location.href = 'Login.aspx';</script>");
        }
    }
}
