<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Department.aspx.cs" Inherits="ERP.HRM.Department" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title><asp:Literal runat="server" Text="<%$ Resources:Department.aspx,Title%>" /></title>
</head>
<body>
    <form id="frmDepartment" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
        <f:RegionPanel ID="Panel1" runat="server" ShowBorder="false">
            <Regions>
                <f:Region runat="server" Split="true" Width="200px" Title="<%$ Resources:Department.aspx,Organization%>" EnableCollapse="true" ID="leftPanel" Position="Left" Layout="Fit">
                    <Items>
                        <f:Tree ID="tData" runat="server" OnNodeCommand="tData_NodeCommand">
                            <Nodes>
                            </Nodes>  
                        </f:Tree>
                    </Items>
                </f:Region>
                <f:Region runat="server" ID="mainRegion" Position="Center" Title="<%$ Resources:Department.aspx,Title%>" Layout="Fit">
                    <Items>
                        <f:Grid ID="dgvData" runat="server" AllowPaging="True" EnableCheckBoxSelect="True" AllowSorting="True" DataKeyNames="id" EnableMultiSelect="false" EnableSummary="True" EnableRowDoubleClickEvent="True"  EnableTextSelection="True" OnRowDoubleClick="DgvData_RowDoubleClick" OnSort="DgvData_Sort">
                            <Toolbars>
                                <f:Toolbar ID="Toolbar3" runat="server">
                                    <Items>
                                        <f:Button ID="BtnAdd" runat="server" Icon="Add" Text="<%$ Resources:Department.aspx,Add%>" OnClick="BtnAdd_Click"></f:Button>
                                    </Items>
                                </f:Toolbar>
                            </Toolbars>
                            <Columns>
                                <f:RowNumberField EnablePagingNumber="true"/>
                                <f:BoundField ID="BoundField2" runat="server" SortField="ApplicationOrg" ColumnID="colApplicationOrg" DataField="ApplicationOrg" HeaderText="<%$ Resources:Department.aspx,ApplicationOrg%>" HtmlEncodeFormatString="True" >
                                </f:BoundField>
                                <f:BoundField ID="BoundField6" runat="server" SortField="DepartmentCode" ColumnID="colDepartmentCode" DataField="DepartmentCode" HeaderText="<%$ Resources:Department.aspx,DepartmentCode%>" HtmlEncodeFormatString="True" >
                                </f:BoundField>
                                <f:BoundField ID="BoundField1" runat="server" SortField="DepartmentName" ColumnID="colDepartmentName" DataField="DepartmentName" HeaderText="<%$ Resources:Department.aspx,DepartmentName%>" HtmlEncodeFormatString="True" >
                                </f:BoundField>
                                <f:BoundField ID="BoundField4" runat="server" SortField="Remark" ColumnID="colRemark" DataField="Remark" HeaderText="<%$ Resources:Department.aspx,Remark%>" HtmlEncodeFormatString="True">
                                </f:BoundField>
                            </Columns>
                        </f:Grid>                     
                        <f:Window ID="wData" runat="server" BodyPadding="5px" IsModal="true" Width="600px" Hidden="True" Layout="Table" TableConfigColumns="2">
                            <Toolbars>
                                <f:Toolbar ID="Toolbar4" runat="server">
                                    <Items>
                                        <f:Button ID="BtnSave" runat="server" Icon="Disk" Text="<%$ Resources:Department.aspx,Save%>" OnClick="BtnSave_Click"></f:Button>
                                    </Items>
                                </f:Toolbar>
                            </Toolbars>                    
                            <Items>
                                <f:TextBox ID="txtDepartmentCode" runat="server" Label="<%$ Resources:Department.aspx,DepartmentCode%>" LabelAlign="Right" Required="True" RequiredMessage="<%$ Resources:Department.aspx,DepartmentCodeCanNotBlank%>"></f:TextBox>
                                <f:TextBox ID="txtDepartmentName" runat="server" Label="<%$ Resources:Department.aspx,DepartmentName%>" LabelAlign="Right" Required="True" RequiredMessage="<%$ Resources:Department.aspx,DepartmentNameCanNotBlank%>"></f:TextBox>
                                <f:TextBox ID="txtRemark" runat="server" Label="<%$ Resources:Department.aspx,Remark%>" LabelAlign="Right"></f:TextBox>
                            </Items>
                        </f:Window>
                    </Items>
                </f:Region>
            </Regions>
        </f:RegionPanel>
    </form>
</body>
</html>