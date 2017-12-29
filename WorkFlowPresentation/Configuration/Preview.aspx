<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Preview.aspx.cs" Inherits="WorkFlowPresentation.Configuration.Preview" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" xmlns:v="urn:schemas-microsoft-com:vml" xmlns:o="urn:schemas-microsoft-com:office:office" >
<head id="Head1" runat="server">
    <title>RoleManagerment</title>
   <link href="../CSS/MainCSS.css" rel="stylesheet" type="text/css" />
 
<style type="text/css"> 
v\:* { 
BEHAVIOR: url(#default#VML) 
} 
o\:* { 
BEHAVIOR: url(#default#VML) 
} 
.shape { 
BEHAVIOR: url(#default#VML) 
} 
</style> 

</head>
<body>

    <form id="Form1" method="post" runat="server">
            <asp:ScriptManager ID="ScriptManager1" runat="server" />
		<div align="left" id="Line" runat="server">
		</div>
        <div align="center" >
		<table class="tableBorder" id="table2"  border="0" >
			<tr>
				<td id="tabletitlelink" style="FONT-WEIGHT: bold; FONT-SIZE:large; BACKGROUND-IMAGE: url(images/admin_bg_1.gif); COLOR: white; BACKGROUND-COLOR: #44aaaa; width: 100%;"
					align="center" colspan="6" height="25">Preview</td>
			</tr>
			<tr>			    			    
			    <td align="right" style= "font-size:large;position:relative; right:30px;">
			        <UCNav:Nav ID="Nav1" BackUrl="SystemManagement.aspx" LoginUrl="../Login.aspx" runat="server" />
			    </td>
			</tr>
		</table>
		</div>
		
		
	    <div style="background-color:#e4edf9" >
		<div id="treeview" runat="server" >
		</div>
		<br /><br /><br />
		<table  border="0" align="center">
		<tr>
							<td style="height: 10px; text-align:center">Current System: > <span id="divSystemName" runat="server" style="color:Red; font-size:22px;"></span></td>
						</tr>
		    <tr>
			    <td align="center">
			                <asp:GridView ID="dgvRouting" runat="server" 
                                    CellPadding ="3" HeaderStyle-HorizontalAlign="Left"
                                    AutoGenerateColumns="False" BackColor="#E7E7E7" BorderColor="#CCCCCC" 
                                    Width="770px" >
                                <FooterStyle CssClass="GridViewFooterStyle" />
                                <RowStyle CssClass="GridViewRowStyle" Wrap="False" />
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
                                     <asp:BoundField DataField="ConditionID"  HeaderText="ConditionID"  ReadOnly="true">
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ConditionDescription" HeaderText="ConditionDescription" >
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                </Columns>
                            </asp:GridView>
			         </td>
		        </tr>
		  </table>

		  </div>
	</form>
    <p>
&nbsp;</p>
</body>
</html>