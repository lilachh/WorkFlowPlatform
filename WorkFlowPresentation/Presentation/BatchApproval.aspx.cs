using System;
using System.Web;
using System.Web.UI;
using System.Data;
using WorkFlowEngine.BLL;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace WorkFlowPresentation.Presentation
{
    public partial class BatchApproval : System.Web.UI.Page
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
            if (!IsPostBack)
            {
                if (Session["EMPLID"] == null )
                {
                  
                    EmplID = Convert.ToInt32(Session["EMPLID"].ToString());
                    SystemID = 2;
                    return;
                }
                EmplID =Convert.ToInt32(Session["EMPLID"].ToString());
                SystemID = Convert.ToInt32(Request.QueryString["SystemID"]);
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

                BindGridView();

            }
        }

        protected void dgvPendinglist_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    GridViewRow row = e.Row;
                    System.Web.UI.HtmlControls.HtmlGenericControl ct = (System.Web.UI.HtmlControls.HtmlGenericControl)row.Cells[4].FindControl("iframe");
                    string url = (string)WorkFlowEngine.BLL.SystemType.GetSUrl("select QuerySUrl from SystemType where systemID=" + SystemID + "");
                    ct.Attributes["src"] = url + "?documentID=" + row.Cells[2].Text.Trim() + "";

                    CheckBox cbxApprove = (CheckBox)row.Cells[0].FindControl("cbxApprove");
                    CheckBox cbxReject = (CheckBox)row.Cells[0].FindControl("cbxReject");

                    cbxApprove.Attributes.Add("onclick", "javascript:CBXApproveClick(" + e.Row.RowIndex + ");");
                    cbxReject.Attributes.Add("onclick", "javascript:CBXRejectClick(" + e.Row.RowIndex + ");");
                }
            }
            catch (Exception ex)
            {
                ((log4net.ILog)Session["logger"]).Error(Session["EMPLID"] + "  " + ex.Message);
                throw ex;
            }
            
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                int documentID, sequencID, approveNeededID;
                CheckBox cbxApprove;
                CheckBox cbxReject;
                string key = string.Empty;
                bool flag = false;
                int lastSelectRowID = LastSelectRowID();
               
                foreach (GridViewRow gyr in this.dgvPendinglist.Rows)
                {
                    if (gyr.RowType == DataControlRowType.DataRow)
                    {
                        documentID = Convert.ToInt32(gyr.Cells[2].Text);
                        sequencID = Convert.ToInt32(gyr.Cells[3].Text);
                        approveNeededID = Convert.ToInt32(gyr.Cells[4].Text);
                        cbxApprove = (CheckBox)gyr.Cells[0].FindControl("cbxApprove");
                        cbxReject = (CheckBox)gyr.Cells[0].FindControl("cbxReject");
                        WorkFlowEngine.BLL.Document document = new Document(SystemID, documentID);
                        key = "js";
                        key += gyr.RowIndex.ToString();
                     
                        if (cbxApprove.Checked == true)
                        {
                            
                            document.DoApproval(sequencID, approveNeededID, EmplID, string.Empty);
                            if (gyr.RowIndex ==lastSelectRowID)
                            {
                                ClientScript.RegisterClientScriptBlock(GetType(), key, "<script>PreApprove(" + gyr.RowIndex.ToString() + ",'false');</script>");
                            }
                            else
                            {
                                ClientScript.RegisterClientScriptBlock(GetType(), key, "<script>PreApprove(" + gyr.RowIndex.ToString() + ",'true');</script>");
                            }
                                flag = true;
                        }
                        if (cbxReject.Checked == true)
                        {
                            document.DoReject(sequencID, approveNeededID, EmplID, string.Empty);
                            if (gyr.RowIndex == lastSelectRowID)
                            {
                                ClientScript.RegisterClientScriptBlock(GetType(), key, "<script>PreReject(" + gyr.RowIndex.ToString() + ",'false');</script>");
                            }
                            else
                            {
                                ClientScript.RegisterClientScriptBlock(GetType(), key, "<script>PreReject(" + gyr.RowIndex.ToString() + ",'true');</script>");
                            }
                            flag = true;
                        }
                    }
                }
                if (flag)
                {
                    //ClientScript.RegisterStartupScript(GetType(),"123", "<script>ReLoad();</script>");
                   //Response.Write("<script language=javascript>window.location.href=window.location.href;</script>");
                }
                
            }
            catch (Exception ex)
            {
                ((log4net.ILog)Session["logger"]).Error(Session["EMPLID"] + "  " + ex.Message);
                throw ex;
            }
        }

        private int LastSelectRowID()
        {
            CheckBox cbxApprove=new CheckBox();
            CheckBox cbxReject = new CheckBox(); ;
            for(int i=dgvPendinglist.Rows.Count-1;i>=0;i--)
            {
                GridViewRow gyr=dgvPendinglist.Rows[i];
                if (gyr.RowType == DataControlRowType.DataRow)
                {
                    cbxApprove = (CheckBox)gyr.Cells[0].FindControl("cbxApprove");
                    cbxReject = (CheckBox)gyr.Cells[0].FindControl("cbxReject");
                }
                if (cbxApprove.Checked == true)
                {
                    return gyr.RowIndex;
                }
                if (cbxReject.Checked == true)
                {
                    return gyr.RowIndex;
                }
            }
            return 0;
        }


        private void BindGridView()
        {
            try
            {
                DataSet ds = new DataSet();
                ApproveList approveList = new ApproveList(int.Parse(Session["EMPLID"].ToString()));
                ds = approveList.GetApproveList(SystemID);
                string rowFilter = "1=1";
                if (dpFromDate.Value != string.Empty)
                    rowFilter += " and SubmitDate >= '" + dpFromDate.Value + "'";
                if (dpToDate.Value != string.Empty)
                    rowFilter += " and SubmitDate <= '" + dpToDate.Value + "'";
                ds.Tables[0].DefaultView.RowFilter = rowFilter;
                this.dgvPendinglist.DataSource = ds.Tables[0];
                this.dgvPendinglist.DataBind();
                this.btnSubmit.Visible = true;
                this.RadioButton1.Visible = true;
                this.RadioButton2.Visible = true;
                if (this.dgvPendinglist.Rows.Count == 0)
                {
                    this.btnSubmit.Visible = false;
                    this.RadioButton1.Visible = false;
                    this.RadioButton2.Visible = false;
                }

                this.RadioButton1.Attributes.Add("onclick", "javascript:radioApprove(" + this.dgvPendinglist.Rows.Count + ");");
                this.RadioButton2.Attributes.Add("onclick", "javascript:radioReject(" + this.dgvPendinglist.Rows.Count + ");");
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

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            BindGridView();
        }

    }
}
