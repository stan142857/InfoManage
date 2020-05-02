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
        }
        protected void GVYPDetail_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument)-1;
            string RLUSERID = ((LinkButton)this.GVCalendar.Rows[index].Cells[1].FindControl("LinkButton1")).Text.Trim();


        }
        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// FORK源--未使用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GVCalendar_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            int index = Convert.ToInt32(e.CommandArgument.ToString()) - 1;
            string RLID = this.GVCalendar.Rows[index].Cells[0].Text.Trim();
            string RLUserid = ((LinkButton)this.GVCalendar.Rows[index].Cells[1].FindControl("LinkButton1")).Text.Trim();
            string RLText = ((TextBox)this.GVCalendar.Rows[index].Cells[1].FindControl("TextBox1")).Text.Trim();
            string RLBuilTime  = ((Label)this.GVCalendar.Rows[index].Cells[1].FindControl("Label3")).Text.Trim();
            SqlHelper shr = new SqlHelper();
            string sqlMyself = string.Format("update ZYF_QT_RL set RLText = '{0}'," +
                "RLUpdateTime=GETDATE() where USERID='{1}' and RLID='{2}'", RLUserid,RLText, RLID); 
            
            if(RLUserid == Request.QueryString["userid"].Trim())
            {
                shr.ExeNoQuery(sqlMyself);
                shr.CloseConn();
                BtnFork.Text = "----";
            }
            else
            {
                //DropDownList2.Text = RLUserid;
            }
        }

        #region 店铺订单-库存查询
        protected void BtnQuerykc_Click(object sender, EventArgs e)
        {
            GVYPDetail.Visible = true;
            GVDD.Visible = false;

            string userid = Request.QueryString["userid"].Trim();
            string sqls = string.Format(" select YFID,YPName,YFKCResidueNum,YFKCPrice," +
                "YFKCBuildTime,YFKCQualityGuauaPeriod,YFKCOutsideTimeUp,YFKCExceedQGP,YFKCShare," +
                "ROW_NUMBER() OVER(ORDER BY YFID asc) as SerialN from(select * from ZYF_JG_YFKC where YFID" +
                " in (select YFID from ZYF_QT_YFPZ )) as AAAA inner join(select YPName, YPID " +
                "from ZYF_JG_YP where YPName like '%{0}%') as bbbb on AAAA.YPID = bbbb.YPID ", DDLYP.SelectedItem.Text.ToString());

            SqlHelper shr = new SqlHelper();
            DataTable dt = shr.Query(sqls);

            GVYPDetail.DataSource = dt;
            GVYPDetail.DataBind();

        }
        //查询订单
        protected void BtnQuerydd_Click(object sender, EventArgs e)
        {

            GVDD.Visible = true;
            GVYPDetail.Visible = false;

            string sql = string.Format("select *,ROW_NUMBER() OVER(ORDER BY DDID asc) as SerialN from ZYF_QT_DD WHERE USERID = '{0}'  and DDFinish = 0", Request.QueryString["userid"].Trim());
            SqlHelper shr = new SqlHelper();
            DataTable dt = shr.Query(sql);
            GVDD.DataSource = dt;
            GVDD.DataBind();
        }

        protected void GVDD_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument) - 1;

            string DDID = GVDD.Rows[index].Cells[0].Text;
            string sqlDeal = string.Format("update ZYF_QT_DD set DDFinish = 1 where DDID = '{0}'",DDID);
            SqlHelper shr = new SqlHelper();
            shr.ExeNoQuery(sqlDeal);
            shr.CloseConn();

        }
        #endregion

        #region 插入日历
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
            string RLid = DropDownList2.SelectedItem.Text;
            string RLUserid = DropDownList3.SelectedItem.Text;
            string text = TextBox6.Text.ToString().Trim() + "--"+RLid;
            string only = TextBox7.Text.ToString().Trim();

            string sql = string.Format("insert into ZYF_QT_RL values('{0}', '{1}', {2}, " +
                "GETDATE(), GETDATE())", userid, text, only);

            string sqlright = string.Format("select USERID from ZYF_QT_RL where RLSYS = 1 and RLID = '{0}'", RLUserid);

            SqlHelper shr = new SqlHelper();
            DataTable dt = shr.Query(sqlright);
            //已确认可FORK
            if (dt.Rows.Count == 1)
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
        #endregion
        #region 绑定表格
        public void BindGVYPDetail()
        {
            string userid = Request.QueryString["userid"].Trim();
            string sql = string.Format(" select YFID,YPName,YFKCResidueNum,YFKCPrice,YFKCBuildTime,YFKCQualityGuauaPeriod,YFKCOutsideTimeUp,YFKCExceedQGP,YFKCShare,ROW_NUMBER() OVER(ORDER BY YFID asc) as SerialN " +
                " from(select * from ZYF_JG_YFKC where YFID = (select YFID from ZYF_QT_YFPZ where USERID = '{0}')) as AAAA" +
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
        public void HideOthers()
        {
            PanelAddCal.Visible = false;
            PanelFork.Visible = false;
        }
        #endregion

    }
}