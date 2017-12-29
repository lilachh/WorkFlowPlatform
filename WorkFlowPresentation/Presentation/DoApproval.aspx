<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DoApproval.aspx.cs" Inherits="WorkFlowPresentation.Presentation.DoApproval" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Do Approve</title>
        
    <link href="../CSS/MainCSS.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">

        document.domain = "amd.com";

        function ApproveClientClick() 
        {
            if (window.parent.frames[1].document.getElementById('btnApprove').onclick()) 
            {
                window.parent.frames[1].document.getElementById('btnApprove').click()
                return true;
            }
            else 
            {
                return false;
            }
        }

        function RejectClientClick() 
        {
            if (window.parent.frames[1].document.getElementById('btnReject').onclick()) 
            {
                window.parent.frames[1].document.getElementById('btnReject').click()
                return true;
            }
            else 
            {
                return false;
            }
        }
        
    </script>
</head>
<body style="width: auto; height: auto">
    <form id="form1" runat="server">
    <div style="text-align:center">
        <table>
            <tr>
                <td colspan="2" style="height:10px"></td>	            
            </tr>
            <tr>
	            <td align="center" colspan="2">
	                <asp:Button ID="btnApprove" runat="server" Text="Approve" Width="96px" OnClientClick="javascript:return ApproveClientClick();"
                        onclick="btnApprove_Click" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnReject" runat="server" Text="Reject" width="96px"  OnClientClick="javascript:return RejectClientClick();"
                        onclick="btnReject_Click" />
	                &nbsp;</td>
            </tr>
            <tr>
                <td colspan="2" style="height:10px"></td>	            
            </tr>
            <tr>
                <td align="right"><asp:Label ID="lblCondition" runat="server" Text="Select Condition:"></asp:Label></td>
                <td align="left">
                    <asp:DropDownList ID="ddlCondition" runat="server" Height="16px" 
                        Width="286px"></asp:DropDownList></td>	            
            </tr>
            <tr>
                <td align="right">Remarks:</td>
                <td align="left">
                    <asp:TextBox ID="txbRemark" runat="server" TextMode="MultiLine" 
                        Height="42px" Width="287px"></asp:TextBox></td>	            
            </tr>
            </table>
    </div>
    </form>
</body>
</html>
