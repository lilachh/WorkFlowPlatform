/* <p>Description: Add new sequence</p>
 * <p>@version: 1.0.1</p>
 * <p>Modifition history (Date / Version / Author / Description)</p>
 * <p>------------------------------------------------------------------</p>
 * <p>2009-03-10 / 1.0.1 / Zhang Yunsong / Setting up class</p>
 * <p>2009-03-12 / 1.0.1 / Zhang Yunsong / Implement functions of cbxSystem_CheckedChanged() and btnAddSequence_Click()</p>
 * <p>2009-03-13 / 1.0.1 / Zhang Yunsong / Implement AddInitialSequence()</p>
 * <p>...</p>
 */

using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WorkFlowEngine;
using System.Data;
using WorkFlowEngine.BLL;
using System.Data.SqlClient;


namespace WorkFlowPresentation.Configuration
{
    public partial class AddSequence : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                int systemID = -1;
                if (Request["SystemID"] == null || Request["SystemID"] == string.Empty ||
                    !int.TryParse(Request["SystemID"], out systemID) || Request["SystemName"] == null)
                {
                    Response.Redirect("SystemManagement.aspx", true);                                        
                }

                this.divSystemName.InnerText = Request["SystemName"];

                CheckInitialSequence(systemID);

                BindApproverRole();

