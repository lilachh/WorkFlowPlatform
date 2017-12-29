<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Routing.aspx.cs" Inherits="WorkFlowPresentation.Configuration.Routing" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Routing</title>
   <link href="../CSS/MainCSS.css" rel="stylesheet" type="text/css" />
   
   <script language="javascript" type="text/javascript">
       function CheckBeforeAdd()
       {
           if (document.getElementById("txbSystemName").value == "")
           {
               alert("System name is mandatory!");
               document.getElementById("txbSystemName").focus();
               return false;
           }           

           return true;
       }

   </script>
</head>
<body>
    <form id="Form1" method="post" runat="server">
		<div align="center">
        <br />
		<table class="tableBorder" id="table2" height="151" cellSpacing="1" cellPadding="3" border="0" style=" HEIGHT: 129px">
			<tr>
				<td id="tabletitlelink" style="FONT-WEIGHT: bold; FONT-SIZE: large; BACKGROUND-IMAGE: url(images/admin_bg_1.gif); COLOR: white; BACKGROUND-COLOR: #44aaaa; width: 100%;"
					align="center" colspan="6" height="25">Routing</td>
			</tr>					
				<tr>			    			    
			    			    
			    <td align="right" style= "font-size:large;position:relative; right:30px;">
			        <UCNav:Nav ID="Nav1" BackUrl="SystemManagement.aspx" LoginUrl="../Login.aspx" runat="server" />
			    </td>
			</tr>
			</tr>
			<tr>
				<td class="forumRowHighlight" colspan="6" height="17" valign="middle" style="width: 100%">
				    <table border="0" width="100%" id="table5" cellspacing="3" cellpadding="0">
				    <tr><td align="left">&nbsp;</td></tr>
						<tr>
							<td style="height: 15px" align="center">
							<div id="divResult" runat="server" style="color:Red; font-size:22px;"></div>
							</td>
						</tr>
						<tr>
							<td style="height: 15px" align="center">
                                                                &nbsp;Sequence From:&nbsp;
                                
                                <asp:DropDownList ID="ddlSequenceFrom" runat="server" 
                                                                    ontextchanged="ddlSequenceFrom_TextChanged" AutoPostBack="True">
                                </asp:DropDownList>
                                &nbsp;&nbsp;&nbsp; Sequence To:&nbsp; 
                                <asp:DropDownList ID="ddlSequenceTo" runat="server">
                                </asp:DropDownList> &nbsp;&nbsp;
                                <asp:Button ID="btnAdd" runat="server" OnClientClick="return CheckBeforeAdd();" 
                                    Text="Add" Width="83px" onclick="btnAdd_Click" />
                            &nbsp;&nbsp;&nbsp;
                                                                <asp:Button ID="btnShowAll" runat="server" 
                                                                    Text="Show All Routing" onclick="btnShowAll_Click" />
                            </td>
						</tr>
                        <asp:Panel ID="pnlCondition" runat="server" Visible="False">
                      
						<tr>
							<td style="height: 15px" align="center">
                                                                Condition Description: 
                                <asp:TextBox ID="tbxConditionDesc" runat="server" Width="150px"></asp:TextBox>
                            </td>
						</tr>
						
						</asp:Panel>
						<tr>
							<td style="height: 10px"></td>
						</tr>
						<tr>
							<td style="height: 10px; text-align:center">Current System: >Current System: <span id="divSystemName" runat="server" style="color:Red; font-size:22px;"></span></td>
						</tr>		
			            <tr>
			                <td align="center">
			                <asp:GridView ID="dgvRouting" runat="server" 
                                    CellPadding ="3" HeaderStyle-HorizontalAlign="Left"
                                    AutoGenerateColumns="False" BackColor="#E7E7E7" BorderColor="#CCCCCC" 
                                    Width="770px" onrowcancelingedit="dgvRouting_RowCancelingEdit" 
                                    onrowcommand="dgvRouting_RowCommand" onrowediting="dgvRouting_RowEditing" onrowupdating="dgvRouting_RowUpdating" 
                                     >
                                
                                <FooterStyle CssClass="GridViewFooterStyle" />
                                <RowStyle CssClass="GridViewRowStyle" />
                                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                                <PagerStyle CssClass="GridViewPagerStyle" />
                                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                                <HeaderStyle CssClass="GridViewHeaderStyle" HorizontalAlign="Left" />
                                
                                <Columns>
                                    <asp:BoundField DataField="SystemID" ReadOnly="true" HeaderText="System ID"  >
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>    
                                    <asp:BoundField DataField="MainID"  HeaderText="MainID" ReadOnly="true" >
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="MainDescription"  HeaderText="MainDescription"  ReadOnly="true">
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                      <asp:BoundField DataField="NextID"  HeaderText="NextD"  ReadOnly="true">
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="NextDescription"  HeaderText="NextDescription" ReadOnly="true" >
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ConditionID" HeaderText="ConditionID" >
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ConditionDescription" HeaderText="ConditionDescription" >
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:CommandField HeaderText="Edit" ShowEditButton="true" ItemStyle-HorizontalAlign="Left" />
                                    <asp:TemplateField HeaderText="Delete" >
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbnDelete" runat="server" CommandName="DeleteSystem">Delete</asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                     
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
</body>
</html>
