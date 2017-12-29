<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DoWithdrawal.aspx.cs" Inherits="WorkFlowPresentation.Presentation.DoWithdrawal" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title></title>
    
     <link href="../CSS/MainCSS.css" rel="stylesheet" type="text/css" />
     
     <script language="javascript" type="text/javascript">

         document.domain = "amd.com";

         function WithDrawClientClick() 
         {
             if (window.parent.frames[1].document.getElementById('btnWithdraw').onclick()) 
             {
                 window.parent.frames[1].document.getElementById('btnWithdraw').click()
                 return true;
             }
             else 
             {
                 return false;
             }
         }
                
    </script>
    
</head>

<body>
    <form id="form2" runat="server" style="vertical-align:bottom">
        <div style="text-align:center; vertical-align:bottom">
            <table>
                <tr>
                    <td colspan="2" style="height:10px"></td>	            
                </tr>
                <tr>
                    <td colspan="2" style="height:10px"></td>
                </tr>
                <tr>
	                <td align="center" colspan="2">
	                     <asp:Button ID="btnWithDraw" runat="server" Text="WithDraw"  onclick="btnWithDraw_Click" OnClientClick="javascript:return WithDrawClientClick();"/>
                    </td>
                </tr>
                            
             </table>
        </div>
    </form>
</body>
</html>
