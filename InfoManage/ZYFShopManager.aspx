<%@ Page Title="" Language="C#" MasterPageFile="~/InfoManage.Master" AutoEventWireup="true" CodeBehind="ZYFShopManager.aspx.cs" Inherits="InfoManage.ZYFShopManager" EnableEventValidation="false"%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style7 {
            width: 148px;
        }
        .auto-stylemiddle {
            width: 70%;
        }
        .auto-styleleft {
            width: 20%;
            left:auto;
            top:0px;
        }
        .auto-style8 {
            height: 22px;
        }
        .auto-style10 {
            height: 22px;
            width: 158px;
        }
        .auto-style11 {
            width: 158px;
        }
        .auto-style1center {
            left:50%
        }
        .auto-style12 {
            height: 27px;
        }
        .auto-style13 {
            width: 158px;
            height: 27px;
        }
        .auto-style15 {
            width: 94%;
            height: 8px;
        }
        .auto-style5 {
            width: 100%;
        }
        .auto-style16 {
            width: 100%;
        }
        .auto-stylequery {
            width: 60%;
        }
        .auto-styleRL {
            width: 40%;
            top:0px;
            left:0px;
        }
        .auto-styleManageDeal {
            width: 20%;
            top:0px;
            right:0px;
        }
        .auto-style19 {
            width: 70%;
            height: 573px;
        }
        .auto-style20 {
            height: 714px;
        }
        .auto-style21 {
            width: 20%;
            left: auto;
            top: 0px;
            height: 617px;
        }
        .auto-style22 {
            width: 70%;
            height: 17px;
        }
        .auto-style23 {
            height: 594px;
        }
        .auto-style29 {
            width: 70%;
            height: 23px;
        }
        .auto-style30 {
            height: 39px;
        }
        .auto-style31 {
            height: 23px;
        }
        .auto-style33 {
            left: auto;
            top: 0px;
        }
        .auto-style34 {
            width: 297px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <asp:Label ID="Label4" runat="server" Text="Label"></asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="auto-style20">
        <tr>
            <td class="auto-style21" rowspan="3">
                <table class="auto-style15">
                    <tr>
                        <td colspan="2">
                <asp:GridView ID="GVCalendar" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" CellPadding="4" GridLines="Horizontal" Height="190px" Width="381px" OnRowCommand="GVCalendar_RowCommand" HorizontalAlign="Justify">
                    <Columns>
                        <asp:BoundField DataField="RLID" HeaderText="ID" />
                        <asp:TemplateField HeaderText="一月展" FooterText="---">
                            <EditItemTemplate>
                                <table class="auto-style5">
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label2" runat="server" Text='<%# Eval("USERID") %>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextBox2" runat="server" Text='<%# Eval("RLText") %>'></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <table class="auto-style5">
                                    <tr>
                                        <td>
                                            <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# Eval("USERID") %>' CommandArgument='<%# Eval("SerialN") %>' ToolTip="点击修改，其他人日历可用fork"></asp:LinkButton>
                                        </td>
                                        <td class="auto-style7">
                                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Eval("RLText") %>' Height="18px" Width="160px"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label3" runat="server" Text='<%# Eval("RLBuildTime") %>'></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle BackColor="White" ForeColor="#333333" />
                    <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="White" ForeColor="#333333" />
                    <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F7F7F7" />
                    <SortedAscendingHeaderStyle BackColor="#487575" />
                    <SortedDescendingCellStyle BackColor="#E5E5E5" />
                    <SortedDescendingHeaderStyle BackColor="#275353" />
                </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="BtnInsert" runat="server" OnClick="BtnInsert_Click" Text="插入日历" />
                        </td>
                        <td>
                            <asp:Button ID="BtnFork" runat="server" OnClick="BtnFork_Click" Text="FORK来源" />
                        </td>
                    </tr>
                </table>
                <asp:Panel ID="PanelFork" runat="server" Width="231px" HorizontalAlign="Justify" Visible="False">
                    <table class="auto-style5" border="1">
                        <tr>
                            <td>源ID：</td>
                            <td>
                                <asp:DropDownList ID="DropDownList2" runat="server" DataSourceID="RLID" DataTextField="RLID" DataValueField="RLID">
                                    <asp:ListItem>AD00001</asp:ListItem>
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="RLID" runat="server" ConnectionString="<%$ ConnectionStrings:InfoManage %>" SelectCommand="SELECT [RLID], [USERID] FROM [ZYF_QT_RL] WHERE ([RLSYS] = @RLSYS)">
                                    <SelectParameters>
                                        <asp:Parameter DefaultValue="1" Name="RLSYS" Type="Int32" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
                                <asp:DropDownList ID="DropDownList3" runat="server" DataSourceID="RLID" DataTextField="USERID" DataValueField="USERID">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>内容：</td>
                            <td>
                                <asp:TextBox ID="TextBox6" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>公开：</td>
                            <td>
                                <asp:TextBox ID="TextBox7" runat="server" TextMode="Number" ToolTip="0为私有，1为公开">0</asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td>
                                <asp:Button ID="Btnconfirmfork" runat="server" OnClick="Btnconfirmfork_Click" Text="确认" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="PanelAddCal" runat="server" Width="231px" Height="110px" Direction="LeftToRight" HorizontalAlign="Left" Visible="False">
                    <table class="auto-style5" border="1">
                        <tr>
                            <td class="auto-style8">账户：</td>
                            <td class="auto-style10">
                                <asp:Label ID="LabelAccount" runat="server" Text="Label"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>内容：</td>
                            <td class="auto-style11">
                                <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>公开：</td>
                            <td class="auto-style11">
                                <asp:TextBox ID="TextBox4" runat="server" TextMode="Number" ToolTip="0为私有，1为公开">0</asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style12"></td>
                            <td class="auto-style13">
                                <asp:Button ID="BtnAddCal" runat="server" OnClick="BtnAddCal_Click" Text="确认添加" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <br />
                <br />
                <br />
            </td>
            <td class="auto-style22">
                &nbsp;</td>
            <td rowspan="2" class="auto-style23">&nbsp;</td>
            <td class="auto-style23" rowspan="2">
                                <table class="auto-stylequery" __designer:mapid="44e">
                                    <tr __designer:mapid="44f">
                                        <td style="border-color: #000000; border-style: double; border-width: inherit;" __designer:mapid="450" colspan="3" class="auto-style1center">&nbsp;</td>
                                    </tr>
                                    <tr __designer:mapid="463">
                                        <td __designer:mapid="464" style="border-color: #000000; border-style: double; border-width: inherit;" colspan="2">
                                            <asp:DropDownList ID="DDLYP" runat="server" AutoPostBack="True" DataSourceID="YPSelect" DataTextField="YPName" DataValueField="YPName" Height="17px" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" Width="89px">
                                                <asp:ListItem>药品</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:SqlDataSource ID="YPSelect" runat="server" ConnectionString="<%$ ConnectionStrings:InfoManageConnectionString %>" SelectCommand="SELECT DISTINCT [YPName] FROM [ZYF_JG_YP]"></asp:SqlDataSource>
                                        </td>
                                        <td __designer:mapid="465" class="auto-style16" style="border-color: #000000; border-style: double; border-width: inherit;">
                                            <asp:Button ID="BtnQuerykc" runat="server" OnClick="BtnQuerykc_Click" Text="查询药品" />
                                        </td>
                                    </tr>
                                    <tr __designer:mapid="457">
                                        <td style="border-color: #000000; border-style: double; border-width: inherit;" __designer:mapid="458" class="auto-style12" colspan="2">
                                            <asp:DropDownList ID="DDLDD" runat="server" DataSourceID="DDID" DataTextField="DDID" DataValueField="DDID" Height="16px" Width="83px">
                                            </asp:DropDownList>
                                            <asp:SqlDataSource ID="DDID" runat="server" ConnectionString="<%$ ConnectionStrings:InfoManage %>" SelectCommand="SELECT DISTINCT [DDID], [USERID] FROM [ZYF_QT_DD] WHERE ([DDFinish] = @DDFinish)">
                                                <SelectParameters>
                                                    <asp:Parameter DefaultValue="0" Name="DDFinish" Type="Int32" />
                                                </SelectParameters>
                                            </asp:SqlDataSource>
                                        </td>
                                        <td class="auto-style16" style="border-color: #000000; border-style: double; border-width: inherit;" __designer:mapid="45e">
                                            <asp:Button ID="BtnQuerydd" runat="server" OnClick="BtnQuerydd_Click" Text="查询总订单" />
                                        </td>
                                    </tr>
                                    <tr __designer:mapid="460">
                                        <td style="border-color: #000000; border-style: double; border-width: inherit;" __designer:mapid="461" class="auto-style34">&nbsp;</td>
                                        <td style="border-color: #000000; border-style: double; border-width: inherit;" colspan="2" __designer:mapid="461">&nbsp;</td>
                                    </tr>
                                </table>
                    </td>
        </tr>
        <tr>
            <td class="auto-style19">
                <br />
                    <asp:GridView ID="GVYPDetail" runat="server" AutoGenerateColumns="False" BackColor="#CCCCCC" BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" OnRowCommand="GVYPDetail_RowCommand" CellSpacing="2" ForeColor="Black" HorizontalAlign="Justify" Width="100%">
                        <Columns>
                            <asp:BoundField DataField="YFID" HeaderText="药房ID" >
                            <HeaderStyle BorderStyle="None" />
                            </asp:BoundField>
                            <asp:BoundField DataField="YPName" HeaderText="药品名称" />
                            <asp:BoundField DataField="YFKCResidueNum" HeaderText="药房库存" />
                            <asp:BoundField DataField="YFKCPrice" HeaderText="药价" />
                            <asp:BoundField DataField="YFKCBuildTime" HeaderText="入库时间" />
                            <asp:BoundField DataField="YFKCQualityGuauaPeriod" HeaderText="保质期" />
                            <asp:BoundField DataField="YFKCOutsideTimeUp" HeaderText="最新取出" />
                            <asp:BoundField DataField="YFKCExceedQGP" HeaderText="过期" />
                            <asp:BoundField DataField="YFKCShare" HeaderText="可共享" />
                            <asp:TemplateField HeaderText="购买">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LBtnBuy" runat="server" CommandArgument='<%# Eval("SerialN") %>'>可购买</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="#CCCCCC" />
                        <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
                        <RowStyle BackColor="White" />
                        <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                        <SortedAscendingHeaderStyle BackColor="#808080" />
                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                        <SortedDescendingHeaderStyle BackColor="#383838" />
                    </asp:GridView>
                <asp:GridView ID="GVDD" runat="server" AllowPaging="True" AutoGenerateColumns="False" CellPadding="4" HorizontalAlign="Center" Visible="False" Width="100%" ForeColor="#333333" GridLines="None" OnRowCommand="GVDD_RowCommand">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField DataField="DDID" HeaderText="序号" />
                        <asp:BoundField DataField="USERID" HeaderText="用户ID" />
                        <asp:BoundField DataField="YPID" HeaderText="药品ID" />
                        <asp:BoundField DataField="DDNum" HeaderText="订单数目" />
                        <asp:BoundField DataField="DDPrice" HeaderText="订单价格" />
                        <asp:TemplateField HeaderText="处理">
                            <ItemTemplate>
                                <asp:LinkButton ID="LBtnDeal" runat="server" CommandArgument='<%# Eval("SerialN") %>'>完成</asp:LinkButton>
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
            </td>
        </tr>
        <tr>
            <td class="auto-style29">
                &nbsp;</td>
            <td class="auto-style30">&nbsp;</td>
            <td class="auto-style31">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style33" colspan="4">
                <br />
                <br />
            </td>
        </tr>
        </table>
</asp:Content>
