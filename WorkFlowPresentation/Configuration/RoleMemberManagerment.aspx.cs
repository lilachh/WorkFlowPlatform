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
using WorkFlowEngine;

namespace WorkFlowPresentation.Configuration
{
    public partial class RoleMemberManagerment : System.Web.UI.Page
    {
        private int RoleID
        {
            get
            {
                return (int)ViewState["roleID"];
            }
            set
            {
                ViewState["roleID"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    tbxEmplID.Attributes.Add("onkeyup", "JudgeEmplid()");
                    RoleID = Convert.ToInt32(Request.QueryString["roleID"].ToString());
                    FillGridView();
                }
                catch (Exception ex)
                {
                    log4net.ILog Logger = (log4net.ILog)Session["logger"];
                    Logger.Error("EmplID:" + Session["EMPLID"].ToString() + "---" + ex.Message);
                    throw ex;
                }
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
                sqlQuery = "SELECT  a.RoleID, b.RoleDescription, a.EmplID, c.NAME FROM  RoleMembers AS a LEFT OUTER JOIN ApproverRole AS b ON a.RoleID = b.RoleID LEFT OUTER JOIN EmployeeInfo AS c ON a.EmplID = c.EMPLID where a.roleID=" + RoleID + "";
                DataSet ds = WorkFlowEngine.BLL.ResponsibilityRole.GetMember(sqlQuery);
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
            try
            {
                //check if the emplID exists or not
                string sqlQuery = "select count(*) from  RoleMembers where emplid = '" + tbxEmplID.Text + "' and roleid=" + RoleID + "";
                if (WorkFlowEngine.BLL.ResponsibilityRole.IfMemberExist(sqlQuery))
                {
                    this.divResult.InnerText = "Sorry, the emplID exists already, please try another one!";
                    return;
                }

                // Checl the emplid exists or not
                sqlQuery = "select count(*) from  employeeInfo where emplid = '" + tbxEmplID.Text + "' and emp_status='A'";
                if (!WorkFlowEngine.BLL.ResponsibilityRole.IfMemberExist(sqlQuery))
                {
                    this.divResult.InnerText = "Sorry, the emplID doesn't exists in system, please try another one!";
                    return;
                }

                WorkFlowEngine.BLL.ResponsibilityRole.AddMember(RoleID, Convert.ToInt32(tbxEmplID.Text));
                FillGridView();
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

        /// <summary>
        /// Add Member For Role
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected void dgvApproverRole_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void btnHide_Click(object sender, EventArgs e)
        {
            try
            {
                string sqlQuery = "SELECT name from employeeInfo where emplid=" + tbxEmplID.Text + "";
                object ob = WorkFlowEngine.BLL.ResponsibilityRole.GetName(sqlQuery);
                if (ob == null)
                {
                    lblName.Text = "Not Exist";
                }
                else
                {

                    lblName.Text = ob.ToString();
                }
                Page.ClientScript.RegisterStartupScript(GetType(), "key", "<script> document.all('btnAdd').focus();</script>");
            }
            catch (Exception ex)
            {
                log4net.ILog Logger = (log4net.ILog)Session["logger"];
                Logger.Error("EmplID:" + Session["EMPLID"].ToString() + "---" + ex.Message);
                throw ex;
            }
        }

        protected void dgvApproverRole_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int rowIndex = e.RowIndex;
                int roleID = Convert.ToInt32(dgvApproverRole.Rows[rowIndex].Cells[0].Text.Trim());
                int emplid = Convert.ToInt32(dgvApproverRole.Rows[rowIndex].Cells[2].Text.Trim());
                WorkFlowEngine.BLL.ResponsibilityRole.DeleteMember(roleID, emplid);
                FillGridView();
            }
            catch (Exception ex)
            {
                log4net.ILog Logger = (log4net.ILog)Session["logger"];
                Logger.Error("EmplID:" + Session["EMPLID"].ToString() + "---" + ex.Message);
                throw ex;
            }
        }
    }
}

