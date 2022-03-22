<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Position.aspx.cs" Inherits="ERP.HRM.Position" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title><asp:Literal runat="server" Text="<%$ Resources:Position.aspx,Title%>" /></title>
</head>
<body>
    <form id="frmPosition" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="RegionPanel1" runat="server" />
        <f:RegionPanel ID="RegionPanel1" runat="server" ShowBorder="false">
            <Regions>
                <f:Region runat="server" Split="true" Width="200px" Title="<%$ Resources:Position.aspx,Organization%>" EnableCollapse="true" ID="leftPanel" Position="Left" Layout="Fit">
                    <Items>
                        <f:Tree ID="tData" runat="server" OnNodeCommand="tData_NodeCommand">
                            <Nodes>
                            </Nodes>  
                        </f:Tree>
                    </Items>
                </f:Region>
                <f:Region runat="server" ID="mainRegion" Position="Center" ShowHeader="false">
                    <Items>
                        <f:Panel runat="server" Title="<%$ Resources:Position.aspx,Department%>">
                            <Items>
                                <f:Grid ID="dgvDepartment" runat="server" AllowPaging="True" EnableCheckBoxSelect="True" AllowSorting="True" DataKeyNames="id" EnableMultiSelect="false" EnableSummary="True" OnSort="DgvDepartment_Sort" EnableRowSelectEvent="True" OnRowSelect="DgvDepartment_RowSelect">
                                    <Columns>
                                        <f:RowNumberField EnablePagingNumber="true"/>
                                        <f:BoundField ID="BoundField2" runat="server" SortField="ApplicationOrg" ColumnID="colApplicationOrg" DataField="ApplicationOrg" HeaderText="<%$ Resources:Position.aspx,ApplicationOrg%>" HtmlEncodeFormatString="True" >
                                        </f:BoundField>
                                        <f:BoundField ID="BoundField6" runat="server" SortField="DepartmentCode" ColumnID="colDepartmentCode" DataField="DepartmentCode" HeaderText="<%$ Resources:Position.aspx,DepartmentCode%>" HtmlEncodeFormatString="True" >
                                        </f:BoundField>
                                        <f:BoundField ID="BoundField1" runat="server" SortField="DepartmentName" ColumnID="colDepartmentName" DataField="DepartmentName" HeaderText="<%$ Resources:Position.aspx,DepartmentName%>" HtmlEncodeFormatString="True" >
                                        </f:BoundField>
                                        <f:BoundField ID="BoundField4" runat="server" SortField="Remark" ColumnID="colDepartmentRemark" DataField="Remark" HeaderText="<%$ Resources:Position.aspx,Remark%>" HtmlEncodeFormatString="True">
                                        </f:BoundField>
                                    </Columns>
                                </f:Grid>
                            </Items>
                        </f:Panel>
                        <f:Panel runat="server" Title="<%$ Resources:Position.aspx,Position%>">
                            <Items>
                                <f:Grid ID="dgvPosition" runat="server" AllowPaging="True" EnableCheckBoxSelect="True" AllowSorting="True" DataKeyNames="id" EnableMultiSelect="false" EnableSummary="True" OnSort="DgvPosition_Sort" EnableRowDoubleClickEvent="True" OnRowDoubleClick="DgvPosition_RowDoubleClick">
                                    <Toolbars>
                                        <f:Toolbar ID="Toolbar3" runat="server">
                                            <Items>
                                                <f:Button ID="BtnAdd" runat="server" Icon="Add" Text="<%$ Resources:Position.aspx,Add%>" OnClick="BtnAdd_Click"></f:Button>
                                            </Items>
                                        </f:Toolbar>
                                    </Toolbars>
                                    <Columns>
                                        <f:BoundField ID="BoundField5" runat="server" SortField="PositionCode" ColumnID="colPositionCode" DataField="PositionCode" HeaderText="<%$ Resources:Position.aspx,PositionCode%>" HtmlEncodeFormatString="True" Width="200px" >
                                        </f:BoundField>
                                        <f:BoundField ID="BoundField7" runat="server" SortField="PositionName" ColumnID="colPositionName" DataField="PositionName" HeaderText="<%$ Resources:Position.aspx,PositionName%>" HtmlEncodeFormatString="True" >
                                        </f:BoundField>
                                        <f:BoundField ID="BoundField8" runat="server" SortField="Remark" ColumnID="colRemark" DataField="Remark" HeaderText="<%$ Resources:Position.aspx,Remark%>" HtmlEncodeFormatString="True">
                                        </f:BoundField>
                                    </Columns>
                                </f:Grid>
                            </Items>
                        </f:Panel>
                        <f:Window ID="wData" runat="server" BodyPadding="5px" IsModal="true" Width="600px" Hidden="True" Layout="Table" TableConfigColumns="2">
                            <Toolbars>
                                <f:Toolbar ID="Toolbar4" runat="server">
                                    <Items>
                                        <f:Button ID="BtnSave" runat="server" Icon="Disk" Text="<%$ Resources:Position.aspx,Save%>" OnClick="BtnSave_Click"></f:Button>
                                    </Items>
                                </f:Toolbar>
                            </Toolbars>                    
                            <Items>
                                <f:TextBox ID="txtPositionCode" runat="server" Label="<%$ Resources:Position.aspx,PositionCode%>" LabelAlign="Right" Required="True" RequiredMessage="<%$ Resources:Position.aspx,PositionCodeCanNotBlank%>"></f:TextBox>
                                <f:TextBox ID="txtPositionName" runat="server" Label="<%$ Resources:Position.aspx,PositionName%>" LabelAlign="Right" Required="True" RequiredMessage="<%$ Resources:Position.aspx,PositionNameCanNotBlank%>"></f:TextBox>
                                <f:TextBox ID="txtRemark" runat="server" Label="<%$ Resources:Position.aspx,Remark%>" LabelAlign="Right"></f:TextBox>
                            </Items>
                        </f:Window>
                    </Items>
                </f:Region>
            </Regions>
        </f:RegionPanel>
    </form>
</body>
</html>