using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WorkFlowEngine.BLL;

namespace WorkFlowPresentation.Presentation
{
    public partial class ReqeustDetail : System.Web.UI.Page
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

                    QuerBUrl = SystemType.GetQueryUrl(SysID);
                    if (QuerBUrl == null || QuerBUrl == string.Empty)
                        QuerBUrl = (string)SystemType.GetSUrl("select QueryBUrl from SystemType where SystemID=" + SysID + "");
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
}
