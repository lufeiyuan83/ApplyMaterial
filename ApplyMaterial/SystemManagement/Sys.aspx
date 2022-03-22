<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Sys.aspx.cs" Inherits="ERP.SystemManagement.Sys" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title><asp:Literal runat="server" Text="<%$ Resources:Sys.aspx,Title%>" /></title>
</head>
<body>
    <form id="frmSys" runat="server">
    <div>
        <f:PageManager ID="PageManager1" runat="server">
        </f:PageManager>
        <f:Panel ID="Panel1" runat="server" BodyPadding="5px" ShowBorder="true" ShowHeader="true" Title="<%$ Resources:Sys.aspx,Search%>" EnableCollapse="True" Layout="Table">
            <Toolbars>
                <f:Toolbar ID="Toolbar2" runat="server">
                    <Items>
                        <f:Button ID="btnSearch" runat="server" Icon="SystemSearch" ToolTip="<%$ Resources:Sys.aspx,Search%>" Text="<%$ Resources:Sys.aspx,Search%>" OnClick="btnSearch_Click">
                        </f:Button>
                        <f:Button ID="btnReset" runat="server" Icon="Erase" ToolTip="<%$ Resources:Sys.aspx,Reset%>" Text="<%$ Resources:Sys.aspx,Reset%>" OnClick="btnReset_Click">
                        </f:Button>
                    </Items>
                </f:Toolbar>
            </Toolbars> 
            <Items>      
                <f:DropDownList ID="cmbESystemName" runat="server" Label="<%$ Resources:Sys.aspx,System%>" LabelAlign="Right" LabelWidth="80px" TableRowspan="2"></f:DropDownList>
            </Items> 
        </f:Panel>
        <f:Panel ID="pData" runat="server" BodyPadding="5px" ShowHeader="false">
            <Items>
                <f:Grid ID="dgvData" runat="server" AllowPaging="True" AllowSorting="True" DataKeyNames="id" EnableMultiSelect="false" OnRowCommand="dgvData_RowCommand" EnableSummary="True" EnableRowDoubleClickEvent="True" OnRowDoubleClick="dgvData_RowDoubleClick" OnSort="dgvData_Sort" ShowHeader="false">
                    <Toolbars>
                        <f:Toolbar ID="Toolbar1" runat="server">
                            <Items>
                                <f:Button ID="btnAdd" runat="server" Icon="Add" ToolTip="<%$ Resources:Sys.aspx,Add%>" Text="<%$ Resources:Sys.aspx,Add%>" OnClick="btnAdd_Click">
                                </f:Button>
                            </Items>
                        </f:Toolbar>
                    </Toolbars>
                    <Columns>
                        <f:RowNumberField EnablePagingNumber="true" />
                        <f:LinkButtonField TextAlign="Center" ConfirmText="<%$ Resources:Sys.aspx,DeleteRecordQuestion%>" Icon="Delete" ConfirmTarget="Top" ColumnID="colDelete" HeaderText="&nbsp;" Width="40px" CommandName="Delete" />
                        <f:BoundField ID="BoundField3" runat="server" SortField="SystemCode" ColumnID="colSystemCode" DataField="SystemCode" HeaderText="<%$ Resources:Sys.aspx,SystemCode%>" HtmlEncodeFormatString="True">
                        </f:BoundField>
                        <f:BoundField ID="BoundField4" runat="server" SortField="SystemName" ColumnID="colSystemName" DataField="SystemName" HeaderText="<%$ Resources:Sys.aspx,SystemName%>" HtmlEncodeFormatString="True" Width="200px">
                        </f:BoundField>
                        <f:BoundField ID="BoundField5" runat="server" SortField="SystemDescription" ColumnID="colSystemDescription" DataField="SystemDescription" HeaderText="<%$ Resources:Sys.aspx,SystemDescription%>" HtmlEncodeFormatString="True" Width="200px">
                        </f:BoundField>
                        <f:BoundField ID="BoundField6" runat="server" SortField="CreateUserID" ColumnID="colCreateUserID" DataField="CreateUserID" HeaderText="<%$ Resources:Sys.aspx,CreateUserID%>" HtmlEncodeFormatString="True">
                        </f:BoundField>
                        <f:BoundField ID="BoundField13" runat="server" SortField="CreateDateTime" ColumnID="colCreateDateTime" DataField="CreateDateTime" HeaderText="<%$ Resources:Sys.aspx,CreateDateTime%>" HtmlEncodeFormatString="True" Width="150px">
                        </f:BoundField>
                        <f:BoundField ID="BoundField14" runat="server" SortField="Remark" ColumnID="colRemark" DataField="Remark" HeaderText="<%$ Resources:Sys.aspx,Remark%>" HtmlEncodeFormatString="True">
                        </f:BoundField>
                    </Columns>
                </f:Grid>
            </Items>
        </f:Panel>        
        <f:Window ID="pEdit" runat="server" BodyPadding="5px" IsModal="true" Width="600px" Hidden="True" Layout="Table" TableConfigColumns="2">
            <Toolbars>
                <f:Toolbar ID="Toolbar3" runat="server">
                    <Items>
                        <f:Button ID="btnConfirm" runat="server" Icon="BulletTick" ToolTip="<%$ Resources:Sys.aspx,Confirm%>" Text="<%$ Resources:Sys.aspx,Confirm%>" OnClick="btnConfirm_Click">
                        </f:Button>
                    </Items>
                </f:Toolbar>
            </Toolbars>                    
            <Items>
                <f:TextBox ID="txtSystemCode" runat="server" Label="<%$ Resources:Sys.aspx,SystemCode%>" LabelAlign="Right" Required="True" RequiredMessage="<%$ Resources:Sys.aspx,SystemCodeCanNotBlank%>"></f:TextBox>
                <f:TextBox ID="txtSystemName" runat="server" Label="<%$ Resources:Sys.aspx,SystemName%>" LabelAlign="Right" Required="True" RequiredMessage="<%$ Resources:Sys.aspx,SystemNameCanNotBlank%>"></f:TextBox>
                <f:TextBox ID="txtSystemDescription" runat="server" Label="<%$ Resources:Sys.aspx,SystemDescription%>" LabelAlign="Right"></f:TextBox>
                <f:TextBox ID="txtRemark" runat="server" Label="<%$ Resources:Sys.aspx,Remark%>" LabelAlign="Right"></f:TextBox>
            </Items>
        </f:Window>
    </div>        
    </form>
</body>
</html>