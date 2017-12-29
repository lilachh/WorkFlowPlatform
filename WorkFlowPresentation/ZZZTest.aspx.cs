using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WorkFlowEngine.BLL;
using System.Collections;

namespace WorkFlowPresentation
{
    public partial class ZZZTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            List<RequestorChoice> choiceList = new List<RequestorChoice>();
            RequestorChoice rc = new RequestorChoice(2, 14, 200, 2);
            choiceList.Add(rc);
            WorkFlow.NewRequest(2, 14, 449587, 449587, -1, -1, choiceList);
            
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            DataSet ds = WorkFlowEngine.BLL.WorkFlow.GetApprovalFlow(2, 6);
        }

        protected void btnApproveFlow_Click(object sender, EventArgs e)
        {
            //Response.Redirect("Presentation\\GeneratedLogs.aspx?systemID="+tbxSystemID.Text+"&documentID="+tbxDocumentID.Text+"");
        }

        protected void btnGeneNewRequest_Click(object sender, EventArgs e)
        {
           
            int sysID = int.Parse(txbSystemID.Text);
            int docID = int.Parse(txbDocumentID.Text);
            int requestorID = int.Parse(txbRequestorID.Text);
            int ownerID = int.Parse(txbOwnerID.Text);
            List<RequestorChoice> choiceList = new List<RequestorChoice>();
            RequestorChoice rc = new RequestorChoice(sysID, docID, -1, 1);
            choiceList.Add(rc);
            WorkFlow.NewRequest(sysID, docID, requestorID, ownerID, -1, -1, choiceList);
        }

        protected void btnSetSession_Click(object sender, EventArgs e)
        {
            Session["EMPLID"] = int.Parse(ddlSession.Text);
        }
    }
}
