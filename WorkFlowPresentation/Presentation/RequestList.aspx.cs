using System;
using System.Web;
using System.Web.UI;
using System.Data;
using WorkFlowEngine.BLL;

namespace WorkFlowPresentation.Presentation
{
    public partial class RequestList : System.Web.UI.Page
    {
        public int EmplID
        {
            get
            {
                return (int)ViewState["emplID"];
            }
            set
            {
                ViewState["emplID"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    EmplID = Convert.ToInt32(Session["EMPLID"].ToString());
                    int temp;
                    if (Session["EMPLID"] == null || !int.TryParse(Session["EMPLID"].ToString(), out temp) || !int.TryParse(Request["SystemID"], out temp))
                    {
                        return;
                    }

                    this.divSystemName.InnerText = Request["SystemName"];

                    // get all systems
                    string sqlQuery = "select -1 SystemID, 'All' SystemName union select SystemID, SystemName from SystemType order by SystemID";
                    DataSet ds = SystemType.GetSystem(sqlQuery);

                    this.ddlSystem.DataSource = ds.Tables[0];
                    this.ddlSystem.DataTextField = "SystemName";
                    this.ddlSystem.DataValueField = "SystemID";
                    this.ddlSystem.DataBind();

                    if (Request["SystemID"] != "-1")
                    {

                        if (SystemType.IfHaveNavigate(int.Parse(Request["SystemID"].ToString())))
                        {
                          Response.Redirect(SystemType.GetNavigateUrl(int.Parse(Request["SystemID"].ToString())) + "?emplID=" + Session["EMPLID"].ToString() + "");
                        }
                        this.ddlSystem.SelectedValue = Request["SystemID"].ToString();
                        this.ddlSystem.Enabled = false;
                    }
                }
                catch (Exception ex)
                {
                    log4net.ILog Logger = (log4net.ILog)Session["logger"];
                    Logger.Error("EmplID:" + Session["EMPLID"].ToString() + "---" + ex.Message);
                    throw ex;
                }
            }
        }

        private void BindGridView()
        {
            try
            {
                DataSet ds = new DataSet();
                QueryList queryList = new QueryList(int.Parse(Session["EMPLID"].ToString()));

                if (Request["SystemID"] == "-1")
                {
                    ds = queryList.GetQueryList();
                }
                else
                {
                    ds = queryList.GetQueryList(int.Parse(Request["SystemID"]));
                }
                string rowFilter = "1=1";
                switch (ddlType.SelectedIndex)
                {
                    case 0:
                        rowFilter += " and RequestorID=" + EmplID + "";
                        break;
                    case 1:
                        rowFilter += " and OwnerID=" + EmplID + "";
                        break;
                    case 2:
                        ds = queryList.GetApprovedList();
                        break;
                }
                if (ddlSystem.SelectedValue != "-1")
                {
                    rowFilter += " and SystemID=" + int.Parse(ddlSystem.SelectedValue) + "";
                }
                if (dpFromDate.Value != string.Empty)
                    rowFilter += " and SubmitDate >= '" + dpFromDate.Value + "'";

                if (dpToDate.Value != string.Empty)
                    rowFilter += " and SubmitDate <= '" + dpToDate.Value + "'";
                ds.Tables[0].DefaultView.RowFilter = rowFilter;
                this.dgvRequestlist.DataSource = ds.Tables[0];
                this.dgvRequestlist.DataBind();
            }
            catch (Exception ex)
            {
                log4net.ILog Logger = (log4net.ILog)Session["logger"];
                Logger.Error("EmplID:" + Session["EMPLID"].ToString() + "---" + ex.Message );
                throw ex;
            }
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            BindGridView();
        }
    }
}
