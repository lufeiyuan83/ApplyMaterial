<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ERP.Login" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title><asp:Literal runat="server" Text="<%$ Resources:Title%>" /></title>
</head>
<body style="background-image: url('res/loading/bg.png'); height:100%">
    <div style="height:422.4px; width: 553.8px; top: 25%; left: 20%; position: fixed;">
        <img src="res/loading/pic.png" style="height:100%;width:100%" />
    </div>   
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" runat="server"/>
        <f:Window ID="Window1" runat="server" Title="<%$ Resources:Title %>" IsModal="false" EnableClose="false" Width="350px">
            <Items>
                <f:SimpleForm ID="SimpleForm1" runat="server" ShowBorder="false" BodyPadding="10px" LabelWidth="80px" ShowHeader="false">
                    <Items>
                        <f:TextBox ID="txtUserName" Label="<%$ Resources:Username %>" Required="true" runat="server" RequiredMessage="<%$ Resources:UsernameCanNotBlank %>" ShowRedStar="True">
                        </f:TextBox>
                        <f:TextBox ID="txtPassword" Label="<%$ Resources:Password %>" TextMode="Password" Required="true" runat="server" RequiredMessage="<%$ Resources:PasswordCanNotBlank %>" ShowRedStar="True">
                        </f:TextBox>
                        <f:TextBox ID="txtCaptcha" Label="<%$ Resources:Captcha %>" Required="true" runat="server" MaxLength="6" MinLength="6" RequiredMessage="<%$ Resources:CaptchaCanNotBlank %>" ShowRedStar="True" MaxLengthMessage="<%$ Resources:CaptchaLength %>" MinLengthMessage="<%$ Resources:CaptchaLength %>">
                        </f:TextBox>
                        <f:Panel CssStyle="padding-left:60px;" ShowBorder="false" ShowHeader="false" runat="server">
                            <Items>                                
                                <f:Image ID="imgCaptcha" CssStyle="float:left;width:110px;" runat="server"></f:Image>
                                <f:LinkButton CssStyle="float:left;margin-top:8px;" ID="BtnRefresh" Text="<%$ Resources:Refresh %>" runat="server" OnClick="BtnRefresh_Click"></f:LinkButton>
                                <f:LinkButton CssStyle="float:left;margin-top:8px;margin-left:8px" ID="BtnForgotPassword" Text="<%$ Resources:ForgotPassword %>" Enabled="false" runat="server" OnClick="BtnForgotPassword_Click"></f:LinkButton>
                            </Items>
                        </f:Panel>
                        <f:Panel CssStyle="padding-left:65px; " ShowBorder="false" ShowHeader="false" runat="server" Layout="HBox" >
                            <Items>
                                <f:LinkButton ID="LinkButton1" runat="server" Text="简体中文" Label="zh_CN" ShowLabel="False" OnClientClick="clickLinkButton('zh_CN');" Width="80px" ></f:LinkButton>
                                <f:LinkButton ID="LinkButton2" runat="server" Text="繁體中文" Label="zh_TW" ShowLabel="False" OnClientClick="clickLinkButton('zh_TW');" Width="80px"></f:LinkButton>
                                <f:LinkButton ID="LinkButton3" runat="server" Text="English" Label="en" ShowLabel="False" OnClientClick="clickLinkButton('en');" Width="80px"></f:LinkButton>
                            </Items>
                        </f:Panel>
                    </Items>
                </f:SimpleForm>
            </Items>
            <Toolbars>
                <f:Toolbar ID="Toolbar1" runat="server" Position="Bottom" ToolbarAlign="Right">
                    <Items>
                        <f:Button ID="BtnLogin" Text="<%$ Resources:Login %>" Type="Submit" ValidateForms="SimpleForm1" ValidateTarget="Top" runat="server" OnClick="BtnLogin_Click">
                        </f:Button>
                    </Items>
                </f:Toolbar>
            </Toolbars>
        </f:Window>
        <f:Window ID="wForgotPassword" Title="<%$ Resources:FindPassword %>" runat="server" BodyPadding="10px" Height="200px" IsModal="true" Hidden="True">                 
            <Items>                
                <f:TextBox  ID="txtEmail" Label="<%$ Resources:Email %>" runat="server" Required="True" RequiredMessage="<%$ Resources:EmailCanNotBlank %>" Enabled="false">
                </f:TextBox>
                <f:TextBox  ID="txtCiphertext" Label="<%$ Resources:SecretKey %>" runat="server" Required="True" RequiredMessage="<%$ Resources:SecretKeyCanNotBlank %>" ShowRedStar="True" TabIndex="1" FocusOnPageLoad="True">
                </f:TextBox>
                <f:TextBox ID="txtNewPassword" Label="<%$ Resources:NewPassword %>" Required="true" runat="server" RequiredMessage="<%$ Resources:NewPasswordCanNotBlank %>" ShowRedStar="True" TabIndex="2" TextMode="Password">
                </f:TextBox>
                <f:TextBox ID="txtConfirmPassword" Label="<%$ Resources:ConfirmPassword %>" Required="true" runat="server" RequiredMessage="<%$ Resources:ComfirmPasswordCanNotBlank %>" ShowRedStar="True" TabIndex="3" TextMode="Password">
                </f:TextBox>
            </Items>
            <Toolbars>
                <f:Toolbar ID="Toolbar2" runat="server" Position="Bottom" ToolbarAlign="Right">
                    <items>
                        <f:Button ID="BtnSendCiphertext" runat="server" Text="<%$ Resources:SendSecretKey %>" OnClick="BtnSendCiphertext_Click"></f:Button>
                        <f:Button ID="BtnOK" runat="server" Text="<%$ Resources:OK %>" OnClick="BtnOK_Click"></f:Button>
                    </items>
                </f:Toolbar>
            </Toolbars>
        </f:Window>      
    </form>
    <script>
        function clickLinkButton(label) {  
            F.cookie('Language', label, {
                expires: 100  // 单位：天
            });
            top.window.location.reload();
        }
    </script>
</body>
</html>
