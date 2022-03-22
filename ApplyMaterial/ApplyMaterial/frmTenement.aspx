<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmTenement.aspx.cs" Inherits="ApplyMaterial.ApplyMaterial.frmTenement" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>租户信息</title>
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
                <f:DropDownList ID="cmbETenementName" runat="server" Label="租户" LabelAlign="Right" TableRowspan="2" EnableEdit="True" LabelWidth="40px"></f:DropDownList>
            </Items> 
        </f:Panel>
        <f:Panel ID="pData" runat="server" BodyPadding="5px">
            <Items>
                <f:Grid ID="dgvData" runat="server" AllowPaging="True" EnableCheckBoxSelect="True" AllowSorting="True" DataKeyNames="id" EnableMultiSelect="false" OnRowCommand="dgvData_RowCommand" EnableSummary="True" EnableRowDoubleClickEvent="True" OnRowDoubleClick="dgvData_RowDoubleClick" EnableRowSelectEvent="True" OnRowSelect="dgvData_RowSelect" OnSort="dgvData_Sort">
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
                        <f:BoundField ID="BoundField2" runat="server" SortField="TenementCode" ColumnID="colTenementCode" DataField="TenementCode" HeaderText="租户代码" HtmlEncodeFormatString="True" Width="60px">
                        </f:BoundField>
                        <f:BoundField ID="BoundField3" runat="server" SortField="TenementName" ColumnID="colTenementName" DataField="TenementName" HeaderText="租户名称" HtmlEncodeFormatString="True" Width="150px">
                        </f:BoundField>
                        <f:BoundField ID="BoundField4" runat="server" SortField="Contact" ColumnID="colContact" DataField="Contact" HeaderText="联系人" HtmlEncodeFormatString="True">
                        </f:BoundField>
                        <f:BoundField ID="BoundField5" runat="server" SortField="MobilePhone" ColumnID="colMobilePhone" DataField="MobilePhone" HeaderText="移动电话" HtmlEncodeFormatString="True">
                        </f:BoundField>
                        <f:BoundField ID="BoundField6" runat="server" SortField="Telephone" ColumnID="colTelephone" DataField="Telephone" HeaderText="固定电话" HtmlEncodeFormatString="True">
                        </f:BoundField>
                        <f:BoundField ID="BoundField7" runat="server" SortField="Email" ColumnID="colEmail" DataField="Email" HeaderText="邮箱" HtmlEncodeFormatString="True">
                        </f:BoundField>
                        <f:BoundField ID="BoundField12" runat="server" SortField="Address" ColumnID="colAddress" DataField="Address" HeaderText="联系地址" HtmlEncodeFormatString="True">
                        </f:BoundField>
                        <f:BoundField ID="BoundField10" runat="server" SortField="Price" ColumnID="colPrice" DataField="Price" HeaderText="价格" HtmlEncodeFormatString="True">
                        </f:BoundField>
                        <f:BoundField ID="BoundField11" runat="server" SortField="Unit" ColumnID="colUnit" DataField="Unit" HeaderText="单位" HtmlEncodeFormatString="True">
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
                        <f:TextBox ID="txtTenementCode" runat="server" Label="租户代码" LabelAlign="Right" Required="True" RequiredMessage="租户代码不能为空" ShowRedStar="True"></f:TextBox>
                        <f:TextBox ID="txtTenementName" runat="server" Label="租户名称" LabelAlign="Right" Required="True" RequiredMessage="租户名称不能为空" ShowRedStar="True"></f:TextBox>
                        <f:TextBox ID="txtContact" runat="server" Label="联系人" LabelAlign="Right"></f:TextBox>
                        <f:TextBox ID="txtMobilePhone" runat="server" Label="联系电话" LabelAlign="Right" Required="True" RequiredMessage="联系电话不能为空" ShowRedStar="True"></f:TextBox>
                        <f:TextBox ID="txtTelephone" runat="server" Label="固定电话" LabelAlign="Right"></f:TextBox>
                        <f:TextBox ID="txtEmail" runat="server" Label="邮箱" LabelAlign="Right"></f:TextBox>
                        <f:TextBox ID="txtAddress" runat="server" Label="地址" LabelAlign="Right"></f:TextBox>
                        <f:NumberBox ID="txtPrice" runat="server" Label="价格" MinValue="0" NoNegative="true" LabelAlign="Right"></f:NumberBox>
                        <f:DropDownList ID="cmbUnit" runat="server" Label="单位" LabelAlign="Right">
                            <f:ListItem Text="RMB" Value="RMB" Selected="True" />
                            <f:ListItem Text="USD" Value="USD" />
                        </f:DropDownList>
                        <f:TextBox ID="txtRemark" runat="server" Label="备注" LabelAlign="Right"></f:TextBox>
                    </Items>
                </f:Window>                
                <f:Grid ID="dgvAccount" runat="server" AllowPaging="True" EnableCheckBoxSelect="True" AllowSorting="True" DataKeyNames="id" EnableMultiSelect="false" EnableSummary="True" OnRowCommand="dgvAccount_RowCommand" EnableRowDoubleClickEvent="True" OnRowDoubleClick="dgvAccount_RowDoubleClick">
                    <Toolbars>
                        <f:Toolbar ID="Toolbar5" runat="server">
                            <Items>                                
                                <f:Button ID="btnAddAccount" runat="server" Icon="Add" ToolTip="新增租户账号" Text="新增租户账号" OnClick="btnAddAccount_Click">
                                </f:Button>
                                <f:Button ID="btnGrant" runat="server" Icon="Add" ToolTip="系统模块授权" Text="授权" OnClick="btnGrant_Click">
                                </f:Button>
                            </Items>
                        </f:Toolbar>
                    </Toolbars>
                    <Columns>
                        <f:LinkButtonField TextAlign="Center" ConfirmText="删除选中行？" Icon="Delete" ConfirmTarget="Top" ColumnID="colDeleteAccount" HeaderText="&nbsp;" Width="40px" CommandName="Delete" />
                        <f:BoundField ID="BoundField9" runat="server" ColumnID="colUserName" DataField="UserName" HeaderText="租户账号" HtmlEncodeFormatString="True">
                        </f:BoundField>
                    </Columns>
                </f:Grid>
                <f:Window ID="pAccount" runat="server" BodyPadding="5px" IsModal="true" Width="300px" Hidden="True" Layout="Table" TableConfigColumns="1">
                    <Toolbars>
                        <f:Toolbar ID="Toolbar4" runat="server">
                            <Items>
                                <f:Button ID="btnConfirmAccount" runat="server" Icon="BulletTick" ToolTip="确定" Text="确定" OnClick="btnConfirmAccount_Click">
                                </f:Button>
                            </Items>
                        </f:Toolbar>
                    </Toolbars>                    
                    <Items>
                        <f:TextBox ID="txtUserName" runat="server" Label="账号" LabelAlign="Right" Required="True" RequiredMessage="账号不能为空" ShowRedStar="True"></f:TextBox>
                        <f:TextBox ID="txtPassword" runat="server" Label="密码" LabelAlign="Right" Required="True" RequiredMessage="密码不能为空" TextMode="Password" ShowRedStar="True"></f:TextBox>
                        <f:TextBox ID="txtConfirmPassword" runat="server" Label="确认密码" LabelAlign="Right" Required="True" RequiredMessage="确认密码不能为空" TextMode="Password" ShowRedStar="True"></f:TextBox>                        
                        <f:TextBox ID="txtConnectionString" runat="server" Label="连接字符串" LabelAlign="Right" Required="True" RequiredMessage="连接字符串不能为空" ShowRedStar="True"></f:TextBox>
                    </Items>
                </f:Window>
                <f:Window ID="wGrant" runat="server" BodyPadding="5px" IsModal="true" Title="授权" Width="300px" Height="300px" Hidden="True" Layout="Fit" EnableMaximize="True" EnableResize="True">
                    <Toolbars>
                        <f:Toolbar ID="Toolbar6" runat="server">
                            <Items>
                                <f:DropDownList ID="cmbSystemName" runat="server" Label="系统" LabelAlign="Right" TableRowspan="2" AutoPostBack="True" OnSelectedIndexChanged="cmbSystemName_SelectedIndexChanged" LabelWidth="40px" ></f:DropDownList>     
                                <f:Button ID="btnUpdatePrice" runat="server" Icon="BulletTick" ToolTip="模块定价" Text="模块定价" OnClick="btnUpdatePrice_Click"></f:Button>
                            </Items>
                        </f:Toolbar>
                    </Toolbars>
                    <Items>                        
                        <f:Tree ID="tData" runat="server" OnNodeCheck="tData_NodeCheck">

                        </f:Tree>
                    </Items>
                </f:Window>
                <f:Window ID="wAuthConfig" runat="server" BodyPadding="5px" IsModal="true" Title="授权" Width="300px" Hidden="True" EnableMaximize="True" EnableResize="True">
                    <Toolbars>
                        <f:Toolbar ID="Toolbar7" runat="server">
                            <Items>
                                <f:Button ID="btnSave" runat="server" ToolTip="保存" Text="保存" Icon="Disk" OnClick="btnSave_Click">
                                </f:Button>
                            </Items>
                        </f:Toolbar>
                    </Toolbars>
                    <Items>   
                        <f:DatePicker ID="dtpStartDate" runat="server" Label="租期开始时间" LabelAlign="Right"></f:DatePicker>
                        <f:DatePicker ID="dtpEndDate" runat="server" Label="租期结束时间" LabelAlign="Right"></f:DatePicker>
                        <f:NumberBox ID="txtDPrice" runat="server" Label="价格" LabelAlign="Right"></f:NumberBox>
                        <f:DropDownList ID="cmbDUnit" runat="server" Label="单位" LabelAlign="Right">                     
                            <f:ListItem Text="RMB" Value="RMB" Selected="True" />
                            <f:ListItem Text="USD" Value="USD" />
                        </f:DropDownList>
                    </Items>
                </f:Window>
            </Items>
        </f:Panel> 
    </div>        
    </form>
</body>
</html>