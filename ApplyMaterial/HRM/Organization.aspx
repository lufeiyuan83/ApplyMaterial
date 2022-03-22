<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Organization.aspx.cs" Inherits="ERP.HRM.Organization" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title><asp:Literal runat="server" Text="<%$ Resources:Organization.aspx,Title%>" /></title>
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
                                        <f:Button runat="server" Icon="Add" Text="<%$ Resources:Organization.aspx,AddSiblingNode%>" ID="BtnAddSiblingNode" OnClick="BtnAddSiblingNode_Click">
                                        </f:Button>
                                        <f:Button runat="server" Icon="Add" Text="<%$ Resources:Organization.aspx,AddSubNode%>" ID="BtnAddSubNode" OnClick="BtnAddSubNode_Click">
                                        </f:Button>
                                        <f:Button runat="server" Icon="TableEdit" Text="<%$ Resources:Organization.aspx,UpdateNode%>" ID="BtnUpdateNode" OnClick="BtnUpdateNode_Click" IconUrl="~/res/icon/update.png">
                                        </f:Button>
                                        <f:Button runat="server" Icon="Delete" Text="<%$ Resources:Organization.aspx,DeleteNode%>" ID="BtnDeleteNode" ConfirmText="<%$ Resources:Organization.aspx,DeleteNodeQuestion%>" OnClick="BtnDeleteNode_Click">
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
                                        <f:MenuButton ID="mbAddSiblingNode" runat="server" Icon="Add" Text="<%$ Resources:Organization.aspx,AddSiblingNode%>" OnClick="BtnAddSiblingNode_Click"></f:MenuButton>
                                        <f:MenuButton ID="mbAddSubNode" runat="server" Icon="Add" Text="<%$ Resources:Organization.aspx,AddSubNode%>" OnClick="BtnAddSubNode_Click"></f:MenuButton>
                                        <f:MenuButton ID="mbUpdateNode" runat="server"  IconUrl="~/res/icon/update.png" Text="<%$ Resources:Organization.aspx,UpdateNode%>" OnClick="BtnUpdateNode_Click"></f:MenuButton>
                                        <f:MenuButton ID="mbDeleteNode" runat="server" Icon="Delete" Text="<%$ Resources:Organization.aspx,DeleteNode%>" ConfirmText="<%$ Resources:Organization.aspx,DeleteNodeQuestion%>" OnClick="BtnDeleteNode_Click"></f:MenuButton>
                                    </Items>
                                </f:Menu>                              
                            </Items>
                        </f:Panel>                         
                        <f:Window ID="wData" runat="server" BodyPadding="5px" IsModal="true" Width="600px" Hidden="True" Layout="Table" TableConfigColumns="2">
                            <Toolbars>
                                <f:Toolbar ID="Toolbar4" runat="server">
                                    <Items>
                                        <f:Button ID="BtnSave" runat="server" Icon="Disk" Text="<%$ Resources:Organization.aspx,Save%>" OnClick="BtnSave_Click"></f:Button>
                                    </Items>
                                </f:Toolbar>
                            </Toolbars>                    
                            <Items>
                                <f:TextBox ID="txtOrgCode" runat="server" Label="<%$ Resources:Organization.aspx,OrgCode%>" LabelAlign="Right" Required="True" RequiredMessage="<%$ Resources:Organization.aspx,OrgCodeCanNotBlank%>"></f:TextBox>
                                <f:TextBox ID="txtOrgName" runat="server" Label="<%$ Resources:Organization.aspx,OrgName%>" LabelAlign="Right" Required="True" RequiredMessage="<%$ Resources:Organization.aspx,OrgNameCanNotBlank%>"></f:TextBox>
                                <f:TextBox ID="txtOrgShortName" runat="server" Label="<%$ Resources:Organization.aspx,OrgShortName%>" LabelAlign="Right"></f:TextBox>
                                <f:TextBox ID="txtOrgDescription" runat="server" Label="<%$ Resources:Organization.aspx,OrgDescription%>" LabelAlign="Right"></f:TextBox>
                                <f:TextBox ID="txtAffiliation" runat="server" Label="<%$ Resources:Organization.aspx,Affiliation%>" LabelAlign="Right"></f:TextBox>
                                <f:DropDownList ID="cmbOrgType" runat="server" Label="<%$ Resources:Organization.aspx,OrgType%>" LabelAlign="Right"></f:DropDownList>
                                <f:CheckBox ID="cbEnableSelect" runat="server" Label="<%$ Resources:Organization.aspx,EnableSelect%>" LabelAlign="Right"></f:CheckBox>
                                <f:CheckBox ID="cbDefaultValue" runat="server" Label="<%$ Resources:Organization.aspx,DefaultValue%>" LabelAlign="Right"></f:CheckBox>
                                <f:TextBox ID="txtPhone" runat="server" Label="<%$ Resources:Organization.aspx,Phone%>" LabelAlign="Right"></f:TextBox>
                                <f:TextBox ID="txtMobilePhone" runat="server" Label="<%$ Resources:Organization.aspx,MobilePhone%>" LabelAlign="Right"></f:TextBox>
                                <f:TextBox ID="txtEmail" runat="server" Label="<%$ Resources:Organization.aspx,Email%>" LabelAlign="Right"></f:TextBox>
                                <f:TextBox ID="txtFax" runat="server" Label="<%$ Resources:Organization.aspx,Fax%>" LabelAlign="Right"></f:TextBox>
                                <f:TextBox ID="txtAddress" runat="server" Label="<%$ Resources:Organization.aspx,Address%>" LabelAlign="Right"></f:TextBox>
                                <f:TextBox ID="txtRemark" runat="server" Label="<%$ Resources:Organization.aspx,Remark%>" LabelAlign="Right"></f:TextBox>
                            </Items>
                        </f:Window>
                    </Items>
                </f:Panel>
            </Items>
        </f:Panel>
    </form>
    <script>
        function onTreeNodeContextMenu(view, record, item, index, event) {
            var Toolbar1 = document.getElementById('<%= Toolbar1.ClientID %>');
            var Toolbar1Height = Toolbar1.clientHeight || Toolbar1.offsetHeight;            
            F('<%= Menu1.ClientID %>').showAt(event.clientX, event.clientY - Toolbar1Height);            
            event.stopEvent();
        }
    </script>
</body>
</html>
