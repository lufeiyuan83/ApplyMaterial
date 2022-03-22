<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CodeRule.aspx.cs" Inherits="ERP.SystemManagement.CodeRule" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title><asp:Literal runat="server" Text="<%$ Resources:CodeRule.aspx,Title%>" /></title>
</head>
<body>
    <form id="frmCodeRule" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
        <f:Panel ID="Panel1" runat="server" ShowBorder="false" ShowHeader="false" Layout="Region">
            <Items>
                <f:Panel runat="server" ID="panelCenterRegion" RegionPosition="Center" ShowHeader="false">
                    <Items>
                        <f:Grid ID="dgvData" runat="server" AllowPaging="True" EnableCheckBoxSelect="True" AllowSorting="True" DataKeyNames="id" EnableMultiSelect="false" EnableSummary="True" EnableRowDoubleClickEvent="True"  EnableRowSelectEvent="True"  EnableTextSelection="True" OnRowDoubleClick="DgvData_RowDoubleClick" OnRowSelect="DgvData_RowSelect" OnSort="DgvData_Sort">
                            <Toolbars>
                                <f:Toolbar ID="Toolbar3" runat="server">
                                    <Items>
                                        <f:Button ID="BtnAdd" runat="server" Icon="Add" Text="<%$ Resources:CodeRule.aspx,Add%>" OnClick="BtnAdd_Click"></f:Button>
                                        <f:Button ID="BtnRelate" runat="server" Icon="Link" Text="<%$ Resources:CodeRule.aspx,ConnectBusiness%>" OnClick="BtnRelate_Click"></f:Button>
                                    </Items>
                                </f:Toolbar>
                            </Toolbars>
                            <Columns>
                                <f:RowNumberField EnablePagingNumber="true"/>
                                <f:BoundField ID="BoundField6" runat="server" SortField="Code" ColumnID="colCode" DataField="Code" HeaderText="<%$ Resources:CodeRule.aspx,Code%>" HtmlEncodeFormatString="True" >
                                </f:BoundField>
                                <f:BoundField ID="BoundField1" runat="server" SortField="Name" ColumnID="colName" DataField="Name" HeaderText="<%$ Resources:CodeRule.aspx,Name%>" HtmlEncodeFormatString="True" >
                                </f:BoundField>
                                <f:BoundField ID="BoundField2" runat="server" SortField="Rule" ColumnID="colRule" DataField="Rule" HeaderText="<%$ Resources:CodeRule.aspx,Rule%>" Width="250px" HtmlEncodeFormatString="True">
                                </f:BoundField>
                                <f:BoundField ID="BoundField5" runat="server" SortField="RuleEN" ColumnID="colRuleEN" DataField="RuleEN" HeaderText="<%$ Resources:CodeRule.aspx,RuleEN%>" Width="250px" HtmlEncodeFormatString="True">
                                </f:BoundField>
                                <f:BoundField ID="BoundField3" runat="server" SortField="Rev" ColumnID="colRev" DataField="Rev" HeaderText="<%$ Resources:CodeRule.aspx,Rev%>" Width="50px" HtmlEncodeFormatString="True">
                                </f:BoundField>
                                <f:BoundField ID="BoundField7" runat="server" SortField="Schema" ColumnID="colSchema" DataField="Schema" HeaderText="<%$ Resources:CodeRule.aspx,Schema%>" HtmlEncodeFormatString="True">
                                </f:BoundField>
                                <f:BoundField ID="BoundField8" runat="server" SortField="TableName" ColumnID="colTableName" DataField="TableName" HeaderText="<%$ Resources:CodeRule.aspx,TableName%>" HtmlEncodeFormatString="True">
                                </f:BoundField>
                                <f:BoundField ID="BoundField9" runat="server" SortField="Field" ColumnID="colField" DataField="Field" HeaderText="<%$ Resources:CodeRule.aspx,Field%>" HtmlEncodeFormatString="True">
                                </f:BoundField>
                                <f:CheckBoxField RenderAsStaticField="true" DataField="IsActive" HeaderText="<%$ Resources:CodeRule.aspx,IsActive%>"  Width="60px"/>
                                <f:BoundField ID="BoundField4" runat="server" SortField="Remark" ColumnID="colRemark" DataField="Remark" HeaderText="<%$ Resources:CodeRule.aspx,Remark%>" HtmlEncodeFormatString="True">
                                </f:BoundField>
                            </Columns>
                        </f:Grid>
                        <f:Panel runat="server" ID="pData" RegionPosition="Center" ShowHeader="false">
                            <Toolbars>
                                <f:Toolbar ID="Toolbar1" runat="server">
                                    <Items>
                                        <f:Button runat="server" Icon="Add" Text="<%$ Resources:CodeRule.aspx,AddSiblingNode%>" ID="BtnAddSiblingNode" OnClick="BtnAddSiblingNode_Click">
                                        </f:Button>
                                        <f:Button runat="server" Icon="Add" Text="<%$ Resources:CodeRule.aspx,AddSubNode%>" ID="BtnAddSubNode" OnClick="BtnAddSubNode_Click">
                                        </f:Button>
                                        <f:Button runat="server" Icon="TableEdit" Text="<%$ Resources:CodeRule.aspx,UpdateNode%>" ID="BtnUpdateNode" OnClick="BtnUpdateNode_Click" IconUrl="~/res/icon/update.png">
                                        </f:Button>
                                        <f:Button runat="server" Icon="Delete" Text="<%$ Resources:CodeRule.aspx,DeleteNode%>" ID="BtnDeleteNode" ConfirmText="<%$ Resources:CodeRule.aspx,DeleteNodeQuestion%>" OnClick="BtnDeleteNode_Click">
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
                                        <f:MenuButton ID="mbAddSiblingNode" runat="server" Icon="Add" Text="<%$ Resources:CodeRule.aspx,AddSiblingNode%>" OnClick="BtnAddSiblingNode_Click"></f:MenuButton>
                                        <f:MenuButton ID="mbAddSubNode" runat="server" Icon="Add" Text="<%$ Resources:CodeRule.aspx,AddSubNode%>" OnClick="BtnAddSubNode_Click"></f:MenuButton>
                                        <f:MenuButton ID="mbUpdateNode" runat="server"  IconUrl="~/res/icon/update.png" Text="<%$ Resources:CodeRule.aspx,UpdateNode%>" OnClick="BtnUpdateNode_Click"></f:MenuButton>
                                        <f:MenuButton ID="mbDeleteNode" runat="server" Icon="Delete" Text="<%$ Resources:CodeRule.aspx,DeleteNode%>" ConfirmText="<%$ Resources:CodeRule.aspx,QuestionDeleteNode%>" OnClick="BtnDeleteNode_Click"></f:MenuButton>
                                    </Items>
                                </f:Menu>                              
                            </Items>
                        </f:Panel>                        
                        <f:Window ID="wRelateBusiness" runat="server" BodyPadding="5px" IsModal="true" Title="<%$ Resources:CodeRule.aspx,ConnectBusiness%>" Width="600px" Hidden="True" Layout="Table" TableConfigColumns="2">
                            <Toolbars>
                                <f:Toolbar ID="Toolbar4" runat="server">
                                    <Items>
                                        <f:Button ID="BtnSaveRelateBusiness" runat="server" Icon="Disk" Text="<%$ Resources:CodeRule.aspx,Save%>" OnClick="BtnSaveRelateBusiness_Click"></f:Button>
                                    </Items>
                                </f:Toolbar>
                            </Toolbars>                    
                            <Items>
                                <f:TextBox ID="txtSchema" runat="server" Label="<%$ Resources:CodeRule.aspx,Schema%>" LabelAlign="Right" Required="True" RequiredMessage="<%$ Resources:CodeRule.aspx,SchemaCanNotBlank%>"></f:TextBox>
                                <f:TextBox ID="txtTableName" runat="server" Label="<%$ Resources:CodeRule.aspx,TableName%>" LabelAlign="Right" Required="True" RequiredMessage="<%$ Resources:CodeRule.aspx,TableNameCanNotBlank%>"></f:TextBox>
                                <f:TextBox ID="txtField" runat="server" Label="<%$ Resources:CodeRule.aspx,Field%>" LabelAlign="Right" Required="True" RequiredMessage="<%$ Resources:CodeRule.aspx,FieldCanNotBlank%>"></f:TextBox>
                            </Items>
                        </f:Window>
                        <f:Window ID="wCodeRule" runat="server" BodyPadding="5px" IsModal="true" Title="<%$ Resources:CodeRule.aspx,Add%>" Width="760px" Hidden="True" Layout="Table" TableConfigColumns="2">
                            <Toolbars>
                                <f:Toolbar ID="Toolbar2" runat="server">
                                    <Items>
                                        <f:Button ID="BtnSaveRule" runat="server" Icon="Disk" Text="<%$ Resources:CodeRule.aspx,Save%>" OnClick="BtnSaveRule_Click"></f:Button>
                                        <f:DropDownList ID="cmbFixedRule" runat="server" Label="<%$ Resources:CodeRule.aspx,FixedRule%>" LabelAlign="Right" LabelWidth="80px"></f:DropDownList>
                                        <f:Button ID="BtnAppend" runat="server" Icon="Add" Text="<%$ Resources:CodeRule.aspx,AddFixedRule%>" OnClick="BtnAppend_Click"></f:Button>
                                    </Items>
                                </f:Toolbar>
                            </Toolbars>                    
                            <Items>
                                <f:TextBox ID="txtCode" runat="server" Label="<%$ Resources:CodeRule.aspx,Code%>" LabelAlign="Right" Required="True" RequiredMessage="<%$ Resources:CodeRule.aspx,CodeCanNotBlank%>" Width="350px"></f:TextBox>
                                <f:TextBox ID="txtCodeRule" runat="server" Label="<%$ Resources:CodeRule.aspx,Name%>" LabelAlign="Right" Required="True" RequiredMessage="<%$ Resources:CodeRule.aspx,NameCanNotBlank%>" Width="350px"></f:TextBox>
                                <f:NumberBox ID="nbRev" runat="server" Label="<%$ Resources:CodeRule.aspx,Rev%>" LabelAlign="Right" MinValue="1" NoNegative="true" NoDecimal="true" Text="1" Required="true" Width="350px"></f:NumberBox>
                                <f:TextBox ID="txtRule" runat="server" Label="<%$ Resources:CodeRule.aspx,Rule%>" LabelAlign="Right" Required="True" RequiredMessage="<%$ Resources:CodeRule.aspx,RuleCanNotBlank%>" Width="350px"></f:TextBox>  
                                <f:TextBox ID="txtRuleEN" runat="server" Label="<%$ Resources:CodeRule.aspx,RuleEN%>" LabelAlign="Right" Required="True" RequiredMessage="<%$ Resources:CodeRule.aspx,RuleENCanNotBlank%>" Width="350px"></f:TextBox>  
                                <f:CheckBox ID="cbIsActive" runat="server" Label="<%$ Resources:CodeRule.aspx,IsActive%>" LabelAlign="Right" Checked="true" Width="350px"></f:CheckBox> 
                                <f:TextBox ID="txtRemark" runat="server" Label="<%$ Resources:CodeRule.aspx,Remark%>" LabelAlign="Right" Width="350px"></f:TextBox>
                            </Items>
                        </f:Window>
                        <f:Window ID="wCodeNode" runat="server" BodyPadding="5px" IsModal="true" Title="<%$ Resources:CodeRule.aspx,Add%>" Width="670px" Hidden="True" Layout="Table" TableConfigColumns="2">
                            <Toolbars>
                                <f:Toolbar ID="Toolbar6" runat="server">
                                    <Items>
                                        <f:Button ID="BtnConfirm" runat="server" Icon="BulletTick" ToolTip="<%$ Resources:CodeRule.aspx,OK%>" Text="<%$ Resources:CodeRule.aspx,OK%>" OnClick="BtnConfirm_Click">
                                        </f:Button>
                                    </Items>
                                </f:Toolbar>
                            </Toolbars>                    
                            <Items>
                                <f:TextBox ID="txtParameterCode" runat="server" Label="<%$ Resources:CodeRule.aspx,ParameterCode%>" LabelAlign="Right" Required="True" RequiredMessage="<%$ Resources:CodeRule.aspx,ParameterCodeCanNotBlank%>" LabelWidth="150px"></f:TextBox>
                                <f:TextBox ID="txtParameterName" runat="server" Label="<%$ Resources:CodeRule.aspx,ParameterName%>" LabelAlign="Right" Required="True" RequiredMessage="<%$ Resources:CodeRule.aspx,ParameterNameCanNotBlank%>" LabelWidth="150px"></f:TextBox>
                                <f:TextBox ID="txtParameterValue" runat="server" Label="<%$ Resources:CodeRule.aspx,ParameterValue%>" LabelAlign="Right" LabelWidth="150px"></f:TextBox>
                                <f:DropDownList ID="cmbParameterType" runat="server" Label="<%$ Resources:CodeRule.aspx,ParameterType%>" LabelAlign="Right" LabelWidth="150px"></f:DropDownList>
                                <f:TextBox ID="txtParameterDescription" runat="server" Label="<%$ Resources:CodeRule.aspx,ParameterDescription%>" LabelAlign="Right" LabelWidth="150px"></f:TextBox>                                
                                <f:CheckBox ID="cbIsDefault" runat="server" Label="<%$ Resources:CodeRule.aspx,IsDefault%>" LabelAlign="Right" LabelWidth="150px"></f:CheckBox>
                                <f:NumberBox ID="txtSortNo" runat="server" Label="<%$ Resources:CodeRule.aspx,SortNo%>" LabelAlign="Right" MinValue="1" NoNegative="true" NoDecimal="true" Text="1" Required="true" LabelWidth="150px"></f:NumberBox>
                                <f:NumberBox ID="txtGroupNo" runat="server" Label="<%$ Resources:CodeRule.aspx,GroupNo%>" LabelAlign="Right" MinValue="1" NoNegative="true" NoDecimal="true" Text="1" Required="true" LabelWidth="150px"></f:NumberBox>
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