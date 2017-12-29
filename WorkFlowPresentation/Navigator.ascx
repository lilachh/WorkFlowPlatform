<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Navigator.ascx.cs" Inherits="WorkFlowPresentation.Navigator" %>

<span style="font-size:medium">
<b><a href="../main.html">Main</a></b> | 
<asp:LinkButton ID="lbBack" runat="server" Text="Back" Font-Bold="true" 
    onclick="lbBack_Click" ></asp:LinkButton> |
<asp:LinkButton ID="lbExit" runat="server" Text="Exit" Font-Bold="true" 
    onclick="lbExit_Click"></asp:LinkButton>
 </span>