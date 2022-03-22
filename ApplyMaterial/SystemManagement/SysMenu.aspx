<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SysMenu.aspx.cs" Inherits="ERP.SystemManagement.SysMenu" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title><asp:Literal runat="server" Text="<%$ Resources:SysMenu.aspx,Title%>" /></title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
        <f:Panel ID="Panel1" runat="server" ShowBorder="false" ShowHeader="false" Layout="Region">
            <Items>
                <f:Panel runat="server" ID="panelCenterRegion" RegionPosition="Center" ShowHeader="false">
                    <Items>
                        <f:Panel runat="server" ID="pData" RegionPosition="Center" ShowHeader="false">
                            <Toolbars>
                                <f:Toolbar ID="Toolbar1" runat="server">
                                    <Items>
                                        <f:DropDownList ID="cmbESystemCode" runat="server" Label="<%$ Resources:SysMenu.aspx,System%>" LabelWidth="50px" LabelAlign="Right" AutoPostBack="True" OnSelectedIndexChanged="cmbESystemCode_SelectedIndexChanged">
                                        </f:DropDownList>
                                        <f:ToolbarSeparator ID="ToolbarSeparator1" runat="server">
                                        </f:ToolbarSeparator>
                                        <f:Button runat="server" Icon="Add" Text="<%$ Resources:SysMenu.aspx,AddSiblingNode%>" ID="btnAddSiblingNode" OnClick="btnAddSiblingNode_Click">
                                        </f:Button>
                                        <f:Button runat="server" Icon="Add" Text="<%$ Resources:SysMenu.aspx,AddSubNode%>" ID="btnAddSubNode" OnClick="btnAddSubNode_Click">
                                        </f:Button>
                                        <f:Button runat="server" Icon="BulletTick" Text="<%$ Resources:SysMenu.aspx,Update%>" ID="btnUpdate" OnClick="btnUpdate_Click">
                                        </f:Button>
                                        <f:Button runat="server" Icon="Delete" Text="<%$ Resources:SysMenu.aspx,Delete%>" ID="btnDelete" OnClick="btnDelete_Click" ConfirmText="<%$ Resources:SysMenu.aspx,DeleteQuestion%>">
                                        </f:Button>
                                    </Items>
                                </f:Toolbar>
                            </Toolbars>
                            <Items>
                                <f:Panel runat="server" ID="Panel2" Layout="HBox" ShowHeader="false">
                                    <Items>
                                        <f:Tree ID="tData" runat="server" ShowHeader="false" Width="400px" BoxFlex="1" OnNodeCommand="tData_NodeCommand">

                                        </f:Tree>
                                        <f:Grid ID="dgvData" runat="server"  BoxFlex="2" AllowPaging="True" AllowSorting="True" DataKeyNames="id" EnableMultiSelect="false" OnRowCommand="dgvData_RowCommand" EnableSummary="True" EnableRowDoubleClickEvent="True" OnRowDoubleClick="dgvData_RowDoubleClick" OnSort="dgvData_Sort" ShowHeader="false">
                                            <Toolbars>
                                                <f:Toolbar ID="Toolbar2" runat="server">
                                                    <Items>
                                                        <f:Button ID="btnAdd" runat="server" Icon="Add" ToolTip="<%$ Resources:SysMenu.aspx,Add%>" Text="<%$ Resources:SysMenu.aspx,Add%>" OnClick="btnAdd_Click">
                                                        </f:Button>
                                                    </Items>
                                                </f:Toolbar>
                                            </Toolbars>
                                            <Columns>
                                                <f:RowNumberField EnablePagingNumber="true" />
                                                <f:LinkButtonField TextAlign="Center" ConfirmText="<%$ Resources:SysMenu.aspx,DeleteRecordQuestion%>" Icon="Delete" ConfirmTarget="Top" ColumnID="colDelete" HeaderText="&nbsp;" Width="40px" CommandName="Delete" />
                                                <f:BoundField ID="BoundField1" runat="server" SortField="Code" ColumnID="colCode" DataField="Code" HeaderText="<%$ Resources:SysMenu.aspx,Code%>" HtmlEncodeFormatString="True"></f:BoundField>
                                                <f:BoundField ID="BoundField3" runat="server" SortField="Name" ColumnID="colName" DataField="Name" HeaderText="<%$ Resources:SysMenu.aspx,Name%>" HtmlEncodeFormatString="True"></f:BoundField>
                                            </Columns>
                                        </f:Grid>
                                    </Items>
                                </f:Panel>
                            </Items>
                        </f:Panel>                        
                        <f:Window ID="pEdit" runat="server" BodyPadding="5px" IsModal="true" Width="600px" Hidden="True" Layout="Table" TableConfigColumns="2">
                            <Toolbars>
                                <f:Toolbar ID="Toolbar6" runat="server">
                                    <Items>
                                        <f:Button ID="btnConfirm" runat="server" Icon="BulletTick" ToolTip="<%$ Resources:SysMenu.aspx,Confirm%>" Text="<%$ Resources:SysMenu.aspx,Confirm%>" OnClick="btnConfirm_Click">
                                        </f:Button>
                                    </Items>
                                </f:Toolbar>
                            </Toolbars>                    
                            <Items>
                                <f:TextBox ID="txtNodeCode" runat="server" Label="<%$ Resources:SysMenu.aspx,NodeCode%>" LabelAlign="Right" Required="True" RequiredMessage="<%$ Resources:SysMenu.aspx,NodeCodeCanNotBlank%>"></f:TextBox>
                                <f:TextBox ID="txtMenuName" runat="server" Label="<%$ Resources:SysMenu.aspx,MenuName%>" LabelAlign="Right" Required="True" RequiredMessage="<%$ Resources:SysMenu.aspx,MenuNameCanNotBlank%>"></f:TextBox>
                                <f:TextBox ID="txtMenuNameEn" runat="server" Label="<%$ Resources:SysMenu.aspx,MenuNameEn%>" LabelAlign="Right"></f:TextBox>
                                <f:TextBox ID="txtMenuNameTh" runat="server" Label="<%$ Resources:SysMenu.aspx,MenuNameTh%>" LabelAlign="Right"></f:TextBox>
                                <f:TextBox ID="txtNavigateUrl" runat="server" Label="URL" LabelAlign="Right"></f:TextBox>
                                <f:TextBox ID="txtIcon" runat="server" Label="<%$ Resources:SysMenu.aspx,Icon%>" LabelAlign="Right"></f:TextBox>
                                <f:CheckBox ID="cbExpanded" runat="server" Label="<%$ Resources:SysMenu.aspx,Expanded%>" LabelAlign="Right"></f:CheckBox>
                                <f:NumberBox ID="txtSortNo" runat="server" Label="<%$ Resources:SysMenu.aspx,SortNo%>" LabelAlign="Right" MinValue="1" NoNegative="true" NoDecimal="true" Text="1" Required="true"></f:NumberBox>
                            </Items>
                        </f:Window>                                                
                        <f:Window ID="wAccess" runat="server" BodyPadding="5px" IsModal="true" Width="500px" Hidden="True" Layout="Table" TableConfigColumns="2">
                            <Toolbars>
                                <f:Toolbar ID="Toolbar3" runat="server">
                                    <Items>
                                        <f:Button ID="btnSave" runat="server" Icon="BulletTick" ToolTip="<%$ Resources:SysMenu.aspx,Save%>" Text="<%$ Resources:SysMenu.aspx,Save%>" OnClick="btnSave_Click">
                                        </f:Button>
                                    </Items>
                                </f:Toolbar>
                            </Toolbars>                    
                            <Items>  
                                <f:TextBox ID="txtCode" runat="server" Label="<%$ Resources:SysMenu.aspx,Code%>" LabelAlign="Right" Required="True" RequiredMessage="<%$ Resources:SysMenu.aspx,CodeCanNotBlank%>"></f:TextBox> 
                                <f:TextBox ID="txtName" runat="server" Label="<%$ Resources:SysMenu.aspx,Name%>" LabelAlign="Right" Required="True" RequiredMessage="<%$ Resources:SysMenu.aspx,NameCanNotBlank%>" LabelWidth="50px"></f:TextBox>                               
                            </Items>
                        </f:Window>
                    </Items>
                </f:Panel>
            </Items>
        </f:Panel>
    </form>
</body>
</html>