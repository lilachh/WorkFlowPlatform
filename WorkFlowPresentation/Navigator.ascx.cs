using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WorkFlowPresentation
{
    public partial class Navigator : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                backUrl = "Login.aspx";
                loginUrl = "../Login.aspx";
            }
        }

        #region Fields

        private string backUrl;
        private string loginUrl;

        #endregion

        #region Properties

        public string BackUrl
        {
            get
            {
                return backUrl;
            }

            set
            {
                backUrl = value;
            }
        }

        public string LoginUrl
        {
            get
            {
                return loginUrl;
            }

            set
            {
                loginUrl = value;
            }
        }

        #endregion

        protected void lbBack_Click(object sender, EventArgs e)
        {            
            Response.Redirect(backUrl);
        }

        protected void lbExit_Click(object sender, EventArgs e)
        {
            log4net.ILog Logger = (log4net.ILog)Session["logger"];
            Logger.Info(Session["EMPLID".ToString()] + " logged off.");
            
            Session["EMPLID"] = null;
            Response.Write("<script>window.parent.location.href = '" + loginUrl + "';</script>");
        }
    }
}