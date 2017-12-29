using System;
using System.Web;
using System.Web.UI;
using System.Data;
using WorkFlowEngine.BLL;

namespace WorkFlowPresentation.Presentation
{
    public partial class PendingList : System.Web.UI.Page
    {
        public int SystemID
        {
            get
            {
                return (int)ViewState["systemID"];
            }
            set
            {
                ViewState["systemID"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    int temp;
                    if (Session["EMPLID"] == null || !int.TryParse(Session["EMPLID"].ToString(), out temp) || !int.TryParse(Request.QueryString["SystemID"], out temp))
                    {
                        return;
                    }

                    SystemID = Convert.ToInt32(Request.QueryString["SystemID"]);
                    if (WorkFlowEngine.BLL.SystemType.IfNeedBatch(SystemID))
                    {
                        Response.Redirect("BatchApproval.aspx?SystemID=" + SystemID + "&SystemName=" + Request.QueryString["SystemName"] + "");
                    }

                    this.divSystemName.InnerText = Request["SystemName"];

                    // get all systems
                    string sqlQuery = "select -1 SystemID, 'All' SystemName union select SystemID, SystemName from SystemType order by SystemID";
                    DataSet ds = SystemType.GetSystem(sqlQuery);

                    this.ddlSystem.DataSource = ds.Tables[0];
                    this.ddlSystem.DataTextField = "SystemName";
                    this.ddlSystem.DataValueField = "SystemID";
                    this.ddlSystem.DataBind();

                    if (SystemID != -1)
                    {
                        this.ddlSystem.SelectedValue = SystemID.ToString();
                        this.ddlSystem.Enabled = false;
                    }

                    //BindGridView(null);
                    btnQuery_Click(null, new EventArgs());
                }
                catch (Exception ex)
                {
                    log4net.ILog Logger = (log4net.ILog)Session["logger"];
                    Logger.Error("EmplID:" + Session["EMPLID"].ToString() + "---" + ex.Message);
                    throw ex;
                }
            }
        }

        private void BindGridView(string rowFilter)
        {
            try
            {
                DataSet ds = new DataSet();
                ApproveList approveList = new ApproveList(int.Parse(Session["EMPLID"].ToString()));

                if (Request["SystemID"] == "-1")
                {
                    ds = approveList.GetApproveList();
                }
                else
                {
                    ds = approveList.GetApproveList(int.Parse(Request["SystemID"]));
                }

                if (rowFilter != null && rowFilter != string.Empty)
                    ds.Tables[0].DefaultView.RowFilter = rowFilter;

                this.dgvPendinglist.DataSource = ds.Tables[0];
                this.dgvPendinglist.DataBind();
            }
            catch (Exception ex)
            {
                log4net.ILog Logger = (log4net.ILog)Session["logger"];
                Logger.Error("EmplID:" + Session["EMPLID"].ToString() + "---" + ex.Message);
                throw ex;
            }
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                string rowFilter = "SystemID = SystemID";

                if (ddlSystem.SelectedValue != "-1")
                    rowFilter += " and SystemID =" + ddlSystem.SelectedValue;

                if (dpFromDate.Value != string.Empty)
                    rowFilter += " and SubmitDate >= '" + dpFromDate.Value + "'";

                if (dpToDate.Value != string.Empty)
                    rowFilter += " and SubmitDate <= '" + dpToDate.Value + "'";
                BindGridView(rowFilter);
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
