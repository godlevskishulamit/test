using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Configuration;

namespace SaleSineDLl
{
    public class Functions
    {
        private static Dictionary<string, string> connStrDic = new Dictionary<string, string>();

        private static string getConnStr(string connKey)
        {
            if (!connStrDic.ContainsKey(connKey))
            {
                ConnectionStringSettings mySetting = ConfigurationManager.ConnectionStrings[connKey];
                if (mySetting == null || string.IsNullOrEmpty(mySetting.ConnectionString))
                    throw new Exception("Fatal error: missing connecting string in app.config file connKey is [" + connKey + "]");
                connStrDic.Add(connKey, DecryptConnStr(mySetting.ConnectionString));
            }
            return connStrDic[connKey];
        }

        public static DataTable ExQuery(string query, string constrName)
        {
            StringCollection result = new StringCollection();
            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(getConnStr(constrName)))
            {
                SqlDataReader reader = null;
                try
                {
                    SqlCommand cmd = new SqlCommand(query);
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection;
                    connection.Open();
                    reader = cmd.ExecuteReader();
                    dt.Load(reader);
                }
                catch (Exception e)
                {
                    //CustomLog.MyLogger.error(e);
                    return null;
                }
                return dt;
            }
        }
        public static DataTable ExQueryWithConnStr(string query, string constrStr)
        {

           
            StringCollection result = new StringCollection();
            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(constrStr))
            {
                SqlDataReader reader = null;
                try
                {
                    SqlCommand cmd = new SqlCommand(query);
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection;
                    connection.Open();
                    reader = cmd.ExecuteReader();
                    dt.Load(reader);
                }
                catch (Exception e)
                {
                    //CustomLog.MyLogger.error(e);
                    return null;
                }
                return dt;
            }
        }

        public static DataTable ExQueryWithParametersWithConnStr(string query, string constrStr,string[][] parameterList)
        {
            StringCollection result = new StringCollection();
            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(constrStr))
            {
                SqlDataReader reader = null;
                try
                {
                    SqlCommand cmd = new SqlCommand(query);
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection;
                    for (int i = 0; i < parameterList.Count(); i++)
                        cmd.Parameters.AddWithValue(parameterList[i][0].ToString(), parameterList[i][1].ToString());
                    connection.Open();
                    reader = cmd.ExecuteReader();
                    dt.Load(reader);
                }
                catch (Exception e)
                {
                    //CustomLog.MyLogger.error(e);
                    return null;
                }
                return dt;
            }
        }

        public static int ExNONQuery(string query, string constrName)
        {

            using (SqlConnection connection = new SqlConnection(getConnStr(constrName)))
            {
                int ans = 0;
                try
                {
                    SqlCommand cmd = new SqlCommand(query);
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection;
                    connection.Open();
                    ans = cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    //CustomLog.MyLogger.error(e);
                    return 0;
                }
                return ans;
            }
        }

        public static int ExNONQueryWithConnStr(string query, string constrStr)
        {

            using (SqlConnection connection = new SqlConnection(constrStr))
            {
                int ans = 0;
                try
                {
                    SqlCommand cmd = new SqlCommand(query);
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection;
                    connection.Open();
                    ans = cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    //CustomLog.MyLogger.error(e);
                    return 0;
                }
                return ans;
            }
        }

        private static string DecryptConnStr(string connStr)
        {
            System.Data.SqlClient.SqlConnectionStringBuilder builder = new System.Data.SqlClient.SqlConnectionStringBuilder();
            builder.ConnectionString = connStr;
            //builder.Password = RSAEncryption.Encryption.DecryptString(builder.Password);
            return builder.ConnectionString;

        }
        public static bool connTest(string constrName)
        {
            using (SqlConnection connection = new SqlConnection(getConnStr(constrName)))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("");
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection;
                    connection.Open();
                    return true;
                }
                catch (Exception e)
                {
                    //CustomLog.MyLogger.error(e);
                    return false;
                }
            }
        }
        public static bool connTestWithConnStr(string connStr)
        {
            using (SqlConnection connection = new SqlConnection(connStr))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("");
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection;
                    connection.Open();
                    return true;
                }
                catch (Exception e)
                {
                    //CustomLog.MyLogger.error(e);
                    return false;
                }
            }
        }

        
    }
}

