<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApproveMaterial.aspx.cs" Inherits="ERP.ApplyMaterial.ApproveMaterial" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title><asp:Literal runat="server" Text="<%$ Resources:ApproveMaterial.aspx,Title%>" /></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <f:PageManager ID="PageManager1" runat="server">
            </f:PageManager>
            <f:Grid ID="DgvData" runat="server" AllowPaging="True" EnableCheckBoxSelect="True" AllowSorting="True" DataKeyNames="id" EnableMultiSelect="true" EnableSummary="True" EnableRowDoubleClickEvent="True" OnRowDoubleClick="DgvData_RowDoubleClick" EnableRowSelectEvent="True" OnRowSelect="DgvData_RowSelect" OnSort="DgvData_Sort" EnableTextSelection="True">
                <Toolbars>
                    <f:Toolbar ID="Toolbar1" runat="server">
                        <Items>
                            <f:Button ID="BtnApprove" runat="server" Icon="Accept" Text="<%$ Resources:ApproveMaterial.aspx,Approve%>" OnClick="BtnApprove_Click"></f:Button>
                            <f:Button ID="BtnReject" runat="server" Icon="Cross" Text="<%$ Resources:ApproveMaterial.aspx,Reject%>" OnClick="BtnReject_Click"></f:Button>
                            <f:TextBox ID="txtComments" runat="server" Label="<%$ Resources:ApproveMaterial.aspx,Comments%>" LabelAlign="Right" LabelWidth="60px"></f:TextBox>
                        </Items>
                    </f:Toolbar>
                </Toolbars>
                <Columns>
                    <f:RowNumberField EnablePagingNumber="true"/>
                    <f:BoundField ID="BoundField1" runat="server" SortField="MaterialId" ColumnID="colMaterialId" DataField="MaterialId" HeaderText="<%$ Resources:ApproveMaterial.aspx,MaterialId%>" HtmlEncodeFormatString="True" Width="150px">
                    </f:BoundField>
                    <f:BoundField ID="BoundField5" runat="server" SortField="Rev" ColumnID="colRev" DataField="Rev" HeaderText="<%$ Resources:ApproveMaterial.aspx,Rev%>" HtmlEncodeFormatString="True" Width="60px">
                    </f:BoundField>
                    <f:BoundField ID="BoundField2" runat="server" SortField="Specification" ColumnID="colSpecification" DataField="Specification" HeaderText="<%$ Resources:ApproveMaterial.aspx,Specification%>" HtmlEncodeFormatString="True">
                    </f:BoundField>
                    <f:BoundField ID="BoundField3" runat="server" SortField="ProductionName" ColumnID="colProductionName" DataField="ProductionName" HeaderText="<%$ Resources:ApproveMaterial.aspx,ProductionName%>" HtmlEncodeFormatString="True">
                    </f:BoundField>
                    <f:BoundField ID="BoundField4" runat="server" SortField="Remark" ColumnID="colRemark" DataField="Remark" HeaderText="<%$ Resources:ApproveMaterial.aspx,Remark%>" HtmlEncodeFormatString="True">
                    </f:BoundField>
                </Columns>
            </f:Grid>
            <f:Grid ID="dgvWorkFlow" runat="server" DataKeyNames="id" EnableSummary="True">
                <Columns>
                    <f:BoundField  ID="BoundField6" runat="server" DataField="Step" ColumnID="colStep" HeaderText="<%$ Resources:ApproveMaterial.aspx,Step%>" Width="50px">
                    </f:BoundField>
                    <f:BoundField  ID="BoundField25" runat="server" DataField="FlowNodeName" ColumnID="colFlowNodeName" HeaderText="<%$ Resources:ApproveMaterial.aspx,FlowNodeName%>">
                    </f:BoundField>
                    <f:BoundField  ID="BoundField26" runat="server" DataField="Approver" ColumnID="colApprover" HeaderText="<%$ Resources:ApproveMaterial.aspx,Approver%>">
                    </f:BoundField>
                    <f:BoundField  ID="BoundField27" runat="server" DataField="ApproverName" ColumnID="colApproverName" HeaderText="<%$ Resources:ApproveMaterial.aspx,ApproverName%>">
                    </f:BoundField>
                    <f:BoundField ID="BoundField7" runat="server" ColumnID="colApproveDateTime" DataField="ApproveDateTime" HeaderText="<%$ Resources:ApproveMaterial.aspx,ApproveDateTime%>" HtmlEncodeFormatString="True" Width="150px">
                    </f:BoundField>
                    <f:BoundField ID="BoundField28" runat="server" ColumnID="colResult" DataField="Result" HeaderText="<%$ Resources:ApproveMaterial.aspx,Result%>" HtmlEncodeFormatString="True">
                    </f:BoundField>
                    <f:BoundField ID="BoundField29" runat="server" ColumnID="colWFRemark" DataField="Remark" HeaderText="<%$ Resources:ApproveMaterial.aspx,Comments%>" HtmlEncodeFormatString="True">
                    </f:BoundField>
                </Columns>
            </f:Grid>            
            <f:Window ID="pEdit" runat="server" BodyPadding="5px" Height="150px" IsModal="true" Width="600px" Hidden="True" Layout="Table" TableConfigColumns="2">                 
                <Items>
                    <f:TextBox ID="txtMaterialClass" Readonly="true" runat="server" Label="<%$ Resources:ApproveMaterial.aspx,MaterialClass%>" LabelAlign="Right" LabelWidth="120px"></f:TextBox>
                    <f:TextBox ID="txtMainUnit" Readonly="true" runat="server" Label="<%$ Resources:ApproveMaterial.aspx,Unit%>" LabelAlign="Right" LabelWidth="120px"></f:TextBox>                    
                    <f:TextBox ID="txtSourceCode" Readonly="true" runat="server" Label="<%$ Resources:ApproveMaterial.aspx,SourceCode%>" LabelAlign="Right" LabelWidth="120px"></f:TextBox>
                    <f:TextBox ID="txtGroupCode" Readonly="true" runat="server" Label="<%$ Resources:ApproveMaterial.aspx,GroupCode%>" LabelAlign="Right" Text="HZ01" LabelWidth="120px"></f:TextBox>
                    <f:TextBox ID="txtFourthGroupCode" Readonly="true" runat="server" Label="<%$ Resources:ApproveMaterial.aspx,FourthGroupCode%>" LabelAlign="Right" LabelWidth="120px"></f:TextBox>
                    <f:TextBox ID="txtABCCode" Readonly="true" runat="server" Label="<%$ Resources:ApproveMaterial.aspx,ABCCode%>" LabelAlign="Right" LabelWidth="120px"></f:TextBox>
                    <f:CheckBox ID="cbIsBonded" Readonly="true" runat="server" Label="<%$ Resources:ApproveMaterial.aspx,IsBonded%>" LabelAlign="Right" LabelWidth="120px"/>
                    <f:TextBox ID="txtRemark" Readonly="true" runat="server" Label="<%$ Resources:ApproveMaterial.aspx,Remark%>" LabelAlign="Right" LabelWidth="120px"></f:TextBox>
                </Items>
            </f:Window>
        </div>
    </form>
</body>
</html>
