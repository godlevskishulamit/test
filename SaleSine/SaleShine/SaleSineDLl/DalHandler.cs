using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Configuration;


namespace SaleSineDLl
{

    public class DalHandler
    {

        public string GetConnectionString(string sap_conn_name)
        {
            ConnectionStringSettings mySetting = ConfigurationManager.ConnectionStrings[sap_conn_name];
            if (mySetting == null || string.IsNullOrEmpty(mySetting.ConnectionString))
                throw new Exception("Fatal error: missing connecting string in app.config file connKey is [" + sap_conn_name + "]");
          //  CustomLog.MyLogger.info("try");
            return mySetting.ConnectionString;
        }
        public string GetConnectionStringDbName(string sap_conn_name)
        {
            GetConnectionString(sap_conn_name);
            System.Data.Common.DbConnectionStringBuilder builder = new System.Data.Common.DbConnectionStringBuilder();
            builder.ConnectionString = GetConnectionString(sap_conn_name);

          
            string database = builder["Initial Catalog"] as string;
            return database;
        }

        public bool SubmitChanges(ref DataTable tbl, string ConnectionString, string spUpDateName, string spInsertName, string spDeleteName)
        {
            SqlDataAdapter da = new SqlDataAdapter();
            SqlTransaction myTran = null;
            SqlConnection con = null;

            try
            {
                if (!string.IsNullOrEmpty(spUpDateName))
                {
                    da.UpdateCommand = CreateSPCommand(spUpDateName, ConnectionString);
                }
                if (!string.IsNullOrEmpty(spInsertName))
                {
                    da.InsertCommand = CreateSPCommand(spInsertName, ConnectionString);
                }
                if (!string.IsNullOrEmpty(spDeleteName))
                {
                    da.DeleteCommand = CreateSPCommand(spDeleteName, ConnectionString);
                }
                //da.ContinueUpdateOnError = True

                con = new SqlConnection(ConnectionString);
                con.Open();

                myTran = con.BeginTransaction();
                if (da.InsertCommand != null)
                {
                    da.InsertCommand.Connection = con;
                    da.InsertCommand.Transaction = myTran;
                }
                if (da.UpdateCommand != null)
                {
                    da.UpdateCommand.Connection = con;
                    da.UpdateCommand.Transaction = myTran;
                }
                if (da.DeleteCommand != null)
                {
                    da.DeleteCommand.Connection = con;
                    da.DeleteCommand.Transaction = myTran;
                }

                da.Update(tbl);

                myTran.Commit();
                myTran = null;



            }
            catch (Exception Ex)
            {
                if (myTran != null)
                {
                    myTran.Rollback();
                }
                throw Ex;
                return false;
            }
            finally
            {
                if (con != null)
                {
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                }
            }
            return true;
        }
        public bool SubmitChanges(ref DataTable tbl, object[][] insertParams, object[][] updateParams, object[][] deleteParams, string ConnectionString, string spUpDateName, string spInsertName, string spDeleteName)
        {
            SqlDataAdapter da = new SqlDataAdapter();
            SqlTransaction myTran = null;
            SqlConnection con = null;


            try
            {

                if (!string.IsNullOrEmpty(spUpDateName))
                {
                    if (updateParams == null)
                    {
                        da.UpdateCommand = CreateSPCommand(spUpDateName, ConnectionString);
                    }
                    else
                    {
                        da.UpdateCommand = CreateSPCommand(spUpDateName, updateParams, ConnectionString);
                    }
                }
                if (!string.IsNullOrEmpty(spInsertName))
                {
                    if (insertParams == null)
                    {
                        da.InsertCommand = CreateSPCommand(spInsertName, ConnectionString);
                    }
                    else
                    {
                        da.InsertCommand = CreateSPCommand(spInsertName, insertParams, ConnectionString);
                    }
                }
                if (!string.IsNullOrEmpty(spDeleteName))
                {
                    if (deleteParams == null)
                    {
                        da.DeleteCommand = CreateSPCommand(spDeleteName, ConnectionString);
                    }
                    else
                    {
                        da.DeleteCommand = CreateSPCommand(spDeleteName, deleteParams, ConnectionString);
                    }
                }


                //da.ContinueUpdateOnError = True

                con = new SqlConnection(ConnectionString);
                con.Open();

                myTran = con.BeginTransaction();
                if (da.InsertCommand != null)
                {
                    da.InsertCommand.Connection = con;
                    da.InsertCommand.Transaction = myTran;
                }
                if (da.UpdateCommand != null)
                {
                    da.UpdateCommand.Connection = con;
                    da.UpdateCommand.Transaction = myTran;
                }
                if (da.DeleteCommand != null)
                {
                    da.DeleteCommand.Connection = con;
                    da.DeleteCommand.Transaction = myTran;
                }

                da.Update(tbl);

                myTran.Commit();
                myTran = null;


            }
            catch (Exception Ex)
            {
                if (myTran != null)
                {
                    myTran.Rollback();
                }
                throw Ex;
                return false;
            }
            finally
            {
                if (con != null)
                {
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                }
            }
            return true;

        }

