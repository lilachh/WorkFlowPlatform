<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BatchApproval.aspx.cs" Inherits="WorkFlowPresentation.Presentation.BatchApproval" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Pending List</title>
    <link href="../CSS/MainCSS.css" rel="stylesheet" type="text/css" /> 
 
    <script language="javascript" type="text/javascript" >
    document.domain = "amd.com";
    function Approve(index,flag)
    {
         
        window.frames[index].document.getElementById('btnApprove').click();
        if(flag==false)
        {
          
            window.location.href=window.location.href;
        }
        else
        {
           
        }
    }
    
    function PreApprove(index,flag)
    {
       
        setTimeout('Approve('+index+','+flag+')',500);
    }
    
    function Reject(index,flag)
    {
        window.frames[index].document.getElementById('btnReject').click();
        if(flag==false)
        {
            
            window.location.href=window.location.href;
        }
        else
        {

        }
    }
    
    function PreReject(index,flag)
    {
        setTimeout('Reject('+index+','+flag+')',500);
    }    
    

    
    function CBXApproveClick(RowIndex)
    {
        // dgvPendinglist_ctl03_cbxApprove
        var cbxID = 'dgvPendinglist_ctl';
        if (RowIndex <= 7)
            cbxID = cbxID + '0' + (RowIndex + 2);
        else
            cbxID = cbxID + (RowIndex + 2);
        var cbxReject = document.getElementById(cbxID + "_cbxReject");
        var cbxApprove = document.getElementById(cbxID + "_cbxApprove");
        
        if (cbxReject.checked)
        {
            alert("You have chosen Reject! ");
            cbxApprove.checked=false;
        }
    }
    
   
    
    function CBXRejectClick(RowIndex)
    {
        // dgvPendinglist_ctl03_cbxApprove
        var cbxID = 'dgvPendinglist_ctl';
        if (RowIndex <= 7)
            cbxID = cbxID + '0' + (RowIndex + 2);
        else
            cbxID = cbxID + (RowIndex + 2);
            
         var cbxReject = document.getElementById(cbxID + "_cbxReject");
        var cbxApprove = document.getElementById(cbxID + "_cbxApprove");
        
        if (cbxApprove.checked)
        {
            alert("You have chosen Approve! ");
            cbxReject.checked=false;
        }
    }
    
    function radioApprove(rowCount)
    {
      for (var RowIndex = 0; RowIndex < rowCount; RowIndex++)
      {
      // dgvPendinglist_ctl03_cbxApprove
        var cbxID = 'dgvPendinglist_ctl';
        if (RowIndex <= 7)
            cbxID = cbxID + '0' + (RowIndex + 2);
        else
            cbxID = cbxID + (RowIndex + 2);
            
        var cbxApprove = document.getElementById(cbxID + "_cbxApprove");
        var cbxReject = document.getElementById(cbxID + "_cbxReject");
        cbxApprove.checked = true;
        cbxReject.checked = false;
      }
    }
    
    
    function radioReject(rowCount)
    {
      for (var RowIndex = 0; RowIndex < rowCount; RowIndex++)
      {
      // dgvPendinglist_ctl03_cbxApprove
        var cbxID = 'dgvPendinglist_ctl';
        if (RowIndex <= 7)
            cbxID = cbxID + '0' + (RowIndex + 2);
        else
            cbxID = cbxID + (RowIndex + 2);
            
        var cbxApprove = document.getElementById(cbxID + "_cbxApprove");
        var cbxReject = document.getElementById(cbxID + "_cbxReject");
        cbxApprove.checked = false;
        cbxReject.checked = true;
      }
    }

 </script>
<script language="javascript" type="text/javascript" src="../DatePicker/WdatePicker.js"></script> 


    <style type="text/css">
        .style1
        {
            width: 32px;
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
			        <UCNav:Nav ID="UCNav1" BackUrl="../main.html" runat="server" />
			    </td>
			</tr>
			<tr>
				<td class="forumRowHighlight" colspan="6" height="17" valign="middle" style="width: 100%" headerstyle-width="30">
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
			                <td align="center">
			                <asp:GridView ID="dgvPendinglist" runat="server" 
                                    CellPadding ="3" HeaderStyle-HorizontalAlign="Left" 
                                    AutoGenerateColumns="False" BackColor="#E7E7E7" BorderColor="#CCCCCC" 
                                    Width="950px" 
                                    onrowdatabound="dgvPendinglist_RowDataBound">
                                
                                <FooterStyle CssClass="GridViewFooterStyle" />
                                <RowStyle CssClass="GridViewRowStyle"  HorizontalAlign="Left"/>
                                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                                <PagerStyle CssClass="GridViewPagerStyle" />
                                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                                <HeaderStyle CssClass="GridViewHeaderStyle" HorizontalAlign="Left" />
                                <Columns>
                                        <asp:TemplateField HeaderText="Approve" ItemStyle-Width="25px" HeaderStyle-Width="25px">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="cbxApprove" runat="server" />
                                            </ItemTemplate>

                                        <HeaderStyle Width="25px"></HeaderStyle>
                                        <ItemStyle Width="25px"></ItemStyle>
                                        </asp:TemplateField>
                           
                                        <asp:TemplateField HeaderText="Reject" ItemStyle-Width="25px" HeaderStyle-Width="25px">
                                             <ItemTemplate>
                                                <asp:CheckBox ID="cbxReject" runat="server" />
                                             </ItemTemplate><HeaderStyle Width="25px"></HeaderStyle>
                                            <ItemStyle Width="25px"></ItemStyle>
                                        </asp:TemplateField>
                              
                                        <asp:BoundField DataField="DocumentID" HeaderText="DocumentID"  >
                                          <ItemStyle CssClass="hidden" />
                                        <HeaderStyle CssClass="hidden" />
                                        <FooterStyle CssClass="hidden" />
                                         </asp:BoundField>
                              
                                        <asp:BoundField DataField="SequenceID"  HeaderText="SequenceID" >
                                        <ItemStyle CssClass="hidden" />
                                        <HeaderStyle CssClass="hidden" />
                                        <FooterStyle CssClass="hidden" />
                                        </asp:BoundField>
                                        
                                        <asp:BoundField DataField="ApproveNeededID"  HeaderText="ApproveNeededID" >
                                        <ItemStyle CssClass="hidden" />
                                        <HeaderStyle CssClass="hidden" />
                                        <FooterStyle CssClass="hidden" />
                                        </asp:BoundField>
                              
                                    <asp:TemplateField HeaderText="Detail Information" >
                                    <ItemTemplate>
                                        <iframe id="iframe" name="iframe" runat="server"   width="800px" scrolling="no" height="100px"></iframe>
                                    </ItemTemplate>
                                    </asp:TemplateField>           
                                </Columns>
                            </asp:GridView>
			                    <br />
			                </td>
			            </tr>			            
			 
						</tr>		
					</table>
           
					<table Width="100%">
			                    <tr>
			                    <td align="left">
                            <asp:RadioButton ID="RadioButton1" runat="server" GroupName="batchSelect" 
                              Text="Approve All" />
                            <asp:RadioButton ID="RadioButton2" runat="server" GroupName="batchSelect" 
                              Text="Reject All" />
                                   </td>
			                    </tr>
                                </table>
			                    <table Width="100%">
			                    <tr>
			                    <td align="center">
                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" onclick="btnSubmit_Click" 
                                        /></td>
			                    </tr>
                                </table>

				</td>
			</tr>	
		</table>
	</div>

	</form>

</body>
</html>

