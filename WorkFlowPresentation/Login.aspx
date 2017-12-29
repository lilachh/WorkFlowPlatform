<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WorkFlowPresentation.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<html>
	<head>
		<title>Welcome to AMD(Suzhou) Work Flow System</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio 7.0"/>
		<meta name="CODE_LANGUAGE" content="C#"/>
		<meta name="vs_defaultClientScript" content="JavaScript"/>
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5"/>
		
		<script language="javascript" type="text/javascript">		
		
		function check()
		{
		    if(document.getElementById("txbOpename").value=="")
		    {
		        alert("Please enter your user name!");
		        document.getElementById("txbOpename").focus();
		        return false;
		    }
		    
		    if(document.getElementById("txbPwd").value=="")
		    {
		        alert("Please enter your password!");
		        document.getElementById("txbPwd").focus();
		        return false;
		    }
		    
		    return true;	
		}
	
		function LoadPage()
		{
		    if ( document.getElementById("txbOpename").value == "")
		        document.getElementById("txbOpename").focus();
		    else
		         document.getElementById("txbPwd").focus();
		         
		}
		
		
		</script>
	    <style type="text/css">
            .style1
            {
                height: 150px;
                width: 340px;
            }
        </style>
	</head>
	<body onload="javascript:LoadPage();" style="background-color: #799ae1">
	<form action="" runat="server" id="login">
	  <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                        
       <div style="vertical-align:middle">
		<table style="vertical-align:middle"  cellspacing="0" cellpadding="0" width="805px;" align="center" bgcolor="#ffffff" style="height: 597px">
			<tbody>
			    <tr>
				    <td bgcolor="#f7f3f7" height="13"></td>
			    </tr>
				<tr>
					<td valign="top">
						<table cellspacing="0" cellpadding="0" width="100%" border="0">
							<tbody>
								<tr>
									<td bgcolor="#f7f3f7" height="13" background="images/bg_line.gif"></td>
								</tr>
								<tr>
                                    <td width="150" height="61">
                                        <a href="http://szintranet/">
                                            <img height="61" src="images/amd_suzhou.gif" width="150" border="0"/></a>
                                     </td>       
                                     <td width="480" height="61">        
                                        <a href="Index.aspx">
                                            <img height="60" src="images/work2.jpg" width="480" border="0"/></a>
                                     </td>    
                                     <td width="129" height="61">   
                                        <a href="http://amdonline/">
                                            <img height="60" src="images/amd_adv.gif" width="129" border="0"/></a>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="782" bgcolor="#709a00" colspan="3" height="24">
                                        <strong><font color="#ffffff"><cite>
                                            <marquee onmouseover="stop()" onmouseout="start()" scrollamount="1" scrolldelay="15">Welcome To Workflow Platform......</marquee>
                                        </cite></font></strong>
                                    </td>
                                </tr>
							</tbody>
						</table>
						<br/>
						<br />
						<br />
                      
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <Triggers>
                        </Triggers>
                        <ContentTemplate> 
						    <table height="150" cellspacing="0" cellpadding="0" width="100%" align="center" bgcolor="#fffbff">
							    <tbody>
								    <tr>
									    <td valign="middle" align="right" bgcolor="#fffbff" class="style1"><img height="71" src="images/pic_login.gif" width="154"></td>
									    <td valign="middle" bgcolor="#fffbff" style="height: 150px">
										    <table cellspacing="0" cellpadding="0" border="0">
											    <tr>
												    <td  valign="middle">						
												        <asp:textbox id="txbopename" runat="server" 
                                                            ontextchanged="txbopename_TextChanged" AutoPostBack="True" 
                                                            TabIndex="1"></asp:textbox>&nbsp;&nbsp;
												    </td>
												    <td rowspan="3" valign="middle"><asp:imagebutton id="cmdimgok" runat="server" 
                                                            imageurl="images/submit.gif" onclientclick="return check();"  tabindex="3"
                                                            onclick="cmdimgok_Click"></asp:imagebutton></td>
											    </tr>
											    <tr>
												    <td style="width: 115px; height: 1px;"></td>
											    </tr>
											    <tr>
												    <td valign="middle">
												        <asp:textbox id="txbpwd" runat="server" tabindex="2" textmode="password" 
                                                            width="148px"></asp:textbox></td>
											    </tr>
											    <tr>
											        <td colspan="2" align="left"><asp:Label ID="loginStatus" runat="server" 
                                                            Font-Bold="True" ForeColor="Red"></asp:Label></td>
											    </tr>
										    </table>
									    </td>									    
								    </tr>
							    </tbody>
						    </table>
						    </ContentTemplate>
						    </asp:UpdatePanel>
					
					</td>
				</tr>				
                <tr>
                    <td align="center" height="17">
                        <img height="15" src="images/wire.gif" width="192" border="0" alt="no picture found" />
                    </td>
                </tr>
                <tr>
                    <td align="center" height="17">
                        <font color="#008000" size="4"><b><a href="http://suzssdgprod01/newWorkFLow Platform/doc/305-WorkFLow Platform%20policy%20.doc">
                                User Guide</a> </b></font>
                    </td>
                </tr>                
                <tr>
                    <td align="center" height="17">
                        <a href="mailto:Yun-Song.Zhang@amd.com?subject=comments on WorkFLow Platform system">
                                    <font face="comic sans ms" color="#996600" size="3">comments &amp; questions mail to Zhang Yunsong</font></a> 
                    </td>
                </tr>
                <tr>
                    <td align="center" height="17">
                        <font face="comic sans ms" color="#996600" size="3">copyright 2009 amd(suzhou)ltd.</font>
                    </td>
                </tr>
                
                <tr>
                    <td width="782" bgcolor="#709a00" height="13">
                        <strong><font color="#ffffff"><cite>
                            <marquee onmouseover="stop()" onmouseout="start()" scrollamount="1" scrolldelay="15">Workflow Platform is an important indicator of paperless....</marquee>
                        </cite></font></strong>
                    </td>
                </tr>
                <tr>
					<td bgcolor="#f7f3f7" height="13"></td>
				</tr>
			</tbody>
		</table>	
		</div>
	</form>
	</body>
</html>
