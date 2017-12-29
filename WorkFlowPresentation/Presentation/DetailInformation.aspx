<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DetailInformation.aspx.cs" Inherits="WorkFlowPresentation.Presentation.DetailInformation" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Detail Information</title>
    <script language="javascript" type="text/javascript">
        function ApproveClientClick()
        {
            return document.getElementById("cbx1").checked;
        }
    </script>
</head>
<body bgcolor="#CC99FF"  >
    <form id="form1" runat="server">
    <div align="center">
    <br /><br /><br />
            <p style="font-size:x-large; color: #FF00FF; font-weight: bolder;">Detail Information
            <br />
                <asp:Button ID="btnApprove" runat="server" Text="Approve" 
                    OnClientClick="javascript:return ApproveClientClick();" 
                    onclick="btnApprove_Click" />
            </p>
    </div>
    <div align="center">
        <p style="font-size:x-large; color: #FF00FF; font-weight: bolder;">
            <asp:CheckBox ID="cbx1" runat="server" Text="true" />
            <br />
                <asp:Label ID="lblInformation" runat="server" Text="" ForeColor="#FFFF99"></asp:Label>
            </p>
    </div>
    </form>
</body>
</html>
