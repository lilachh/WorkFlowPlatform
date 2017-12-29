using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WorkFlowEngine;
using System.Data;

namespace WorkFlowPresentation.Configuration
{
    public partial class SystemDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request["SystemID"] == null || Request["SystemID"] == "")
                {
                    Response.Redirect("SystemManagement.aspx", true);
                }
                string sqlQuery =
                   "select SystemID, SystemName,QueryBUrl,QuerySurl,ApplyUrl,QueryUrl, NavigateUrl from SystemType where SystemID = " + Request["SystemID"];

                DataSet ds = WorkFlowEngine.BLL.SystemType.GetSystem(sqlQuery);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    this.txbSystemID.Text = Request["SystemID"];
                    this.txbSystemName.Text = ds.Tables[0].Rows[0]["SystemName"].ToString();
                    this.txbBUrl.Text = ds.Tables[0].Rows[0]["QueryBUrl"].ToString();
                    this.txbSUrl.Text = ds.Tables[0].Rows[0]["QuerySurl"].ToString();
                    this.txbApplyUrl.Text = ds.Tables[0].Rows[0]["ApplyUrl"].ToString();
                    this.txbQueryUrl.Text = ds.Tables[0].Rows[0]["QueryUrl"].ToString();
                    this.txbNavigateUrl.Text = ds.Tables[0].Rows[0]["NavigateUrl"].ToString();
                }
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("SystemManagement.aspx", true);
        }
    }
}
