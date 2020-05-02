<%@ Page Title="" Language="C#" MasterPageFile="~/InfoManage.Master" AutoEventWireup="true" CodeBehind="ZYFLogin.aspx.cs" Inherits="InfoManage.administor" Async="true"%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style7 {
            height: 32px;
        }
        .auto-stylelogo {
            width:100%;
            height:100%;
        }
        .auto-style8 {
            width: 16px;
        }
        .center {
            width: 100%;
            margin:0 auto;
        }
        .auto-style10 {
            margin: 0 auto;
        }
        .auto-style11 {
            height: 25px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    </asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="auto-style5">
    <tr>
        <td>
            <table class="auto-style5" style="background-color:cyan; background-image: url('Img/backgroundAll.jpg');">
                <tr>
                    <td rowspan="3">&nbsp;</td>
                    <td colspan="6" rowspan="3" class="auto-stylelogo">
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" Height="238px" Width="689px">
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                    <asp:BoundField DataField="SerialN" HeaderText="序号" />
                                    <asp:BoundField DataField="YPName" HeaderText="药品名称" />
                                    <asp:BoundField DataField="YFName" HeaderText="药房名称" />
                                    <asp:BoundField DataField="YFKCPrice" HeaderText="价格" />
                                </Columns>
                                <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                                <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                                <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                                <SortedAscendingCellStyle BackColor="#FDF5AC" />
                                <SortedAscendingHeaderStyle BackColor="#4D0000" />
                                <SortedDescendingCellStyle BackColor="#FCF6C0" />
                                <SortedDescendingHeaderStyle BackColor="#820000" />
                            </asp:GridView>
                    </td>
                    <td colspan="2">&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style8">
                        <asp:Panel ID="PanelRegister" runat="server" CssClass="auto-style10" Width="154px">
                            <table class="auto-style5">
                                <tr>
                                    <td>
                                        <asp:TextBox ID="TBEmail" runat="server" TextMode="Email">.com</asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style11">
                                        <asp:Button ID="Btncon" runat="server" OnClick="Btncon_Click" Text="发送验证码" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="TBPass" runat="server" TextMode="Password"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="TBPasscon" runat="server" TextMode="Password"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="TBPassNum" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Button ID="BtnReg" runat="server" OnClick="BtnReg_Click" Text="注册" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:LinkButton ID="LBBACK" runat="server" OnClick="LinkButton1_Click" Visible="False" ForeColor="White">注册完成，点击登陆</asp:LinkButton>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                    <td>
                        <asp:Panel ID="PanelLogin" runat="server">
                            <table class="auto-style5" style="text-align:center">
                                <tr>
                                    <td class="auto-style7">
                                        <asp:TextBox ID="TBUSERID" runat="server" Height="22px" Width="156px">AD00001</asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style4">
                                        <asp:TextBox ID="TBPassword" runat="server" Height="22px" TextMode="Password" Width="156px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Button ID="BtnLogin" runat="server" OnClick="BtnLogin_Click" Text="登陆" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:LinkButton ID="LBtnRegister" runat="server" OnClick="LBtnRegister_Click" ForeColor="White">没有账号？点击注册</asp:LinkButton>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="LblTip" runat="server" Visible="False" ForeColor="White"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style8">&nbsp;</td>
                    <td rowspan="4">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td rowspan="8">&nbsp;</td>
                    <td class="auto-style8">&nbsp;</td>
                    <td rowspan="4">
                        <asp:Panel ID="PanelPassGet" runat="server">
                        </asp:Panel>
                    </td>
                    <td rowspan="4">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td class="auto-style8" rowspan="2">&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td class="auto-style8" rowspan="5">&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td rowspan="4">&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>
                        <asp:Label ID="Labeltip" runat="server" Text="Label" Visible="False"></asp:Label>
                    </td>
                    <td>&nbsp;</td>
                </tr>
            </table>
        </td>
    </tr>
    </table>
</asp:Content>
