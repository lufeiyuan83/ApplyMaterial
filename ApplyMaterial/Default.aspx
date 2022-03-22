<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ERP.Default" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title><asp:Literal runat="server" Text="<%$ Resources:Title%>" /></title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="RegionPanel1" runat="server"></f:PageManager>
        <f:RegionPanel ID="RegionPanel1" ShowBorder="false" runat="server">
            <Regions>
                <f:Region ID="topPanel" Height="60px" ShowBorder="false" ShowHeader="false" Position="Top" Layout="Fit" runat="server">
                    <Toolbars>
                        <f:Toolbar ID="Toolbar1" Position="Bottom" runat="server">
                            <Items>
                                <f:ToolbarFill runat="server" />
                                <f:Button ID="BtnChangePassword" runat="server" Icon="Key" ToolTip="<%$ Resources:ChangePassword%>" OnClick="BtnChangePassword_Click">
                                </f:Button>
                                <f:ToolbarSeparator runat="server" />
                                <f:Button ID="btnHelp" EnablePostBack="false" Icon="Help" Text="<%$ Resources:Help%>" runat="server">
                                </f:Button>
                                <f:ToolbarSeparator runat="server" />
                                <f:Button ID="BtnExit" runat="server" Icon="UserRed" Text="<%$ Resources:Exit%>" ConfirmText="<%$ Resources:QuestionExit%>" OnClick="BtnExit_Click">
                                </f:Button>
                            </Items>
                        </f:Toolbar>
                    </Toolbars>
                    <Items>
                        <f:ContentPanel ShowBorder="false" ShowHeader="false" BodyStyle="background-color:#1C3E7E;" ID="ContentPanel1" runat="server">
                            <div style="font-size: 20px; color:White; font-weight:bold; padding: 5px 10px; ">
                                <a class="title" href="Default.aspx" style="color:White;text-decoration:none;"><asp:Literal runat="server" Text="<%$ Resources:Title%>" /></a>
                            </div>
                        </f:ContentPanel>
                    </Items>
                </f:Region>
                <f:Region ID="leftPanel" Split="true" Width="200px" ShowHeader="true" Title="<%$ Resources:Menu%>" EnableCollapse="true" Layout="Fit" Position="Left" runat="server">
                    <Items>
                        <f:Tree runat="server" ID="leftMenuTree">     
                            <Nodes>
                            </Nodes>    
                        </f:Tree>  
                    </Items>
                </f:Region>
                <f:Region ID="mainRegion" ShowHeader="false" Layout="Fit" Position="Center" runat="server">
                    <Items>
                        <f:TabStrip ID="mainTabStrip" EnableTabCloseMenu="true" ShowBorder="false" runat="server">
                            <Tabs>
                                <f:Tab ID="Tab1" Title="<%$ Resources:Home %>" Layout="Fit" Icon="House" runat="server">
                                    <Items>
                                         <f:Panel ID="Panel2" runat="server" ShowBorder="True" Layout="Region">
                                            <Items>
                                                <f:Panel ID="Panel1" runat="server" Layout="Region" RegionSplit="True" RegionPosition="Center">
                                                    <Items>
                                                        <f:Panel ID="Panel4" runat="server" Layout="VBox">
                                                            <Items>
                                                                <f:Panel ID="Panel3" BoxFlex="1" runat="server" ShowHeader="true" Title="<%$ Resources:MyToDoList%>" AutoScroll="True">
                                                                    <Items>                                                                        
                                                                        <f:Tree ID="tToDoList" runat="server" ShowHeader="False">

                                                                        </f:Tree>
                                                                    </Items>
                                                                </f:Panel>
                                                                <f:Panel ID="Panel5" BoxFlex="1" runat="server" ShowHeader="true" Title="<%$ Resources:MyDoneList%>" AutoScroll="True">
                                                                    <Items>                                                                                                                                           
                                                                        <f:Tree ID="tDone" runat="server" ShowHeader="False">

                                                                        </f:Tree>
                                                                    </Items>
                                                                </f:Panel>                                                                                                                
                                                            </Items>
                                                        </f:Panel>                                                       
                                                    </Items>
                                                </f:Panel>
                                            </Items>
                                        </f:Panel>
                                    </Items>
                                </f:Tab>
                            </Tabs>
                        </f:TabStrip>
                    </Items>
                </f:Region>
                <f:Region ID="bottomPanel" RegionPosition="Bottom" ShowBorder="false" ShowHeader="false" EnableCollapse="false" runat="server" Layout="Fit">
                    <Items>
                        <f:ContentPanel ID="ContentPanel3" runat="server" ShowBorder="false" ShowHeader="false">
                            <table class="bottomtable">
                                <tr>
                                    <td style="width: 100px;"><asp:Literal runat="server" Text="<%$ Resources:Rev%>" />:1.0.0</td>
                                    <td style="width: 150px;"><asp:Literal runat="server" Text="<%$ Resources:Administrator%>" />:卢飞远</td>
                                    <td style="width: 150px;"><asp:Literal runat="server" Text="<%$ Resources:Telephone%>" />:18823886287</td>
                                    <td style="text-align: center;">Copyright © 2020-2021 </td>
                                    <td style="width: 300px; text-align: center;"><f:Label ID="lblLogon" runat="server" Label="<%$ Resources:Operator%>" LabelAlign="Left" LabelWidth="60px"></f:Label></td>
                                </tr>
                            </table>
                        </f:ContentPanel>
                    </Items>
                </f:Region>
            </Regions>
        </f:RegionPanel>
        <f:Window ID="wChangePassword" Title="<%$ Resources:ChangePassword %>" runat="server" BodyPadding="10px" IsModal="true" Hidden="True">                 
            <Items>
                <f:TextBox  ID="txtOldPassword" Label="<%$ Resources:OldPassword %>" runat="server" Required="True" RequiredMessage="<%$ Resources:OldPasswordCanNotBlank %>" ShowRedStar="True" TabIndex="1" TextMode="Password" FocusOnPageLoad="True" LabelWidth="120px">
                </f:TextBox>
                <f:TextBox ID="txtNewPassword" Label="<%$ Resources:NewPassword %>" Required="true" runat="server" RequiredMessage="<%$ Resources:NewPasswordCanNotBlank %>" ShowRedStar="True" TabIndex="2" TextMode="Password" LabelWidth="120px">
                </f:TextBox>
                <f:TextBox ID="txtConfirmPassword" Label="<%$ Resources:ConfirmPassword %>" Required="true" runat="server" RequiredMessage="<%$ Resources:ConfirmPasswordCanNotBlank %>" ShowRedStar="True" TabIndex="3" TextMode="Password" LabelWidth="120px">
                </f:TextBox>
            </Items>
            <Toolbars>
                <f:Toolbar ID="Toolbar2" runat="server" Position="Bottom" ToolbarAlign="Right">
                    <items>
                        <f:Button ID="BtnOK" runat="server" Text="<%$ Resources:OK %>" OnClick="BtnOK_Click"></f:Button>
                    </items>
                </f:Toolbar>
            </Toolbars>
        </f:Window>
    </form>    
    <script>
        var menuClientID = '<%= leftMenuTree.ClientID %>';
        var toDoListClientID = '<%= tToDoList.ClientID %>';
        var doneClientID = '<%= tDone.ClientID %>';
        var tabStripClientID = '<%= mainTabStrip.ClientID %>';
        // 页面控件初始化完毕后，会调用用户自定义的onReady函数
        F.ready(function () {

            // 初始化主框架中的树(或者Accordion+Tree)和选项卡互动，以及地址栏的更新
            // treeMenu： 主框架中的树控件实例，或者内嵌树控件的手风琴控件实例
            // mainTabStrip： 选项卡实例
            // createToolbar： 创建选项卡前的回调函数（接受tabConfig参数）
            // updateLocationHash: 切换Tab时，是否更新地址栏Hash值
            // refreshWhenExist： 添加选项卡时，如果选项卡已经存在，是否刷新内部IFrame
            // refreshWhenTabChange: 切换选项卡时，是否刷新内部IFrame
            F.util.initTreeTabStrip(F(menuClientID), F(tabStripClientID), null, true, false, false);
            F.util.initTreeTabStrip(F(toDoListClientID), F(tabStripClientID), null, true, false, false);
            F.util.initTreeTabStrip(F(doneClientID), F(tabStripClientID), null, true, false, false);
        });
    </script>
</body>
</html>
