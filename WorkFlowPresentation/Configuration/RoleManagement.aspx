<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RoleManagement.aspx.cs" Inherits="WorkFlowPresentation.Configuration.RoleManagement" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>RoleManagerment</title>
   <link href="../CSS/MainCSS.css" rel="stylesheet" type="text/css" />
   
   <script language="javascript" type="text/javascript">
       function CheckBeforeAdd()
       {
           if (document.getElementById("ddlAddRoleType").value == "")
           {
               alert("Select a role type please!");
               document.getElementById("ddlAddRoleType").focus();
               return false;
           }

           if (document.getElementById("txbRoleDescription").value == "")
           {
               alert("Enter the role description please!");
               document.getElementById("txbRoleDescription").focus();
               return false;
           }

           if (document.getElementById("ddlAddRoleType").value == "0" && document.getElementById("txbGrade").value == "")
           {
               alert("Enter the report line GRADE please!");
               document.getElementById("txbGrade").focus();
               return false;
           }

           return true;
       }

       function RoleTypeChange()
       {
           if (document.getElementById("ddlAddRoleType").value == "0")
           {
               document.getElementById("txbGrade").disabled = false;
           }
           else
           {
               document.getElementById("txbGrade").disabled = true;
           }
       }
   </script>
    <style type="text/css">
        #table5
        {
            width: 76%;
        }
        .style2
        {
            height: 15px;
            width: 79px;
        }
        .style3
        {
            width: 79px;
        }
        .style4
        {
            height: 15px;
            width: 842px;
        }
    </style>
</head>
<body>
    <form id="Form1" method="post" runat="server">
		<div align="center">
        <br />
		<table class="tableBorder" id="table2" height="151" cellSpacing="1" cellPadding="3" border="0" style=" HEIGHT: 129px">
			<tr>
				<td id="tabletitlelink" style="FONT-WEIGHT: bold; FONT-SIZE: large; BACKGROUND-IMAGE: url(images/admin_bg_1.gif); COLOR: white; BACKGROUND-COLOR: #44aaaa; width: 100%;"
					align="center" colspan="6" height="25">Role Management</td>
			</tr>					
			<tr>
				<td class="forumRowHighlight" colspan="6" height="17" valign="middle" style="width: 100%">
				    <table border="0" id="table5" cellspacing="3" cellpadding="0">
						<tr>
							<td style="height: 10px" colspan="2"><hr /></td>
						</tr>
						<tr>
						    <td  align="right" style="color: #FF0000; font-weight: bold;" class="style3">Query: &nbsp;&nbsp;</td>
							<td align="left" class="style4">
							    RoleType:&nbsp;
                                <asp:DropDownList ID="ddlSelectRole" runat="server" Width="131px" Height="16px">
                                    <asp:ListItem Value="-1"> All</asp:ListItem>
                                    <asp:ListItem Value="0">report line</asp:ListItem>
                                    <asp:ListItem Value="1">responsibility</asp:ListItem>
                                </asp:DropDownList>
                                &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;Role Description: &nbsp;<asp:TextBox 
                                    ID="txbSelectRoleDescription" runat="server" Width="130px"></asp:TextBox>
                                &nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnSearch" runat="server" Text="Query" Width="83px" 
                                    onclick="btnSearch_Click" />
                            </td>
						</tr>														
						<tr>
							<td style="height: 10px" colspan="2"><hr /></td>
						</tr>
						<tr>
						    <td align="right" style="color: #FF0000; font-weight: bold;" class="style3">Add Role:&nbsp;&nbsp; </td>
							<td align="left" class="style4">
                                RoleType:&nbsp;
                                <asp:DropDownList ID="ddlAddRoleType" runat="server" Width="131px" 
                                    onchange="javascript:RoleTypeChange()">
                                    <asp:ListItem></asp:ListItem>
                                    <asp:ListItem Value="0">report line</asp:ListItem>
                                    <asp:ListItem Value="1">responsibility</asp:ListItem>
                                </asp:DropDownList>
                                &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;Role Description:&nbsp;
                                <asp:TextBox ID="txbRoleDescription" runat="server" Width="131px"></asp:TextBox>
                                &nbsp;&nbsp;&nbsp;&nbsp; Grade: &nbsp;<asp:TextBox ID="txbGrade" runat="server" Width="38px" 
                                    MaxLength="2"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnAdd" runat="server" OnClientClick="return CheckBeforeAdd();" 
                                    Text="Add" Width="83px" onclick="btnAdd_Click" />
                            </td>
						</tr>
						<tr>
							<td style="height: 10px" colspan="2"><hr /></td>
						</tr>
						<tr>
							<td style="height: 10px" align="center" colspan="2">
							    <div id="divResult" runat="server" style="color:Red; font-size:22px;"></div>
							</td>
						</tr>	
			            <tr>
			                <td align="center" colspan="2">
			                <asp:GridView ID="dgvApproverRole" runat="server" 
                                    CellPadding ="3" HeaderStyle-HorizontalAlign="Left"
                                    AutoGenerateColumns="False" BackColor="#E7E7E7" BorderColor="#CCCCCC"  
                                    AllowPaging="True" Width="100%" 
                                    onrowcommand="dgvApproverRole_RowCommand" 
                                    onrowdatabound="dgvApproverRole_RowDataBound" 
                                    onrowediting="dgvApproverRole_RowEditing" 
                                    onrowcancelingedit="dgvApproverRole_RowCancelingEdit" 
                                    onrowupdating="dgvApproverRole_RowUpdating" PageSize="20" >
                                
                                <FooterStyle CssClass="GridViewFooterStyle" />
                                <RowStyle CssClass="GridViewRowStyle" />
                                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                                <PagerStyle CssClass="GridViewPagerStyle" />
                                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                                <HeaderStyle CssClass="GridViewHeaderStyle" HorizontalAlign="Left" />
                                
                                <Columns>
                                    <asp:BoundField DataField="RoleID" ReadOnly="true" HeaderText="Role ID" >
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>    
                                    <asp:BoundField DataField="RoleType" ReadOnly="true" HeaderText="Role Type" >
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>                                                                        
                                    <asp:TemplateField HeaderText="Role Description" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <%#Eval("RoleDescription") %>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txbRoleDescription" runat="server" Text='<%#Eval("RoleDescription") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>                                                
                                    </asp:TemplateField>                                    
                                    <asp:BoundField DataField="Grade" ReadOnly="true" HeaderText="Grade">
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>                                            
	                                <asp:TemplateField HeaderText="Add Member" >
                                        <ItemTemplate>
	                                        <asp:LinkButton  runat="server" CommandName="AddMember" CommandArgument='<%#Eval("RoleID")%>' 
	                                            ID="lbnAddMember"  Text="Add Member"  />
	                                    </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
	                                </asp:TemplateField>
	                                <asp:CommandField HeaderText="Edit" ShowEditButton="true" 
                                        ItemStyle-HorizontalAlign="Left" >
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:CommandField>
                                </Columns>
                            </asp:GridView>
			                </td>
			            </tr>			            
			            <tr>
							<td class="style2"></td>
						</tr>			
					</table>
				</td>
			</tr>	
		</table>
	</div>
	</form>
</body>
</html>