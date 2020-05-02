<%@ Page Title="" Language="C#" MasterPageFile="~/InfoManage.Master" AutoEventWireup="true" CodeBehind="ZYFYH.aspx.cs" Inherits="InfoManage.ZYFYH" %>
<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style1{
            width:50%;
        }
        .styleLU{
            width:50%;
        }
        .style1LD{
            width:50%;
        }
        .style1RU{
            width:50%;
        }
        .style1RD{
            width:50%;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <table class="auto-style5">
        <tr>
            <td>
    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
            </td>
            <td>
                <asp:TreeView ID="TreeView1" runat="server" OnSelectedNodeChanged="TreeView1_SelectedNodeChanged">
                    <Nodes>
                        <asp:TreeNode Text="订购" Value="订购"></asp:TreeNode>
                        <asp:TreeNode Text="查看订单" Value="查看订单"></asp:TreeNode>
                    </Nodes>
                </asp:TreeView>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="PanelAll" runat="server" Height="530px">
        <br />
        <br />
        <br />
        <table class="auto-style5">
            <tr>
                <td class="styleLU">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" Height="100%" OnRowCommand="GridView1_RowCommand" Width="100%">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:BoundField DataField="SerialN" HeaderText="序号" />
                            <asp:BoundField DataField="YPName" HeaderText="药品名称" />
                            <asp:BoundField DataField="YFName" HeaderText="药房名称" />
                            <asp:BoundField DataField="YFKCPrice" HeaderText="价格" />
                            <asp:TemplateField HeaderText="数目">
                                <ItemTemplate>
                                    <asp:TextBox ID="TextBox1" runat="server" Height="16px" TextMode="Number" Width="90px">1</asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="购买">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%# Eval("SerialN") %>'>购买</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
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
                    <asp:Label ID="Label2" runat="server" Text="Label" Visible="False"></asp:Label>
                    <br />
                </td>
                <td class="style1RU">
                    <asp:GridView ID="GVDD" runat="server" AllowPaging="True" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" Height="100%" HorizontalAlign="Center" OnRowCommand="GVDD_RowCommand" Visible="False" Width="100%">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:BoundField DataField="SerialN" HeaderText="序号" />
                            <asp:BoundField DataField="YPName" HeaderText="药品名" />
                            <asp:BoundField DataField="YPID" HeaderText="药品ID" />
                            <asp:BoundField DataField="DDNum" HeaderText="订单数目" />
                            <asp:BoundField DataField="DDPrice" HeaderText="订单价格" />
                            <asp:TemplateField HeaderText="处理">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LBtnDeal" runat="server" CommandArgument='<%# Eval("DDID") %>'>退订</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EditRowStyle BackColor="#7C6F57" />
                        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#E3EAEB" />
                        <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#F8FAFA" />
                        <SortedAscendingHeaderStyle BackColor="#246B61" />
                        <SortedDescendingCellStyle BackColor="#D4DFE1" />
                        <SortedDescendingHeaderStyle BackColor="#15524A" />
                    </asp:GridView>
                    <asp:Label ID="Label3" runat="server" Text="Label" Visible="False"></asp:Label>
                    <br />
                </td>
            </tr>
            <tr>
                <td class="style1LD">&nbsp;</td>
                <td class="style1RD">&nbsp;</td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
