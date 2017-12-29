<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SystemManagement.aspx.cs" Inherits="WorkFlowPresentation.Configuration.SystemManagement" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>RoleManagerment</title>
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

       function Reset()
       {
           document.getElementById("txbSystemName").value = "";
           document.getElementById("txbBUrl").value = "";
           document.getElementById("txbSUrl").value = "";
           document.getElementById("divResult").innerHTML = "";

           document.getElementById("txbSystemName").focus();

           return false;
       }

   </script>
    <style type="text/css">
        #table5
        {
            width: 84%;
        }
        .style2
        {
            width: 301px;
        }
        .style3
        {
            height: 15px;
            width: 380px;
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
					align="center" colspan="6" height="25">System Management</td>
			</tr>					
			<tr>
				<td class="forumRowHighlight" colspan="6" height="17" valign="middle" style="width: 100%">
				
				    <table border="0" id="table5" cellspacing="3" cellpadding="0">
						
						<tr>
							<td style="height: 10px" colspan="2">
							    <table>							    
							        <tr>
							            <td style="height: 15px" align="center" colspan="2">
							            <div id="divResult" runat="server" style="color:Red; font-size:22px;"></div>
							            </td>
						            </tr>
						            <tr>
						                <td align="right" class="style2" >
						                    System Name:
						                </td>
							            <td align="left" class="style3">
                                            <asp:TextBox ID="txbSystemName" runat="server" Width="168px"></asp:TextBox>
                                            
                                        </td>
						            </tr>
						            <tr>
						                <td align="right" class="style2" >
						                    Big Url:
						                </td>
							            <td align="left" class="style3">
                                            <asp:TextBox ID="txbBUrl" runat="server" Width="300px"></asp:TextBox>
                                            </td>
						            </tr>
						            <tr>
						                <td align="right" class="style2" >
						                    Small Url:
						                </td>
							            <td align="left" class="style3">
                                            <asp:TextBox ID="txbSUrl" runat="server" Width="299px"></asp:TextBox>
                                            </td>
						            </tr>
						            <tr>
						                <td align="right" class="style2" >
						                    Apply Url:</td>
							            <td align="left" class="style3">
                                            <asp:TextBox ID="txbApplyUrl" runat="server" Width="299px"></asp:TextBox>
                                            </td>
						            </tr>
						            <tr>
						                <td align="right" class="style2" >
						                    Query Url:</td>
							            <td align="left" class="style3">
                                            <asp:TextBox ID="txbQueryUrl" runat="server" Width="299px"></asp:TextBox>
                                            </td>
						            </tr>
						            <tr>
						                <td align="right" class="style2" >
						                    Navigate Url: </td>
							            <td align="left" class="style3">
                                            <asp:TextBox ID="txbNavigateUrl" runat="server" Width="299px"></asp:TextBox>
                                            </td>
						            </tr>
						            <tr>
							            <td style="height: 15px" align="center" colspan="2">
                                               <asp:Button ID="btnAdd" runat="server" OnClientClick="return CheckBeforeAdd();" 
                                                Text="Add" Width="83px" onclick="btnAdd_Click" />                             
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                               <asp:Button ID="btnReset" runat="server" OnClientClick="return Reset();" 
                                                Text="Reset" Width="83px" />                             
                                            </td>
						            </tr>
						            <tr>
						                <td height="10px" colspan="2"><hr /></td>
						            </tr>
							    
							    </table>
							</td>
						</tr>
						<tr>
							<td style="height: 10px" colspan="2"></td>
						</tr>	
			            <tr>
			                <td align="center" colspan="2">
			                <asp:GridView ID="dgvSystem" runat="server" 
                                    CellPadding ="3" HeaderStyle-HorizontalAlign="Left"
                                    AutoGenerateColumns="False" BackColor="#E7E7E7" BorderColor="#CCCCCC" 
                                    Width="770px" 
                                    onrowupdating="dgvSystem_RowUpdating" onrowcommand="dgvSystem_RowCommand" 
                                    onrowcancelingedit="dgvSystem_RowCancelingEdit" 
                                    onrowediting="dgvSystem_RowEditing" >
                                
                                <FooterStyle CssClass="GridViewFooterStyle" />
                                <RowStyle CssClass="GridViewRowStyle"  Wrap="false"/>
                                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                                <PagerStyle CssClass="GridViewPagerStyle" />
                                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                                <HeaderStyle CssClass="GridViewHeaderStyle" HorizontalAlign="Left" />
                                
                                <Columns>
                                    <asp:BoundField DataField="SystemID" ReadOnly="true" HeaderText="System ID" >
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>  
                                    <asp:BoundField DataField="SystemName"  HeaderText="System Name" >
                                        <ItemStyle HorizontalAlign="Left" />
                                         </asp:BoundField> 
                                    <asp:BoundField DataField="QueryBUrl"  HeaderText="Big Url" Visible="false" >
                                        <ItemStyle HorizontalAlign="Left" />
                                         </asp:BoundField> 
                                    <asp:BoundField DataField="QuerySurl"  HeaderText="Small Url" Visible="false" >
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Applyurl"  HeaderText="Apply Url" Visible="false" >
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Queryurl"  HeaderText="Query Url" Visible="false" >
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="NavigateUrl"  HeaderText="Navigate Url" Visible="false" >
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>                                    			                        			                        
                                    <asp:TemplateField HeaderText="Add Sequence" >
                                        <ItemTemplate>
	                                        <a href='AddSequence.aspx?SystemID=<%#Eval("SystemID") %>&SystemName=<%#Eval("SystemName")%>'>Add Sequence</a>
	                                    </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" ForeColor="#FF3300" />
			                        </asp:TemplateField>
	                                <asp:TemplateField HeaderText="Routing" >
                                        <ItemTemplate>
                                             <asp:LinkButton  runat="server" CommandName="Routing" CommandArgument='<%#Eval("SystemID")%>' 
	                                            ID="lbnRouting"  Text="Routing"  />
	                                    </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" ForeColor="#FF3300" />
	                                </asp:TemplateField>
	                                <asp:TemplateField HeaderText="Preview" >
                                        <ItemTemplate>
	                                        <asp:LinkButton  runat="server" CommandName="Preview" CommandArgument='<%#Eval("SystemID")%>' 
	                                            ID="lbnAddMember"  Text="Preview"  />
	                                    </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
	                                </asp:TemplateField>
	                                <asp:TemplateField HeaderText="Detail" >
                                        <ItemTemplate>
	                                        <a href='SystemDetail.aspx?SystemID=<%#Eval("SystemID") %>'>Detail</a>
	                                    </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" ForeColor="#FF3300" />
			                        </asp:TemplateField>
			                        <asp:TemplateField HeaderText="Edit" >
                                        <ItemTemplate>
	                                        <a href='EditSystem.aspx?SystemID=<%#Eval("SystemID") %>'>Edit</a>
	                                    </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" ForeColor="#FF3300" />
			                        </asp:TemplateField>	                                	                                
                                    <asp:TemplateField HeaderText="Delete">
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
							<td style="height: 15px" colspan="2"></td>
						</tr>			
					</table>
				</td>
			</tr>	
		</table>
	</div>
	</form>
</body>
</html>
