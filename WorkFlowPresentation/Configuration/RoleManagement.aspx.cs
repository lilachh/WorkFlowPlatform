using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WorkFlowEngine.BLL;

namespace WorkFlowPresentation.Configuration
{
    public partial class RoleManagement : System.Web.UI.Page
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
                //FillGridView();
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
                    "select RoleType oriRoleType, case RoleType when '0' then 'report line' when '1' then 'responsibility' when '2' then 'Self' end RoleType, " +
                    "RoleID, RoleDescription, Grade from ApproverRole ";

                sqlQuery += " where RoleDescription like '%" + txbSelectRoleDescription.Text.Replace("'", "''") + "%' ";
                if (ddlSelectRole.SelectedValue != "-1")
                    sqlQuery += " and RoleType = '" + ddlSelectRole.SelectedValue + "'";

                sqlQuery += " order by RoleID";

                DataSet ds = ApproverRole.GetRoles(sqlQuery);

                this.dgvApproverRole.DataSource = ds.Tables[0];
                this.dgvApproverRole.DataBind();
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
            // check if the description exists or not
            if (ApproverRole.RoleDescriptionExists(txbRoleDescription.Text.Trim()))
            {
                this.divResult.InnerText = "Role description exists, please try another one!";
                return;
            }

            // get max roleid

            int maxRoleID = ApproverRole.GetNextMaxRoleID();
            bool addResult = false;

            if (this.ddlAddRoleType.SelectedValue == "0")   // report line
            {
                addResult = ApproverRole.AddReportLineRole(ddlAddRoleType.SelectedValue, maxRoleID, txbRoleDescription.Text.Replace("'", "''"), int.Parse(txbGrade.Text));
            }
            else if (ddlAddRoleType.SelectedValue == "1")   // responsibility
            {
                addResult = ApproverRole.AddResponsibility(ddlAddRoleType.SelectedValue, maxRoleID, txbRoleDescription.Text.Replace("'", "''"));
            }

            try
            {
                if (!addResult)
                {
                    this.divResult.InnerText = "Sorry, add failed. Please contact your administrator!";
                }
                else
                {
                    this.txbSelectRoleDescription.Text = this.txbRoleDescription.Text;
                    FillGridView();

                    this.divResult.InnerText = "Add successfully!";
                }
            }
            catch (Exception ex)
            {
                log4net.ILog Logger = (log4net.ILog)Session["logger"];
                Logger.Error("EmplID:" + Session["EMPLID"].ToString() + "---" + ex.Message);
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
        
        protected void dgvApproverRole_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "AddMember")
                {
                    int rowIndex = ((GridViewRow)((LinkButton)e.CommandSource).NamingContainer).RowIndex;
                    LinkButton lb = (LinkButton)dgvApproverRole.Rows[rowIndex].Cells[1].FindControl("lbnAddMember");
                    string roleID = dgvApproverRole.Rows[rowIndex].Cells[0].Text.Trim();
                    Response.Redirect("RoleMemberManagerment.aspx?roleID=" + roleID + "");
                }
            }
            catch (Exception ex)
            {
                log4net.ILog Logger = (log4net.ILog)Session["logger"];
                Logger.Error("EmplID:" + Session["EMPLID"].ToString() + "---" + ex.Message);
                throw ex;
            }
        }

        protected void dgvApproverRole_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView dr = (DataRowView)e.Row.DataItem;
                    string roleType = dr["oriRoleType"].ToString();

                    // members can only be added into responsibility role
                    if (roleType != "1")
                    {
                        LinkButton lbnAddMember = (System.Web.UI.WebControls.LinkButton)e.Row.FindControl("lbnAddMember");
                        lbnAddMember.Attributes.Add("onClick", "javascript:alert('Sorry, can not add member to report line role!'); return false;");
                    }
                }
            }
            catch (Exception ex)
            {
                log4net.ILog Logger = (log4net.ILog)Session["logger"];
                Logger.Error("EmplID:" + Session["EMPLID"].ToString() + "---" + ex.Message);
                throw ex;
            }
        }

        protected void dgvApproverRole_RowEditing(object sender, GridViewEditEventArgs e)
        {
            this.dgvApproverRole.EditIndex = e.NewEditIndex;
            FillGridView();
        }

        protected void dgvApproverRole_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                GridViewRow row = dgvApproverRole.Rows[e.RowIndex];
                string newDescription = ((TextBox)(row.Cells[2].Controls[1])).Text.Trim().Replace("'", "''");

                if (newDescription == "")
                {
                    this.divResult.InnerText = "Role description can't be empty!";
                    return;
                }

                // check if the description exists or not
                if (ApproverRole.RoleDescriptionExists(newDescription))
                {
                    this.divResult.InnerText = "Sorry, the role description exists already, please try another one!";
                    return;
                }

                string roleID = row.Cells[0].Text;

                if (ApproverRole.UpdateRoleDescription(int.Parse(roleID), newDescription))
                {
                    this.divResult.InnerText = "";
                    dgvApproverRole.EditIndex = -1;
                    FillGridView();

                    this.divResult.InnerText = "Edit completed!";
                }
                else
                {
                    this.divResult.InnerText = "Sorry, unable to change role description, please contact your administrator!";
                }
            }
            catch (Exception ex)
            {
                log4net.ILog Logger = (log4net.ILog)Session["logger"];
                Logger.Error("EmplID:" + Session["EMPLID"].ToString() + "---" + ex.Message);
                throw ex;
            }
        }

        protected void dgvApproverRole_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            this.divResult.InnerText = "";
            dgvApproverRole.EditIndex = -1;
            FillGridView();
        }
    }
}
