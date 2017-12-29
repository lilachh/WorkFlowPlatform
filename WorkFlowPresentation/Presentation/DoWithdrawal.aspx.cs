using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WorkFlowEngine;

namespace WorkFlowPresentation.Presentation
{
    public partial class DoWithdrawal : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    int emplID = int.Parse(Session["EMPLID"].ToString());
                    int sysID = int.Parse(Request.QueryString["SystemID"]);
                    int docID = int.Parse(Request.QueryString["DocumentID"]);
                    WorkFlowEngine.BLL.Document document = new WorkFlowEngine.BLL.Document(sysID, docID);
                    if (document.IfWithDrawVisible(emplID) && document.GetDocHeaderStatus() == "pending")
                    {
                        btnWithDraw.Visible = true;
                    }
                    else
                    {
                        btnWithDraw.Visible = false;
                    }
                }
                catch (Exception ex)
                {
                    ((log4net.ILog)Session["logger"]).Error(Session["EMPLID"] + "  " + ex.Message);
                    throw ex;
                }

            }
        }

        protected void btnWithDraw_Click(object sender, EventArgs e)
        {
            try
            {
                int sysID = int.Parse(Request.QueryString["SystemID"]);
                int docID = int.Parse(Request.QueryString["DocumentID"]);
                WorkFlowEngine.BLL.Document document = new WorkFlowEngine.BLL.Document(sysID, docID);
                document.UpdateDocHeaderAsCanceled();

                Response.Write("<script language=javascript>alert('Cancel Successfully!')</script>");
                Response.Write("<script language=javascript>parent.location.href='RequestList.aspx?SystemID=-1';</script>");
            }
            catch (Exception ex)
            {
                ((log4net.ILog)Session["logger"]).Error(Session["EMPLID"] + "  " + ex.Message);
                throw ex;
            }
        }

    }
}
