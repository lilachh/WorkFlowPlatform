<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GeneratedLogs.aspx.cs" Inherits="WorkFlowPresentation.Presentation.GeneratedLogs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
   <link href="../CSS/MainCSS.css" rel="stylesheet" type="text/css" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
    <p class="FrameHeader">Approver Flow</p>
        <asp:GridView ID="ApproverFlow" runat="server" 
            CellPadding ="3" HeaderStyle-HorizontalAlign="Left" DataKeyNames="SequenceID"
                AutoGenerateColumns="False" BackColor="#E7E7E7" BorderColor="#CCCCCC" Width="700px">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <RowStyle CssClass="GridViewRowStyle" Wrap="false" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" HorizontalAlign="Left"/>           
            <Columns>
       
                <asp:BoundField DataField="SequenceID" 
                    HeaderText="SequenceID" />
                <asp:BoundField DataField="RoleDescription" 
                    HeaderText="Role" />
                <asp:BoundField DataField="Approver"  HeaderText="Approver" />  
                <asp:BoundField DataField="Status"  HeaderText="Status" />  
                <asp:BoundField DataField="ApproveDate" HeaderText="ApproveDate" />    
                <asp:BoundField DataField="remark" HeaderText="Remark" />           
            </Columns>
        </asp:GridView>
    </div>
    </form>
</body>
</html>
