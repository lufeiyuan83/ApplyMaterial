<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApplyMaterial.aspx.cs" Inherits="ERP.ApplyMaterial.ApplyMaterial"%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title><asp:Literal runat="server" Text="<%$ Resources:ApplyMaterial.aspx,Title%>" /></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>  
            <f:PageManager ID="PageManager1" runat="server">
            </f:PageManager>
            <f:Panel ID="pHead" runat="server" BodyPadding="5px" Layout="HBox">
                <Items>
                    <f:DropDownList ID="CmbCodeRule" runat="server" Label="<%$ Resources:ApplyMaterial.aspx,CodeRule%>" LabelAlign="Right" LabelWidth="100px" DataValueField="MappingValue1" DataTextField="MappingValue2" OnSelectedIndexChanged="CmbCodeRule_SelectedIndexChanged" AutoPostBack="True">
                    </f:DropDownList>
                    <f:DropDownList ID="cmbBuCode" runat="server" Label="<%$ Resources:ApplyMaterial.aspx,BuCode%>" LabelAlign="Right" DataValueField="MappingValue1" DataTextField="MappingValue2" LabelWidth="120px" Width="400px" EnableMultiSelect="True">
                    </f:DropDownList>
                </Items>
            </f:Panel> 
            <f:Panel ID="pData" runat="server" Layout="HBox">
                <Toolbars>
                    <f:Toolbar ID="Toolbar2" runat="server">
                        <Items>
                            <f:Button ID="BtnSaveAndCreate" runat="server" Icon="Add" Text="<%$ Resources:ApplyMaterial.aspx,SaveAndCreate%>" OnClick="BtnSaveAndCreate_Click"></f:Button>
                            <f:Label ID="LblMessage" runat="server"  LabelWidth="200px" LabelAlign="Right" ShowLabel="False"></f:Label>
                        </Items>
                    </f:Toolbar>
                </Toolbars>
                <Items>

                </Items>
            </f:Panel>
            <f:Panel ID="pTipTop" runat="server" BodyPadding="10px">
                <Items>
                    <f:Form ID="frmTipTop" ShowBorder="false" ShowHeader="false" Width="1000px" BodyPadding="5 5 0 5" runat="server">
                        <Rows>
                            <f:FormRow>
                                <Items>
                                    <f:TextBox ID="txtMaterialId" runat="server" Label="<%$ Resources:ApplyMaterial.aspx,MaterialId%>" LabelAlign="Right" Readonly="true" LabelWidth="120px"></f:TextBox>
                                    <f:DropDownList ID="cmbMaterialClass" runat="server" Label="<%$ Resources:ApplyMaterial.aspx,MaterialClass%>" LabelAlign="Right" DataValueField="MappingValue1" DataTextField="MappingValue2" LabelWidth="120px"></f:DropDownList>
                                    <f:DropDownList ID="cmbMainUnit" runat="server" Label="<%$ Resources:ApplyMaterial.aspx,Unit%>" LabelAlign="Right" DataValueField="MappingValue1" DataTextField="MappingValue2" LabelWidth="120px"></f:DropDownList>                                 
                                </Items>
                            </f:FormRow>
                            <f:FormRow>
                                <Items>
                                    <f:TextBox ID="txtProductionName" runat="server" Label="<%$ Resources:ApplyMaterial.aspx,ProductionName%>" LabelAlign="Right" Required="true" ShowRedStar="true" LabelWidth="120px"></f:TextBox>                    
                                    <f:TextBox ID="txtSpecification" runat="server" Label="<%$ Resources:ApplyMaterial.aspx,Specification%>" LabelAlign="Right" Required="true" ShowRedStar="true" LabelWidth="120px" AutoPostBack="True" OnTextChanged="txtSpecification_TextChanged"></f:TextBox>
                                    <f:DropDownList ID="cmbSourceCode" runat="server" Label="<%$ Resources:ApplyMaterial.aspx,SourceCode%>" LabelAlign="Right" DataValueField="MappingValue1" DataTextField="MappingValue2" LabelWidth="120px"></f:DropDownList>  
                                </Items>
                            </f:FormRow>
                            <f:FormRow>
                                <Items>
                                    <f:TextBox ID="txtGroupCode" runat="server" Label="<%$ Resources:ApplyMaterial.aspx,GroupCode%>" LabelAlign="Right" Text="HZ01" Required="true" ShowRedStar="true" LabelWidth="120px"></f:TextBox>
                                    <f:DropDownList ID="cmbFourthGroupCode" runat="server" Label="<%$ Resources:ApplyMaterial.aspx,FourthGroupCode%>" LabelAlign="Right" DataValueField="MappingValue1" DataTextField="MappingValue2" LabelWidth="120px"></f:DropDownList>
                                    <f:DropDownList ID="cmbABCCode" runat="server" Label="<%$ Resources:ApplyMaterial.aspx,FourthGroupCode%>" LabelAlign="Right" DataValueField="MappingValue1" DataTextField="MappingValue2" LabelWidth="120px"></f:DropDownList>
                                </Items>
                            </f:FormRow>
                            <f:FormRow>
                                <Items>
                                    <f:TextBox ID="txtRev" runat="server" Label="<%$ Resources:ApplyMaterial.aspx,Rev%>" LabelAlign="Right" Text="1.0" LabelWidth="120px" AutoPostBack="True" OnTextChanged="txtRev_TextChanged"></f:TextBox>
                                    <f:CheckBox ID="cbIsBonded" runat="server" Label="<%$ Resources:ApplyMaterial.aspx,IsBonded%>" LabelAlign="Right" LabelWidth="120px"/>
                                    <f:TextBox ID="txtRemark" runat="server" Label="<%$ Resources:ApplyMaterial.aspx,Remark%>" LabelAlign="Right" LabelWidth="120px"></f:TextBox>
                                </Items>
                            </f:FormRow>
                            <f:FormRow>
                                <Items>
                                    <f:DropDownList ID="cmbRepairMoldType" runat="server" Label="<%$ Resources:ApplyMaterial.aspx,RepairMoldType%>" LabelAlign="Right" DataValueField="MappingValue1" DataTextField="MappingValue2" LabelWidth="120px" AutoPostBack="True" Hidden="True" OnSelectedIndexChanged="cmbRepairMoldType_SelectedIndexChanged"></f:DropDownList>
                                </Items>
                            </f:FormRow>
                        </Rows>
                    </f:Form>
                </Items>
            </f:Panel> 
        </div>
    </form>
</body>
</html>
