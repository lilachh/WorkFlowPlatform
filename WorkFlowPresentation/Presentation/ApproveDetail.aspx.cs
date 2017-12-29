using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using WorkFlowEngine.BLL;

namespace WorkFlowPresentation.Presentation
{
    public partial class ApproveDetail : System.Web.UI.Page
    {
        public int SysID
        {
            get
            {
                return (int)ViewState["sysID"];
            }
            set
            {
                ViewState["sysID"] = value;
            }
        }

        public int DocID
        {
            get
            {
                return (int)ViewState["docID"];
            }
            set
            {
                ViewState["docID"] = value;
            }
        }

        public int SeqID
        {
            get
            {
                return (int)ViewState["seqID"];
            }
            set
            {
                ViewState["seqID"] = value;
            }
        }


        public int ApproveNeededID
        {
            get
            {
                return (int)ViewState["approveNeededID"];
            }
            set
            {
                ViewState["approveNeededID"] = value;
            }
        }

        public string LastStep
        {
            get
            {
                return (string)ViewState["lastStep"];
            }
            set
            {
                ViewState["lastStep"] = value;
            }
        }

        public String QuerBUrl
        {
            get
            {
                return (string)ViewState["querBUrl"];
            }
            set
            {
                ViewState["querBUrl"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    SysID = int.Parse(Request.QueryString["SystemID"]);
                    DocID = int.Parse(Request.QueryString["DocumentID"]);
                    SeqID = int.Parse(Request.QueryString["SequenceID"]);
                  
                    
                    ApproveNeededID = int.Parse(Request.QueryString["ApproveNeededID"]);

                    // redirect to the special url if it exists
                    Sequence sequence = new Sequence(SysID, SeqID);
                    LastStep = sequence.IsLastSequence;
                    string specificUrl = sequence.GetSpecificUrl();
                    if (specificUrl != null && specificUrl != string.Empty)
                    {
                        //specificUrl = specificUrl + "?SystemID=" + SysID + "&DocumentID=" + DocID + "&SequenceID=" + SeqID;
                        QuerBUrl = specificUrl;
                        //Response.Write("<script language=javascript>RequestDetail.location.href='" + specificUrl + "';</script>");
                    }
                    else
                    {
                        string sqlQuery = "select QueryBUrl from SystemType where SystemID=" + SysID;
                        QuerBUrl = (string)WorkFlowEngine.BLL.SystemType.GetSUrl(sqlQuery);
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
}
