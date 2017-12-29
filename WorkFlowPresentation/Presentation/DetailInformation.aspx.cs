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

namespace WorkFlowPresentation.Presentation
{
    public partial class DetailInformation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int systemID = int.Parse(Request.QueryString["SystemID"]);
                int documentID = int.Parse(Request.QueryString["DocumentID"]);
                int sequenceID;
                try
                {
                    sequenceID = int.Parse(Request.QueryString["SequenceID"]);
                }
                catch
                {
                    this.lblInformation.Text = "SystemID:" + documentID.ToString() + "  DocumentID:" + documentID.ToString();
                    return;
                }
                this.lblInformation.Text = "SystemID:" + documentID.ToString() + "  DocumentID:" + documentID.ToString() +
                    "   SequenceID:" + sequenceID.ToString();
            }
        }

        protected void btnApprove_Click(object sender, EventArgs e)
        {
            this.lblInformation.Text = this.lblInformation.Text + " " + "x";
        }
    }

}
