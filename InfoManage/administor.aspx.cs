using System;
using System.Collections.Generic;
using System.Data;
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
            using (InfoManageEntities db = new InfoManageEntities())
            {
                SqlHelper shr = new SqlHelper();
                string sql = string.Format("select * from ZYF_YH where USERID = '{0}' and USERPassword = '{1}' ",TBUSERID.Text.Trim(),Hash(TBPassword.Text.Trim()));
                DataTable dt = shr.Query(sql);
                if (dt.Rows.Count==1)
                {
                    var query = from item in db.ZYF_YH
                                where item.USERID == TBUSERID.Text.Trim()
                                select item;
                }
                else
                {
                    LblTip.Visible = true;
                    LblTip.Text = " 提交有误，请重新登陆 !";
                }
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