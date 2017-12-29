<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="test.aspx.cs" Inherits="WorkFlowPresentation.Presentation.test" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    
     
 <script language="javascript" type="text/javascript">
    function Approve() 
    {
        document.getElementById("tb1").value='approve';
    }
    function Reject() 
    {
         document.getElementById("tb1").value='reject';
    }
 </script>

</head>

<body style="width:600px;height:200px">
    <form id="form1" runat="server">
    <div>
        <br />
        <asp:Button ID="btnApprove" runat="server" Text="Button" OnClientClick="javascript:return Approve();" />
        <asp:Button ID="btnReject" runat="server" Text="Button" OnClientClick="javascript:return Reject();" />
        <asp:TextBox ID="tb1" runat="server"></asp:TextBox>
   
    </div>
    </form>
</body>
</html>
