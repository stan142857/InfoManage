using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InfoManage
{
    public partial class administor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HideOthers();
            PanelLogin.Visible = true;
            BindGVDetails();
        }

        protected void BtnLogin_Click(object sender, EventArgs e)
        {
            string useridTemp;
            SqlHelper shr = new SqlHelper();
            string sql = string.Format("select * from ZYF_YH where USERID = '{0}' and USERPassword = '{1}' ",TBUSERID.Text.Trim(),Hash(TBPassword.Text.Trim()));
            DataTable dt = shr.Query(sql);
            //更新登录时间
            SqlParameter[] sp = { new SqlParameter("@USERID", SqlDbType.VarChar) };

            if (dt.Rows.Count==1)
            {
                useridTemp = dt.Rows[0][0].ToString();
                sp[0].Value = useridTemp;
                shr.ExeNoQueryProc("Proc_ZYF_UpdateLoginSuccTimeUSERLoginSuccTime", sp);

                if (useridTemp.StartsWith("AD"))
                {
                    Response.Redirect("~/ZYFShopManager.aspx?userid=" + dt.Rows[0][0].ToString());
                }
                else if(useridTemp.StartsWith("DB"))
                {
                    LblTip.Visible = true;
                    LblTip.Text = " 欢迎您，数据库维护者 !";
                }
                else if(useridTemp.StartsWith("YH"))
                {
                    Response.Redirect("~/ZYFYH.aspx?userid=" + dt.Rows[0][0].ToString());
                }else if(useridTemp.EndsWith(".com"))
                {
                    Response.Redirect("~/ZYFYH.aspx?userid=" + dt.Rows[0][0].ToString());
                }
            }
            else
            {
                sp[0].Value = TBUSERID.Text.Trim();
                shr.ExeNoQueryProc("Proc_ZYF_UpdateLoginFailTimeUSERLoginFailTime", sp);
                LblTip.Visible = true;
                LblTip.Text = " 登录失败，提交有误，请重新登陆 !";
            }
        }
        #region 函数
        public void HideOthers()
        {
            PanelLogin.Visible = false;
            PanelPassGet.Visible = false;
            PanelRegister.Visible = false;
        }
        public string Hash(string data)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(data);
            byte[] hash = SHA256Managed.Create().ComputeHash(bytes);
            byte[] hash1 = MD5.Create().ComputeHash(hash);

            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < hash1.Length; i++)
            {
                builder.Append(hash1[i].ToString("X2"));
            }

            return builder.ToString();
        }
        public void BindGVDetails()
        {
            string sql = "select YFKCID,YPName,YFName,YFKCResidueNum,YFKCPrice,ROW_NUMBER() OVER(ORDER BY YFKCPrice asc) as SerialN from (select * from ZYF_JG_YFKC) as aaa" +
                " inner join(select YFName, YFID from ZYF_JG_YF)as bbb on aaa.YFID = bbb.YFID " +
                " inner join(select YPID, YPName from ZYF_JG_YP)as ccc on aaa.YPID = ccc.YPID";
            SqlHelper shr = new SqlHelper();
            DataTable dt = shr.Query(sql);

            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
        #endregion
        protected void LBtnRegister_Click(object sender, EventArgs e)
        {
            PanelLogin.Visible = false;
            PanelRegister.Visible = true;
        }
        protected void Btncon_Click(object sender, EventArgs e)
        {
            PanelRegister.Visible = true;
            PanelLogin.Visible = false;

            MailHelper mh = new MailHelper();

            string sqlmail = string.Format("select USERID from ZYF_YH where USERID = '{0}'",TBEmail.Text.Trim());
            SqlHelper shr = new SqlHelper();
            DataTable dt = shr.Query(sqlmail);
            if (dt.Rows.Count == 0)
            {
                Random rd = new Random();
                string rconfirm = Convert.ToString(rd.Next(10000,99999));

                Labeltip.Text = rconfirm;

                string message = "尊敬的用户，您的验证码为：" + rconfirm;
                string subject = "验证码注册";
                mh.SendMail(TBEmail.Text.Trim(), message, subject);

                LBBACK.Visible = true;
                LBBACK.Text = "邮件已成功发送";
                LBBACK.Enabled = false;
            }
            else
            {
                LBBACK.Visible = true;
                LBBACK.Text = "无法使用此邮箱";
                LBBACK.Enabled = false;
            }
        }

        protected void BtnReg_Click(object sender, EventArgs e)
        {
            if (TBPassNum.Text.Trim()==Labeltip.Text && TBPass.Text.Trim()== TBPasscon.Text.Trim())
            {
                string sqlin = string.Format("insert into ZYF_YH values('{0}', '{1}'," +
                    " GETDATE(), GETDATE(), GETDATE(), 'ZC', 1, 'ZC', 'YH', 2, 127," +
                    " 'ZC', 0, 0)",TBEmail.Text.Trim(),Hash(TBPass.Text.Trim()));
                SqlHelper shr = new SqlHelper();
                try
                {
                    shr.ExeNoQuery(sqlin);
                    LBBACK.Visible = true;
                    LBBACK.Text = "注册成功，点击登陆";
                    LBBACK.Enabled = true;
                }
                catch(Exception ex)
                {
                    LBBACK.Visible = true;
                    LBBACK.Text = "注册失败";
                    LBBACK.Enabled = false;
                }
            }
            PanelRegister.Visible = true;
            PanelLogin.Visible = false;
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            PanelRegister.Visible = false;
            PanelLogin.Visible = true;
        }
    }
}