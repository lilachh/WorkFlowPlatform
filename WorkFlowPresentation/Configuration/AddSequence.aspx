<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddSequence.aspx.cs" Inherits="WorkFlowPresentation.Configuration.AddSequence" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>RoleManagerment</title>
   <link href="../CSS/MainCSS.css" rel="stylesheet" type="text/css" />
   
   <script language="javascript" type="text/javascript">
       function CheckBeforeAddSequence()
       {
           if (document.getElementById("txbSequenceID").value == "")
           {
               alert("Sequence ID is mandatory!");
               document.getElementById("txbSequenceID").focus();
               return false;
           }
           
           if (document.getElementById("txbSequenceDescription").value == "")
           {
               alert("Sequence description is mandatory!");
               document.getElementById("txbSequenceDescription").focus();
               return false;
           }

           if (document.getElementById("lbxRole").length == 0)
           {
               alert("Select at least one approver role!");
               document.getElementById("ddlRole").focus();
               return false;           
           }

//           if (document.getElementById("cbxLastSequence").checked == false && document.getElementById("cbxHasSubsequence").checked == false)
//           {
//               alert("Select between Last Sequence and Has SubSequence please!");
//               return false;
//           }

//           alert(document.getElementById("lbxRole").size);
//           if (document.getElementById("lbxRole").size == 0)
//           {
//               alert("Select a role first!");
//               document.getElementById("ddlRole").focus();
//               return false;
//           }
           
           return true;
       }

       function Reset()
       {
           document.getElementById("txbSequenceID").value = "";
           document.getElementById("txbSequenceDescription").value = "";
           document.getElementById("cbxLastSequence").checked = false;
           document.getElementById("cbxHasSubsequence").checked = false;
           document.getElementById("cbxFromRequestor").checked = false;
           document.getElementById("cbxHasSubsequence").disabled = false;
           document.getElementById("cbxFromRequestor").disabled = false;
           document.getElementById("ddlRole").selectedIndex = 0;
           document.getElementById("cbxSequenceNeedall").checked = false;
           document.getElementById("divResult").innerHTML = "";

           var l = document.getElementById("lbxRole").length;
           for (var i = 0; i <= l; i++)
           {
               document.getElementById("lbxRole").remove(0);
           }
           
           return false;
           
       }

       function ClickLastSequence()
       {
           if (document.getElementById("cbxLastSequence").checked)
           {
               document.getElementById("cbxHasSubsequence").checked = false;
               document.getElementById("cbxFromRequestor").checked = false;
               document.getElementById("cbxHasSubsequence").disabled = true;
               document.getElementById("cbxFromRequestor").disabled = true;

           }
           else
           {
               document.getElementById("cbxHasSubsequence").disabled = false;
               document.getElementById("cbxFromRequestor").disabled = false;
           }
       }

       function ClickFromQuestor()
       {
           if (document.getElementById("cbxFromRequestor").checked && document.getElementById("cbxHasSubsequence").checked == false)
           {
               document.getElementById("cbxFromRequestor").checked = false;
               alert("Select Has Subsequence first!");
           }                      
       }

       function ClientClickSelectRole()
       {           
           if (document.getElementById("ddlRole").selectedIndex == 0)
           {
               alert("Select a role first!");
               document.getElementById("ddlRole").focus();
               return false;
           }

           return true;
       }
   </script>
 
