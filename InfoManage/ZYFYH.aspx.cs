using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Threading;

namespace InfoManage
{
    public partial class ZYFYH : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Label1.Text = "欢迎您：" + Request.QueryString["userid"].Trim();
            BindGridView1();
            BindGVDD();
        }
        public void BindGridView1()
        {
            string sql = "select YFKCID,YPName,YFName,YFKCResidueNum,YFKCPrice,ROW_NUMBER() OVER(ORDER BY YFKCPrice asc) as SerialN from (select * from ZYF_JG_YFKC) as aaa" +
                " inner join(select YFName, YFID from ZYF_JG_YF)as bbb on aaa.YFID = bbb.YFID " +
                " inner join(select YPID, YPName from ZYF_JG_YP)as ccc on aaa.YPID = ccc.YPID";
            SqlHelper shr = new SqlHelper();
            DataTable dt = shr.Query(sql);

            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
        #region 表格展现
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument) - 1;
            string ypname = GridView1.Rows[index].Cells[1].Text.ToString();
            string yppric = GridView1.Rows[index].Cells[3].Text.ToString();
            string  ypnumb = ((TextBox)(this.GridView1.Rows[index].Cells[4].FindControl("TextBox1"))).Text.ToString();
            string yfkcid = Convert.ToString(e.CommandName);
            
            string sqlypid = string.Format("select YPID from ZYF_JG_YFKC where YFKCID = '{0}'", yfkcid);
            SqlHelper shr = new SqlHelper();
            DataTable dtypid = shr.Query(sqlypid);//药品ID
            
            string sqlDDid = "select top(1) DDID from ZYF_QT_DD  order by DDID desc";
            DataTable dtddid = shr.Query(sqlDDid);
            int ddnum = Convert.ToInt32(dtddid.Rows[0][0].ToString());//max订单ID

            string userid = Request.QueryString["userid"].Trim();
            string strHasRows = string.Format("select * from ZYF_QT_DD where USERID='{0}' AND YFKCID = '{1}'", userid, yfkcid);//已经有此类订单，更新数目
            DataTable dtHasRows = shr.Query(strHasRows);
            //插入数据库
            int DDID = ddnum +1;
            string USERID = userid;
            string YPID = dtypid.Rows[0][0].ToString();
            string DDNum = ypnumb;
            string DDPrice = yppric;
            string strInsert = string.Format("insert into ZYF_QT_DD values('{0}', '{1}','{2}', {3}, {4}, '0000000000000000'," +
                " GETDATE(), GETDATE(), GETDATE(), 0)",  USERID,yfkcid, YPID, DDNum, DDPrice);
            string strUpdate = string.Format("update ZYF_QT_DD set DDNum = '{0}',DDFinish=0 where USERID='{1}' and YPID = '{2}' AND YFKCID = '{3}'", DDNum,USERID,YPID,yfkcid);

            int i = dtHasRows.Rows.Count;

            Label2.Visible = true;
            if (dtHasRows.Rows.Count == 0)
            {
                shr.ExeNoQuery(strInsert);
                Label2.Text = "下订单";
            }
            else if (dtHasRows.Rows.Count == 1)
            {
                shr.ExeNoQuery(strUpdate);
                Label2.Text = "更新订单";
            }
            Label2.Text = Label2.Text.ToString()+ "成功！";
        }

        protected void GVDD_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument)-1;
            string DDID = GVDD.Rows[index].Cells[0].Text.ToString();


            string strEndDD = string.Format("update ZYF_QT_DD set DDFinish = 1,DDCancelTime=getdate() where DDID = {0}",DDID);
            SqlHelper shr = new SqlHelper();
            shr.ExeNoQuery(strEndDD);
            shr.CloseConn();
            Label3.Visible = true;
            Label3.Text = "退订完成!";
        }
        public void BindGVDD()
        {
            string bindgvdd = string.Format("select *,ROW_NUMBER() OVER(ORDER BY DDID asc) as SerialN from " +
                "(select * from ZYF_QT_DD where USERID = '{0}' AND DDFinish = 0) as aaa " +
                "inner join(select YPID, YPName from ZYF_JG_YP) as bbb on aaa.YPID = bbb.YPID", Request.QueryString["userid"].Trim());
            SqlHelper shr = new SqlHelper();
            DataTable dt =  shr.Query(bindgvdd);

            if (dt.Rows.Count <= 0)
            {
                Label3.Visible = true;
                Label3.Text = "您暂时没有订单，请重新预约";
            }
            else
            {
                Label3.Visible = false;
            }

            GVDD.DataSource = dt;
            GVDD.DataBind();
        }
        #endregion
        protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
        {
            if (TreeView1.SelectedValue == "订购")
            {
                GridView1.Visible = true;
                GVDD.Visible = false;
                Label2.Visible = false;
                Label3.Visible = false;
            }
            else if (TreeView1.SelectedValue == "查看订单")
            {
                GridView1.Visible = false;
                GVDD.Visible = true;
                Label2.Visible = false;
                Label3.Visible = false;
            }else if(TreeView1.SelectedValue == "退出")
            {
                Response.Redirect("~/ZYFLogin.aspx");
            }
        }
    }
}