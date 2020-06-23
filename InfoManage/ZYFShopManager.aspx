<%@ Page Title="" Language="C#" MaintainScrollPositionOnPostback=true MasterPageFile="~/InfoManage.Master" AutoEventWireup="true" CodeBehind="ZYFShopManager.aspx.cs" Inherits="InfoManage.ZYFShopManager" EnableEventValidation="false"%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
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
        .auto-style35 {
            width: 136px;
        }
        .auto-style36 {
            width: 102%;
            height: 404px;
        }
        .auto-style37 {
            height: 10px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <table class="auto-style16">
        <tr>
            <td class="auto-style35">
    <asp:Label ID="Label4" runat="server" Text="Label"></asp:Label>
            </td>
            <td>
                <asp:TreeView ID="TreeView1" runat="server" ExpandDepth="0" ImageSet="Events" OnSelectedNodeChanged="TreeView1_SelectedNodeChanged">
                    <HoverNodeStyle Font-Underline="False" ForeColor="Red" />
                    <Nodes>
                        <asp:TreeNode Text="本药房" Value="操作">
                            <asp:TreeNode Text="查询总订单" Value="查询总订单"></asp:TreeNode>
                            <asp:TreeNode Text="添加药品" Value="新建节点"></asp:TreeNode>
                        </asp:TreeNode>
                        <asp:TreeNode Text="日历" Value="日历">
                            <asp:TreeNode Text="新建日历" Value="新建日历"></asp:TreeNode>
                            <asp:TreeNode Text="修改日历" Value="修改日历"></asp:TreeNode>
                        </asp:TreeNode>
                        <asp:TreeNode Text="退出" Value="退出"></asp:TreeNode>
                    </Nodes>
                    <NodeStyle Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" HorizontalPadding="5px" NodeSpacing="0px" VerticalPadding="0px" />
                    <ParentNodeStyle Font-Bold="False" />
                    <SelectedNodeStyle Font-Underline="True" HorizontalPadding="0px" VerticalPadding="0px" />
                </asp:TreeView>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="auto-style20">
        <tr>
            <td class="auto-style21" rowspan="3">
                <table class="auto-style15">
                    <tr>
                        <td colspan="2">
                <asp:GridView ID="GVCalendar" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" CellPadding="4" GridLines="Horizontal" Height="16px" Width="407px" OnRowCommand="GVCalendar_RowCommand" HorizontalAlign="Justify" AllowPaging="True" OnPageIndexChanging="GVCalendar_PageIndexChanging" PageSize="6">
                    <Columns>
                        <asp:BoundField DataField="RLID" HeaderText="ID" />
                        <asp:TemplateField HeaderText="月内">
                            <ItemTemplate>
                                <table class="auto-style16">
                                    <tr>
                                        <td class="auto-style37">
                                            <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%# Eval("SerialN") %>' Text='<%# Eval("USERID") %>'></asp:LinkButton>
                                        </td>
                                        <td class="auto-style37">
                                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Eval("RLText") %>'></asp:TextBox>
                                        </td>
                                        <td class="auto-style37">
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
                <asp:Panel ID="PanelFork" runat="server" Width="264px" HorizontalAlign="Justify" Visible="False" Height="159px">
                    <table class="auto-style5" border="1">
                        <tr>
                            <td style="background-color: #0000FF">源ID：</td>
                            <td style="background-color: #0000FF">
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
                            <td style="background-color: #0000FF">内容：</td>
                            <td style="background-color: #0000FF">
                                <asp:TextBox ID="TextBox6" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="background-color: #0000FF">公开：</td>
                            <td style="background-color: #0000FF">
                                <asp:TextBox ID="TextBox7" runat="server" TextMode="Number" ToolTip="0为私有，1为公开">0</asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="background-color: #0000FF">&nbsp;</td>
                            <td style="background-color: #0000FF">
                                <asp:Button ID="Btnconfirmfork" runat="server" OnClick="Btnconfirmfork_Click" Text="确认" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="PanelAddCal" runat="server" Width="266px" Height="103px" Direction="LeftToRight" HorizontalAlign="Left" Visible="False">
                    <table class="auto-style5" border="1">
                        <tr>
                            <td class="auto-style8" style="background-color: #0000FF">账户：</td>
                            <td class="auto-style10" style="background-color: #0000FF">
                                <asp:Label ID="LabelAccount" runat="server" Text="Label"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="background-color: #0000FF">内容：</td>
                            <td class="auto-style11" style="background-color: #0000FF">
                                <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="background-color: #0000FF">公开：</td>
                            <td class="auto-style11" style="background-color: #0000FF">
                                <asp:TextBox ID="TextBox4" runat="server" TextMode="Number" ToolTip="0为私有，1为公开">0</asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style12" style="background-color: #0000FF"></td>
                            <td class="auto-style13" style="background-color: #0000FF">
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
                </td>
            <td rowspan="2" class="auto-style23">&nbsp;</td>
            <td class="auto-style23" rowspan="2">
                                <table class="auto-stylequery" __designer:mapid="44e">
                                    <tr __designer:mapid="44f">
                                        <td style="border-color: #000000; border-style: double; border-width: inherit;" __designer:mapid="450" colspan="3" class="auto-style1center">&nbsp;</td>
                                    </tr>
                                    <tr __designer:mapid="463">
                                        <td __designer:mapid="464" style="padding: inherit; border-color: #000000; border-style: double; border-width: inherit; background-color: #FFFFFF;" colspan="2">
                                            <asp:Button ID="BtnQuerykc" runat="server" OnClick="BtnQuerykc_Click" Text="查询药品" Height="34px" Width="79px" />
                                        </td>
                                        <td __designer:mapid="465" class="auto-style16" style="padding: inherit; border-color: #000000; border-style: double; border-width: inherit; background-color: #FFFFFF;">
                                            <asp:DropDownList ID="DDLYP" runat="server" DataSourceID="YPSelect" DataTextField="YPName" DataValueField="YPName" Height="19px" Width="96px">
                                                <asp:ListItem>药品</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr __designer:mapid="457">
                                        <td style="padding: inherit; border-color: #000000; border-style: double; border-width: inherit; background-color: #FFFFFF;" __designer:mapid="458" class="auto-style12" colspan="2">
                                            <asp:Button ID="BtnQueryddUnfinish" runat="server" OnClick="BtnQueryddUnfinish_Click" Text="查询订单" Height="36px" Width="82px" />
                                        </td>
                                        <td class="auto-style16" style="padding: inherit; border-color: #000000; border-style: double; border-width: inherit; background-color: #FFFFFF;" __designer:mapid="45e">
                                            <asp:DropDownList ID="DDLDD" runat="server" Height="17px" Width="93px">
                                                <asp:ListItem></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr __designer:mapid="460">
                                        <td style="padding: inherit; border-color: #000000; border-style: double; border-width: inherit; background-color: #FFFFFF;" __designer:mapid="461" class="auto-style34">
                                            <asp:Button ID="BtnAddYP" runat="server" OnClick="BtnAddYP_Click" Text="药品添加" Height="30px" Width="80px" />
                                        </td>
                                        <td style="padding: inherit; border-color: #000000; border-style: double; border-width: inherit; background-color: #FFFFFF;" colspan="2" __designer:mapid="461">
                                            <asp:Button ID="BtnQueryddAll" runat="server" Height="32px" OnClick="BtnQueryddAll_Click" Text="查询总订单" Width="78px" />
                                        </td>
                                    </tr>
                                </table>
                                            <asp:SqlDataSource ID="YPSelect" runat="server" ConnectionString="<%$ ConnectionStrings:InfoManageConnectionString %>" SelectCommand="SELECT DISTINCT [YPName] FROM [ZYF_JG_YP]"></asp:SqlDataSource>
                    </td>
        </tr>
        <tr>
            <td class="auto-style19">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" Visible="False">
                    <ContentTemplate>
                        <asp:ScriptManager ID="ScriptManager1" runat="server">
                        </asp:ScriptManager>
                        <table class="auto-style36">
                            <tr>
                                <td class="auto-style1center" style="background-color: #00FFFF; ">添加新药</td>
                                <td class="auto-style12" style="background-color: #00FFFF; ">
                                    <asp:TextBox ID="TBName" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style1center" style="background-color: #00FFFF; ">
                                    <asp:LinkButton ID="LBtnAddOldYP" runat="server" OnClick="LBtnAddOldYP_Click">入库已有</asp:LinkButton>
                                </td>
                                <td class="auto-style12" style="background-color: #00FFFF; ">
                                    <asp:DropDownList ID="DDLOldYP" runat="server" AutoPostBack="True" OnTextChanged="DDLOldYP_TextChanged">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style1center" style="background-color: #00FFFF; ">种类</td>
                                <td class="auto-style12" style="background-color: #00FFFF; ">
                                    <asp:TextBox ID="TBKind" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style1center" style="background-color: #00FFFF; ">入库数目</td>
                                <td class="auto-style12" style="background-color: #00FFFF; ">
                                    <asp:TextBox ID="TBINKCNUM" runat="server" TextMode="Number"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style1center" style="background-color: #00FFFF; ">入库药房</td>
                                <td class="auto-style12" style="background-color: #00FFFF; ">
                                    <asp:DropDownList ID="DropDownList4" runat="server" DataSourceID="RuKuYaoFang" DataTextField="YFID" DataValueField="YFID">
                                    </asp:DropDownList>
                                    <asp:SqlDataSource ID="RuKuYaoFang" runat="server" ConnectionString="<%$ ConnectionStrings:InfoManage %>" SelectCommand="SELECT * FROM [ZYF_QT_YFPZ] WHERE ([USERID] = @USERID)">
                                        <SelectParameters>
                                            <asp:QueryStringParameter Name="USERID" QueryStringField="userid" Type="String" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style1center" style="background-color: #00FFFF; ">价格</td>
                                <td class="auto-style12" style="background-color: #00FFFF; ">
                                    <asp:TextBox ID="TBINKCPRICE" runat="server" TextMode="Number"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style1center" style="background-color: #00FFFF; ">供应来源</td>
                                <td class="auto-style12" style="background-color: #00FFFF; ">
                                    <asp:DropDownList ID="DDLGYS" runat="server" DataSourceID="GYSID" DataTextField="GYSID" DataValueField="GYSID">
                                    </asp:DropDownList>
                                    <asp:SqlDataSource ID="GYSID" runat="server" ConnectionString="<%$ ConnectionStrings:InfoManage %>" SelectCommand="SELECT DISTINCT [GYSID] FROM [ZYF_JG_GYS]"></asp:SqlDataSource>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style1center" style="background-color: #00FFFF; ">保存方式</td>
                                <td class="auto-style12" style="background-color: #00FFFF; ">
                                    <asp:TextBox ID="TBSave" runat="server" ToolTip="干燥存储"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style1center" style="background-color: #00FFFF; ">条形码</td>
                                <td class="auto-style12" style="background-color: #00FFFF; ">
                                    <asp:TextBox ID="TBBarCode" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style1center" style="background-color: #00FFFF; ">售卖方式</td>
                                <td class="auto-style12" style="background-color: #00FFFF; ">
                                    <asp:TextBox ID="TBSell" runat="server" ToolTip="袋"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style1center" style="background-color: #00FFFF; ">可共享</td>
                                <td class="auto-style12" style="background-color: #00FFFF; ">
                                    <asp:DropDownList ID="DDLINKCGX" runat="server">
                                        <asp:ListItem>0</asp:ListItem>
                                        <asp:ListItem>1</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style1center" style="background-color: #00FFFF; ">
                                    <asp:Label ID="LabelADDYP" runat="server" ForeColor="Red" Visible="False"></asp:Label>
                                </td>
                                <td class="auto-style12" style="background-color: #00FFFF; ">
                                    <asp:Button ID="BtnAdd" runat="server" OnClick="BtnAdd_Click" Text="确认" />
                                </td>
                            </tr>
                        </table>
                        <br />
                    </ContentTemplate>
                </asp:UpdatePanel>
                    <asp:Panel ID="PanelYPDetailsChange" runat="server" Visible="False">
                        <table class="auto-style16">
                            <tr>
                                <td style="background-image: url('Img/毕设.png')">库存ID</td>
                                <td style="background-image: url('Img/毕设.png')">
                                    <asp:Label ID="LabelYFKCID" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="background-image: url('Img/毕设.png')">药房ID</td>
                                <td style="background-image: url('Img/毕设.png')">
                                    <asp:Label ID="LabelYFID" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="background-image: url('Img/毕设.png')">药品名</td>
                                <td style="background-image: url('Img/毕设.png')">
                                    <asp:Label ID="LabelYPName" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="background-image: url('Img/毕设.png')">数目</td>
                                <td style="background-image: url('Img/毕设.png')">
                                    <asp:TextBox ID="TBNumber" runat="server" Height="16px" TextMode="Number" Width="95px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="background-image: url('Img/毕设.png')">价格</td>
                                <td style="background-image: url('Img/毕设.png')">
                                    <asp:TextBox ID="TBPrice" runat="server" Height="16px" TextMode="Number" Width="96px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="background-image: url('Img/毕设.png')">药店取货</td>
                                <td style="background-image: url('Img/毕设.png')">
                                    <asp:CheckBox ID="CBConfirm" runat="server" Text="共享确认" OnCheckedChanged="CBConfirm_CheckedChanged" />
                                </td>
                            </tr>
                            <tr>
                                <td style="background-image: url('Img/毕设.png')">开单</td>
                                <td style="background-image: url('Img/毕设.png')">
                                    <asp:CheckBox ID="CBSell" runat="server" OnCheckedChanged="CBSell_CheckedChanged" Text="开单确认" />
                                </td>
                            </tr>
                            <tr>
                                <td style="background-image: url('Img/毕设.png')">
                                    <asp:Label ID="Labeltip" runat="server" Visible="False"></asp:Label>
                                </td>
                                <td style="background-image: url('Img/毕设.png')">
                                    <asp:Button ID="BtnYPDetailsChange" runat="server" OnClick="BtnYPDetailsChange_Click" Text="修改/开方" />
                                </td>
                            </tr>
                        </table>
                </asp:Panel>
                    <asp:GridView ID="GVYPDetail" runat="server" AutoGenerateColumns="False" BackColor="#CCCCCC" BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="0" CellSpacing="1" ForeColor="Black" HorizontalAlign="Justify" Width="100%" AllowPaging="True" OnRowCommand="GVYPDetail_RowCommand">
                        <AlternatingRowStyle BackColor="#FF9999" BorderColor="#CC6699" BorderStyle="Dotted" />
                        <Columns>
                            <asp:BoundField DataField="YFID" HeaderText="药房ID" >
                            <HeaderStyle BorderStyle="None" />
                            </asp:BoundField>
                            <asp:BoundField DataField="YPName" HeaderText="药品名" />
                            <asp:BoundField DataField="YFKCResidueNum" HeaderText="数目" />
                            <asp:BoundField DataField="YFKCPrice" HeaderText="价格" />
                            <asp:BoundField DataField="YFKCBuildTime" HeaderText="入库时间" />
                            <asp:BoundField DataField="YFKCQualityGuauaPeriod" HeaderText="保质期" />
                            <asp:BoundField DataField="YFKCOutsideTimeUp" HeaderText="更新" />
                            <asp:BoundField DataField="YFKCExceedQGP" HeaderText="过期" />
                            <asp:BoundField DataField="YFKCShare" HeaderText="共享" />
                            <asp:TemplateField HeaderText="操作">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LBtnBuy" runat="server" CommandArgument='<%# Eval("SerialN") %>' CommandName='<%# Eval("YFKCID") %>' Text="修改"></asp:LinkButton>
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
                <asp:GridView ID="GVDD" runat="server" AllowPaging="True" AutoGenerateColumns="False" CellPadding="4" HorizontalAlign="Center" Visible="False" Width="100%" ForeColor="#333333" GridLines="None" OnRowCommand="GVDD_RowCommand" OnPageIndexChanging="GVDD_PageIndexChanging" PageSize="20">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField DataField="DDID" HeaderText="序号" />
                        <asp:BoundField DataField="USERID" HeaderText="用户ID" />
                        <asp:BoundField DataField="YPName" HeaderText="药品名" />
                        <asp:BoundField DataField="YPID" HeaderText="药品ID" />
                        <asp:BoundField DataField="DDNum" HeaderText="订单数目" />
                        <asp:BoundField DataField="DDPrice" HeaderText="订单价格" />
                        <asp:TemplateField HeaderText="处理">
                            <ItemTemplate>
                                <asp:LinkButton ID="LBtnDeal" runat="server" CommandArgument='<%# Eval("SerialN") %>' Text='<%# Eval("DDFinish") %>' ToolTip="0为未处理"></asp:LinkButton>
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
                <asp:Label ID="LBLYPRightCheck" runat="server" Visible="False"></asp:Label>
            </td>
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