        #region Not Need
        //public bool SubmitChangesWithOutPut(ref DataTable tbl, object[][] insertParams, object[][] updateParams, object[][] deleteParams, string ConnectionString, string spUpDateName, string spInsertName, string spDeleteName, ref object[] insertOut, ref object[] updateOut,
        //ref object[] deleteOut)
        //{

        //    SqlDataAdapter da = new SqlDataAdapter();
        //    SqlTransaction myTran = null;
        //    SqlConnection con = null;

        //    try {

        //        if (!string.IsNullOrEmpty(spUpDateName)) {
        //            da.UpdateCommand = CreateSPCommandWithValues(spUpDateName, ConnectionString, updateParams, ref updateOut);
        //        }
        //        if (!string.IsNullOrEmpty(spInsertName)) {
        //            da.InsertCommand = CreateSPCommandWithValues(spInsertName, ConnectionString, insertParams, ref insertOut);
        //        }
        //        if (!string.IsNullOrEmpty(spDeleteName)) {
        //            da.DeleteCommand = CreateSPCommandWithValues(spDeleteName, ConnectionString, deleteParams, ref deleteOut);
        //        }

        //        da.ContinueUpdateOnError = True

        //        con = new SqlConnection(ConnectionString);
        //        con.Open();

        //        myTran = con.BeginTransaction();
        //        if (da.InsertCommand != null) {
        //            da.InsertCommand.Connection = con;
        //            da.InsertCommand.Transaction = myTran;
        //        }
        //        if (da.UpdateCommand != null) {
        //            da.UpdateCommand.Connection = con;
        //            da.UpdateCommand.Transaction = myTran;
        //        }
        //        if (da.DeleteCommand != null) {
        //            da.DeleteCommand.Connection = con;
        //            da.DeleteCommand.Transaction = myTran;
        //        }

        //        da.Update(tbl);

        //        myTran.Commit();
        //        myTran = null;

        //        int i = 0;

        //        for (i = 0; i <= insertOut.Length - 1; i++) {
        //            if (da.InsertCommand.Parameters.Item(insertOut(i).ToString()).Value.ToString() == DBNull.Value.ToString()) {
        //                insertOut(i) = 1;
        //            } else {
        //                insertOut(i) = (object)da.InsertCommand.Parameters.Item(insertOut(i).ToString()).Value;
        //            }
        //        }

        //        for (i = 0; i <= updateOut.Length - 1; i++) {
        //            if (da.UpdateCommand.Parameters.Item(updateOut(i).ToString()).Value.ToString() == DBNull.Value.ToString()) {
        //                updateOut(i) = 1;
        //            } else {
        //                updateOut(i) = (object)da.UpdateCommand.Parameters.Item(updateOut(i).ToString()).Value;
        //            }


        //        }

        //        for (i = 0; i <= deleteOut.Length - 1; i++) {
        //            if (da.DeleteCommand.Parameters.Item(deleteOut(i).ToString()).Value.ToString() == DBNull.Value.ToString()) {
        //                deleteOut(i) = 1;
        //            } else {
        //                deleteOut(i) = (object)da.DeleteCommand.Parameters.Item(deleteOut(i).ToString()).Value;
        //            }
        //        }

        //    } catch (Exception Ex) {
        //        if (myTran != null) {
        //            myTran.Rollback();
        //        }
        //        throw Ex;
        //        return false;

        //    } finally {
        //        if (con != null) {
        //            if (con.State == ConnectionState.Open) {
        //                con.Close();
        //            }
        //        }
        //    }

        //    return true;

        //}
        #endregion