                BindExistingSequence(systemID);
            }
        }

        private void CheckInitialSequence(int systemID)
        {
            try
            {
                if (!WorkFlow.InitialSequenceExists(systemID))
                {
                    // hasn't set the initial sequence yet
                    this.rbnRequestor.Enabled = true;
                    this.rbnApprover.Enabled = true;
                    this.btnAddSequence.Enabled = false;
                }
                else
                {
                    this.rbnRequestor.Enabled = false;
                    this.rbnApprover.Enabled = false;
                    this.btnAddSequence.Enabled = true;
                }
            }
            catch (SqlException ex)
            {
                log4net.ILog Logger = (log4net.ILog)Session["logger"];
                Logger.Error("EmplID:"+Session["EMPLID"].ToString() +"---"+ex.Message );
                throw ex;
            }
            catch (Exception ex)
            {
                log4net.ILog Logger = (log4net.ILog)Session["logger"];
                Logger.Error("EmplID:" + Session["EMPLID"].ToString() + "---" + ex.Message);
                throw ex;
            }

        }

        /// <summary>
        /// Get all approverrole with addition one more empty row
        /// </summary>
        private void BindApproverRole()
        {
            try
            {
                DataSet ds = ApproverRole.GetAllRole();
                if (ds == null || ds.Tables.Count == 0)
                {
                    this.divResult.InnerText = "Sorry, unable to load role info, please contact your administrator!";
                    return;
                }

                this.ddlRole.DataSource = ds.Tables[0];
                this.ddlRole.DataTextField = "RoleDescription";
                this.ddlRole.DataValueField = "RoleID";
                this.ddlRole.DataBind();

                ListItem listItem = new ListItem("", "-1");
                this.ddlRole.Items.Insert(0, listItem);
            }
            catch (Exception ex)
            {
                log4net.ILog Logger = (log4net.ILog)Session["logger"];
                Logger.Error("EmplID:" + Session["EMPLID"].ToString() + "---" + ex.Message);
                throw ex;
            }
        }

        /// <summary>
        /// Get all sequences of a system
        /// </summary>
        /// <param name="systemID">systme id</param>
        private void BindExistingSequence(int systemID)
        {
            string sqlQuery=string.Empty;
            try
            {
                 sqlQuery =
                    " select mwf.SystemID, mwf.SequenceID, SequenceDescription, isnull(RoleDescription, 'System') RoleDescription, case SequenceNeedAll when '0' then 'NO' when '1' then 'YES' end SequenceNeedAll, NextSequenceID, " +
                    " case IsLastSequence when '0' then 'NO' when '1' then 'YES' end IsLastSequence, case HasSubSequence when '0' then 'NO' when '1' then 'YES' end HasSubSequence, " +
                    " case ValueFrom when '0' then 'From Requestor' when '1' then 'From Approver' end ValueFrom, URL " +
                    " from MainWorkFlow mwf left outer join ApproverRole ar on mwf.RoleID = ar.RoleID left outer join SpecificUrl su on mwf.SystemID = su.SystemID and mwf.SequenceID = su.SequenceID " +
                    " where mwf.SystemID = " + Request["SystemID"] +
                    " order by SequenceID ";

                DataSet ds = WorkFlow.GetSystemSequences(sqlQuery);

                if (ds == null)
                {
                    this.divResult.InnerText = "Sorry, unable to load sequence info, please contact your administrator!";
                    return;
                }

                this.dgvSequences.DataSource = ds.Tables[0];
                this.dgvSequences.DataBind();
            }
            catch (Exception ex)
            {
                log4net.ILog Logger = (log4net.ILog)Session["logger"];
                Logger.Error("EmplID:" + Session["EMPLID"].ToString() + "---" + ex.Message + " --- Sql:  " + sqlQuery);
                throw ex;
            }
        }

        /// <summary>
        /// Select a role for new sequence
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSelectRole_Click(object sender, EventArgs e)
        {
            if (lbxRole.Items.Contains(ddlRole.SelectedItem))
            {
                this.divResult.InnerText = "Selected role exists already, try another one!";
                return;
            }
            else
            {
                lbxRole.Items.Add(ddlRole.SelectedItem);
                lbxRole.DataBind();
                lbxRole.SelectedIndex = -1;
                ddlRole.Focus();
            }
        }

        /// <summary>
        /// Add new sequence
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAddSequence_Click(object sender, EventArgs e)
        {
            try
            {
                if (Sequence.RoleIDExists(int.Parse(Request["SystemID"]), int.Parse(txbSequenceID.Text)))
                {
                    this.divResult.InnerText = "Sequence ID exists already, please try another one!";
                    return;
                }

                Sequence sequence = new Sequence();

                sequence.SystemID = int.Parse(Request["SystemID"]);
                //sequence.SequenceID = WorkFlow.GenerateNextSequenceID(sequence.SystemID);
                sequence.SequenceID = int.Parse(txbSequenceID.Text);
                sequence.SequenceDescription = this.txbSequenceDescription.Text.Replace("'", "''");

                if (cbxLastSequence.Checked)
                {
                    sequence.IsLastSequence = "1";  // yes
                }
                else if (cbxHasSubsequence.Checked)
                {
                    sequence.HasSubSequence = "1";  // yes

                    if (!cbxFromRequestor.Checked)
                    {
                        sequence.ValueFrom = "1";   // value from approver
                    }
                }

                // collect selected roles
                int[] roleID = new int[lbxRole.Items.Count];
                for (int i = 0; i < lbxRole.Items.Count; i++)
                {
                    roleID[i] = int.Parse(lbxRole.Items[i].Value);
                }

                sequence.RoleID = roleID;

                if (cbxSequenceNeedall.Checked && sequence.RoleID.Length > 1)
                {
                    sequence.SequenceNeedAll = "1"; // yes
                }

                if (cbxActor.Checked)
                {
                    sequence.IsActor = "1"; // yes
                }

                if (WorkFlow.AddSequence(sequence))
                {
                    if (txbUrl.Text != "")
                    {
                        sequence.UpdateUrl(txbUrl.Text);
                    }

                    ClearContents();

                    divResult.InnerText = "Add successfully!";
                }
                else
                {
                    divResult.InnerText = "Sorry, unable to add new sequence, please contact your administrator!";
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
        /// Add initial sequence with sequenceid be -1
        /// </summary>
        private void AddInitialSequence()
        {
            try
            {
                // Add initial sequence
                Sequence sequence = new Sequence();
                sequence.SystemID = int.Parse(Request["SystemID"]);
                sequence.RoleID = new int[] { -1 };
                sequence.SequenceDescription = "initial sequence";

                if (rbnRequestor.Checked)
                {
                    sequence.HasSubSequence = "1";  // has subsequence
                    sequence.ValueFrom = "0";   // must from requestor
                }

                if (WorkFlow.AddSequence(sequence))
                {
                    rbnApprover.Enabled = false;
                    rbnRequestor.Enabled = false;
                    btnAddSequence.Enabled = true;

                    ClearContents();
                }
                else
                {
                    divResult.InnerText = "Sorry, unable to add new sequence, please contact your administrator!";
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
        /// Clear contents for next adding action
        /// </summary>
        public void ClearContents()
        {
            txbSequenceID.Text = "";
            txbSequenceDescription.Text = "";

            cbxLastSequence.Checked = false;
            cbxHasSubsequence.Checked = false;
            cbxFromRequestor.Checked = false;

            cbxHasSubsequence.Enabled = true;
            cbxFromRequestor.Enabled = true;

            ddlRole.SelectedIndex = -1;
            cbxSequenceNeedall.Checked = false;
            lbxRole.Items.Clear();

            // rebind data
            BindExistingSequence(int.Parse(Request["SystemID"]));
        }

        /// <summary>
        /// Delete a sequence. Only sequence unused can be deleted
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void dgvSequences_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                divResult.InnerText = "";

                string sequenceID = this.dgvSequences.DataKeys[e.RowIndex].Value.ToString();

                if (WorkFlow.DeleteSequence(int.Parse(Request["SystemID"]), int.Parse(sequenceID)))
                {
                    BindExistingSequence(int.Parse(Request["SystemID"]));
                    CheckInitialSequence(int.Parse(Request["SystemID"]));
                    divResult.InnerText = "Delete successful!";
                }
                else
                {
                    divResult.InnerText = "Unable to delete this sequence, please contact your administrator!";
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
        /// Begin with requestor's choice
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rbnRequestor_CheckedChanged(object sender, EventArgs e)
        {
            AddInitialSequence();
        }

        /// <summary>
        /// Begin with approver
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rbnApprover_CheckedChanged(object sender, EventArgs e)
        {
            AddInitialSequence();
        }

        protected void dgvSequences_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewRow row = dgvSequences.Rows[e.NewEditIndex];
            int sequenceID = int.Parse(row.Cells[0].Text);

            if (sequenceID == -1)
            {
                this.divResult.InnerText = "Can not update url of initial sequence!";
                return;
            }

            this.divResult.InnerText = "";
            this.dgvSequences.EditIndex = e.NewEditIndex;
            BindExistingSequence(int.Parse(Request["SystemID"]));
        }

        protected void dgvSequences_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            try
            {
                this.divResult.InnerText = "";
                dgvSequences.EditIndex = -1;
                BindExistingSequence(int.Parse(Request["SystemID"]));
            }
            catch (Exception ex)
            {
                log4net.ILog Logger = (log4net.ILog)Session["logger"];
                Logger.Error("EmplID:" + Session["EMPLID"].ToString() + "---" + ex.Message);
                throw ex;
            }
        }

        protected void dgvSequences_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                GridViewRow row = dgvSequences.Rows[e.RowIndex];

                int systemID = int.Parse(Request["SystemID"]);
                int sequenceID = int.Parse(row.Cells[0].Text);
                string newUrl = ((TextBox)(row.Cells[7].Controls[1])).Text.Trim();

                //if (newUrl == "")
                //{
                //    this.divResult.InnerText = "Sequence URL can't be empty!";
                //    return;
                //}

                Sequence sequence = new Sequence(systemID, sequenceID);

                if (sequence.UpdateUrl(newUrl))
                {
                    dgvSequences.EditIndex = -1;
                    BindExistingSequence(systemID);

                    this.divResult.InnerText = "Edit Completed!";
                }
                else
                {
                    this.divResult.InnerText = "Sorry, unable to change sequence URL, please contact your administrator!";
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
