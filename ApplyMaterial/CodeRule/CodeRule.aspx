<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CodeRule.aspx.cs" Inherits="ApplyMaterial.CodeRule.CodeRule" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>编码原则</title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
        <f:Panel ID="Panel1" runat="server" ShowBorder="false" ShowHeader="false" Layout="Region">
            <Items>
                <f:Panel runat="server" ID="panelCenterRegion" RegionPosition="Center" ShowHeader="false">
                    <Items>
                        <f:Grid ID="dgvData" runat="server" AllowPaging="True" EnableCheckBoxSelect="True" AllowSorting="True" DataKeyNames="id" EnableMultiSelect="false" EnableSummary="True" EnableRowDoubleClickEvent="True"  EnableRowSelectEvent="True"  EnableTextSelection="True" OnRowDoubleClick="dgvData_RowDoubleClick" OnRowSelect="dgvData_RowSelect" OnSort="dgvData_Sort">
                            <Toolbars>
                                <f:Toolbar ID="Toolbar3" runat="server">
                                    <Items>
                                        <f:Button ID="btnAdd" runat="server" Icon="Add" Text="新增" OnClick="btnAdd_Click"></f:Button>
                                        <f:Button ID="btnRelate" runat="server" Icon="Link" Text="关联业务" OnClick="btnRelate_Click"></f:Button>
                                    </Items>
                                </f:Toolbar>
                            </Toolbars>
                            <Columns>
                                <f:RowNumberField EnablePagingNumber="true"/>
                                <f:BoundField ID="BoundField6" runat="server" SortField="Code" ColumnID="colCode" DataField="Code" HeaderText="编码原则代码" HtmlEncodeFormatString="True" >
                                </f:BoundField>
                                <f:BoundField ID="BoundField1" runat="server" SortField="Name" ColumnID="colName" DataField="Name" HeaderText="编码原则" HtmlEncodeFormatString="True" >
                                </f:BoundField>
                                <f:BoundField ID="BoundField2" runat="server" SortField="Rule" ColumnID="colRule" DataField="Rule" HeaderText="规则" Width="250px" HtmlEncodeFormatString="True">
                                </f:BoundField>
                                <f:BoundField ID="BoundField5" runat="server" SortField="RuleEN" ColumnID="colRuleEN" DataField="RuleEN" HeaderText="规则代码" Width="250px" HtmlEncodeFormatString="True">
                                </f:BoundField>
                                <f:BoundField ID="BoundField3" runat="server" SortField="Rev" ColumnID="colRev" DataField="Rev" HeaderText="版本" Width="50px" HtmlEncodeFormatString="True">
                                </f:BoundField>
                                <f:BoundField ID="BoundField7" runat="server" SortField="Schema" ColumnID="colSchema" DataField="Schema" HeaderText="数据库/表空间" HtmlEncodeFormatString="True">
                                </f:BoundField>
                                <f:BoundField ID="BoundField8" runat="server" SortField="TableName" ColumnID="colTableName" DataField="TableName" HeaderText="表名" HtmlEncodeFormatString="True">
                                </f:BoundField>
                                <f:BoundField ID="BoundField9" runat="server" SortField="Field" ColumnID="colField" DataField="Field" HeaderText="字段" HtmlEncodeFormatString="True">
                                </f:BoundField>
                                <f:CheckBoxField RenderAsStaticField="true" DataField="IsActive" HeaderText="是否有效"  Width="60px"/>
                                <f:BoundField ID="BoundField4" runat="server" SortField="Remark" ColumnID="colRemark" DataField="Remark" HeaderText="备注" HtmlEncodeFormatString="True">
                                </f:BoundField>
                            </Columns>
                        </f:Grid>
                        <f:Panel runat="server" ID="pData" RegionPosition="Center" ShowHeader="false">
                            <Toolbars>
                                <f:Toolbar ID="Toolbar1" runat="server">
                                    <Items>
                                        <f:Button runat="server" Icon="Add" Text="新增同级节点" ID="btnAddSiblingNode" OnClick="btnAddSiblingNode_Click">
                                        </f:Button>
                                        <f:Button runat="server" Icon="Add" Text="新增子节点" ID="btnAddSubNode" OnClick="btnAddSubNode_Click">
                                        </f:Button>
                                        <f:Button runat="server" Icon="TableEdit" Text="修改" ID="btnUpdateNode" OnClick="btnUpdateNode_Click" IconUrl="~/res/icon/update.png">
                                        </f:Button>
                                        <f:Button runat="server" Icon="Delete" Text="删除" ID="btnDeleteNode" ConfirmText="你真的要删除该记录吗？" OnClick="btnDeleteNode_Click">
                                        </f:Button>
                                    </Items>
                                </f:Toolbar>
                            </Toolbars>
                            <Items>
                                <f:Tree ID="tData" runat="server" ShowHeader="false">
                                    <Nodes>
                                    </Nodes>
                                    <Listeners>
                                        <f:Listener Event="beforeitemcontextmenu" Handler="onTreeNodeContextMenu" />
                                    </Listeners>
                                </f:Tree> 
                                <f:Menu ID="Menu1" runat="server">
                                    <Items>
                                        <f:MenuButton ID="mbAddSiblingNode" runat="server" Icon="Add" Text="新增同级节点" OnClick="btnAddSiblingNode_Click"></f:MenuButton>
                                        <f:MenuButton ID="mbAddSubNode" runat="server" Icon="Add" Text="新增子节点" OnClick="btnAddSubNode_Click"></f:MenuButton>
                                        <f:MenuButton ID="mbUpdateNode" runat="server"  IconUrl="~/res/icon/update.png" Text="修改" OnClick="btnUpdateNode_Click"></f:MenuButton>
                                        <f:MenuButton ID="mbDeleteNode" runat="server" Icon="Delete" Text="删除" ConfirmText="你真的要删除该记录吗？" OnClick="btnDeleteNode_Click"></f:MenuButton>
                                    </Items>
                                </f:Menu>                              
                            </Items>
                        </f:Panel>                        
                        <f:Window ID="wRelateBusiness" runat="server" BodyPadding="5px" IsModal="true" Title="关联业务" Width="600px" Hidden="True" Layout="Table" TableConfigColumns="2">
                            <Toolbars>
                                <f:Toolbar ID="Toolbar4" runat="server">
                                    <Items>
                                        <f:Button ID="btnSaveRelateBusiness" runat="server" Icon="Disk" Text="保存" OnClick="btnSaveRelateBusiness_Click"></f:Button>
                                    </Items>
                                </f:Toolbar>
                            </Toolbars>                    
                            <Items>
                                <f:TextBox ID="txtSchema" runat="server" Label="数据库/表空间" LabelAlign="Right" Required="True" RequiredMessage="数据库/表空间不能为空"></f:TextBox>
                                <f:TextBox ID="txtTableName" runat="server" Label="表名" LabelAlign="Right" Required="True" RequiredMessage="表名不能为空"></f:TextBox>
                                <f:TextBox ID="txtField" runat="server" Label="字段" LabelAlign="Right" Required="True" RequiredMessage="字段不能为空"></f:TextBox>
                            </Items>
                        </f:Window>
                        <f:Window ID="wCodeRule" runat="server" BodyPadding="5px" IsModal="true" Title="新增" Width="760px" Hidden="True" Layout="Table" TableConfigColumns="2">
                            <Toolbars>
                                <f:Toolbar ID="Toolbar2" runat="server">
                                    <Items>
                                        <f:Button ID="btnSaveRule" runat="server" Icon="Disk" Text="保存" OnClick="btnSaveRule_Click"></f:Button>
                                        <f:DropDownList ID="cmbFixedRule" runat="server" Label="内置规则" LabelAlign="Right" LabelWidth="60px"></f:DropDownList>
                                        <f:Button ID="btnAppend" runat="server" Icon="Add" Text="添加内置规则" OnClick="btnAppend_Click"></f:Button>
                                    </Items>
                                </f:Toolbar>
                            </Toolbars>                    
                            <Items>
                                <f:TextBox ID="txtCode" runat="server" Label="编码原则代码" LabelAlign="Right" Required="True" RequiredMessage="编码原则代码不能为空" Width="350px"></f:TextBox>
                                <f:TextBox ID="txtCodeRule" runat="server" Label="编码原则" LabelAlign="Right" Required="True" RequiredMessage="编码原则不能为空" Width="350px"></f:TextBox>
                                <f:NumberBox ID="nbRev" runat="server" Label="版本" LabelAlign="Right" MinValue="1" NoNegative="true" NoDecimal="true" Text="1" Required="true" Width="350px"></f:NumberBox>
                                <f:TextBox ID="txtRule" runat="server" Label="规则" LabelAlign="Right" Required="True" RequiredMessage="规则不能为空" Width="350px"></f:TextBox>  
                                <f:TextBox ID="txtRuleEN" runat="server" Label="规则代码" LabelAlign="Right" Required="True" RequiredMessage="规则代码不能为空" Width="350px"></f:TextBox>  
                                <f:CheckBox ID="cbIsActive" runat="server" Label="是否有效" LabelAlign="Right" Checked="true" Width="350px"></f:CheckBox> 
                                <f:TextBox ID="txtRemark" runat="server" Label="备注" LabelAlign="Right" Width="350px"></f:TextBox>
                            </Items>
                        </f:Window>
                        <f:Window ID="wCodeNode" runat="server" BodyPadding="5px" IsModal="true" Title="新增" Width="760px" Hidden="True" Layout="Table" TableConfigColumns="2">
                            <Toolbars>
                                <f:Toolbar ID="Toolbar6" runat="server">
                                    <Items>
                                        <f:Button ID="btnConfirm" runat="server" Icon="BulletTick" ToolTip="确定" Text="确定" OnClick="btnConfirm_Click">
                                        </f:Button>
                                    </Items>
                                </f:Toolbar>
                            </Toolbars>                    
                            <Items>
                                <f:TextBox ID="txtParameterCode" runat="server" Label="参数编码" LabelAlign="Right" Required="True" RequiredMessage="参数编码不能为空" Width="350px"></f:TextBox>
                                <f:TextBox ID="txtParameterName" runat="server" Label="参数名称" LabelAlign="Right" Required="True" RequiredMessage="参数名称不能为空" Width="350px"></f:TextBox>
                                <f:TextBox ID="txtParameterValue" runat="server" Label="参数值" LabelAlign="Right" Width="350px"></f:TextBox>
                                <f:DropDownList ID="cmbParameterType" runat="server" Label="参数类型" LabelAlign="Right" Width="350px"></f:DropDownList>
                                <f:TextBox ID="txtParameterDescription" runat="server" Label="描述" LabelAlign="Right" Width="350px"></f:TextBox>                                
                                <f:CheckBox ID="cbIsDefault" runat="server" Label="是否默认值" LabelAlign="Right" Width="350px"></f:CheckBox>
                                <f:NumberBox ID="txtSortNo" runat="server" Label="排序号" LabelAlign="Right" MinValue="1" NoNegative="true" NoDecimal="true" Text="1" Required="true" Width="350px"></f:NumberBox>
                                <f:NumberBox ID="txtGroupNo" runat="server" Label="分组号" LabelAlign="Right" MinValue="1" NoNegative="true" NoDecimal="true" Text="1" Required="true" Width="350px"></f:NumberBox>
                            </Items>
                        </f:Window>
                    </Items>
                </f:Panel>
            </Items>
        </f:Panel>
    </form>
    <script>
        function onTreeNodeContextMenu(view, record, item, index, event) {
            var dgvData = document.getElementById('<%= dgvData.ClientID %>');
            var dgvDataHeight = dgvData.clientHeight || dgvData.offsetHeight;
            var Toolbar1 = document.getElementById('<%= Toolbar1.ClientID %>');
            var Toolbar1Height = Toolbar1.clientHeight || Toolbar1.offsetHeight;            
            F('<%= Menu1.ClientID %>').showAt(event.clientX, event.clientY - dgvDataHeight - Toolbar1Height);            
            event.stopEvent();
        }
    </script>
</body>
</html>