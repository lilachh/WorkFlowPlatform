<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PendingList.aspx.cs" Inherits="WorkFlowPresentation.Presentation.PendingList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Pending list</title>
   <link href="../CSS/MainCSS.css" rel="stylesheet" type="text/css" /> 
    <script language="javascript" type="text/javascript" src="../DatePicker/WdatePicker.js"></script>
    <style type="text/css">
        .style1
        {
            width: 73px;
        }
        .style2
        {
            width: 175px;
        }
        .style3
        {
            width: 162px;
        }
        .style4
        {
            width: 157px;
        }
    </style>
</head>
<body>
    <form id="Form1" method="post" runat="server">
		<div align="center">
        <br />
		<table class="tableBorder" id="table2" height="151" cellSpacing="1" cellPadding="3" border="0" style=" HEIGHT: 129px">
			<tr>
				<td id="tabletitlelink" style="FONT-WEIGHT: bold; FONT-SIZE:large; BACKGROUND-IMAGE: url(images/admin_bg_1.gif); COLOR: white; BACKGROUND-COLOR: #44aaaa; width: 100%;"
					align="center" colspan="6" height="25">Pending list of system: <span id="divSystemName" runat="server" style="color:Red; font-size:22px;">xxx</span></td>
			</tr>
			<tr>			    			    
			    <td align="right" style= "font-size:large;position:relative; right:30px;">
			        <UCNav:Nav ID="UCNav1" BackUrl="../main.html" LoginUrl="../Login.aspx" runat="server" />
			    </td>
			</tr>
			<tr>
				<td class="forumRowHighlight" colspan="6" height="17" valign="middle" style="width: 100%">
				    <table border="0" width="100%" id="table5" cellspacing="3" cellpadding="0">											
						<tr>
				            <td align="center">
				                <table>
				                    <tr>
				                        <td  align="right" style="color: #FF0000; font-weight: bold;">Query: &nbsp;&nbsp;</td>
				                        <td class="style2">System:&nbsp; <asp:DropDownList ID="ddlSystem" runat="server" 
                                                Width="116px" ></asp:DropDownList></td>
				                        <td >From:&nbsp; <input class="Wdate" type="text" id="dpFromDate" runat="server" 
                                                onfocus="WdatePicker({isShowClear:false,readOnly:true})" style="width: 99px"/></td>
				                        <td class="style4">To:&nbsp; <input class="Wdate" type="text" id="dpToDate" runat="server" onfocus="WdatePicker({isShowClear:false,readOnly:true})" style="width: 99px"/></td>
				                        <td><asp:Button ID="btnQuery" runat="server" Text="Query" Width="78px" 
                                                onclick="btnQuery_Click" /></td>
				                    </tr>
				                </table>
				            
				            </td>
			            </tr>
						<tr>
				            <td align="center"><div id="divResult" runat="server" style="color:Red; font-size:22px;"></div></td>
			            </tr>
			            <tr>
			                <td align="center">
			                <asp:GridView ID="dgvPendinglist" runat="server" 
                                    CellPadding ="3" HeaderStyle-HorizontalAlign="Left" DataKeyNames="SequenceID"
                                    AutoGenerateColumns="False" BackColor="#E7E7E7" BorderColor="#CCCCCC" Width="750px">
                                
                                <FooterStyle CssClass="GridViewFooterStyle" />
                                <RowStyle CssClass="GridViewRowStyle" />
                                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                                <PagerStyle CssClass="GridViewPagerStyle" />
                                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                                <HeaderStyle CssClass="GridViewHeaderStyle" HorizontalAlign="Left" />
                                
                                <Columns>
                                    <asp:BoundField DataField="SystemID" ReadOnly="true" Visible="false">
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="SystemName" ReadOnly="true" HeaderText="System">
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>                                    
                                    <asp:TemplateField HeaderText="Document ID" >
                                        <ItemTemplate>
	                                        <a href='ApproveDetail.aspx?SystemID=<%#Eval("SystemID") %>&DocumentID=<%#Eval("DocumentID")%>&SequenceID=<%#Eval("SequenceID")%>&ApproveNeededID=<%#Eval("ApproveNeededID")%>'><%#Eval("DocumentID")%></a>
	                                    </ItemTemplate>
	                                    <ItemStyle HorizontalAlign="Left" />
	                                </asp:TemplateField>
                                    <asp:BoundField DataField="SequenceID" ReadOnly="true" Visible="false">
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ApproveNeededID" ReadOnly="true" HeaderText="ApproveNeededID" Visible="false">
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="EMPLID" ReadOnly="true" Visible="false">
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="NAME" ReadOnly="true" HeaderText="Requestor Name">
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="SubmitDate" ReadOnly="true" HeaderText="Submit Date" >
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField> 
                                    <asp:BoundField DataField="RoleDescription" ReadOnly="true" HeaderText="Current Role" >
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>                                    
                                </Columns>
                            </asp:GridView>
			                </td>
			            </tr>			            
			            <tr>
							<td style="height: 15px"></td>
						</tr>		
					</table>
				</td>
			</tr>	
		</table>
	</div>
	</form>
    <p>
&nbsp;</p>
</body>
</html>

