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
                    Response.Redirect("~/ZYFShopManage.aspx?userid=" + dt.Rows[0][0].ToString());
                }
                else if(useridTemp.StartsWith("DB"))
                {
                    LblTip.Visible = true;
                    LblTip.Text = " 欢迎您，数据库维护者 !";
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
        #endregion
    }
}