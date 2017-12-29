using System;
using System.Web;
using System.Web.UI;
using System.Data;
using WorkFlowEngine.BLL;
using System.Data.SqlClient;

namespace WorkFlowPresentation.Presentation
{
    public partial class DoApproval : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request["SystemID"] == null || Request["DocumentID"] == null || Request["SequenceID"] == null)
                {
                    //Response.Redirect("PendingList.aspx?SystemID=-1", true);
                }

                int sysID = int.Parse(Request.QueryString["SystemID"]);
                int docID = int.Parse(Request.QueryString["DocumentID"]);
                int seqID = int.Parse(Request.QueryString["SequenceID"]);

                Sequence sequence = new Sequence(sysID, seqID);

                // disenabed the reject button if it's an actor
                if (sequence.IsActor == "1")
                {
                    this.btnReject.Visible = false;
                }               

                if (sequence.HasSubSequence == "0")
                {
                    this.lblCondition.Visible = false;
                    this.ddlCondition.Visible = false;
                }
                else
                {
                    try
                    {
                        DataSet ds = sequence.GetConditions();
                        this.ddlCondition.DataSource = ds.Tables[0];
                        this.ddlCondition.DataTextField = "ConditionDescription";
                        this.ddlCondition.DataValueField = "ConditionID";
                        this.ddlCondition.DataBind();
                        this.ddlCondition.SelectedIndex = -1;

                        if (sequence.ValueFrom == "0")
                        {
                            int conditionID = WorkFlow.GetRequestorChoice(sysID, seqID, docID);
                            this.ddlCondition.SelectedValue = conditionID.ToString();
                            this.ddlCondition.Enabled = false;
                        }
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
            }

            WorkFlowWebService.WorkFlow x = new WorkFlowWebService.WorkFlow();
        }

        protected void btnApprove_Click(object sender, EventArgs e)
        {
            int sysID = int.Parse(Request.QueryString["SystemID"]);
            int docID = int.Parse(Request.QueryString["DocumentID"]);
            int seqID = int.Parse(Request.QueryString["SequenceID"]);
            int emplid=int.Parse(Session["EMPLID"].ToString());
            int approveNeededID = int.Parse(Request.QueryString["ApproveNeededID"]);
            Document document = new Document(sysID, docID);

            try
            {
                if (ddlCondition.Items.Count == 0)
                {
                    if (document.DoApproval(seqID, approveNeededID, emplid, txbRemark.Text))
                    {
                        Response.Write("<script language=javascript>alert('Approve Successfully!')</script>");
                        Response.Write("<script language=javascript>parent.location.href='PendingList.aspx?SystemID=-1';</script>");
                    }
                    else
                    {
                        Response.Write("<script language=javascript>alert('Approve failed!')</script>");
                        Response.Write("<script language=javascript>parent.location.href='PendingList.aspx?SystemID=-1';</script>");
                    }
                }
                else
                {
                    if (document.DoApproval(seqID, approveNeededID, int.Parse(ddlCondition.SelectedValue), emplid, txbRemark.Text))
                    {
                        Response.Write("<script language=javascript>parent.location.href='PendingList.aspx?SystemID=-1';</script>");
                    }
                }
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

        protected void btnReject_Click(object sender, EventArgs e)
        {
            try
            {
                int sysID = int.Parse(Request.QueryString["SystemID"]);
                int docID = int.Parse(Request.QueryString["DocumentID"]);
                int seqID = int.Parse(Request.QueryString["SequenceID"]);
                int emplid = int.Parse(Session["EMPLID"].ToString());
                int approveNeededID = int.Parse(Request.QueryString["ApproveNeededID"]);
                Document document = new Document(sysID, docID);

                if (document.DoReject(seqID, approveNeededID, emplid, txbRemark.Text))
                {
                    Response.Write("<script language=javascript>alert('Reject Successfully!')</script>");
                    Response.Write("<script language=javascript>parent.location.href='RequestList.aspx?SystemID=-1';</script>");
                }
                else
                {
                    Response.Write("<script language=javascript>alert('Reject failed!')</script>");
                    Response.Write("<script language=javascript>parent.location.href='RequestList.aspx?SystemID=-1';</script>");
                }
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
    }
}
