using System;
using System.Collections;
using System.Collections.Generic;
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
using WorkFlowEngine;

namespace WorkFlowPresentation.Configuration
{
    public partial class Preview : System.Web.UI.Page
    {
        private int SystemID
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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    this.divSystemName.InnerText = Request["SystemName"];
                    SystemID = Convert.ToInt32(Request.QueryString["SystemID"]);
                    WorkFlowEngine.BLL.Tree tree = new WorkFlowEngine.BLL.Tree();
                    InitializeSytem(tree);
                    while (tree.AddSon())
                    {

                    }
                    Display(tree);
                    ShowAll();
                    ShowLine(tree);
                    
                }
                catch (Exception ex)
                {
                    log4net.ILog Logger = (log4net.ILog)Session["logger"];
                    Logger.Error("EmplID:" + Session["EMPLID"].ToString() + "---" + ex.Message);
                    throw ex;
                }
                //ClientScript.RegisterStartupScript(GetType(),"key", "<script>>Line(100,100,200,200);</script>");
                //Response.Write("<v:line StrokeColor='red' StrokeWeight='2' from='10,20' to='200,300' style='POSITION:absolute;'></v:line> ");
            }
        }

        protected void InitializeSytem(WorkFlowEngine.BLL.Tree tree)
        {
            try
            {
                WorkFlowEngine.BLL.Sequence sequence = new WorkFlowEngine.BLL.Sequence(SystemID, -1);
                WorkFlowEngine.BLL.Node1 node = new WorkFlowEngine.BLL.Node1();
                node.Y= 80;
                node.X = 500;
                node.FatherID = -1;
                node.Index = 0;
                node.RowID = 1;
                node.Sequ = sequence;
                tree.Add(node);
            }
            catch(Exception ex)
            {
                log4net.ILog Logger = (log4net.ILog)Session["logger"];
                Logger.Error("EmplID:" + Session["EMPLID"].ToString() + "---" + ex.Message);
                throw ex;
            }
        }

        private string OutPutSpan(int x, int y,int sequenceID)
        { 
            return "<span style='position: absolute; top: "+y.ToString()+"px; left: "+x.ToString()+"px'>"+sequenceID.ToString()+"</span>";
        }

        protected void Display(WorkFlowEngine.BLL.Tree tree)
        {
            try
            {

                int lastNodeRowID = 0;

                foreach (WorkFlowEngine.BLL.Node1 node in tree.ListNode1)
                {
                    if (lastNodeRowID < node.RowID)
                    {
                        lastNodeRowID = node.RowID;
                        this.treeview.InnerHtml += "</br></br></br></br>";
                    }
                    if (node.RowID == 1)
                    {
                        this.treeview.InnerHtml += "<font color='Red'>" + OutPutSpan(node.X,node.Y,node.Sequ.SequenceID)+ "</font>";
                    }
                    else if (node.Sequ.HasSubSequence=="1")
                    {
                        if (node.Sequ.ValueFrom == "0")
                        {
                            this.treeview.InnerHtml += "<font color='Green'>" + OutPutSpan(node.X, node.Y, node.Sequ.SequenceID) + "</font>";
                        }
                        else
                        {
                            this.treeview.InnerHtml += "<font color='Blue'>" + OutPutSpan(node.X, node.Y, node.Sequ.SequenceID) + "</font>";
                        }
                    }
                    else
                    {
                        this.treeview.InnerHtml += "" + OutPutSpan(node.X, node.Y, node.Sequ.SequenceID) + "";
                    }
                }
            }
            catch(Exception ex)
            {
                log4net.ILog Logger = (log4net.ILog)Session["logger"];
                Logger.Error("EmplID:" + Session["EMPLID"].ToString() + "---" + ex.Message);
                throw ex;
            }
        }

        protected void ShowAll()
        {
            try
            {
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

        private void ShowLine(WorkFlowEngine.BLL.Tree tree)
        {
            foreach (WorkFlowEngine.BLL.Node1 nodeFather in tree.ListNode1)
            {
                foreach (WorkFlowEngine.BLL.Node1 nodeSon in tree.ListNode1)
                {
                    if (nodeFather.Index == nodeSon.FatherID)
                    {
                        this.Line.InnerHtml += SingleLine(nodeFather.X+6, nodeFather.Y+13, nodeSon.X+6, nodeSon.Y);
                    }
                }
            }

        }

        private string SingleLine(int fatherX, int fatherY, int sonX, int sonY)
        {
            return "<v:line StrokeColor='red' StrokeWeight='2'  from='" + fatherX.ToString() + "," + fatherY.ToString() + "' to='" + sonX.ToString() + "," + sonY.ToString() + "'></v:line>";
        }
        
    }
}
