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
    public partial class EditSystem : System.Web.UI.Page
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

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            string sqlQuery =
                    " update SystemType  set SystemName = '" + this.txbSystemName.Text.Replace("'", "''") + "',QueryBUrl='" + this.txbBUrl.Text.Replace("'", "''") + "',QuerySUrl='" +
                    this.txbSUrl.Text.Replace("'", "''") + "',ApplyUrl='" + this.txbApplyUrl.Text.Replace("'", "''") + "',QueryUrl='" +
                    this.txbQueryUrl.Text.Replace("'", "''") + "' ,navigateUrl='" + this.txbNavigateUrl.Text.Replace("'", "''") + 
                    "' where systemID = " + Request["SystemID"];

            WorkFlowEngine.BLL.SystemType.UpdateSystem(sqlQuery);

            Response.Write("<script>alert('System edit successfully!'); document.location.href = 'SystemManagement.aspx';</script>");
            //Response.Redirect("SystemManagement.aspx", true);
        }

        protected void btnReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect("SystemManagement.aspx", true);
        }


    }
}
