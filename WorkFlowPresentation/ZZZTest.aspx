<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ZZZTest.aspx.cs" Inherits="WorkFlowPresentation.ZZZTest" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="text-align:center">
            <table>
            <tr>
                <td>Session</td>
                <td>
                    <asp:DropDownList ID="ddlSession" runat="server" Width="124px">
                        <asp:ListItem>447150</asp:ListItem>
                        <asp:ListItem>447151</asp:ListItem>
                        <asp:ListItem>380004</asp:ListItem>
                        <asp:ListItem>451674</asp:ListItem>
                        <asp:ListItem>449587</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="right"><asp:Button ID="btnSetSession" runat="server" 
                        Text="Set Session" onclick="btnSetSession_Click" /></td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td colspan="2"><a href="Default.aspx">Go to Approve</a></td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>SystemID:</td>
                <td><asp:TextBox ID="txbSystemID" runat="server">20</asp:TextBox></td>
            </tr>
            <tr>
                <td>DocumentID:</td>
                <td><asp:TextBox ID="txbDocumentID" runat="server">98</asp:TextBox></td>
            </tr>            
            <tr>
                <td>RequestorID:</td>
                <td><asp:TextBox ID="txbRequestorID" runat="server">380062</asp:TextBox></td>
            </tr>
            <tr>
                <td>OwnerID:</td>
                <td><asp:TextBox ID="txbOwnerID" runat="server">380062</asp:TextBox></td>
            </tr>
            <tr>
                <td colspan="2"></td>
            </tr>
            <tr>
                <td colspan="2"><asp:Button ID="btnGeneNewRequest" runat="server" 
                        Text="Generate New Request" onclick="btnGeneNewRequest_Click" /></td>
            </tr>
            
        </table>           
        </div>
    </form>
</body>
</html>