</head>
<body>
    <form id="Form1" method="post" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
		<div align="center">
        <br />
		<table class="tableBorder" id="table2" height="151" cellSpacing="1" cellPadding="3" border="0" style=" HEIGHT: 129px">
			<tr>			    
			    <td align="right" style= "font-size:large;position:relative; right:30px;">			        
			        <UCNav:Nav ID="Nav1" BackUrl="SystemManagement.aspx" LoginUrl="../Login.aspx" runat="server" />
			    </td>
			</tr>
			<tr>
				<td class="forumRowHighlight" colspan="6" height="17" valign="middle" style="width: 100%">
				    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <Triggers>
                            <asp:PostBackTrigger ControlID="rbnRequestor" />
                            <asp:PostBackTrigger ControlID="rbnApprover" />
                            <asp:PostBackTrigger ControlID="btnAddSequence" />
                        </Triggers>
                         <ContentTemplate>                                                                                  
                            <table>
                                <tr>
						            <td align="center" colspan="2"><div id="divResult" runat="server" style="color:Red; font-size:22px;"></div></td>
					            </tr>
                                <tr>
                                    <td align="right">
                                        Begin with branch:
                                    </td>
                                    <td align="left">
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:RadioButton ID="rbnRequestor" runat="server" AutoPostBack="True" 
                                            Font-Bold="True" Font-Size="12pt" ForeColor="Red" GroupName="System" 
                                            oncheckedchanged="rbnRequestor_CheckedChanged" Text="Yes" />
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:RadioButton ID="rbnApprover" runat="server" AutoPostBack="True" 
                                            Font-Bold="True" Font-Size="12pt" ForeColor="Red" GroupName="System" 
                                            oncheckedchanged="rbnApprover_CheckedChanged" Text="No" />
                                    </td>
                                </tr>
                                <tr>
						            <td align="right" colspan="2"><hr /></td>
                                    
					            </tr>
                                <tr>
                                    <td align="right">
                                        Sequence ID:&nbsp;</td>
                                    <td align="left">
                                        <asp:TextBox ID="txbSequenceID" runat="server" Width="192px"></asp:TextBox>
                                        <asp:CompareValidator ID="CompareValidator2" runat="server" 
                                            ControlToValidate="txbSequenceID" Display="Dynamic" 
                                            ErrorMessage="Sequence id must be an integer!" Operator="DataTypeCheck" 
                                            Type="Integer"></asp:CompareValidator>
                                    </td>
                                </tr>
                                <tr>
						            <td align="right">Sequence Description:&nbsp;</td>
                                    <td align="left">
                                        <asp:TextBox ID="txbSequenceDescription" runat="server" Width="193px"></asp:TextBox>
                                    </td>
					            </tr><tr>
					                <td align="right">Last sequence:&nbsp;</td>
						            <td align="left">
                                        <asp:CheckBox ID="cbxLastSequence" runat="server" 
                                            onclick="ClickLastSequence();" />
                                    </td>
					            </tr><tr>
					                <td align="right">Has subsequence:&nbsp;</td>
						            <td align="left">
                                        <asp:CheckBox ID="cbxHasSubsequence" runat="server" />
                                    </td>
					            </tr>
                                <tr>
                                    <td align="right">
                                        From requestor:&nbsp;</td>
                                    <td align="left">
                                        <asp:CheckBox ID="cbxFromRequestor" runat="server" 
                                            onclick="ClickFromQuestor()"/></td>
                                </tr>
                                <tr>
						            <td align="right" valign="middle">Approver Role: &nbsp;</td>
                                    <td align="left" style="vertical-align:middle">
                                    <table>
                                    <tr>
                                    <td><asp:DropDownList ID="ddlRole" runat="server" Width="155px">
                                        </asp:DropDownList></td>
                                    <td><asp:Button ID="btnSelectRole" runat="server" onclick="btnSelectRole_Click" 
                                            OnClientClick="return ClientClickSelectRole();" Text="Add" Width="62px" /></td>
                                    <td><asp:ListBox ID="lbxRole" runat="server" SelectionMode="Multiple" Width="111px">
                                        </asp:ListBox></td>
                                    </tr>                                                
                                    </table>                                                    
                                    </td>
					            </tr><tr>
						            <td align="right">Sequence need all:&nbsp;</td>
                                    <td align="left">
                                        <asp:CheckBox ID="cbxSequenceNeedall" runat="server" />
                                    </td>
					            </tr>
					            <tr>
						            <td align="right">URL:&nbsp;</td>
                                    <td align="left">
                                       <asp:TextBox ID="txbUrl" runat="server" Width="193px"></asp:TextBox>
                                    </td>
					            </tr>
					            <tr>
					                <td align="right">Actor:&nbsp;</td>
						            <td align="left">
                                        <asp:CheckBox ID="cbxActor" runat="server" />
                                    </td>
					            </tr>
                                <tr>
                                    <td colspan="2" style="height: 15px">
                                    </td>
                                </tr>
                                <tr>                                               
                                    <td align="center" colspan="2">
                                        <asp:Button ID="btnAddSequence" runat="server" onclick="btnAddSequence_Click" 
                                            OnClientClick="return CheckBeforeAddSequence();" Text="Add Sequence" 
                                            Width="102px" />
                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="btnReset" runat="server" OnClientClick="javascript:return Reset();" Text="Reset" Width="102px" />                                                    
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="height: 10px"><hr />
                                    </td>
                                </tr>
                            </table>
                         </ContentTemplate>                               
                     </asp:UpdatePanel>
				    
				    <table border="0" width="100%" id="table5" cellspacing="3" cellpadding="0">
						<tr>
							<td style="height: 15px" align="center">
                                 &nbsp;</td>
						</tr>
						
						<tr>
							<td style="height: 10px; text-align:center">Current System: > <span id="divSystemName" runat="server" style="color:Red; font-size:22px;"></span></td>
						</tr>	
			            <tr>
			                <td align="center">
			                <asp:GridView ID="dgvSequences" runat="server" 
                                    CellPadding ="3" HeaderStyle-HorizontalAlign="Left" DataKeyNames="SequenceID"
                                    AutoGenerateColumns="False" BackColor="#E7E7E7" BorderColor="#CCCCCC" 
                                     onrowdeleting="dgvSequences_RowDeleting" 
                                    onrowcancelingedit="dgvSequences_RowCancelingEdit" 
                                    onrowediting="dgvSequences_RowEditing" 
                                    onrowupdating="dgvSequences_RowUpdating" >
                                
                                <FooterStyle CssClass="GridViewFooterStyle" />
                                <RowStyle CssClass="GridViewRowStyle" />
                                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                                <PagerStyle CssClass="GridViewPagerStyle" />
                                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                                <HeaderStyle CssClass="GridViewHeaderStyle" HorizontalAlign="Left" />
                                
                                <Columns>
                                    <asp:BoundField DataField="SequenceID" ReadOnly="true" HeaderText="SequenceID" >
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="SequenceDescription" ReadOnly="true" HeaderText="Description" >
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="RoleDescription" ReadOnly="true" HeaderText="Role" >
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="IsLastSequence" ReadOnly="true" HeaderText="Last Sequence" >
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="NextSequenceID" ReadOnly="true" HeaderText="Next Sequence" >
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="HasSubSequence" ReadOnly="true" HeaderText="Has SubSequence" >
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ValueFrom" ReadOnly="true" HeaderText="Value From" >
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="URL" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <%#Eval("URL")%>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txbURL" runat="server" Text='<%#Eval("URL") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>                                                
                                    </asp:TemplateField>
                                    <asp:CommandField HeaderText="Edit" ShowEditButton="true" ItemStyle-HorizontalAlign="Left" />
                                    <asp:CommandField HeaderText="Delete" ShowDeleteButton="true" ItemStyle-HorizontalAlign="Left" />
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