        public DataSet GetValues(string ConnectionString, string spName, object[] selectCommandParms)
        {

            try
            {
                DataSet ds = new DataSet();
                object obj = null;

                SqlDataAdapter da = new SqlDataAdapter(CreateSPCommandWithValues(spName, ConnectionString, selectCommandParms, ref obj));
                for (int i = 1; i < selectCommandParms.Length; i++)
                {


                    da.SelectCommand.Parameters[i].Value = selectCommandParms[i];
                }
                da.Fill(ds);
                return ds;
            }
            catch (Exception Ex)
            {
                throw Ex;
                return null;
            }
        }
        public DataTable GetValuesInDataTable(string ConnectionString, string spName, object[] selectCommandParms, string sTableName)
        {

            try
            {

                DataTable dt = new DataTable();
                dt.TableName = sTableName;
                object obj = null;
                SqlDataAdapter da = new SqlDataAdapter(CreateSPCommandWithValues(spName, ConnectionString, selectCommandParms, ref obj));
                if (selectCommandParms != null)
                {
                    for (int i = 1; i < selectCommandParms.Length; i++)
                    {
                        
                        da.SelectCommand.Parameters[i].Value = selectCommandParms[i];
                    }
                }
                da.Fill(dt);
                return dt;
            }
            catch (Exception Ex)
            {
                throw Ex;
                return null;
            }
        }
        public string DecryptConnStr(string connStr)
        {
            System.Data.SqlClient.SqlConnectionStringBuilder builder = new System.Data.SqlClient.SqlConnectionStringBuilder();
            builder.ConnectionString = connStr;
            // builder.Password = RSAEncryption.Encryption.DecryptString(builder.Password);
            return builder.ConnectionString;

        }

        public object RunCommandWithValue(string ConnectionString, string spName, object[] selectCommandParms)
        {


            SqlCommand cmd = new SqlCommand();
            SqlTransaction myTran = null;
            ArrayList obj = null;
            try
            {
                cmd = CreateSPCommandWithValues(spName, ConnectionString, selectCommandParms, ref obj);
                if ((cmd.Connection.State == ConnectionState.Closed))
                {
                    cmd.Connection.Open();
                }
                myTran = cmd.Connection.BeginTransaction();
                cmd.Transaction = myTran;
                object oReturn = null;
                oReturn = cmd.ExecuteScalar();
                myTran.Commit();
                return oReturn;

            }
            catch (Exception Ex)
            {
                if (myTran != null)
                {
                    myTran.Rollback();
                }
                throw Ex;

                return null;
            }
            finally
            {
                if ((cmd.Connection.State == ConnectionState.Open))
                {
                    cmd.Connection.Close();
                }
            }
        }

        public object RunCommandReturnOutPut(string ConnectionString, string spName, object[] selectCommandParms, ref ArrayList Output)
        {


            SqlCommand cmd = new SqlCommand();
            SqlTransaction myTran = null;

