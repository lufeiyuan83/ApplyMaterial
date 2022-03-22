<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmSystem.aspx.cs" Inherits="ApplyMaterial.ApplyMaterial.frmSystem" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>系统</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <f:PageManager ID="PageManager1" runat="server">
        </f:PageManager>
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
                <f:DropDownList ID="cmbESystemName" runat="server" Label="系统名称" LabelAlign="Right" LabelWidth="60px" TableRowspan="2"></f:DropDownList>
            </Items> 
        </f:Panel>
        <f:Panel ID="pData" runat="server" BodyPadding="5px">
            <Items>
                <f:Grid ID="dgvData" runat="server" AllowPaging="True" AllowSorting="True" DataKeyNames="id" EnableMultiSelect="false" OnRowCommand="dgvData_RowCommand" EnableSummary="True" EnableRowDoubleClickEvent="True" OnRowDoubleClick="dgvData_RowDoubleClick" OnSort="dgvData_Sort">
                    <Toolbars>
                        <f:Toolbar ID="Toolbar1" runat="server">
                            <Items>
                                <f:Button ID="btnAdd" runat="server" Icon="Add" ToolTip="新增" Text="新增" OnClick="btnAdd_Click">
                                </f:Button>
                            </Items>
                        </f:Toolbar>
                    </Toolbars>
                    <Columns>
                        <f:RowNumberField EnablePagingNumber="true" />
                        <f:LinkButtonField TextAlign="Center" ConfirmText="删除选中行？" Icon="Delete" ConfirmTarget="Top" ColumnID="colDelete" HeaderText="&nbsp;" Width="40px" CommandName="Delete" />
                        <f:BoundField ID="BoundField3" runat="server" SortField="SystemCode" ColumnID="colSystemCode" DataField="SystemCode" HeaderText="系统代码" HtmlEncodeFormatString="True">
                        </f:BoundField>
                        <f:BoundField ID="BoundField4" runat="server" SortField="SystemName" ColumnID="colSystemName" DataField="SystemName" HeaderText="系统名称" HtmlEncodeFormatString="True">
                        </f:BoundField>
                        <f:BoundField ID="BoundField5" runat="server" SortField="SystemDescription" ColumnID="colSystemDescription" DataField="SystemDescription" HeaderText="系统描述" HtmlEncodeFormatString="True">
                        </f:BoundField>
                        <f:BoundField ID="BoundField6" runat="server" SortField="CreateUserID" ColumnID="colCreateUserID" DataField="CreateUserID" HeaderText="创建人" HtmlEncodeFormatString="True">
                        </f:BoundField>
                        <f:BoundField ID="BoundField13" runat="server" SortField="CreateDateTime" ColumnID="colCreateDateTime" DataField="CreateDateTime" HeaderText="创建时间" HtmlEncodeFormatString="True" Width="150px">
                        </f:BoundField>
                        <f:BoundField ID="BoundField14" runat="server" SortField="Remark" ColumnID="colRemark" DataField="Remark" HeaderText="备注" HtmlEncodeFormatString="True">
                        </f:BoundField>
                    </Columns>
                </f:Grid>
                <f:Window ID="pEdit" runat="server" BodyPadding="5px" IsModal="true" Title="新增" Width="600px" Hidden="True" Layout="Table" TableConfigColumns="2">
                    <Toolbars>
                        <f:Toolbar ID="Toolbar3" runat="server">
                            <Items>
                                <f:Button ID="btnConfirm" runat="server" Icon="BulletTick" ToolTip="确定" Text="确定" OnClick="btnConfirm_Click">
                                </f:Button>
                            </Items>
                        </f:Toolbar>
                    </Toolbars>                    
                    <Items>
                        <f:TextBox ID="txtSystemCode" runat="server" Label="系统代码" LabelAlign="Right" Required="True" RequiredMessage="系统代码不能为空"></f:TextBox>
                        <f:TextBox ID="txtSystemName" runat="server" Label="系统名称" LabelAlign="Right" Required="True" RequiredMessage="系统名称不能为空"></f:TextBox>
                        <f:TextBox ID="txtSystemDescription" runat="server" Label="系统描述" LabelAlign="Right"></f:TextBox>
                        <f:TextBox ID="txtRemark" runat="server" Label="备注" LabelAlign="Right"></f:TextBox>
                    </Items>
                </f:Window>
            </Items>
        </f:Panel> 
    </div>        
    </form>
</body>
</html>