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
            Label4.Text = "欢迎您：店员"+Request.QueryString["userid"].Trim();
            LabelAccount.Text = Request.QueryString["userid"].Trim();//插入日历中控件
            BindGVYPDetail();
            BindGVCalendar();
            BindDDLDD();
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

        }
        #endregion
        #region 查询订单
        protected void BtnQueryddUnfinish_Click(object sender, EventArgs e)
        {
            GVDD.Visible = true;
            GVYPDetail.Visible = false;

            string sql = string.Format("select * from (select *, " +
                "ROW_NUMBER() OVER(ORDER BY DDID asc) as SerialN from ZYF_QT_DD WHERE USERID like" +
                "  '{0}' AND DDFinish = 0) as aa inner join(select YPName, YPID from ZYF_JG_YP)as bb on aa.YPID =" +
                " bb.YPID", Request.QueryString["userid"].Trim());
            SqlHelper shr = new SqlHelper();
            DataTable dt = shr.Query(sql);
            GVDD.DataSource = dt;
            GVDD.DataBind();
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
        }
        #endregion
        #region 日历
        //add
        protected void BtnInsert_Click(object sender, EventArgs e)
        {
            HideOthers();
            PanelAddCal.Visible = true;
        }
        //FORK
        protected void BtnFork_Click(object sender, EventArgs e)
        {
            HideOthers();
            PanelFork.Visible = true;
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
                shr.CloseConn();
            }
            else
            {
                TextBox6.Text = "";
                TextBox6.Text = "无源对象，请重新检查源ID";
            }
        }
        // FORK
        protected void GVCalendar_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString()) - 1;
            string RLID = this.GVCalendar.Rows[index].Cells[0].Text.Trim();
            string RLUserid = ((LinkButton)this.GVCalendar.Rows[index].Cells[1].FindControl("LinkButton1")).Text.Trim();
            string RLText = ((TextBox)this.GVCalendar.Rows[index].Cells[1].FindControl("TextBox1")).Text.Trim();
            string RLBuilTime = ((Label)this.GVCalendar.Rows[index].Cells[1].FindControl("Label3")).Text.Trim();
            SqlHelper shr = new SqlHelper();
            string sqlMyself = string.Format("update ZYF_QT_RL set RLText = '{0}'," +
                "RLUpdateTime=GETDATE() where USERID='{1}' and RLID='{2}'", RLUserid, RLText, RLID);

            if (RLUserid == Request.QueryString["userid"].Trim())
            {
                shr.ExeNoQuery(sqlMyself);
                shr.CloseConn();
            }
            else
            {
                //DropDownList2.Text = RLUserid;
            }
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
            string sql = string.Format("select * from (SELECT * from ZYF_JG_YFKC)as " +
                "a inner join( select * from ZYF_QT_YFPZ where USERID " +
                "= '{0}' )as b On a.YFID = b.YFID", Request.QueryString["userid"].Trim());
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

            TBName.Enabled = true;
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
            TBName.Text = "";

            HideOthers();
            UpdatePanel1.Visible = true;
        }
        //入库新药
        protected void LBtnNewYP_Click(object sender, EventArgs e)
        {
            HideOthers();
            UpdatePanel1.Visible = true;
            TBName.Enabled = true;
            TBName.Text = "";
        }
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
            //药品添加
            string YPIDDT = "select top 1 YPID from ZYF_JG_YP order by YPID desc";
            SqlHelper shr = new SqlHelper();
            DataTable dt = shr.Query(YPIDDT);
            string YPID = dt.Rows[0][0].ToString();//取出药品ID

            char[] ch = { 'Y', 'P' };
            YPID = YPID.Trim(ch);
            int YPIDINT = Convert.ToInt32(YPID) + 1;
            string YPIDLast = "YP" + YPIDINT.ToString();
            string sql = string.Format("insert into ZYF_JG_YP values('" +
                "{0}', '{1}', '', '{2}', '{3}', '', '{4}', GETDATE(), GETDATE(), '{5}', " +
                "'{6}')", YPIDLast, TBName.Text.ToString().Trim(), TBKind.Text.ToString().Trim(),
                DDLGYS.Text.ToString(), TBSave.Text.ToString().Trim(), TBSell.Text.ToString().Trim(),
                TBBarCode.Text.ToString().Trim());

            HideOthers();
            UpdatePanel1.Visible = true;
            LabelADDYP.Visible = true;

            
            //对比是否存在变化，如果没有，则不重复添加
            string sqlYPUnsame = string.Format("select YPID, YPName, YPKind, GYSID, YPSellUnit, YPSaveStyle, " +
                "YPBarCode from ZYF_JG_YP where YPName = '{0}'", DDLOldYP.Text.ToString()); 
            DataTable dtYPUnsame = shr.Query(sqlYPUnsame);
            int i = 0;
            i += EqualsTBtoDT(TBName.Text, dtYPUnsame.Rows[0][1].ToString());
            i += EqualsTBtoDT(TBKind.Text, dtYPUnsame.Rows[0][2].ToString());
            i += EqualsTBtoDT(DDLGYS.Text, dtYPUnsame.Rows[0][3].ToString());
            i += EqualsTBtoDT(TBSell.Text, dtYPUnsame.Rows[0][4].ToString());
            i += EqualsTBtoDT(TBSave.Text, dtYPUnsame.Rows[0][5].ToString());
            i += EqualsTBtoDT(TBBarCode.Text, dtYPUnsame.Rows[0][6].ToString());
            if (i == 6)
            {
                LabelADDYP.Text = "已存在，添加成功！";
            }
            else
            {
                try
                {
                    shr.ExeNoQuery(sql);
                    LabelADDYP.Text = "添加成功！";
                }
                catch (Exception EX)
                {
                    LabelADDYP.Text = "添加失败！";
                }
            }
            //药房库存添加
            string YFKCIDDT = "select top 1 YFKCID from ZYF_JG_YFKC order by YFKCID desc";
            SqlHelper shrYFKC = new SqlHelper();
            DataTable dtYFKC = shr.Query(YFKCIDDT);
            string YFKCID = dtYFKC.Rows[0][0].ToString();//取出药品ID

            char[] chYFKC = { 'Y', 'F','K','C'};
            YPID = YPID.Trim(chYFKC);
            int YPFKCIDINT = Convert.ToInt32(YPID) + 1;
            string YFKCIDLast = "YP" + YPIDINT.ToString();

            string insertYFKC = string.Format("INSERT INTO ZYF_JG_YFKC VALUES" +
                "('{0}', '{1}', '{2}', '{3}', '{4}', GETDATE(), 5, GETDATE(), GETDATE(), 0, '{5}'" +
                ")",YFKCIDLast,DropDownList4.Text.ToString(),YPIDLast,TBINKCNUM.Text.ToString().Trim(),TBINKCPRICE.Text.ToString().Trim(),DDLINKCGX.Text.ToString());
            try
            {
                shrYFKC.ExeNoQuery(insertYFKC);
                LabelADDYP.Text = LabelADDYP.Text+ "入库成功！";
            }
            catch(Exception ex)
            {
                LabelADDYP.Text = LabelADDYP.Text + "入库失败！";
            }
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
            string YFKCID = changeYFKC;

            LabelYFKCID.Text = YFKCID;
            LabelYFID.Text = YFID;
            LabelYPName.Text = YPName;
            TBNumber.Text = YPNum;
            TBPrice.Text = YPPrice;

            HideOthers();
            PanelYPDetailsChange.Visible = true;

        }
        protected void BtnYPDetailsChange_Click(object sender, EventArgs e)
        {
            string sqlInsert = string.Format("update ZYF_JG_YFKC set YFKCResidueNum = {0},YFKCPrice = {1}," +
                "YFKCInsideTimeUp = GETDATE()",TBNumber.Text.ToString().Trim(),TBPrice.Text.ToString().Trim());
            SqlHelper shr = new SqlHelper();
            shr.ExeNoQuery(sqlInsert);

            Labeltip.Visible = true;
            Labeltip.Text = "修改成功！";

            HideOthers();
            PanelYPDetailsChange.Visible = true;
        }
        #endregion

    }
}