            try
            {
                cmd = CreateSPCommandWithValues(spName, ConnectionString, selectCommandParms, ref Output);
                if ((cmd.Connection.State == ConnectionState.Closed))
                {
                    cmd.Connection.Open();
                }
                myTran = cmd.Connection.BeginTransaction();
                cmd.Transaction = myTran;
                object oReturn = null;
                oReturn = cmd.ExecuteScalar();
                myTran.Commit();


                if (Output != null)
                {
                    Output = new ArrayList();
                    for (int i = 0; i < cmd.Parameters.Count; i++)
                    {

                        if (cmd.Parameters[i].Direction == ParameterDirection.InputOutput)
                        {
                            Output.Add(cmd.Parameters[i].SqlValue.ToString());
                        }

                    }
                }
                return oReturn;
            }
            catch (Exception Ex)
            {
                if (myTran != null)
                {
                    myTran.Rollback();
                }
                throw Ex;

                return null;
            }
            finally
            {
                if ((cmd.Connection.State == ConnectionState.Open))
                {
                    cmd.Connection.Close();
                }
            }
        }
        private SqlCommand CreateSPCommand(string spName, string strConn)
        {

            try
            {

                SqlConnection cn = new SqlConnection(strConn);
                SqlCommand cmd = new SqlCommand(spName, cn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlCommandBuilder cb = new SqlCommandBuilder();
                cn.Open();
                SqlCommandBuilder.DeriveParameters(cmd);
                cn.Close();

                foreach (SqlParameter param in cmd.Parameters)
                {
                    param.SourceVersion = DataRowVersion.Current;
                    // Use the param name to name the source column for the DA
                    param.SourceColumn = Convert.ToString(param.ParameterName).Remove(0, 1);

                }
                return cmd;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private SqlCommand CreateSPCommand(string spName, object[][] spParams, string strConn)
        {

            try
            {

                SqlConnection cn = new SqlConnection(strConn);
                SqlCommand cmd = new SqlCommand(spName, cn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlCommandBuilder cb = new SqlCommandBuilder();
                int i = 0;
                cn.Open();
                SqlCommandBuilder.DeriveParameters(cmd);
                cn.Close();

                foreach (SqlParameter param in cmd.Parameters)
                {
                    param.SourceVersion = DataRowVersion.Current;
                    // Use the param name to name the source column for the DA
                    if (param.ParameterName.StartsWith("@glbl_"))
                    {
                        i = 0;
                        while (param.Value == null & i < spParams.GetLength(0))
                        {
                            if ((Convert.ToString(spParams[i][0])).Equals(param.ParameterName))
                            {
                                param.Value = spParams[i][1];
                            }
                            i += 1;
                        }
                    }
                    else
                    {
                        param.SourceColumn = Convert.ToString(param.ParameterName).Remove(0, 1);
                    }
                }
                return cmd;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private SqlCommand CreateSPCommandWithValues(string spName, string strConn, object[] spParams, ref object outPutParameters)
        {
            try
            {
                if ((outPutParameters != null))
                {
                    //outPutParameters.Clear()
                }

                SqlConnection cn = new SqlConnection(strConn);
                SqlCommand cmd = new SqlCommand(spName, cn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlCommandBuilder cb = new SqlCommandBuilder();
                cn.Open();
                SqlCommandBuilder.DeriveParameters(cmd);
                cn.Close();

                int i = 0;
                int j = 0;
                foreach (SqlParameter param in cmd.Parameters)
                {
                    param.SourceVersion = DataRowVersion.Current;
                    // Use the param name to name the source column for the DA
                    if (spParams != null & param.ParameterName.StartsWith("@glbl_"))
                    {
                        i = 0;
                        while (param.Value == null & i < spParams.GetLength(0))
                        {
                            if ((Convert.ToString(spParams[i])).Equals(param.ParameterName))
                            {
                                param.Value = spParams[i];
                            }
                            i += 1;
                        }
                    }
                    else if (((outPutParameters != null)) & (param.Direction == ParameterDirection.Output | param.Direction == ParameterDirection.InputOutput))
                    {
                        object obj = new object();
                        obj = (object)param.ParameterName;
                        outPutParameters = obj;
                        param.Value = DBNull.Value;
                        j += 1;
                    }
                    else
                    {
                        param.SourceColumn = Convert.ToString(param.ParameterName).Remove(0, 1);
                    }

                }

                return cmd;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private SqlCommand CreateSPCommandWithValues(string spName, string strConn, object[] selectCommandParms, ref ArrayList outPutParameters)
        {

            try
            {

                if ((outPutParameters != null))
                {
                    outPutParameters.Clear();
                }

                SqlConnection cn = new SqlConnection(strConn);
                SqlCommand cmd = new SqlCommand(spName, cn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlCommandBuilder cb = new SqlCommandBuilder();
                cn.Open();
                SqlCommandBuilder.DeriveParameters(cmd);
                cn.Close();

                int i = 0;
                foreach (SqlParameter param in cmd.Parameters)
                {
                    param.SourceVersion = DataRowVersion.Current;
                    param.SourceColumn = Convert.ToString(param.ParameterName).Remove(0, 1);
                    if ((selectCommandParms != null))
                    {
                        if ((param.Direction == ParameterDirection.Output | param.Direction == ParameterDirection.InputOutput | selectCommandParms[i] == null))
                        {
                            param.Value = DBNull.Value;
                        }
                        else
                        {
                            param.Value = selectCommandParms[i];
                        }

                        if (((outPutParameters != null)))
                        {
                            if ((param.Direction == ParameterDirection.Output | param.Direction == ParameterDirection.InputOutput))
                            {
                                object obj = new object();
                                obj = (object)param.ParameterName;
                                outPutParameters.Add(obj);

                            }
                        }
                        i += 1;
                    }
                }
                return cmd;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public object GetSclar(string ConnectionString, string spName, object[] selectCommandParms)
        {


            SqlCommand cmd = new SqlCommand();
            SqlTransaction myTran = null;
            object obj = null;
            try
            {
                cmd = CreateSPCommandWithValues(spName, ConnectionString, selectCommandParms, ref obj);
                if ((cmd.Connection.State == ConnectionState.Closed))
                {
                    cmd.Connection.Open();
                }
                myTran = cmd.Connection.BeginTransaction();
                cmd.Transaction = myTran;
                object oReturn = null;
                oReturn = cmd.ExecuteScalar();
                myTran.Commit();
                return oReturn;

            }
            catch (Exception Ex)
            {
                if (myTran != null)
                {
                    myTran.Rollback();
                }
                throw Ex;
                return null;
            }
            finally
            {
                if ((cmd.Connection.State == ConnectionState.Open))
                {
                    cmd.Connection.Close();
                }
            }
        }

    }
}