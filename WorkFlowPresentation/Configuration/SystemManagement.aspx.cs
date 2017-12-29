using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WorkFlowEngine;

namespace WorkFlowPresentation.Configuration
{
    public partial class SystemManagement : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["EMPLID"] == null || (Session["EMPLID"].ToString() != "451674" && Session["EMPLID"].ToString() != "454017" && Session["EMPLID"].ToString() != "380616"))
            {
                Response.Write("<script>alert('Sorry, you are not allowed to access this page!');window.location.href = '../main.html';</script>");
                return;
            }

            if (!Page.IsPostBack)
            {
                FillGridView();
            }
        }

        /// <summary>
        /// Fill GridView with select data
        /// </summary>
        private void FillGridView()
        {
            string sqlQuery = string.Empty;
            try
            {
                sqlQuery =
                   "select systemID,systemName,case Status when '0' then 'No Configuration' when '1' then 'Configured' end Status," +
                   "QueryBUrl,QuerySurl,applyurl,queryurl, NavigateUrl from SystemType order by systemID";

                DataSet ds = WorkFlowEngine.BLL.SystemType.GetSystem(sqlQuery);
                this.dgvSystem.DataSource = ds.Tables[0];
                this.dgvSystem.DataBind();
            }
            catch (Exception ex)
            {
                log4net.ILog Logger = (log4net.ILog)Session["logger"];
                Logger.Error("EmplID:" + Session["EMPLID"].ToString() + "---" + ex.Message + " --- Sql:  " + sqlQuery);
                throw ex;
            }
        }

        /// <summary>
        /// Add new role type
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string sqlQuery = string.Empty;
            try
            {
                sqlQuery = "select count(*) from SystemType where SystemName = '" + txbSystemName.Text.Trim().Replace("'", "''") + "'";
                if (WorkFlowEngine.BLL.SystemType.IfSystemExist(sqlQuery))
                {
                    this.divResult.InnerText = "Sorry, the system name exists already, please try another one!";
                    return;
                }

                WorkFlowEngine.BLL.SystemType.AddSystem(txbSystemName.Text.Trim(), txbBUrl.Text.Trim(),
                    txbSUrl.Text.Trim(), txbApplyUrl.Text.Trim(), txbQueryUrl.Text.Trim(), txbNavigateUrl.Text.Trim());
                FillGridView();
            }
            catch (Exception ex)
            {
                log4net.ILog Logger = (log4net.ILog)Session["logger"];
                Logger.Error("EmplID:" + Session["EMPLID"].ToString() + "---" + ex.Message + " --- Sql:  " + sqlQuery);
                throw ex;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            FillGridView();
        }

        protected void dgvSystem_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                GridViewRow row = dgvSystem.Rows[e.RowIndex];
                string newName = ((TextBox)(row.Cells[1].Controls[0])).Text.Trim();
                string queryBUrl = ((TextBox)(row.Cells[2].Controls[0])).Text.Trim();
                string querySUrl = ((TextBox)(row.Cells[3].Controls[0])).Text.Trim();
                string applyUrl = ((TextBox)(row.Cells[4].Controls[0])).Text.Trim();
                string queryUrl = ((TextBox)(row.Cells[5].Controls[0])).Text.Trim();
                string navigateUrl = ((TextBox)(row.Cells[6].Controls[0])).Text.Trim();

                if (newName == "")
                {
                    this.divResult.InnerText = "Role description can't be empty!";
                    return;
                }

                /*
                // check if the description exists or not
                string sqlQuery = "select count(*) from SystemType where SystemName = '" + newName.Replace("'", "''") + "'";
                if (WorkFlowEngine.BLL.SystemType.IfSystemExist(sqlQuery))
                {
                    this.divResult.InnerText = "Sorry, the system name exists already, please try another one!";
                    return;
                }
                */

                int systemID = Convert.ToInt32(row.Cells[0].Text);
                string sqlQuery = 
                    " update SystemType  set SystemName = '" + newName.Replace("'", "''") + "',QueryBUrl='" + queryBUrl + "',QuerySUrl='" + 
                    querySUrl + "',ApplyUrl='" + applyUrl + "',QueryUrl='" + queryUrl + "' ,navigateUrl='" + navigateUrl + "'  where systemID = " + systemID + "";

                WorkFlowEngine.BLL.SystemType.UpdateSystem(sqlQuery);
                this.divResult.InnerText = "";
                dgvSystem.EditIndex = -1;
                FillGridView();
            }
            catch (Exception ex)
            {
                log4net.ILog Logger = (log4net.ILog)Session["logger"];
                Logger.Error("EmplID:" + Session["EMPLID"].ToString() + "---" + ex.Message );
                throw ex;
            }
        }

 

        protected void dgvSystem_RowEditing(object sender, GridViewEditEventArgs e)
        {
            this.dgvSystem.EditIndex = e.NewEditIndex;
            FillGridView();
        }


        protected void dgvSystem_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "AddSequence")
                {
                    int rowIndex = ((GridViewRow)((LinkButton)e.CommandSource).NamingContainer).RowIndex;
                    string systemID = dgvSystem.Rows[rowIndex].Cells[0].Text.Trim();
                    Response.Redirect("AddSequence.aspx?SystemID=" + systemID + "");
                }

                if (e.CommandName == "Routing")
                {
                    int rowIndex = ((GridViewRow)((LinkButton)e.CommandSource).NamingContainer).RowIndex;
                    string systemID = dgvSystem.Rows[rowIndex].Cells[0].Text.Trim();
                    string systemName = dgvSystem.Rows[rowIndex].Cells[1].Text.Trim();
                    Response.Redirect("Routing.aspx?SystemID=" + systemID + "&SystemName=" + systemName + "");
                }

                if (e.CommandName == "Preview")
                {
                    int rowIndex = ((GridViewRow)((LinkButton)e.CommandSource).NamingContainer).RowIndex;
                    string systemID = dgvSystem.Rows[rowIndex].Cells[0].Text.Trim();
                    string systemName = dgvSystem.Rows[rowIndex].Cells[1].Text.Trim();
                    Response.Redirect("Preview.aspx?SystemID=" + systemID + "&SystemName="+systemName+"");
                }

                if (e.CommandName == "DeleteSystem")
                {
                    int rowIndex = ((GridViewRow)((LinkButton)e.CommandSource).NamingContainer).RowIndex;
                    int systemID = Convert.ToInt32(dgvSystem.Rows[rowIndex].Cells[0].Text.Trim());
                    WorkFlowEngine.BLL.SystemType.DeleteSystem(systemID);
                    FillGridView();
                }
            }
            catch (Exception ex)
            {
                log4net.ILog Logger = (log4net.ILog)Session["logger"];
                Logger.Error("EmplID:" + Session["EMPLID"].ToString() + "---" + ex.Message );
                throw ex;
            }
        }

        protected void dgvSystem_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            this.divResult.InnerText = "";
            dgvSystem.EditIndex = -1;
            FillGridView();
        }


       
    }
}
