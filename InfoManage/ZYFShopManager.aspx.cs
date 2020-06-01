using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InfoManage
{
    public partial class ZYFShopManager : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HideOthers();
            string userid = Request.QueryString["userid"].Trim();
            Label4.Text = "本店"+BindYFNAME(userid)+"\n欢迎您：店员" +userid;
            LabelAccount.Text = Request.QueryString["userid"].Trim();//插入日历中控件
            BindGVYPDetail();
            GVYPDetail.Visible = true;
            BindGVCalendar();
        }
        #region 店铺订单-库存查询
        protected void BtnQuerykc_Click(object sender, EventArgs e)
        {
            GVYPDetail.Visible = true;
            GVDD.Visible = false;

            string userid = Request.QueryString["userid"].Trim();
            string sqls = string.Format(" select YFKCID,YFID,YPName,YFKCResidueNum,YFKCPrice," +
                "YFKCBuildTime,YFKCQualityGuauaPeriod,YFKCOutsideTimeUp,YFKCExceedQGP,YFKCShare," +
                "ROW_NUMBER() OVER(ORDER BY YFID asc) as SerialN from(select * from ZYF_JG_YFKC where YFID" +
                " in (select YFID from ZYF_QT_YFPZ )) as AAAA inner join(select YPName, YPID " +
                "from ZYF_JG_YP where YPName like '%{0}%') as bbbb on AAAA.YPID = bbbb.YPID ", DDLYP.SelectedItem.Text.ToString());

            SqlHelper shr = new SqlHelper();
            DataTable dt = shr.Query(sqls);

            GVYPDetail.DataSource = dt;
            GVYPDetail.DataBind();
            shr.CloseConn();
        }
        #endregion
        #region 查询订单
        protected void BtnQueryddUnfinish_Click(object sender, EventArgs e)
        {
            HideOthers();
            GVYPDetail.Visible = false;
            GVDD.Visible = true;

            string userid = Request.QueryString["userid"].Trim();
            String sql = String.Format("select *,ROW_NUMBER() OVER(ORDER BY YFID asc) as SerialN  from (select * from" +
                " (select YFID Y, USERID u from ZYF_QT_YFPZ where USERID = '{0}')" +
                "as a inner join (select YFID, YFKCID T from ZYF_JG_YFKC)as b " +
                "on a.Y = b.YFID inner join(select * from ZYF_QT_DD where DDFinish = 0)" +
                "as c on b.T = c.YFKCID inner join(select YPID m, YPName from ZYF_JG_YP) as e " +
                "on c.YPID = e.m)as d where d.USERID = '{1}'", Request.QueryString["userid"].ToString(), DDLDD.Text);
            SqlHelper shr = new SqlHelper();
            if (DDLDD.Text.ToString() == "")
            {
                BindDDLDD();
            }
            else
            {
                DataTable dt = shr.Query(sql);
                GVDD.DataSource = dt;
                GVDD.DataBind();
            }
            //合理性检测
            LBLYPRightCheck.Visible = true;
            LBLYPRightCheck.Text = "此用户订单无异常状况";
            shr.CloseConn();
        }
        protected void BtnQueryddAll_Click(object sender, EventArgs e)
        {
            GVDD.Visible = true;
            GVYPDetail.Visible = false;
            //--pz - yfkcid - dduser
            string sql = string.Format("select DDID,USERID,YPName,c.YPID,DDNum,DDPrice,SerialN,DDFinish from(" +
                "select YFID from ZYF_QT_YFPZ where USERID = '{0}') as a " +
                "inner join(select* from ZYF_JG_YFKC )as b " +
                "on a.YFID = b.YFID inner join(select*, ROW_NUMBER() OVER(ORDER BY YFKCID asc) as SerialN  from ZYF_QT_DD)as c " +
                "on b.YFKCID = c.YFKCID inner join(select YPID, YPName from ZYF_JG_YP)as d " +
                "on c.YPID = d.YPID", Request.QueryString["userid"].Trim());
            SqlHelper shr = new SqlHelper();
            DataTable dt = shr.Query(sql);
            GVDD.DataSource = dt;
            GVDD.DataBind();
            shr.CloseConn();
            HideOthers();
            GVDD.Visible = true;
        }

        protected void GVDD_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument) - 1;
            int d = 0;
            string deal = ((LinkButton)(this.GVDD.Rows[index].Cells[6].FindControl("LBtnDeal"))).Text.Trim();
            if(deal=="0")
            {
                d = 1;
            }else
            {
                d = 0;
            }
            string DDID = GVDD.Rows[index].Cells[0].Text;
            string sqlDeal = string.Format("update ZYF_QT_DD set DDFinish = {0} where DDID = '{1}'",d,DDID);
            SqlHelper shr = new SqlHelper();
            shr.ExeNoQuery(sqlDeal);
            shr.CloseConn();

            GVDD.Visible = true;
            GVYPDetail.Visible = false;
        }
        #endregion
        #region 日历
        //add
        protected void BtnInsert_Click(object sender, EventArgs e)
        {
            HideOthers();
            PanelAddCal.Visible = true;
            GVDD.Visible = true;
        }
        //FORK
        protected void BtnFork_Click(object sender, EventArgs e)
        {
            HideOthers();
            PanelFork.Visible = true;
            GVDD.Visible = true;
        }
        //add
        protected void BtnAddCal_Click(object sender, EventArgs e)
        {
            string userid = Request.QueryString["userid"].Trim();
            string sql = string.Format("insert into ZYF_QT_RL values('{0}', '{1}', {2}, GETDATE(), GETDATE())", userid,TextBox3.Text.Trim(), TextBox4.Text.Trim());
            SqlHelper shr = new SqlHelper();
            try
            {
                shr.ExeNoQuery(sql);
                shr.CloseConn();
            }catch(Exception ex)
            {
                TextBox3.Text = "插入成功！";
            }
            shr.CloseConn();
        }
        //FORK
        protected void Btnconfirmfork_Click(object sender, EventArgs e)
        {
            string userid = Request.QueryString["userid"].Trim();
            string RLid = DropDownList2.SelectedItem.Text.ToString();
            string RLUserid = DropDownList3.SelectedItem.Text.ToString();
            string text = TextBox6.Text.ToString().Trim() + "->"+RLid+":"+RLUserid;
            string only = TextBox7.Text.ToString().Trim();

            string sql = string.Format("insert into ZYF_QT_RL values('{0}', '{1}', {2}, " +
                "GETDATE(), GETDATE())", userid, text, only);

            string sqlright = string.Format("select USERID from ZYF_QT_RL where RLSYS = 1 and RLID = '{0}'", RLid);

            SqlHelper shr = new SqlHelper();
            DataTable dt = shr.Query(sqlright);
            //已确认可FORK
            if (dt.Rows.Count > 0)
            {
                shr.ExeNoQuery(sql);
            }
            else
            {
                TextBox6.Text = "";
                TextBox6.Text = "无源对象，请重新检查源ID";
            }
            shr.CloseConn();
        }
        // FORK
        protected void GVCalendar_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            SqlHelper shr = new SqlHelper();
            try
            {
                int index = Convert.ToInt32(e.CommandArgument) - 1;
                string RLID = this.GVCalendar.Rows[index].Cells[0].Text.Trim();
                string RLUserid = ((LinkButton)this.GVCalendar.Rows[index].Cells[1].FindControl("LinkButton1")).Text.Trim();
                string RLText = ((TextBox)this.GVCalendar.Rows[index].Cells[1].FindControl("TextBox1")).Text.Trim();
                string RLBuilTime = ((Label)this.GVCalendar.Rows[index].Cells[1].FindControl("Label3")).Text.Trim();

                string datetimeNow = DateTime.Now.AddDays(-30).ToShortDateString();
                string sqlSelf = string.Format("select ROW_NUMBER() OVER(ORDER BY a.RLID asc) as SerialN,* from (select * from ZYF_QT_RL where USERID = " +
                    "'{0}' OR RLText LIKE '%{1}%') as a inner join(select * from ZYF_QT_RL where" +
                    " RLBuildTime > '{2}') as b on a.RLID = b.RLID", RLUserid, RLUserid, datetimeNow);
                DataTable dt = shr.Query(sqlSelf);
                GVCalendar.DataSource = dt;
                GVCalendar.DataBind();
            }
            catch (Exception)
            {

            }
            shr.CloseConn();
        }
        protected void GVCalendar_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GVCalendar.PageIndex = e.NewPageIndex;
            BindGVCalendar();
        }
        #endregion
        #region 绑定表格
        public void BindGVYPDetail()
        {
            string userid = Request.QueryString["userid"].Trim();
            string sql = string.Format(" select YFKCID,YFID,YPName,YFKCResidueNum,YFKCPrice,YFKCBuildTime,YFKCQualityGuauaPeriod,YFKCOutsideTimeUp,YFKCExceedQGP,YFKCShare,ROW_NUMBER() OVER(ORDER BY YFID asc) as SerialN " +
                " from(select * from ZYF_JG_YFKC where YFID in (select YFID from ZYF_QT_YFPZ where USERID = '{0}')) as AAAA" +
                " inner join(select YPName, YPID from ZYF_JG_YP  ) as bbbb on AAAA.YPID = bbbb.YPID ", userid);

            SqlHelper shr = new SqlHelper();
            DataTable dt = shr.Query(sql);

            GVYPDetail.DataSource = dt;
            GVYPDetail.DataBind();
            shr.CloseConn();
        }
        public void BindGVCalendar()
        {
            string userid = Request.QueryString["userid"].Trim();
            //读取日历系统
            string datetimeNow = DateTime.Now.AddDays(-30).ToShortDateString();
            string systemTip = string.Format("SELECT ROW_NUMBER() OVER(ORDER BY RLID asc) as SerialN,RLID,USERID,RLText,RLBuildTime from ZYF_QT_RL where RLSYS = 1 or USERID ='{0}' and USERID in" +
                " (SELECT USERID from ZYF_QT_RL where RLBuildTime > '{1}' or RLUpdateTime > '{2}')", userid, datetimeNow, datetimeNow);

            SqlHelper shr = new SqlHelper();
            DataTable dt = shr.Query(systemTip);
            GVCalendar.DataSource = dt;
            GVCalendar.DataBind();
            shr.CloseConn();
        }
        public void BindDDLDD()
        {
            string sql = string.Format("select distinct c.USERID from(select YFID, USERID from ZYF_QT_YFPZ where " +
                "USERID = '{0}') as a inner join(select YFID, YFKCID from ZYF_JG_YFKC) as b on a.YFID = " +
                "b.YFID inner join(select * from ZYF_QT_DD where DDFinish = 0) as c on b.YFKCID = c.YFKCID", Request.QueryString["userid"].Trim());
            SqlHelper shr = new SqlHelper();
            DataTable dt = shr.Query(sql);
            DDLDD.DataSource = dt.DefaultView;
            DDLDD.DataTextField = dt.Columns[0].ColumnName;
            DDLDD.DataBind();
            shr.CloseConn();
        }
        #endregion
        #region 隐藏
        public void HideOthers()
        {
            PanelAddCal.Visible = false;
            PanelFork.Visible = false;
            PanelYPDetailsChange.Visible = false;
            UpdatePanel1.Visible = false;
            
            GVDD.Visible = false;
            GVYPDetail.Visible = false;

            LabelADDYP.Visible = false;
            Labeltip.Visible = false;
            LBLYPRightCheck.Visible = false;

            TBName.Enabled = true;
        }
        public string BindYFNAME(string re)
        {
            SqlHelper shr = new SqlHelper();
            string sql = "select YFID from ZYF_QT_YFPZ where USERID='" + re + "'";
            DataTable dt = shr.Query(sql);
            shr.CloseConn();
            if (dt.Rows.Count == 0)
            {
                return "";
            }
            else
            {
                re = dt.Rows[0][0].ToString();
                return re;
            }
        }
        #endregion
        #region 添加药品
        protected void DDLOldYP_TextChanged(object sender, EventArgs e)
        {
            string sql = string.Format("select YPID,YPName,YPKind,GYSID,YPSellUnit,YPSaveStyle,YPBarCode from" +
                " ZYF_JG_YP where YPName = '{0}'", DDLOldYP.Text.ToString());
            SqlHelper shr = new SqlHelper();
            DataTable dt = shr.Query(sql);
            TBName.Text = dt.Rows[0][1].ToString();
            TBName.Enabled = false;
            TBKind.Text = dt.Rows[0][2].ToString();
            DDLGYS.Text = dt.Rows[0][3].ToString();
            TBSell.Text = dt.Rows[0][4].ToString();
            TBSave.Text = dt.Rows[0][5].ToString();
            TBBarCode.Text = dt.Rows[0][6].ToString();

            shr.CloseConn();
            HideOthers();
            UpdatePanel1.Visible = true;
        }
        //入库已有
        protected void LBtnAddOldYP_Click(object sender, EventArgs e)
        {
            string sql = "select distinct YPName from ZYF_JG_YP";
            SqlHelper shr = new SqlHelper();
            DataTable dt = shr.Query(sql);
            DDLOldYP.DataSource = dt.DefaultView;
            DDLOldYP.DataTextField = dt.Columns[0].ColumnName;
            DDLOldYP.DataBind();
            shr.CloseConn();

            TBName.Enabled = false;

            HideOthers();
            UpdatePanel1.Visible = true;
        }
        //入库新药
        protected void BtnAddYP_Click(object sender, EventArgs e)
        {
            HideOthers();
            UpdatePanel1.Visible = true;
        }
        protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
        {
            if (TreeView1.SelectedValue == "查询总订单")
            {
                GVDD.Visible = true;
                GVYPDetail.Visible = false;

                string sql = string.Format("select * from (select *, " +
                    "ROW_NUMBER() OVER(ORDER BY DDID asc) as SerialN from ZYF_QT_DD WHERE USERID like" +
                    "  '{0}') as aa inner join(select YPName, YPID from ZYF_JG_YP)as bb on aa.YPID =" +
                    " bb.YPID", Request.QueryString["userid"].Trim());
                SqlHelper shr = new SqlHelper();
                DataTable dt = shr.Query(sql);
                shr.CloseConn();
                GVDD.DataSource = dt;
                GVDD.DataBind();
            }
            else if(TreeView1.SelectedValue == "添加药品")
            {
                HideOthers();
                UpdatePanel1.Visible = true;
            }else if(TreeView1.SelectedValue == "新建日历")
            {
                HideOthers();
                PanelAddCal.Visible = true;
            }else if(TreeView1.SelectedValue == "修改日历")
            {
                HideOthers();
                PanelFork.Visible = true;
            }else if(TreeView1.SelectedValue == "退出")
            {
                Response.Redirect("~/ZYFLogin.aspx");
            }
        }
        protected void BtnAdd_Click(object sender, EventArgs e)
        {
            //判断新旧
            string sqlOldOrNew = string.Format("select * from ZYF_JG_YP where YPName='{0}' and YPKind='{1}'" +
                "and GYSID='{2}' and YPSaveStyle='{3}'  and YPSellUnit='{4}' and YPBarCode ='{5}'", TBName.Text,
                TBKind.Text,DDLGYS.Text.ToString(),TBSave.Text,TBSell.Text,TBBarCode.Text);
            SqlHelper shr = new SqlHelper();
            DataTable dtOldOrNew =  shr.Query(sqlOldOrNew);
            int YPIDINT, YFKCIDINT;
            string YPIDLast,YFKCIDLast;
            string sqlNewInsert;
            string YFKCIDDT;
            DataTable dtYFKCNew;
            string YFKCID;
            string sqlYPUnsame;
            char[] ch = { 'Y', 'P' };
            char[] chYFKC = { 'Y', 'F', 'K', 'C' };

            if (dtOldOrNew.Rows.Count == 0)
            {
                #region 新药
                string YPIDDT = "select top 1 YPID from ZYF_JG_YP order by YPID desc";
                DataTable dt = shr.Query(YPIDDT);
                string YPID = dt.Rows[0][0].ToString();//取出最新药品ID
                YPID = YPID.Trim(ch);

                YPIDINT = Convert.ToInt32(YPID) + 1;//新药ID需要增加
                YPIDLast = "YP" + YPIDINT.ToString();
                sqlNewInsert = string.Format("insert into ZYF_JG_YP values('" +
                    "{0}', '{1}', '', '{2}', '{3}', '', '{4}', GETDATE(), GETDATE(), '{5}', " +
                    "'{6}')", YPIDLast, TBName.Text.ToString().Trim(), TBKind.Text.ToString().Trim(),
                    DDLGYS.Text.ToString(), TBSave.Text.ToString().Trim(), TBSell.Text.ToString().Trim(),
                    TBBarCode.Text.ToString().Trim());
                try
                {
                    shr.ExeNoQuery(sqlNewInsert);
                    LabelADDYP.Text = "添加成功！";
                }
                catch (Exception)
                {
                    LabelADDYP.Text = "添加失败！";
                }
                //新药入库

                //药房库存ID自增
                YFKCIDDT = "select top 1 YFKCID from ZYF_JG_YFKC order by YFKCID desc";
                dtYFKCNew = shr.Query(YFKCIDDT);
                YFKCID = dtYFKCNew.Rows[0][0].ToString();//取出药房库存ID
                YFKCID = YFKCID.Trim(chYFKC);
                YFKCIDINT = Convert.ToInt32(YFKCID) + 1;//药房库存最新ID
                string insertYFKCNewYP = string.Format("INSERT INTO ZYF_JG_YFKC VALUES" +
                "('{0}', '{1}', '{2}', '{3}', '{4}', GETDATE(), 5, GETDATE(), GETDATE(), 0, '{5}'" +
                ")", YFKCIDINT, DropDownList4.Text.ToString(), YPIDLast, TBINKCNUM.Text.ToString().Trim(), TBINKCPRICE.Text.ToString().Trim(), DDLINKCGX.Text.ToString());
                shr.ExeNoQuery(insertYFKCNewYP);
                #endregion
            }
            else
            {
                #region 旧药
                //旧药入库 = 旧药已有，数量再增 + 未有，从新添加
                //对比库存中是否存在变化，如果没有变化，则不重复添加

                sqlYPUnsame = string.Format("select YPID, YPName, YPKind, GYSID, YPSellUnit, YPSaveStyle, " +
                    "YPBarCode from ZYF_JG_YP where YPName = '{0}'", DDLOldYP.Text.ToString());
                DataTable dtYPUnsame = shr.Query(sqlYPUnsame);

                string sqlKCChange = string.Format("select * from ZYF_JG_YFKC where YFID='{0}' and " +
                    "YPID='{1}'",BindYFNAME(Request.QueryString["userid"].ToString()), dtYPUnsame.Rows[0][0].ToString());
                DataTable dtKCChange = shr.Query(sqlKCChange);
                int i = 0;
                if (dtKCChange.Rows.Count > 0)
                {
                    //旧药已有，物品数量再增
                    i = 0;
                }
                else
                {
                    //未有，从新库存添加
                    i = 1;
                }
                //药房库存ID自增
                YFKCIDDT = "select top 1 YFKCID from ZYF_JG_YFKC order by YFKCID desc";
                dtYFKCNew = shr.Query(YFKCIDDT);
                YFKCID = dtYFKCNew.Rows[0][0].ToString();//取出药房库存ID
                YFKCID = YFKCID.Trim(chYFKC);
                YFKCIDINT = Convert.ToInt32(YFKCID) + i;//药房库存最新ID
                YFKCIDLast = "YFKC" + YFKCIDINT.ToString();
                //药房库存添加
                string insertYFKC = string.Format("INSERT INTO ZYF_JG_YFKC VALUES" +
                    "('{0}', '{1}', '{2}', '{3}', '{4}', GETDATE(), 5, GETDATE(), GETDATE(), 0, '{5}'" +
                    ")", YFKCIDLast, DropDownList4.Text.ToString(), dtYPUnsame.Rows[0][0].ToString(), TBINKCNUM.Text.ToString().Trim(), TBINKCPRICE.Text.ToString().Trim(), DDLINKCGX.Text.ToString());

                //检查是否有相同库存档案，相同时不会插入新数据
                string sqlSameYFKC = string.Format("select * from ZYF_JG_YFKC where YFID='{0}' AND YPID='{1}'", DropDownList4.Text.ToString(), dtYPUnsame.Rows[0][0].ToString());
                DataTable dtSameYFKC = shr.Query(sqlSameYFKC);
                if (dtSameYFKC.Rows.Count > 0)
                {
                    LabelADDYP.Text = LabelADDYP.Text + "入库成功.";
                }
                else
                {
                    try
                    {
                        shr.ExeNoQuery(insertYFKC);
                        LabelADDYP.Text = LabelADDYP.Text + "入库成功！";
                    }
                    catch (Exception)
                    {
                        LabelADDYP.Text = LabelADDYP.Text + "入库失败！";
                    }
                }
                #endregion
            }
            shr.CloseConn();
            HideOthers();
            UpdatePanel1.Visible = true;
            LabelADDYP.Visible = true;
        }
        public int EqualsTBtoDT(string TB,string DT)
        {
            int result;
            if (string.Equals(TB, DT))
            {
                result = 1;
                return result;
            }
            else
            {
                return 0;
            }
        }
        #endregion
        #region 药品更改价格与存货
        protected void GVYPDetail_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string changeYFKC = e.CommandName.ToString();
            int index = Convert.ToInt32(e.CommandArgument) - 1;
            
            string YFID = GVYPDetail.Rows[index].Cells[0].Text.ToString();
            string YPName = GVYPDetail.Rows[index].Cells[1].Text.ToString();
            string YPNum = GVYPDetail.Rows[index].Cells[2].Text.ToString();
            string YPPrice = GVYPDetail.Rows[index].Cells[3].Text.ToString();
            string YPGX = GVYPDetail.Rows[index].Cells[8].Text.ToString();
            string YFKCID = changeYFKC;

            LabelYFKCID.Text = YFKCID;
            LabelYFID.Text = YFID;
            LabelYPName.Text = YPName;
            TBNumber.Text = YPNum;
            TBPrice.Text = YPPrice;

            string SQL = string.Format("select YFID from ZYF_QT_YFPZ WHERE USERID = '{0}'", Request.QueryString["userid"].Trim());
            SqlHelper shr = new SqlHelper();
            DataTable dt = shr.Query(SQL);

            if(YFID == dt.Rows[0][0].ToString())
            {
                //本药房操作
                CBConfirm.Checked = false;
                CBConfirm.Enabled = false;
                BtnYPDetailsChange.Text = "";
            }else if(YPGX == "1")
            {
                //其他药房操作
                CBConfirm.Checked = false;
            }
            shr.CloseConn();
            HideOthers();
            PanelYPDetailsChange.Visible = true;
        }
        protected void BtnYPDetailsChange_Click(object sender, EventArgs e)
        {
            string userid = Request.QueryString["userid"].Trim();
            string YPIDSql = string.Format("select YPID from ZYF_JG_YFKC where YFKCID='{0}'",LabelYFKCID.Text.ToString());
            SqlHelper shr = new SqlHelper();
            DataTable dt = shr.Query(YPIDSql);
            string YPID = dt.Rows[0][0].ToString();
            int i = 0;
            string sqlInsert = string.Format("insert into ZYF_QT_DD values('{0}', '{1}','{2}', {3}, {4}, '0000000000000000'," +
                " GETDATE(), GETDATE(), GETDATE(), {5})",userid,LabelYFKCID.Text.ToString(),
                YPID,TBNumber.Text.ToString().Trim(),TBPrice.Text.ToString().Trim(),i);

            string sqlUpdate = string.Format("update ZYF_JG_YFKC set YFKCResidueNum = {0},YFKCPrice = {1}," +
                "YFKCInsideTimeUp = GETDATE() where YFKCID = '{2}'", TBNumber.Text.ToString().Trim(), TBPrice.Text.ToString().Trim(),LabelYFKCID.Text.ToString());

            if (CBSell.Checked == true)
            {
                i = 1;
                shr.ExeNoQuery(sqlInsert);
                Labeltip.Visible = true;
                Labeltip.Text = "开单成功！";
            }
            else
            {
                if (CBConfirm.Checked == false)
                {
                    shr.ExeNoQuery(sqlUpdate);
                    Labeltip.Visible = true;
                    Labeltip.Text = "修改成功！";
                }
                else if (CBConfirm.Checked == true)
                {
                    shr.ExeNoQuery(sqlInsert);
                    Labeltip.Visible = true;
                    Labeltip.Text = "下订单成功！";
                }
            }
            shr.CloseConn();

            HideOthers();
            PanelYPDetailsChange.Visible = true;
        }
        #region 开方/共享/修改
        protected void CBSell_CheckedChanged(object sender, EventArgs e)
        {
            CBConfirm.Checked = false;
        }

        protected void CBConfirm_CheckedChanged(object sender, EventArgs e)
        {
            CBSell.Checked = false;
        }
        #endregion

        #endregion

    }
}