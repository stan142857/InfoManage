<%@ Page Title="" Language="C#" MasterPageFile="~/InfoManage.Master" AutoEventWireup="true" CodeBehind="School.aspx.cs" Inherits="InfoManage.WebForm1" Async ="true"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
    .auto-style5 {
        width: 100%;
    }
    .auto-style8 {
        width: 80px;
    }
        .auto-style13 {
            width: 99%;
        }
        .auto-style15 {
            width: 80px;
            height: 23px;
        }
        .auto-style16 {
            height: 23px;
        }
        .auto-style19 {
            width: 198px;
        }
        .auto-style20 {
            width: 198px;
            height: 23px;
        }
        .auto-style21 {
            width: 107px;
        }
        .auto-style22 {
            width: 87%;
        }
        .auto-style23 {
            width: 432px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="auto-style5">
    <tr>
        <td rowspan="5">&nbsp;</td>
        <td class="auto-style23">&nbsp;</td>
        <td>&nbsp;</td>
        <td rowspan="5">&nbsp;</td>
    </tr>
    <tr>
        <td class="auto-style23">&nbsp;</td>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td class="auto-style23">
            <asp:Panel ID="PanelReg" runat="server" Height="123px" Width="430px">
                <table class="auto-style13">
                    <tr>
                        <td class="auto-style19">
                            <asp:Label ID="LblMail" runat="server" Text="邮箱"></asp:Label>
                        </td>
                        <td class="auto-style8">
                            <asp:TextBox ID="TBAccount" runat="server" TextMode="Email"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Button ID="BtnEmailCom" runat="server" OnClick="BtnEmailCom_Click" Text="发送验证码" Width="140px" />
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style19">
                            <asp:Label ID="LblPassword" runat="server" Text="密码"></asp:Label>
                        </td>
                        <td class="auto-style8">
                            <asp:TextBox ID="TBPassword" runat="server" TextMode="Password"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style20">
                            <asp:Label ID="LblPasswordCom" runat="server" Text="确认密码"></asp:Label>
                        </td>
                        <td class="auto-style15">
                            <asp:TextBox ID="TBPassCom" runat="server" TextMode="Password"></asp:TextBox>
                        </td>
                        <td class="auto-style16">
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style20">确认验证码</td>
                        <td class="auto-style15">
                            <asp:TextBox ID="TBEmailRanCom" runat="server" TextMode="Number"></asp:TextBox>
                        </td>
                        <td class="auto-style16"></td>
                    </tr>
                    <tr>
                        <td class="auto-style19">
                            <asp:Label ID="LabelTip" runat="server" BackColor="Red" BorderColor="White" Visible="False"></asp:Label>
                        </td>
                        <td class="auto-style8">
                            <asp:Button ID="BtnReg" runat="server" OnClick="BtnLogin_Click" Text="注册" />
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                </table>
            </asp:Panel>
        </td>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td class="auto-style23">&nbsp;</td>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td class="auto-style23">
            <asp:Panel ID="PanelLogin" runat="server">
                <table align="center" class="auto-style22">
                    <tr>
                        <td colspan="2">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 登陆</td>
                    </tr>
                    <tr>
                        <td class="auto-style21">邮箱</td>
                        <td>
                            <asp:TextBox ID="LOTBAccount" runat="server" TextMode="Email"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style21">密码</td>
                        <td>
                            <asp:TextBox ID="LOTBPass" runat="server" TextMode="Password"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style21">验证码</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style21">&nbsp;</td>
                        <td>
                            <asp:Button ID="BtnLogin" runat="server" OnClick="BtnLogin_Click1" Text="登陆" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </td>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td colspan="4">
            <asp:Panel ID="PanelInfo" runat="server">
            </asp:Panel>
            <asp:Panel ID="Panel" runat="server">
            </asp:Panel>
            <asp:Panel ID="PanelCache" runat="server" Visible="False">
                <asp:Label ID="LabelEmaRegCache" runat="server" Text="邮箱注册验证码缓冲"></asp:Label>
                <asp:Label ID="LabelEmaRegCache2" runat="server" Text="邮箱注册验证码缓冲"></asp:Label>
            </asp:Panel>
        </td>
    </tr>
    </table>
</asp:Content>
