<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmSysMenu.aspx.cs" Inherits="ApplyMaterial.ApplyMaterial.frmSysMenu" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>系统菜单</title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
        <f:Panel ID="Panel1" runat="server" ShowBorder="false" ShowHeader="false" Layout="Region">
            <Items>
                <f:Panel runat="server" ID="panelCenterRegion" RegionPosition="Center">
                    <Items>
                        <f:Panel runat="server" ID="pData" RegionPosition="Center">
                            <Toolbars>
                                <f:Toolbar ID="Toolbar1" runat="server">
                                    <Items>
                                        <f:DropDownList ID="cmbESystemCode" runat="server" Label="系统" LabelWidth="40px" LabelAlign="Right" AutoPostBack="True" OnSelectedIndexChanged="cmbESystemCode_SelectedIndexChanged">
                                        </f:DropDownList>
                                        <f:ToolbarSeparator ID="ToolbarSeparator1" runat="server">
                                        </f:ToolbarSeparator>
                                        <f:Button runat="server" Icon="Add" Text="新增同级节点" ID="btnAddSiblingNode" OnClick="btnAddSiblingNode_Click">
                                        </f:Button>
                                        <f:Button runat="server" Icon="Add" Text="新增下一级节点" ID="btnAddSubNode" OnClick="btnAddSubNode_Click">
                                        </f:Button>
                                        <f:Button runat="server" Icon="BulletTick" Text="修改" ID="btnUpdate" OnClick="btnUpdate_Click">
                                        </f:Button>
                                        <f:Button runat="server" Icon="Delete" Text="删除" ID="btnDelete" OnClick="btnDelete_Click" ConfirmText="你真的要删除该记录吗？">
                                        </f:Button>
                                    </Items>
                                </f:Toolbar>
                            </Toolbars>
                            <Items>
                                <f:Tree ID="tData" runat="server">

                                </f:Tree>
                                <f:Window ID="pEdit" runat="server" BodyPadding="5px" IsModal="true" Title="新增" Width="600px" Hidden="True" Layout="Table" TableConfigColumns="2">
                                    <Toolbars>
                                        <f:Toolbar ID="Toolbar6" runat="server">
                                            <Items>
                                                <f:Button ID="btnConfirm" runat="server" Icon="BulletTick" ToolTip="确定" Text="确定" OnClick="btnConfirm_Click">
                                                </f:Button>
                                            </Items>
                                        </f:Toolbar>
                                    </Toolbars>                    
                                    <Items>
                                        <f:TextBox ID="txtNodeCode" runat="server" Label="节点代码" LabelAlign="Right" Required="True" RequiredMessage="模节点代码不能为空"></f:TextBox>
                                        <f:TextBox ID="txtMenuName" runat="server" Label="菜单名称" LabelAlign="Right" Required="True" RequiredMessage="菜单名称不能为空"></f:TextBox>
                                        <f:TextBox ID="txtNavigateUrl" runat="server" Label="URL" LabelAlign="Right"></f:TextBox>
                                        <f:TextBox ID="txtIcon" runat="server" Label="图标" LabelAlign="Right"></f:TextBox>
                                        <f:CheckBox ID="cbExpanded" runat="server" Label="是否展开" LabelAlign="Right"></f:CheckBox>
                                        <f:NumberBox ID="txtSortNo" runat="server" Label="排序号" LabelAlign="Right" MinValue="1" NoNegative="true" NoDecimal="true" Text="1" Required="true"></f:NumberBox>
                                    </Items>
                                </f:Window>
                            </Items>
                        </f:Panel>
                    </Items>
                </f:Panel>
            </Items>
        </f:Panel>
    </form>
</body>
</html>