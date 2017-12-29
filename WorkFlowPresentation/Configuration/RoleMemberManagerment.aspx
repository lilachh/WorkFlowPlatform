<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RoleMemberManagerment.aspx.cs" Inherits="WorkFlowPresentation.Configuration.RoleMemberManagerment" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>RoleMemberManagerment</title>
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


       
      function JudgeEmplid() 
      { 
         var len = document.getElementById("tbxEmplid").value; 
         if(len.length==6) 
         {
      
            document.all["btnHide"].click(); 
            //document.all("btn_submit").focus();
         }
         
         if(len.length<6) 
         {
            document.getElementById("lblName").innerText="";
         }  
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
					align="center" height="25">Role Management</td>
			</tr>	
            <tr>			    			    
			    <td align="right" style= "font-size:large;position:relative; right:30px;">			        
			        <UCNav:Nav ID="Nav1" BackUrl="RoleManagement.aspx" LoginUrl="../Login.aspx" runat="server" />
			    </td>
			</tr>
			<tr>
				<td class="forumRowHighlight" height="17" valign="middle" style="width: 100%">
				    <table border="0" width="100%" id="table5" cellspacing="3" cellpadding="0">
													
						<tr>
							<td style="height: 15px" align="center">
							<div id="divResult" runat="server" style="color:Red; font-size:22px;"></div>
							</td>
						</tr>
						<tr>
							<td style="height: 15px" align="center">
                                EmplID:&nbsp;
                                &nbsp;<asp:TextBox ID="tbxEmplID" runat="server"></asp:TextBox>
                                &nbsp;&nbsp;&nbsp;&nbsp;Name:
                                <asp:Label ID="lblName" runat="server" ForeColor="Red"></asp:Label>
                                &nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnAdd" runat="server" OnClientClick="return CheckBeforeAdd();" 
                                    Text="Add" Width="83px" onclick="btnAdd_Click" />
                            </td>
						</tr>
						<tr>
							<td style="height: 10px"></td>
						</tr>	
			            <tr>
			                <td align="center">
			                <asp:GridView ID="dgvApproverRole" runat="server" 
                                    CellPadding ="3" HeaderStyle-HorizontalAlign="Left"
                                    AutoGenerateColumns="False" BackColor="#E7E7E7" BorderColor="#CCCCCC"  
                                    AllowPaging="True" Width="770px" 
                                    onrowcommand="dgvApproverRole_RowCommand" 
                                    onrowdeleting="dgvApproverRole_RowDeleting" >
                                
                                <FooterStyle CssClass="GridViewFooterStyle" />
                                <RowStyle CssClass="GridViewRowStyle" />
                                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                                <PagerStyle CssClass="GridViewPagerStyle" />
                                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                                <HeaderStyle CssClass="GridViewHeaderStyle" HorizontalAlign="Left" />
                                
                                <Columns>
                                    <asp:BoundField DataField="RoleID" HeaderText="Role ID" >
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>    
                                                                 
                                    <asp:BoundField DataField="RoleDescription" HeaderText="Role Description" >
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                          <asp:BoundField DataField="EmplID" HeaderText="EmplID" >
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField> 
                                    <asp:BoundField DataField="Name" HeaderText="Name" >
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField> 
                                    <asp:TemplateField HeaderText="Delete">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbnDelete" runat="server" CommandName="Delete">Delete</asp:LinkButton>
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
		<asp:Button ID="btnHide" runat="server" Text="Hide" 
                 style="display:none;" onclick="btnHide_Click" />  
	</div>
	</form>
</body>
</html>