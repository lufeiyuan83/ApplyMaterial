<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Employee.aspx.cs" Inherits="ERP.HRM.Employee" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title><asp:Literal runat="server" Text="<%$ Resources:Employee.aspx,Title%>" /></title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="RegionPanel1" runat="server" />
        <f:RegionPanel ID="RegionPanel1" runat="server" ShowBorder="false">
            <Regions>
                <f:Region runat="server" Split="true" Width="200px" Title="<%$ Resources:Employee.aspx,Organization%>" EnableCollapse="true" ID="leftPanel" Position="Left" Layout="Fit">
                    <Items>
                        <f:Tree ID="tData" runat="server" OnNodeCommand="tData_NodeCommand">
                            <Nodes>
                            </Nodes>  
                        </f:Tree>
                    </Items>
                </f:Region>
                <f:Region runat="server" ID="mainRegion" Position="Center" ShowHeader="false">
                    <Items>
                        <f:Panel runat="server" Title="<%$ Resources:Employee.aspx,Department%>">
                            <Items>
                                <f:Grid ID="dgvDepartment" runat="server" AllowPaging="True" EnableCheckBoxSelect="True" AllowSorting="True" DataKeyNames="id" EnableMultiSelect="false" EnableSummary="True" OnSort="DgvDepartment_Sort" EnableRowSelectEvent="True" OnRowSelect="DgvDepartment_RowSelect">
                                    <Columns>
                                        <f:RowNumberField EnablePagingNumber="true"/>
                                        <f:BoundField ID="BoundField2" runat="server" SortField="ApplicationOrg" ColumnID="colApplicationOrg" DataField="ApplicationOrg" HeaderText="<%$ Resources:Employee.aspx,ApplicationOrg%>" HtmlEncodeFormatString="True" >
                                        </f:BoundField>
                                        <f:BoundField ID="BoundField6" runat="server" SortField="DepartmentCode" ColumnID="colDepartmentCode" DataField="DepartmentCode" HeaderText="<%$ Resources:Employee.aspx,DepartmentCode%>" HtmlEncodeFormatString="True" >
                                        </f:BoundField>
                                        <f:BoundField ID="BoundField1" runat="server" SortField="DepartmentName" ColumnID="colDepartmentName" DataField="DepartmentName" HeaderText="<%$ Resources:Employee.aspx,DepartmentName%>" HtmlEncodeFormatString="True" >
                                        </f:BoundField>
                                        <f:BoundField ID="BoundField4" runat="server" SortField="Remark" ColumnID="colDepartmentRemark" DataField="Remark" HeaderText="<%$ Resources:Employee.aspx,Remark%>" HtmlEncodeFormatString="True">
                                        </f:BoundField>
                                    </Columns>
                                </f:Grid>
                            </Items>
                        </f:Panel>
                        <f:Panel runat="server" Title="<%$ Resources:Employee.aspx,Employee%>">
                            <Items>
                                <f:Grid ID="dgvEmployee" runat="server" AllowPaging="True" EnableCheckBoxSelect="True" AllowSorting="True" DataKeyNames="id,DepartmentCode" EnableMultiSelect="false" EnableSummary="True" EnableRowDoubleClickEvent="True"  EnableTextSelection="True" OnRowDoubleClick="DgvEmployee_RowDoubleClick" OnSort="DgvEmployee_Sort" OnRowCommand="DgvEmployee_RowCommand">
                                    <Toolbars>
                                        <f:Toolbar ID="Toolbar3" runat="server">
                                            <Items>
                                                <f:Button ID="BtnAdd" runat="server" Icon="Add" Text="<%$ Resources:Employee.aspx,Add%>" OnClick="BtnAdd_Click"></f:Button>
                                            </Items>
                                        </f:Toolbar>
                                    </Toolbars>
                                    <Columns>
                                        <f:LinkButtonField TextAlign="Center" ConfirmText="<%$ Resources:Employee.aspx,DeleteRecordQuestion%>" Icon="Delete" ConfirmTarget="Top" ColumnID="colDelete" HeaderText="&nbsp;" Width="40px" CommandName="Delete" />                        
                                        <f:BoundField ID="BoundField5" runat="server" SortField="EmployeeID" ColumnID="colEmployeeID" DataField="EmployeeID" HeaderText="<%$ Resources:Employee.aspx,EmployeeID%>" HtmlEncodeFormatString="True" >
                                        </f:BoundField>
                                        <f:BoundField ID="BoundField7" runat="server" SortField="EmployeeName" ColumnID="colEmployeeName" DataField="EmployeeName" HeaderText="<%$ Resources:Employee.aspx,EmployeeName%>" HtmlEncodeFormatString="True" >
                                        </f:BoundField>
                                        <f:BoundField ID="BoundField8" runat="server" SortField="EmployeeEnglishName" ColumnID="colEmployeeEnglishName" DataField="EmployeeEnglishName" HeaderText="<%$ Resources:Employee.aspx,EmployeeEnglishName%>" HtmlEncodeFormatString="True" >
                                        </f:BoundField>
                                        <f:BoundField ID="BoundField3" runat="server" SortField="PositionName" ColumnID="colPositionName" DataField="PositionName" HeaderText="<%$ Resources:Employee.aspx,PositionName%>" HtmlEncodeFormatString="True" >
                                        </f:BoundField>
                                        <f:BoundField ID="BoundField9" runat="server" SortField="EmpoyeeGender" ColumnID="colEmpoyeeGender" DataField="EmpoyeeGender" HeaderText="<%$ Resources:Employee.aspx,EmpoyeeGender%>" HtmlEncodeFormatString="True" >
                                        </f:BoundField>
                                        <f:BoundField ID="BoundField10" runat="server" SortField="EmployeeEmail" ColumnID="colEmployeeEmail" DataField="EmployeeEmail" HeaderText="<%$ Resources:Employee.aspx,EmployeeEmail%>" HtmlEncodeFormatString="True" Width="250px" >
                                        </f:BoundField>
                                        <f:BoundField ID="BoundField11" runat="server" SortField="EmployeePhone" ColumnID="colEmployeePhone" DataField="EmployeePhone" HeaderText="<%$ Resources:Employee.aspx,EmployeePhone%>" HtmlEncodeFormatString="True" >
                                        </f:BoundField>
                                        <f:BoundField ID="BoundField13" runat="server" SortField="Remark" ColumnID="colRemark" DataField="Remark" HeaderText="<%$ Resources:Employee.aspx,Remark%>" HtmlEncodeFormatString="True">
                                        </f:BoundField>
                                    </Columns>
                                </f:Grid>  
                            </Items>
                        </f:Panel>                     
                        <f:Window ID="wData" runat="server" BodyPadding="5px" IsModal="true" Width="600px" Hidden="True" Layout="Table" TableConfigColumns="2">
                            <Toolbars>
                                <f:Toolbar ID="Toolbar4" runat="server">
                                    <Items>
                                        <f:Button ID="BtnSave" runat="server" Icon="Disk" Text="<%$ Resources:Employee.aspx,Save%>" OnClick="BtnSave_Click"></f:Button>
                                    </Items>
                                </f:Toolbar>
                            </Toolbars>                    
                            <Items>
                                <f:TextBox ID="txtEmployeeID" runat="server" Label="<%$ Resources:Employee.aspx,EmployeeID%>" LabelAlign="Right" Required="True" RequiredMessage="<%$ Resources:Employee.aspx,EmployeeIDCanNotBlank%>"></f:TextBox>
                                <f:TextBox ID="txtEmployeeName" runat="server" Label="<%$ Resources:Employee.aspx,EmployeeName%>" LabelAlign="Right" Required="True" RequiredMessage="<%$ Resources:Employee.aspx,EmployeeNameCanNotBlank%>"></f:TextBox>
                                <f:TextBox ID="txtEmployeeEnglishName" runat="server" Label="<%$ Resources:Employee.aspx,EmployeeEnglishName%>" LabelAlign="Right"></f:TextBox>
                                <f:DropDownList ID="cmbPositionName" runat="server" Label="<%$ Resources:Employee.aspx,PositionName%>" LabelAlign="Right" ></f:DropDownList>
                                <f:DropDownList ID="cmbEmpoyeeGender" runat="server" Label="<%$ Resources:Employee.aspx,EmpoyeeGender%>" LabelAlign="Right" Required="True" RequiredMessage="<%$ Resources:Employee.aspx,EmpoyeeGenderCanNotBlank%>">
                                    <f:ListItem Selected="True" Text="<%$ Resources:Employee.aspx,Male%>" Value="Male" />
                                    <f:ListItem Text="<%$ Resources:Employee.aspx,Female%>" Value="Female" />
                                </f:DropDownList>
                                <f:TextBox ID="txtEmployeeEmail" runat="server" Label="<%$ Resources:Employee.aspx,EmployeeEmail%>" LabelAlign="Right" Required="True" RequiredMessage="<%$ Resources:Employee.aspx,EmployeeEmailCanNotBlank%>"></f:TextBox>
                                <f:TextBox ID="txtEmployeePhone" runat="server" Label="<%$ Resources:Employee.aspx,EmployeePhone%>" LabelAlign="Right"></f:TextBox>
                                <f:TextBox ID="txtRemark" runat="server" Label="<%$ Resources:Employee.aspx,Remark%>" LabelAlign="Right"></f:TextBox>
                            </Items>
                        </f:Window>
                    </Items>
                </f:Region>
            </Regions>
        </f:RegionPanel>
    </form>
</body>
</html>