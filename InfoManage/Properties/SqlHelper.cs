/************************************************************
* Copyright (c) 2020 All Rights Reserved.
* CLR版本: 4.0.30319.42000
* 机器名称: DESKTOP-9H2IJLA
* 公司名称: 
* 命名空间: InfoManage
* 文件名: SqlHelper
* 版本号: v1.0.0.0
* 唯一标识: e51e3b4e-f97b-4dc2-b35c-268447e84e2e
* 当前的用户域: $userdomins$
* 创建人: yuanlei
* 电子邮箱: stan142857@gmail.com
* 创建时间: 2020-04-11 20:56:43

* 描述
*
*=================================================================
* 修改标识
* 修改时间: 2020-04-11 20:56:43
* 修改人:   yuanlei
* 版本号: v1.0.0.0
* 描述:
*
****************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace InfoManage
{
    public class SqlHelper
    {
        private static string mystr = System.Configuration.ConfigurationManager.ConnectionStrings["InfoManage"].ConnectionString;
        private SqlConnection con = new SqlConnection(mystr);
        SqlDataReader sdr;
        public SqlHelper()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        public SqlConnection getCon()
        {
            return con;
        }

        public SqlDataReader QueryOperationProc(String StrQueryCommand)
        {
            if (con.State == System.Data.ConnectionState.Closed)
                con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = StrQueryCommand;
            if (sdr != null)
                sdr.Close();
            sdr = cmd.ExecuteReader();
            return sdr;
        }
        public DataTable Query(String StrQueryCommand)
        {
            if (con.State == System.Data.ConnectionState.Closed)
                con.Open();
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(StrQueryCommand, con);
            sda.Fill(dt);
            return dt;
        }
        public bool ExeNoQuery(String StrCmd)
        {
            if (con.State == System.Data.ConnectionState.Closed)
                con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = StrCmd;
            try
            {
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw ex;
                return false;
            }
            return true;

        }
        public void CloseConn()
        {
            if (con.State != System.Data.ConnectionState.Closed)
                con.Close();
        }

        public bool ExeNoQueryProc(String cmdName, SqlParameter[] ps)
        {
            if (con.State == System.Data.ConnectionState.Closed)
                con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = cmdName;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            foreach (SqlParameter p in ps)
            {
                cmd.Parameters.Add(p);
            }
            try
            {
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }

        public DataTable QueryProc(String cmdStr, SqlParameter[] ps)
        {
            DataTable dt = new DataTable();
            if (con.State == System.Data.ConnectionState.Closed)
                con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = cmdStr;
            if (ps != null)
            {
                foreach (SqlParameter p in ps)
                {
                    cmd.Parameters.Add(p);
                }
            }
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            con.Close();
            return dt;
        }
    }
}