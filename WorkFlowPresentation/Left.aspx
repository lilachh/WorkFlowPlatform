<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Left.aspx.cs" Inherits="WorkFlowPresentation.Left" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html>
	<head>
		<title>Welcome to AMD(Suzhou) WorkFLow Platform System</title>
		<meta name="generator" content="microsoft visual studio .net 7.1"/>
		<meta name="code_language" content="c#"/>
		<meta name="vs_defaultclientscript" content="javascript"/>
		<meta name="vs_targetschema" content="http://schemas.microsoft.com/intellisense/ie5"/>
		
		<link href="CSS/MainCSS.css" rel="stylesheet" type="text/css" />
		
		<style type="text/css">
		    body { scrollbar-face-color: #799ae1; background: #799ae1; margin: 0px; font: 12px ËÎÌו; scrollbar-highlight-color: #799ae1; scrollbar-shadow-color: #799ae1; scrollbar-3dlight-color: #799ae1; scrollbar-arrow-color: #ffffff; scrollbar-track-color: #aabfec; scrollbar-darkshadow-color: #799ae1 }
	        
	        
	        img { border-right: 0px; border-top: 0px; vertical-align: bottom; border-left: 0px; border-bottom: 0px;}
	        a { font: 12px ËÎÌו; color: #000000; text-decoration: none }
	        a:hover { color: #428eff; text-decoration: underline }
	        .sec_menu { border-right: white 1px solid; background: #d6dff7; overflow: hidden; border-left: white 1px solid; border-bottom: white 1px solid }
	        .menu_title { FILTER: progid:DXImageTransform.Microsoft.AlphaImageLoader(enabled=true,src='Images/admin_left_5.gif',sizingMethod='scale')}
	        .menu_title span { font-weight: bold; left: 8px; color: #215dc6; position: relative; top: 2px }
	        .menu_title2 { }
	        .menu_title2 span { font-weight: bold; left: 8px; color: #428eff; position: relative; top: 2px }
		</style>
	
		<script language="javascript1.2" type="text/javascript">
		    function showsubmenu(sid, img) 
		    {
		        whichel = eval("submenu" + sid);
		        if (whichel.style.display == "none") {
		            eval("submenu" + sid + ".style.display=\"\";");
		            document.getElementById(img).src = 'Images/Collapse.gif'
		        }
		        else {
		            eval("submenu" + sid + ".style.display=\"none\";");
		            document.getElementById(img).src = 'Images/Expand.gif';
		        }
		    }
		</script>
	</head>
	<body>
		<form id="form1" method="post" runat="server">
		    <div style="text-align:left">
			<table width="100%" cellpadding="0" cellspacing="0" border="0" style="text-align:left;">				
				
				<tr>
				    <td align="center" height="25" class="menu_title" onmouseover="this.classname='menu_title2';" onmouseout="this.classname='menu_title';"
				    style="FILTER: progid:DXImageTransform.Microsoft.AlphaImageLoader(enabled=true,src='Images/titlebar.jpg',sizingMethod='scale')">
						<span ><a href="main.html" target="main" style="font-weight: bold; font-size: 16px; left: 8px; color: #215dc6; position: relative; top: 2px">Home</a>&nbsp;&nbsp;|<asp:LinkButton ID="lbExit" runat="server" Text="Exit"  style="font-weight: bold; font-size: 16px; left: 8px; color: #215dc6; position: relative; top: 2px"
                            onclick="lbExit_Click" ></asp:LinkButton>
						</span>
					</td>
				</tr>
				<tr>
				    <td height="30px" align="center" style="FILTER: progid:DXImageTransform.Microsoft.AlphaImageLoader(enabled=true,src='Images/titlebar.jpg',sizingMethod='scale')">
						<span style="font-weight: bold; font-size: 16px; left: 8px; color: #215dc6; position: relative; top: 2px">Menu List</span>
					</td>
				</tr>
				
				<tr>
				    <td height="25" class="menu_title" onmouseover="this.classname='menu_title2';" onmouseout="this.classname='menu_title';"
						id="td3" onclick="showsubmenu(3, 'imgExpand3')">
						<img id="imgExpand3" height="15" alt="No picture found" src="Images/Collapse.gif" width="15"/><span>Apply</span>
					</td>
				</tr>				
				<tr>
				    <td id='submenu3'>
				        <asp:GridView ID="dgvApplylist" runat="server" AutoGenerateColumns="false" ShowHeader="false" Width="100%">
                            <RowStyle CssClass="GridViewRowStyle" />
                            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                    <img alt="No picture found" height="15" src="Images/bullet.gif" width="20" border="0" />
							        <a href='<%#Eval("ApplyUrl") %>?EMPLID=<%=Session["EMPLID"] %>&SystemID=<%#Eval("SystemID") %>' target="main">
							        <%#Eval("SystemName") %></a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
				    </td>
				</tr>
				
				<tr>
				    <td height="25" class="menu_title" onmouseover="this.classname='menu_title2';" onmouseout="this.classname='menu_title';"
						id="td2" onclick="showsubmenu(2, 'imgExpand2')">
						<img id="imgExpand2" height="15" alt="No picture found" src="Images/Collapse.gif" width="15"/><span>Query</span>
					</td>
				</tr>				
				<tr>
				    <td id='submenu2'>
				        <asp:GridView ID="dgvQuerylist" runat="server" AutoGenerateColumns="false" ShowHeader="false" Width="100%">
                            <RowStyle CssClass="GridViewRowStyle" />
                            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                    <img alt="No picture found" height="15" src="Images/bullet.gif" width="20" border="0" />
							        <a href='Presentation/RequestList.aspx?SystemID=<%#Eval("SystemID") %>&SystemName=<%#Eval("SystemName") %>' target="main">
							        <%#Eval("SystemName") %></a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
				    </td>
				</tr>								
				
				<tr>
				    <td height="25" class="menu_title" onmouseover="this.classname='menu_title2';" onmouseout="this.classname='menu_title';"
						id="td1" onclick="showsubmenu(1, 'imgExpand1')">
						<img id="imgExpand1" height="15" alt="No picture found" src="Images/Collapse.gif" /><span>Pending</span>
					</td>
				</tr>				
				<tr>
				    <td id='submenu1'>
				        <asp:GridView ID="dgvPendinglist" runat="server" AutoGenerateColumns="false" ShowHeader="false" Width="100%">
                            <RowStyle CssClass="GridViewRowStyle" />
                            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                    <img alt="No picture found" height="15" src="Images/bullet.gif" width="20" border="0" />
							        <a href='Presentation/Pendinglist.aspx?SystemID=<%#Eval("SystemID") %>&SystemName=<%#Eval("SystemName") %>' target="main">
							        <%#Eval("SystemName") %></a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
				    </td>
				</tr>
				<tr>
					<td height="25" class="menu_title" onmouseover="this.classname='menu_title2';" onmouseout="this.classname='menu_title';"
						id="td7" onclick="showsubmenu(4, 'imgExpand4')">
						<img id="imgExpand4" height="15" alt="No picture found" src="Images/Collapse.gif" width="15"/><span>Admin Config</span></td>
				</tr>
				<tr>
					<td id='submenu4'><div class="sec_menu" style="width:100%">
					    <table cellpadding="0" cellspacing="0" align="center" width="100%">
							<tr class="GridViewRowStyle">
								<td height="20"><img align="middle"  height="15" src="Images/bullet.gif" width="20" border="0">
								    <a href="Configuration/RoleManagement.aspx" target="main">Role Management</a>
                                </td>
							</tr>

							<tr class="GridViewAlternatingRowStyle">
								<td height="20"><img align="middle"  height="15" src="Images/bullet.gif" width="20" border="0">
								    <a href="Configuration/SystemManagement.aspx" target="main">System Management</a>
                                </td>
							</tr>
						</table>
						</div>
					</td>
				</tr>
							
			</table>
			</div>
		</form>
	</body>
</html>
