<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SearchMaterial.aspx.cs" Inherits="ERP.ApplyMaterial.SearchMaterial" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title><asp:Literal runat="server" Text="<%$ Resources:SearchMaterial.aspx,Title%>" /></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <f:PageManager ID="PageManager1" runat="server">
            </f:PageManager>
            <f:Grid ID="dgvData" runat="server" AllowPaging="True" EnableCheckBoxSelect="True" AllowSorting="True" DataKeyNames="id" EnableMultiSelect="true" EnableSummary="True" EnableRowDoubleClickEvent="True" OnRowDoubleClick="DgvData_RowDoubleClick" EnableRowSelectEvent="True" OnRowSelect="DgvData_RowSelect" OnSort="DgvData_Sort" EnableTextSelection="True">
                <Toolbars>
                    <f:Toolbar ID="Toolbar1" runat="server">
                        <Items>
                            <f:DropDownList ID="cmbBuCode" runat="server" Label="<%$ Resources:SearchMaterial.aspx,Bucode%>" LabelAlign="Right" DataValueField="MappingValue1" DataTextField="MappingValue2" LabelWidth="80px" Width="250px" Required="True">
                            </f:DropDownList>
                            <f:TextBox ID="txtMaterialId" runat="server" Label="<%$ Resources:SearchMaterial.aspx,MaterialId%>" LabelAlign="Right" LabelWidth="60px" Width="150px"></f:TextBox>                            
                            <f:TextBox ID="txtProductionName" runat="server" Label="<%$ Resources:SearchMaterial.aspx,ProductionName%>" LabelAlign="Right" LabelWidth="60px" Width="150px"></f:TextBox>
                            <f:TextBox ID="txtSpecification" runat="server" Label="<%$ Resources:SearchMaterial.aspx,Specification%>" LabelAlign="Right" LabelWidth="60px" Width="150px"></f:TextBox>
                            <f:TextBox ID="txtCreateUserId" runat="server" Label="<%$ Resources:SearchMaterial.aspx,Applicant%>" LabelAlign="Right" LabelWidth="60px" Width="150px"></f:TextBox>
                            <f:DatePicker runat="server" ID="dpStartDate" DateFormatString="yyyy-MM-dd" Label="<%$ Resources:SearchMaterial.aspx,StartDate%>" LabelWidth="60px" Width="150px"> </f:DatePicker>
                            <f:DatePicker runat="server" ID="dpEndDate" CompareControl="dpStartDate" DateFormatString="yyyy-MM-dd" CompareOperator="GreaterThan" CompareMessage="<%$ Resources:SearchMaterial.aspx,EndDateGreaterThanStartDate%>" Label="<%$ Resources:SearchMaterial.aspx,EndDate%>" LabelWidth="60px" Width="150px"></f:DatePicker>
                            <f:Button ID="BtnSearch" runat="server" Icon="SystemSearch" Text="<%$ Resources:SearchMaterial.aspx,Search%>" OnClick="BtnSearch_Click"></f:Button>
                            <f:Button ID="BtnExport" EnableAjax="false" DisableControlBeforePostBack="false" runat="server" Text="<%$ Resources:SearchMaterial.aspx,Export%>" OnClick="BtnExport_Click" IconUrl="~/res/icon/export.png"></f:Button>
                            <f:Button ID="BtnTransfer" runat="server" Icon="BulletLightning" Text="<%$ Resources:SearchMaterial.aspx,Transfer%>" OnClick="BtnTransfer_Click"></f:Button>
                        </Items>
                    </f:Toolbar>
                </Toolbars>
                <Columns>
                    <f:RowNumberField EnablePagingNumber="true" Width="40px"/>
                    <f:BoundField ID="BoundField1" runat="server" SortField="MaterialId" ColumnID="colMaterialId" DataField="MaterialId" HeaderText="<%$ Resources:SearchMaterial.aspx,MaterialId%>" HtmlEncodeFormatString="True" Width="150px">
                    </f:BoundField>
                    <f:BoundField ID="BoundField8" runat="server" SortField="Rev" ColumnID="colRev" DataField="Rev" HeaderText="<%$ Resources:SearchMaterial.aspx,Rev%>" HtmlEncodeFormatString="True" Width="60px">
                    </f:BoundField>
                    <f:BoundField ID="BoundField2" runat="server" SortField="Specification" ColumnID="colSpecification" DataField="Specification" HeaderText="<%$ Resources:SearchMaterial.aspx,Specification%>" HtmlEncodeFormatString="True">
                    </f:BoundField>
                    <f:BoundField ID="BoundField3" runat="server" SortField="ProductionName" ColumnID="colProductionName" DataField="ProductionName" HeaderText="<%$ Resources:SearchMaterial.aspx,ProductionName%>" HtmlEncodeFormatString="True">
                    </f:BoundField>
                    <f:BoundField ID="BoundField5" runat="server" SortField="Status" ColumnID="colStatus" DataField="Status" HeaderText="<%$ Resources:SearchMaterial.aspx,Status%>" HtmlEncodeFormatString="True">
                    </f:BoundField>
                    <f:BoundField ID="BoundField9" runat="server" SortField="BuCode" ColumnID="colBuCode" DataField="BuCode" HeaderText="<%$ Resources:SearchMaterial.aspx,BuCode%>" HtmlEncodeFormatString="True">
                    </f:BoundField>
                    <f:CheckBoxField RenderAsStaticField="true" DataField="IsPostSuccess" HeaderText="<%$ Resources:SearchMaterial.aspx,IsPostSuccess%>" />
                    <f:BoundField ID="BoundField10" runat="server" SortField="CreateUserID" ColumnID="colCreateUserID" DataField="CreateUserID" HeaderText="<%$ Resources:SearchMaterial.aspx,Applicant%>" HtmlEncodeFormatString="True">
                    </f:BoundField>
                    <f:BoundField ID="BoundField11" runat="server" SortField="CreateDateTime" ColumnID="colCreateDateTime" DataField="CreateDateTime" HeaderText="<%$ Resources:SearchMaterial.aspx,ApplyDateTime%>" HtmlEncodeFormatString="True" Width="120px">
                    </f:BoundField>
                    <f:BoundField ID="BoundField4" runat="server" SortField="Remark" ColumnID="colRemark" DataField="Remark" HeaderText="<%$ Resources:SearchMaterial.aspx,Remark%>" HtmlEncodeFormatString="True">
                    </f:BoundField>
                </Columns>
            </f:Grid>
            <f:Grid ID="dgvWorkFlow" runat="server" DataKeyNames="id" EnableSummary="True">
                <Columns>
                    <f:BoundField  ID="BoundField6" runat="server" DataField="Step" ColumnID="colStep" HeaderText="<%$ Resources:SearchMaterial.aspx,Step%>" Width="50px">
                    </f:BoundField>
                    <f:BoundField  ID="BoundField25" runat="server" DataField="FlowNodeName" ColumnID="colFlowNodeName" HeaderText="<%$ Resources:SearchMaterial.aspx,FlowNodeName%>">
                    </f:BoundField>
                    <f:BoundField  ID="BoundField26" runat="server" DataField="Approver" ColumnID="colApprover" HeaderText="<%$ Resources:SearchMaterial.aspx,Approver%>">
                    </f:BoundField>
                    <f:BoundField  ID="BoundField27" runat="server" DataField="ApproverName" ColumnID="colApproverName" HeaderText="<%$ Resources:SearchMaterial.aspx,ApproverName%>">
                    </f:BoundField>
                    <f:BoundField ID="BoundField7" runat="server" ColumnID="colApproveDateTime" DataField="ApproveDateTime" HeaderText="<%$ Resources:SearchMaterial.aspx,ApproveDateTime%>" HtmlEncodeFormatString="True" Width="150px">
                    </f:BoundField>
                    <f:BoundField ID="BoundField28" runat="server" ColumnID="colResult" DataField="Result" HeaderText="<%$ Resources:SearchMaterial.aspx,Result%>" HtmlEncodeFormatString="True">
                    </f:BoundField>
                    <f:BoundField ID="BoundField29" runat="server" ColumnID="colWFRemark" DataField="Remark" HeaderText="<%$ Resources:SearchMaterial.aspx,Comments%>" HtmlEncodeFormatString="True">
                    </f:BoundField>
                </Columns>
            </f:Grid>            
            <f:Window ID="pEdit" runat="server" BodyPadding="5px" Height="150px" IsModal="true" Width="600px" Hidden="True" Layout="Table" TableConfigColumns="2">                 
                <Items>
                    <f:TextBox ID="txtMaterialClass" Readonly="true" runat="server" Label="<%$ Resources:SearchMaterial.aspx,MaterialClass%>" LabelAlign="Right" LabelWidth="120px"></f:TextBox>
                    <f:TextBox ID="txtMainUnit" Readonly="true" runat="server" Label="<%$ Resources:SearchMaterial.aspx,Unit%>" LabelAlign="Right" LabelWidth="120px"></f:TextBox>                    
                    <f:TextBox ID="txtSourceCode" Readonly="true" runat="server" Label="<%$ Resources:SearchMaterial.aspx,SourceCode%>" LabelAlign="Right" LabelWidth="120px"></f:TextBox>
                    <f:TextBox ID="txtGroupCode" Readonly="true" runat="server" Label="<%$ Resources:SearchMaterial.aspx,GroupCode%>" LabelAlign="Right" Text="HZ01" LabelWidth="120px"></f:TextBox>
                    <f:TextBox ID="txtFourthGroupCode" Readonly="true" runat="server" Label="<%$ Resources:SearchMaterial.aspx,FourthGroupCode%>" LabelAlign="Right" LabelWidth="120px"></f:TextBox>
                    <f:TextBox ID="txtABCCode" Readonly="true" runat="server" Label="<%$ Resources:SearchMaterial.aspx,ABCCode%>" LabelAlign="Right" LabelWidth="120px"></f:TextBox>
                    <f:CheckBox ID="cbIsBonded" Readonly="true" runat="server" Label="<%$ Resources:SearchMaterial.aspx,IsBonded%>" LabelAlign="Right" LabelWidth="120px"/>
                    <f:TextBox ID="txtRemark" Readonly="true" runat="server" Label="<%$ Resources:SearchMaterial.aspx,Remark%>" LabelAlign="Right" LabelWidth="120px"></f:TextBox>
                </Items>
            </f:Window>
        </div>
    </form>
</body>
</html>
