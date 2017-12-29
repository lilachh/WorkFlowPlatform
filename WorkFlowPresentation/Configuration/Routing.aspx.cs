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

namespace WorkFlowPresentation.Configuration
{
    public partial class Routing : System.Web.UI.Page
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


        public int Flag
        {
            get
            {
                return (int)ViewState["flag"];
            }
            set
            {
                ViewState["flag"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    SystemID = Convert.ToInt32(Request.QueryString["systemID"]);
                    this.divSystemName.InnerText = Request.QueryString["SystemName"];
                    FillDdlFrom();
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
        /// Fill DdlFrom with select data
        /// </summary>
        private void FillDdlFrom()
        {
            //this.ddlSequenceFrom.DataTextField = "RoleDescription";
            string sqlQuery = string.Empty;
            try
            {
                sqlQuery =
                    "select distinct SequenceID,'ID:'+cast(SequenceID as varchar(20))+' '+SequenceDescription as SequenceDescription from MainWorkFlow where sequenceid not in (select sequenceID from MainWorkFlow where IsLastSequence='1' and systemID=" + SystemID + " union(select sequenceID from MainWorkFlow where HasSubSequence='0' and NextSequenceID<>'-1' and systemID=" + SystemID + "))and systemID=" + SystemID + "";

                DataSet ds = WorkFlowEngine.BLL.WorkFlow.GetSequences(sqlQuery);
                this.ddlSequenceFrom.DataSource = ds.Tables[0];
                this.ddlSequenceFrom.DataValueField = "SequenceID";
                ddlSequenceFrom.DataTextField = "SequenceDescription";


                this.ddlSequenceFrom.DataBind();
                ListItem listItem = new ListItem("", "-2");
                this.ddlSequenceFrom.Items.Insert(0, listItem);
            }
            catch (Exception ex)
            {
                log4net.ILog Logger = (log4net.ILog)Session["logger"];
                Logger.Error("EmplID:" + Session["EMPLID"].ToString() + "---" + ex.Message + " --- Sql:  " + sqlQuery);
                throw ex;
            }
        }

        /// <summary>
        /// Fill GridView with select data
        /// </summary>
        private void FillDdlTo(int sequenceIDFrom)
        {
            string sqlQuery = string.Empty;
            try
            {
                 sqlQuery =
                    "select distinct SequenceID,'ID:'+cast(SequenceID as varchar(20))+' '+SequenceDescription as SequenceDescription from MainWorkFlow where systemID=" + SystemID + " and sequenceID>" + sequenceIDFrom + " and sequenceID not in (select NextSequenceID from SubWorkFlow where systemID=" + SystemID + " and MainSequenceID=" + sequenceIDFrom + ")";

                DataSet ds = WorkFlowEngine.BLL.WorkFlow.GetSequences(sqlQuery);
                this.ddlSequenceTo.DataSource = ds.Tables[0];
                this.ddlSequenceTo.DataValueField = "SequenceID";
                ddlSequenceTo.DataTextField = "SequenceDescription";
                this.ddlSequenceTo.DataBind();

                ListItem listItem = new ListItem("", "-1");
                this.ddlSequenceTo.Items.Insert(0, listItem);
            }
            catch (Exception ex)
            {
                log4net.ILog Logger = (log4net.ILog)Session["logger"];
                Logger.Error("EmplID:" + Session["EMPLID"].ToString() + "---" + ex.Message + " --- Sql:  " + sqlQuery);
                throw ex;
            }
        }

        protected void ddlSequenceFrom_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Flag = 1;   // Mean From one sequence
                this.divResult.InnerText = "";
                if (ddlSequenceFrom.SelectedIndex == 0)
                {
                    pnlCondition.Visible = false;
                    DataTable dt = new DataTable();
                    this.ddlSequenceTo.DataSource = dt;
                    this.ddlSequenceTo.DataBind();
                    return;
                }
                pnlCondition.Visible = false;
                int sequenceIDFrom = Convert.ToInt32(ddlSequenceFrom.SelectedValue);
                FillDdlTo(sequenceIDFrom);
                ShowGridView();
            }
            catch (Exception ex)
            {
                log4net.ILog Logger = (log4net.ILog)Session["logger"];
                Logger.Error("EmplID:" + Session["EMPLID"].ToString() + "---" + ex.Message );
                throw ex;
            }
        }

        protected void ShowGridView()
        {
            try
            {
                int sequenceIDFrom = Convert.ToInt32(ddlSequenceFrom.SelectedValue);
                WorkFlowEngine.BLL.Sequence sequence = new WorkFlowEngine.BLL.Sequence(SystemID, sequenceIDFrom);
                DataSet ds = sequence.GetRouting();
                dgvRouting.DataSource = ds.Tables[0];
                dgvRouting.DataBind();
                if (sequence.HasSubSequence == "1")
                {
                    pnlCondition.Visible = true;
                }
            }
            catch (Exception ex)
            {
                log4net.ILog Logger = (log4net.ILog)Session["logger"];
                Logger.Error("EmplID:" + Session["EMPLID"].ToString() + "---" + ex.Message);
                throw ex;
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                this.divResult.InnerText = "";
                if (ddlSequenceFrom.SelectedIndex == 0 || ddlSequenceTo.SelectedIndex == 0)
                {
                    this.divResult.InnerText = "Sorry, Please Select both from sequence and to sequence!";
                    return;
                }
                int sequenceIDFrom = Convert.ToInt32(ddlSequenceFrom.SelectedValue);
                int sequenceIDTo = Convert.ToInt32(ddlSequenceTo.SelectedValue);
                WorkFlowEngine.BLL.Sequence sequence = new WorkFlowEngine.BLL.Sequence(SystemID, sequenceIDFrom);
                if (sequence.HasSubSequence == "1")
                {
                    if (tbxConditionDesc.Text.Length == 0)
                    {
                        this.divResult.InnerText = "Sorry, Please Input Condition Description!";
                        return;
                    }
                    if (!WorkFlowEngine.BLL.WorkFlow.AddSubSequence(SystemID, sequenceIDFrom, sequenceIDTo, tbxConditionDesc.Text))
                    {
                        this.divResult.InnerText = "Sorry, Add failed!";
                        return;
                    }
                    else
                    {
                        ShowGridView();
                    }
                }
                else
                {
                    if (!WorkFlowEngine.BLL.WorkFlow.AddNextSequence(SystemID, sequenceIDFrom, sequenceIDTo))
                    {
                        this.divResult.InnerText = "Sorry, Add failed!";
                        return;
                    }
                    else
                    {
                        ShowGridView();
                    }
                }
                FillDdlFrom();
            }
            catch (Exception ex)
            {
                log4net.ILog Logger = (log4net.ILog)Session["logger"];
                Logger.Error("EmplID:" + Session["EMPLID"].ToString() + "---" + ex.Message);
                throw ex;
            }
        }

        protected void btnShowAll_Click(object sender, EventArgs e)
        {
            ShowAll();
    
        }

        protected void ShowAll()
        {
            try
            {
                Flag = 2;  //Mean Show All
                DataSet ds = WorkFlowEngine.BLL.WorkFlow.ShowAllRouting(SystemID);
                dgvRouting.DataSource = ds.Tables[0];
                dgvRouting.DataBind();
            }
            catch (Exception ex)
            {
                log4net.ILog Logger = (log4net.ILog)Session["logger"];
                Logger.Error("EmplID:" + Session["EMPLID"].ToString() + "---" + ex.Message);
                throw ex;
            }
        }

        protected void dgvRouting_RowEditing(object sender, GridViewEditEventArgs e)
        {
            this.dgvRouting.EditIndex = e.NewEditIndex;
            if (Flag == 1)
            {
                ShowGridView();
            }
            else
            {
                ShowAll();
            }
        }

        protected void dgvRouting_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                GridViewRow row = dgvRouting.Rows[e.RowIndex];
                string newName = ((TextBox)(row.Cells[5].Controls[0])).Text.Trim();

                if (newName == "")
                {
                    this.divResult.InnerText = "Condition description can't be empty!";
                    return;
                }

                int systemID = Convert.ToInt32(row.Cells[0].Text);
                int mainID = Convert.ToInt32(row.Cells[1].Text);
                int nextID = Convert.ToInt32(row.Cells[3].Text);
                string sqlQuery = " update SubWorkFlow  set ConditionDescription = '" + newName.Replace("'", "''") + "' where systemID = " + systemID + " and mainsequenceID=" + mainID + " and nextSequenceID=" + nextID + "";
                WorkFlowEngine.BLL.Sequence.UpdateConditionDescription(sqlQuery);
                this.divResult.InnerText = "";
                dgvRouting.EditIndex = -1;
                if (Flag == 1)
                {
                    ShowGridView();
                }
                else
                {
                    ShowAll();
                }
            }
            catch (Exception ex)
            {
                log4net.ILog Logger = (log4net.ILog)Session["logger"];
                Logger.Error("EmplID:" + Session["EMPLID"].ToString() + "---" + ex.Message);
                throw ex;
            }
        }

        protected void dgvRouting_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            try
            {
                this.divResult.InnerText = "";
                dgvRouting.EditIndex = -1;
                if (Flag == 1)
                {
                    ShowGridView();
                }
                else
                {
                    ShowAll();
                }
            }
            catch (Exception ex)
            {
                log4net.ILog Logger = (log4net.ILog)Session["logger"];
                Logger.Error("EmplID:" + Session["EMPLID"].ToString() + "---" + ex.Message);
                throw ex;
            }
        }

        protected void dgvRouting_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "DeleteSystem")
                {
                    int rowIndex = ((GridViewRow)((LinkButton)e.CommandSource).NamingContainer).RowIndex;
                    int systemID = Convert.ToInt32(dgvRouting.Rows[rowIndex].Cells[0].Text.Trim());
                    int mainID = Convert.ToInt32(dgvRouting.Rows[rowIndex].Cells[1].Text.Trim());
                    int nextID = Convert.ToInt32(dgvRouting.Rows[rowIndex].Cells[3].Text.Trim());
                    if (!WorkFlowEngine.BLL.WorkFlow.DeleteRouting(systemID, mainID, nextID))
                    {
                        this.divResult.InnerText = "Sorry,failed to delete!";
                    }
                    if (Flag == 1)
                    {
                        ShowGridView();
                    }
                    else
                    {
                        ShowAll();
                    }
                    FillDdlFrom();
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
}
