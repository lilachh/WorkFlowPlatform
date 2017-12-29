<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="top.aspx.cs" Inherits="WorkFlowPresentation.top" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html>
	<head>
		<title>top</title>
		<meta http-equiv="content-type" content="text/html; charset=gb2312"/>
		
		<link href="CSS/MainCSS.css" rel="stylesheet" type="text/css" />
        <script language="javascript1.2" type="text/javascript">
            function ShowTime()
            {
                document.getElementById('lbltime').innerHTML = new Date().toLocaleDateString();
                //alert(new Date().toLocaleDateString());
                
            }
		</script>
	    <style type="text/css">
            .style1
            {
                width: 921px;
            }
        </style>
	</head>
	<body style="background-color: #799ae1" >
		<form id="form1" runat="server">
		<table height="32" cellspacing="0" cellpadding="0" width="100%" border="0">
			<tr>
				<td style="border-bottom: #ffffff 0px solid" align="center" class="style1">
				   <b><font color="#ffffff"> Hi,&nbsp;<asp:Label ID="lblName" runat="server" 
                        Font-Size="Larger" ForeColor="Red"></asp:Label>
				     ,&nbsp;Welcome to AMD(Suzhou) Workflow Platform System</font></b>					
				</td>
				<td id="admintabletitlelink1" style="border-bottom: #ffffff 0px solid" align="center" class="style6">
				    <b>
				    <font color="#ffffff">
				    <label id="lbltime"></label>
                    <script>
                        //setinterval("document.getElementById('lbltime').innerhtml=new date().tolocalestring();", 1000);
                        setInterval("javascript:ShowTime();", 1000);
                    </script>
				    </font>
				    </b>
				</td>
				<td align="center">
				   
				</td>
			</tr>
		</table>
		<table cellspacing="0" cellpadding="0" width="100%" border="0">
			<tbody>
				<tr>
					<td style="border-bottom: #135093 0px solid" bgcolor="#135093" height="1"></td>
				</tr>
			</tbody>
		</table>
	    </form>
	</body>
</html>
