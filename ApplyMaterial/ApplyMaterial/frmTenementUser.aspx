<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmTenementUser.aspx.cs" Inherits="ApplyMaterial.ApplyMaterial.frmTenementUser" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>租户账号</title>
</head>
<body>
     <form id="form1" runat="server">
    <div>
        <f:PageManager ID="PageManager1" runat="server"></f:PageManager>
        <f:Panel ID="Panel1" runat="server" BodyPadding="5px" ShowBorder="true" ShowHeader="true" Title="查询" EnableCollapse="True" Layout="Table">
            <Toolbars>
                <f:Toolbar ID="Toolbar2" runat="server">
                    <Items>
                        <f:Button ID="btnSearch" runat="server" Icon="SystemSearch" ToolTip="查询" Text="查询" OnClick="btnSearch_Click">
                        </f:Button>
                        <f:Button ID="btnReset" runat="server" Icon="Erase" ToolTip="重置" Text="重置" OnClick="btnReset_Click">
                        </f:Button>
                    </Items>
                </f:Toolbar>
            </Toolbars> 
            <Items>      
                <f:DropDownList ID="cmbEUserName" runat="server" Label="账号" TableRowspan="2" LabelAlign="Right" LabelWidth="40px"></f:DropDownList>
            </Items> 
        </f:Panel>
        <f:Panel ID="pData" runat="server" BodyPadding="5px">
            <Items>
                <f:Grid ID="dgvData" runat="server" AllowPaging="True" EnableCheckBoxSelect="True" AllowSorting="True" DataKeyNames="id" EnableMultiSelect="false" OnRowCommand="dgvData_RowCommand" EnableSummary="True" OnSort="dgvData_Sort">
                    <Toolbars>
                        <f:Toolbar ID="Toolbar1" runat="server">
                            <Items>
                                <f:Button ID="btnAdd" runat="server" Icon="Add" ToolTip="新增" Text="新增" OnClick="btnAdd_Click">
                                </f:Button>
                                <f:Button ID="btnLocked" runat="server" Icon="Lock" ToolTip="锁定" Text="锁定" OnClick="btnLocked_Click">
                                </f:Button>
                                <f:Button ID="btnUnlocked" runat="server" Icon="LockOpen" ToolTip="解锁" Text="解锁" OnClick="btnUnlocked_Click">
                                </f:Button>
                            </Items>
                        </f:Toolbar>
                    </Toolbars>
                    <Columns>
                        <f:RowNumberField EnablePagingNumber="true" />
                        <f:LinkButtonField TextAlign="Center" ConfirmText="删除选中行？" Icon="Delete" ConfirmTarget="Top" ColumnID="colDelete" HeaderText="&nbsp;" Width="40px" CommandName="Delete" />
                        <f:BoundField ID="BoundField2" runat="server" SortField="UserName" ColumnID="colUserName" DataField="UserName" HeaderText="账号" HtmlEncodeFormatString="True">
                        </f:BoundField>
                        <f:CheckBoxField ID="BoundField4" runat="server" SortField="IsLock" ColumnID="colIsLock" DataField="IsLock" HeaderText="锁定">
                        </f:CheckBoxField>
                    </Columns>
                </f:Grid>
                <f:Window ID="pEdit" runat="server" BodyPadding="5px" IsModal="true" Title="新增" Width="300px" Hidden="True" Layout="Table" TableConfigColumns="2">
                    <Toolbars>
                        <f:Toolbar ID="Toolbar3" runat="server">
                            <Items>
                                <f:Button ID="btnConfirm" runat="server" Icon="BulletTick" ToolTip="确定" Text="确定" OnClick="btnConfirm_Click">
                                </f:Button>
                            </Items>
                        </f:Toolbar>
                    </Toolbars>                    
                    <Items>
                        <f:TextBox ID="txtUserName" runat="server" Label="账号" LabelAlign="Right" LabelWidth="50px" Required="True" RequiredMessage="账号不能为空" ShowRedStar="True"></f:TextBox>
                    </Items>
                </f:Window> 
            </Items>
        </f:Panel> 
    </div>        
    </form>
</body>
</html>