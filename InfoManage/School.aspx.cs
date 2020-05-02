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
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HideOthers();
            PanelLogin.Visible = true;


        }
        #region 注册
        protected void BtnLogin_Click(object sender, EventArgs e)
        {
            string timeBtnSend = DateTime.Now.ToString("MM-dd-HH");
            string timeConfirm = LabelEmaRegCache2.Text + timeBtnSend;// 时间点的确认
            string numConfirm = TBEmailRanCom.Text + timeBtnSend;// 随机数目的确认
            if (TBAccount.Text == null )
            {
                LabelTip.Visible = true;
                LabelTip.Text = "邮箱为空";
            }else if(TBPassCom.Text == null)
            {
                LabelTip.Visible = true;
                LabelTip.Text = "密码为空";
            }else if (TBPassword.Text != TBPassCom.Text)
            {
                LabelTip.Visible = true;
                LabelTip.Text = "密码不一致";
            }else if (numConfirm!= LabelEmaRegCache.Text)
            {
                LabelTip.Visible = true;
                LabelTip.Text = "验证码不一致";
            }
            else if(timeConfirm == LabelEmaRegCache.Text && numConfirm == timeConfirm)
            {
                SqlHelper shr = new SqlHelper();
                SqlParameter[] sp =
                {
                    new SqlParameter("@Email",SqlDbType.NVarChar),
                    new SqlParameter("@Password",SqlDbType.VarChar),
                    new SqlParameter("@RegTime",SqlDbType.DateTime),
                };
                sp[0].Value = TBAccount.Text.ToString().Trim();
                sp[1].Value = TBPassword.Text.ToString().Trim();
                sp[2].Value = DateTime.Now.ToString();
                try
                {
                    shr.ExeNoQueryProc("Proc_InsertAccount", sp);
                    HideOthers();
                    PanelLogin.Visible = true;
                }catch(Exception ex)
                {
                    LabelTip.Visible = true;
                    LabelTip.Text = "注册过程遇到阻碍，请重新注册";
                }
                finally
                {
                    shr.CloseConn();
                }
            }
        }

        protected void BtnEmailCom_Click(object sender, EventArgs e)
        {
            //获取当前时间加在随机数字后面，验证时做减法
            string timeBtnSend = DateTime.Now.ToString("MM-dd-HH");
            
            Random rd = new Random();
            string rConfirm = Convert.ToString(rd.Next(100000, 999999));
            LabelEmaRegCache.Text = rConfirm + timeBtnSend;//用于认证,过一h则变
            LabelEmaRegCache2.Text = rConfirm;//用于拼接
            string sendEmaCom = rConfirm;//用于发送

            string sql = string.Format("select Email from Info_Account where Email = '{0}'",TBAccount.Text.Trim());

            SqlHelper shr = new SqlHelper();
            SqlDataReader sdr = shr.QueryOperationProc(sql);

            if(sdr.HasRows)
            {
                shr.CloseConn();
                TBAccount.Text = "此账号已被注册! ";
            }
            else
            {
                MailHelper mh = new MailHelper();
                string toaddress = TBAccount.Text.Trim();
                string bodyContent = "这是您用于Info管理网站注册邮箱验证码 : " + sendEmaCom+"，每小时更快一次.";
                string subject = "Info管理网站";
                mh.SendMail(toaddress,bodyContent,subject);
            }
        }
        #endregion

        #region 登陆
        protected void BtnLogin_Click1(object sender, EventArgs e)
        {
            SqlHelper shr = new SqlHelper();
            string sql = string.Format("select * from Info_Account where Email = '{0}' and Password = '{1}'", LOTBAccount.Text,LOTBPass.Text);
            
            SqlDataReader sdr = shr.QueryOperationProc(sql);
            if(sdr.HasRows)
            {
                HideOthers();
                PanelInfo.Visible = true;
            }
        }
        #endregion
        public void HideOthers()
        {
            PanelInfo.Visible = false;
            PanelReg.Visible = false;
            PanelLogin.Visible = false;
            PanelCache.Visible = false;
            Panel.Visible = false;

            HideUnits();
        }
        public void HideUnits()
        {
            LabelTip.Visible = false;
        }

    }
}