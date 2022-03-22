<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Role.aspx.cs" Inherits="ERP.PMS.Role" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title><asp:Literal runat="server" Text="<%$ Resources:Role.aspx,Title%>" /></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <f:PageManager ID="PageManager1" runat="server"></f:PageManager>
        <f:Panel ID="pData" runat="server" BodyPadding="5px" ShowHeader="false">
            <Items>
                <f:Grid ID="dgvData" runat="server" AllowPaging="True" AllowSorting="True" DataKeyNames="id" EnableMultiSelect="false" OnRowCommand="dgvData_RowCommand" EnableSummary="True" EnableRowDoubleClickEvent="True" OnRowDoubleClick="dgvData_RowDoubleClick" OnSort="dgvData_Sort" ShowHeader="false">
                    <Toolbars>
                        <f:Toolbar ID="Toolbar1" runat="server">
                            <Items>
                                <f:Button ID="btnAdd" runat="server" Icon="Add" ToolTip="<%$ Resources:Role.aspx,Add%>" Text="<%$ Resources:Role.aspx,Add%>" OnClick="btnAdd_Click"></f:Button>
                                <f:Button ID="btnGrant" runat="server" Icon="Add" ToolTip="<%$ Resources:Role.aspx,Grant%>" Text="<%$ Resources:Role.aspx,Grant%>" OnClick="btnGrant_Click"></f:Button>
                                <f:Button ID="btnAddAccount" runat="server" Icon="Add" ToolTip="<%$ Resources:Role.aspx,AddAccount%>" Text="<%$ Resources:Role.aspx,AddAccount%>" OnClick="btnAddAccount_Click"></f:Button>
                            </Items>
                        </f:Toolbar>
                    </Toolbars>
                    <Columns>
                        <f:RowNumberField EnablePagingNumber="true" />
                        <f:LinkButtonField TextAlign="Center" ConfirmText="<%$ Resources:Role.aspx,DeleteRecordQuestion%>" Icon="Delete" ConfirmTarget="Top" ColumnID="colDelete" HeaderText="&nbsp;" Width="40px" CommandName="Delete" />
                        <f:BoundField ID="BoundField3" runat="server" SortField="RoleCode" ColumnID="colRoleCode" DataField="RoleCode" HeaderText="<%$ Resources:Role.aspx,RoleCode%>" HtmlEncodeFormatString="True">
                        </f:BoundField>
                        <f:BoundField ID="BoundField4" runat="server" SortField="RoleName" ColumnID="colRoleName" DataField="RoleName" HeaderText="<%$ Resources:Role.aspx,RoleName%>" HtmlEncodeFormatString="True" Width="200px">
                        </f:BoundField>
                        <f:BoundField ID="BoundField5" runat="server" SortField="RoleDescription" ColumnID="colRoleDescription" DataField="RoleDescription" HeaderText="<%$ Resources:Role.aspx,RoleDescription%>" HtmlEncodeFormatString="True" Width="200px">
                        </f:BoundField>
                        <f:BoundField ID="BoundField6" runat="server" SortField="CreateUserID" ColumnID="colCreateUserID" DataField="CreateUserID" HeaderText="<%$ Resources:Role.aspx,CreateUserID%>" HtmlEncodeFormatString="True">
                        </f:BoundField>
                        <f:BoundField ID="BoundField13" runat="server" SortField="CreateDateTime" ColumnID="colCreateDateTime" DataField="CreateDateTime" HeaderText="<%$ Resources:Role.aspx,CreateDateTime%>" HtmlEncodeFormatString="True" Width="150px">
                        </f:BoundField>
                        <f:BoundField ID="BoundField14" runat="server" SortField="Remark" ColumnID="colRemark" DataField="Remark" HeaderText="<%$ Resources:Role.aspx,Remark%>" HtmlEncodeFormatString="True">
                        </f:BoundField>
                    </Columns>
                </f:Grid>
            </Items>
        </f:Panel>        
        <f:Window ID="pEdit" runat="server" BodyPadding="5px" IsModal="true" Width="600px" Hidden="True" Layout="Table" TableConfigColumns="2">
            <Toolbars>
                <f:Toolbar ID="Toolbar3" runat="server">
                    <Items>
                        <f:Button ID="btnConfirm" runat="server" Icon="BulletTick" ToolTip="<%$ Resources:Role.aspx,Confirm%>" Text="<%$ Resources:Role.aspx,Confirm%>" OnClick="btnConfirm_Click">
                        </f:Button>
                    </Items>
                </f:Toolbar>
            </Toolbars>                    
            <Items>
                <f:TextBox ID="txtRoleCode" runat="server" Label="<%$ Resources:Role.aspx,RoleCode%>" LabelAlign="Right" Required="True" RequiredMessage="<%$ Resources:Role.aspx,RoleCodeCanNotBlank%>"></f:TextBox>
                <f:TextBox ID="txtRoleName" runat="server" Label="<%$ Resources:Role.aspx,RoleName%>" LabelAlign="Right" Required="True" RequiredMessage="<%$ Resources:Role.aspx,RoleNameCanNotBlank%>"></f:TextBox>
                <f:TextBox ID="txtRoleDescription" runat="server" Label="<%$ Resources:Role.aspx,RoleDescription%>" LabelAlign="Right"></f:TextBox>
                <f:TextBox ID="txtRemark" runat="server" Label="<%$ Resources:Role.aspx,Remark%>" LabelAlign="Right"></f:TextBox>
            </Items>
        </f:Window>             
        <f:Window ID="wAddEmployee" runat="server" BodyPadding="5px" IsModal="true" Width="400px" Hidden="True" Layout="Table" TableConfigColumns="2">               
            <Items>  
                <f:Grid ID="dgvEmployee" runat="server" AllowPaging="True" AllowSorting="True" DataKeyNames="id" EnableMultiSelect="false" EnableSummary="True" EnableTextSelection="True" OnSort="DgvEmployee_Sort" Width="380px" OnRowCommand="DgvEmployee_RowCommand">
                <Toolbars>
                    <f:Toolbar ID="Toolbar2" runat="server">
                        <Items>
                            <f:TextBox ID="txtEmployeeID" runat="server" Label="<%$ Resources:Role.aspx,EmployeeID%>" LabelAlign="Right" LabelWidth="50px"></f:TextBox>
                            <f:Button ID="btnAddEmp" runat="server" Icon="Add" ToolTip="<%$ Resources:Role.aspx,AddEmp%>" Text="<%$ Resources:Role.aspx,AddEmp%>" OnClick="BtnAddEmp_Click">
                            </f:Button>
                        </Items>
                    </f:Toolbar>
                </Toolbars>   
                <Columns>
                    <f:RowNumberField EnablePagingNumber="true" />
                    <f:LinkButtonField TextAlign="Center" ConfirmText="<%$ Resources:Role.aspx,DeleteRecordQuestion%>" Icon="Delete" ConfirmTarget="Top" ColumnID="colDelete" HeaderText="&nbsp;" Width="40px" CommandName="Delete" />                        
                    <f:BoundField ID="BoundField1" runat="server" SortField="EmployeeID" ColumnID="colEmployeeID" DataField="EmployeeID" HeaderText="<%$ Resources:Role.aspx,EmployeeID%>" HtmlEncodeFormatString="True" >
                    </f:BoundField>
                </Columns>
            </f:Grid>                
            </Items>
        </f:Window>
    </div>        
    </form>
</body>
</html>