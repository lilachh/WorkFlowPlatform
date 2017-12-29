<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditSystem.aspx.cs" Inherits="WorkFlowPresentation.Configuration.EditSystem" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>RoleManagerment</title>
   <link href="../CSS/MainCSS.css" rel="stylesheet" type="text/css" />
   
   <script language="javascript" type="text/javascript">
       function CheckBeforeAdd() {
           if (document.getElementById("txbSystemName").value == "") {
               alert("System name is mandatory!");
               document.getElementById("txbSystemName").focus();
               return false;
           }

           return true;
       }

       function Reset() {
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
					align="center" colspan="6" height="25">Edit System</td>
			</tr>
			<tr>			    
			    <td align="right" style= "font-size:large;position:relative; right:30px;">			        
			        <UCNav:Nav ID="Nav1" BackUrl="SystemManagement.aspx" LoginUrl="../Login.aspx" runat="server" />
			    </td>
			</tr>					
			<tr>
				<td class="forumRowHighlight" colspan="6" height="17" valign="middle" style="width: 100%">
				
				    <table border="0" id="table5" cellspacing="3" cellpadding="0">
						
						<tr>
							<td style="height: 10px">
							    <table>							    
							        <tr>
						                <td align="right" class="style2" >
						                    System ID:
						                </td>
							            <td align="left" class="style3">
                                            <asp:TextBox ID="txbSystemID" runat="server" Width="168px" Font-Bold="True" 
                                                ForeColor="Blue" ReadOnly="True"></asp:TextBox>
                                            
                                        </td>
						            </tr>
							        <tr>
						                <td align="right" class="style2" >
						                    System Name:
						                </td>
							            <td align="left" class="style3">
                                            <asp:TextBox ID="txbSystemName" runat="server" Width="168px" Font-Bold="True" 
                                                ForeColor="Blue"></asp:TextBox>
                                            
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                                ControlToValidate="txbSystemName" Display="Dynamic" 
                                                ErrorMessage="System Name is required!"></asp:RequiredFieldValidator>
                                            
                                        </td>
						            </tr>
						            <tr>
						                <td align="right" class="style2" >
						                    Big Url:
						                </td>
							            <td align="left" class="style3">
                                            <asp:TextBox ID="txbBUrl" runat="server" Width="454px"></asp:TextBox>
                                            </td>
						            </tr>
						            <tr>
						                <td align="right" class="style2" >
						                    Small Url:
						                </td>
							            <td align="left" class="style3">
                                            <asp:TextBox ID="txbSUrl" runat="server" Width="454px"></asp:TextBox>
                                            </td>
						            </tr>
						            <tr>
						                <td align="right" class="style2" >
						                    Apply Url:</td>
							            <td align="left" class="style3">
                                            <asp:TextBox ID="txbApplyUrl" runat="server" Width="453px"></asp:TextBox>
                                            </td>
						            </tr>
						            <tr>
						                <td align="right" class="style2" >
						                    Query Url:</td>
							            <td align="left" class="style3">
                                            <asp:TextBox ID="txbQueryUrl" runat="server" Width="452px"></asp:TextBox>
                                            </td>
						            </tr>
						            <tr>
						                <td align="right" class="style2" >
						                    Navigate Url: </td>
							            <td align="left" class="style3">
                                            <asp:TextBox ID="txbNavigateUrl" runat="server" Width="452px"></asp:TextBox>
                                            </td>
						            </tr>
						            <tr>
						                <td height="10px" colspan="2">&nbsp;</td>
						            </tr>
						            <tr>
							            <td style="height: 15px" align="center" colspan="2">
                                           <asp:Button ID="btnEdit" runat="server" Text="Edit" Width="83px" 
                                                onclick="btnEdit_Click"/>                                                                        
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                           <asp:Button ID="btnReturn" runat="server" Text="Return" Width="83px" 
                                                onclick="btnReturn_Click" />                                                                        
                                        </td>
						            </tr>
						            
							    
							    </table>
							</td>
						</tr>
						</table>
				</td>
			</tr>	
		</table>
	</div>
	</form>
</body>
</html>