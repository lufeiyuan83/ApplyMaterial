using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.Win32;
using System.Text;

/// <summary>
/// 数据库底层封装类
/// </summary>
namespace Template
{
    public class DataAccess : IDisposable
    {
        public SqlConnection sqlConnection;
        public SqlCommand sqlCommand;
        public SqlDataAdapter sqlDataAdapter;
        public SqlDataReader sqlDataReader;
        public SqlTransaction sqlTransaction;
        public List<SqlParameter> sqlParameters;
        public SqlBulkCopy sqlBulkCopy;
        public DataTable dataTable;
        public DataSet dataSet;
        private static int timeOut = 60;
        //private static string connectionString = "Data Source=10.148.15.80;Initial Catalog=Template;Persist Security Info=True;User ID=sa;Password=FIS232311109Qq";
        private static string connectionString = "Data Source=.;Initial Catalog=Template;Persist Security Info=True;User ID=sa;Password=FIS232311109Qq";
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        public DataAccess()
        {
            if (!string.IsNullOrEmpty(connectionString))
            {
                this.sqlConnection = new SqlConnection(connectionString);
                this.sqlParameters = new List<SqlParameter>();
                this.sqlBulkCopy = new SqlBulkCopy(this.sqlConnection.ConnectionString, SqlBulkCopyOptions.FireTriggers | SqlBulkCopyOptions.CheckConstraints);
                try
                {
                    this.Open();
                    RegistryKey registryKey = Registry.CurrentUser.OpenSubKey(@"Software\IIVI\FIS\PDE");
                    if (registryKey != null)
                    {
                        timeOut = registryKey.GetValue("TimeOut") == null ? 30 : Convert.ToInt32( registryKey.GetValue("TimeOut"));
                    }
                }
                catch
                {
                    new Exception("network interruption!");
                }
            }
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="m_connString">连接字符串</param>
        public DataAccess(string m_connString)
        {
            if (!string.IsNullOrEmpty(m_connString))
            {
                this.sqlConnection = new SqlConnection(m_connString);
                this.sqlParameters = new List<SqlParameter>();
                this.sqlBulkCopy = new SqlBulkCopy(this.sqlConnection);
                try
                {
                    this.Open();
                    RegistryKey registryKey = Registry.CurrentUser.OpenSubKey(@"Software\IIVI\FIS\PDE");
                    if (registryKey != null)
                    {
                        timeOut = registryKey.GetValue("TimeOut") == null ? 600 : Convert.ToInt32(registryKey.GetValue("TimeOut"));
                        if (timeOut <= 60)
                            timeOut = 600;
                    }
                }
                catch
                {
                    new Exception("network interruption!");
                }
            }
        }
        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
            //try
            //{                
            //    if (this.sqlTransaction != null)
            //        this.sqlTransaction.Dispose();
            //    this.sqlParameters.Clear();
            //    if (this.dataSet != null)
            //        this.dataSet.Dispose();
            //    if (this.sqlDataAdapter != null)
            //        this.sqlDataAdapter.Dispose();
            //    if (this.sqlCommand != null)
            //        this.sqlCommand.Dispose();
            //    if (this.sqlConnection.State == ConnectionState.Open)
            //        this.sqlConnection.Close();
            //    if (this.sqlBulkCopy != null)
            //        this.sqlBulkCopy.Close();
            //    this.Close();
            //    System.GC.SuppressFinalize(this);
            //}
            //catch (Exception ex)
            //{
            //    WriteLog(ex, "Dispose()", strOperator);
            //}
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.sqlTransaction != null)
                    this.sqlTransaction.Dispose();
                this.sqlParameters.Clear();
                if (this.dataSet != null)
                    this.dataSet.Dispose();
                if (this.sqlDataAdapter != null)
                    this.sqlDataAdapter.Dispose();
                if (this.sqlCommand != null)
                    this.sqlCommand.Dispose();
                if (this.sqlConnection.State == ConnectionState.Open)
                    this.sqlConnection.Close();
                if (this.sqlBulkCopy != null)
                    this.sqlBulkCopy.Close();
                this.Close();
            }
        }
        /// <summary>
        /// 打开连接
        /// </summary>
        protected void Open()
        {
            try
            {
                if (this.sqlConnection != null)
                {
                    if (this.sqlConnection.State == ConnectionState.Broken)
                    {
                        this.sqlConnection.Close();
                        this.sqlConnection.Open();
                    }
                    else if (this.sqlConnection.State == ConnectionState.Closed)
                        this.sqlConnection.Open();
                }
            }
            catch (Exception ex)
            {
                WriteLog(ex, "Open()|State=" + this.sqlConnection.State.ToString(), "Export");
            }
        }
        /// <summary>
        /// 关闭连接
        /// </summary>
        protected void Close()
        {
            try
            {
                if (this.sqlConnection.State != ConnectionState.Closed)
                {
                    this.sqlConnection.Close();
                    ClearPool();
                }
            }
            catch (Exception ex)
            {
                WriteLog(ex, "Close()", "Export");
            }
        }
        public void ClearPool()
        {
            SqlConnection.ClearPool(this.sqlConnection);
        }
        /// <summary>
        /// 开始事务
        /// </summary>
        public void BeginTransaction()
        {
            try
            {
                this.sqlCommand = this.sqlConnection.CreateCommand();
                this.sqlCommand.CommandTimeout = timeOut;
                this.sqlTransaction = this.sqlConnection.BeginTransaction();
                this.sqlCommand.Transaction = this.sqlTransaction;
            }
            catch (Exception ex)
            {
                WriteLog(ex, "BeginTransaction()", "Export");
            }
        }
        /// <summary>
        /// 提交事务
        /// </summary>
        public void CommitTransaction()
        {
            try
            {
                //if (this.sqlTransaction != null)
                    this.sqlTransaction.Commit();
            }
            catch (Exception ex)
            {
                WriteLog(ex, "CommitTransaction()", "Export");
            }
        }
        /// <summary>
        /// 回滚事务
        /// </summary>
        public void RollbackTransaction()
        {
            try
            {
                //if (this.sqlTransaction != null)
                    this.sqlTransaction.Rollback();
            }
            catch (Exception ex)
            {
                WriteLog(ex, "RollbackTransaction()", "Export");
            }          
        }
        /// <summary>
        /// 创建一个Sql参数
        /// </summary>
        /// <param name="paramName">参数名</param>
        /// <param name="paramValue">参数值</param>
        /// <returns>Sql参数</returns>
        public SqlParameter CreateSqlParameter(string paramName, object paramValue)
        {
            return new SqlParameter(paramName, paramValue);
        }
        /// <summary>
        /// 创建一个Sql参数
        /// </summary>
        /// <param name="paramName">参数名</param>
        /// <param name="type">Sql类型</param>
        /// <param name="size">大小</param>
        /// <param name="paramValue">参数值</param>
        /// <returns>Sql参数</returns>
        public SqlParameter CreateSqlParameter(string paramName,SqlDbType type,int size, object paramValue)
        {
            SqlParameter param =null;
            if(size==0)
                param = new SqlParameter(paramName, type);
            else
                param = new SqlParameter(paramName, type, size);
            param.Value = paramValue;
            return param;
        }
        /// <summary>
        /// 执行Sql语句
        /// </summary>
        /// <param name="commandText">Sql语句</param>
        /// <returns>影响行数</returns>
        public int ExecuteCommand(string commandText)
        {
            try
            {
                if (this.sqlCommand == null)
                    this.sqlCommand = new SqlCommand(commandText, this.sqlConnection);
                else
                    this.sqlCommand.CommandText = commandText;
                this.sqlCommand.CommandTimeout = timeOut;
                return this.sqlCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                WriteLog(ex, "ExecuteCommand(commandText=" + commandText + ")", "Export");
                throw;
            }
            finally
            {
                //this.sqlCommand.Dispose();
            }
        }
        /// <summary>
        /// 执行带参数的Sql语句
        /// </summary>
        /// <param name="commandText">Sql语句</param>
        /// <param name="parameters">参数列表</param>
        /// <returns>影响行数</returns>
        public int ExecuteCommand(string commandText, IList<SqlParameter> parameters)
        {
            try
            {
                if (this.sqlCommand == null)
                    this.sqlCommand = new SqlCommand(commandText, this.sqlConnection);
                else
                    this.sqlCommand.CommandText = commandText;
                this.sqlCommand.CommandTimeout = timeOut;
                this.sqlCommand.Parameters.Clear();
                if (parameters != null && parameters.Count > 0)
                {
                    foreach (SqlParameter param in parameters)
                        this.sqlCommand.Parameters.Add(param);
                }
                return this.sqlCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                string strParameters = "";
                if (parameters != null && parameters.Count > 0)
                {
                    foreach (SqlParameter param in parameters)
                        strParameters += param.SqlValue.ToString() + ";";
                    strParameters = strParameters.Substring(0, strParameters.Length - 1);
                }
                WriteLog(ex, "ExecuteCommand(commandText=" + commandText + ",parameters=" + strParameters + ")", "Export");
                throw;
            }
            finally
            {
                this.sqlCommand.Parameters.Clear();
                //this.sqlCommand.Dispose();
            }
        }
        /// <summary>
        /// 执行事务
        /// </summary>
        /// <param name="commandTexts">Sql语句列表</param>
        public bool ExecuteTransact(IList<string> commandTexts)
        {
            try
            {
                if (this.sqlCommand == null)
                    this.sqlCommand = this.sqlConnection.CreateCommand();
                this.sqlCommand.CommandType = CommandType.StoredProcedure;
                this.sqlCommand.CommandTimeout = timeOut;
                foreach (string cmdText in commandTexts)
                {                    
                    this.sqlCommand.CommandText = cmdText;
                    this.sqlCommand.ExecuteNonQuery();
                }
                return true;
            }
            catch (Exception ex)
            {
                string strCommandTexts = "";
                if (commandTexts != null && commandTexts.Count > 0)
                {
                    for (int i = 0; i < commandTexts.Count; i++)
                        strCommandTexts += commandTexts[i] + ";";
                    strCommandTexts = strCommandTexts.Substring(0, strCommandTexts.Length - 1);
                }
                WriteLog(ex, "ExecuteTransact(commandTexts=" + strCommandTexts + ")", "Export");
                return false;
            }
            finally
            {
                //this.sqlCommand.Dispose();
            }
        }
        /// <summary>
        /// 执行一个不带参数的存储过程
        /// </summary>
        /// <param name="storedProcedureName">存储过程名</param>
        /// <returns>返回受影响行数</returns>
        public int ExecuteTransact(string storedProcedureName)
        {
            try
            {
                if (this.sqlCommand == null)
                    this.sqlCommand = new SqlCommand(storedProcedureName, this.sqlConnection);
                this.sqlCommand.CommandTimeout = timeOut;
                this.sqlCommand.CommandType = CommandType.StoredProcedure;
                this.sqlCommand.CommandText = storedProcedureName;                
                return this.sqlCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                WriteLog(ex, "ExecuteTransact(storedProcedureName=" + storedProcedureName + ")", "Export");
                return 0;
            }
        }
        /// <summary>
        /// 执行一个带参数的存储过程
        /// </summary>
        /// <param name="storedProcedureName">存储过程名</param>
        /// <param name="parameters">参数列表</param>
        /// <returns>返回受影响行数</returns>
        public int ExecuteTransact(string storedProcedureName, IList<SqlParameter> parameters)
        {
            try
            {
                if (this.sqlCommand == null)
                    this.sqlCommand = new SqlCommand(storedProcedureName, this.sqlConnection);
                this.sqlCommand.CommandTimeout = timeOut;
                this.sqlCommand.CommandType = CommandType.StoredProcedure;
                this.sqlCommand.CommandText = storedProcedureName;
                this.sqlCommand.Parameters.Clear();
                if (parameters != null && parameters.Count > 0)
                {
                    foreach (SqlParameter param in parameters)
                        this.sqlCommand.Parameters.Add(param);
                }
                return this.sqlCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                string strParameters = "";
                if (parameters != null && parameters.Count > 0)
                {
                    foreach (SqlParameter param in parameters)
                        strParameters += param.SqlValue.ToString() + ";";
                    strParameters = strParameters.Substring(0, strParameters.Length - 1);
                }
                WriteLog(ex, "ExecuteTransact(storedProcedureName=" + storedProcedureName + ",parameters=" + strParameters + ")", "Export");
                return 0;
            }
        }
        /// <summary>
        /// 执行一个带参数的存储过程
        /// </summary>
        /// <param name="storedProcedureName">存储过程名</param>
        /// <param name="parameters">参数列表</param>
        /// <returns>返回受影响行数</returns>
        public DataTable GetDataTableByTransact(string storedProcedureName, IList<SqlParameter> parameters)
        {
            try
            {
                if (this.sqlCommand == null)
                    this.sqlCommand = new SqlCommand(storedProcedureName, this.sqlConnection);
                this.sqlCommand.CommandTimeout = timeOut;
                this.sqlCommand.CommandType = CommandType.StoredProcedure;
                this.sqlCommand.CommandText = storedProcedureName;
                this.sqlCommand.Parameters.Clear();
                if (parameters != null && parameters.Count > 0)
                {
                    foreach (SqlParameter param in parameters)
                        this.sqlCommand.Parameters.Add(param);
                }
                this.sqlDataReader = this.sqlCommand.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(this.sqlDataReader);
                return dt;
            }
            catch (Exception ex)
            {
                string strParameters = "";
                if (parameters != null && parameters.Count > 0)
                {
                    foreach (SqlParameter param in parameters)
                        strParameters += param.SqlValue.ToString() + ";";
                    strParameters = strParameters.Substring(0, strParameters.Length - 1);
                }
                WriteLog(ex, "ExecuteTransact(storedProcedureName=" + storedProcedureName + ",parameters=" + strParameters + ")", "Export");
                return null;
            }
        }
        /// <summary>
        /// 读取数据
        /// </summary>
        /// <param name="commandText">Sql语句</param>
        /// <returns>SqlDataReader对象</returns>
        public SqlDataReader ExecuteReader(string commandText)
        {
            try
            {
                if (this.sqlCommand == null)
                    this.sqlCommand = new SqlCommand(commandText, this.sqlConnection);
                else
                    this.sqlCommand.CommandText = commandText;
                this.sqlCommand.CommandTimeout = timeOut;
                return this.sqlCommand.ExecuteReader();
            }
            catch (Exception ex)
            {
                WriteLog(ex, "ExecuteReader(commandText=" + commandText + ")", "Export");
                return null;
            }
        }
        /// <summary>
        /// 读取数据
        /// </summary>
        /// <param name="commandText">Sql语句</param>
        /// <param name="parameters">参数列表</param>
        /// <returns>SqlDataReader对象</returns>
        public SqlDataReader ExecuteReader(string commandText, IList<SqlParameter> parameters)
        {
            try
            {
                if (this.sqlCommand == null)
                    this.sqlCommand = new SqlCommand(commandText, this.sqlConnection);
                else
                    this.sqlCommand.CommandText = commandText;
                this.sqlCommand.CommandTimeout = timeOut;
                this.sqlCommand.Parameters.Clear();
                if (parameters != null && parameters.Count > 0)
                {
                    foreach (SqlParameter param in parameters)
                        this.sqlCommand.Parameters.Add(param);
                }
                return this.sqlCommand.ExecuteReader();
            }
            catch (Exception ex)
            {
                string strParameters = "";
                if (parameters != null && parameters.Count > 0)
                {
                    foreach (SqlParameter param in parameters)
                        strParameters += param.SqlValue.ToString() + ";";
                    strParameters = strParameters.Substring(0, strParameters.Length - 1);
                }
                WriteLog(ex, "ExecuteReader(commandText=" + commandText + ",parameters=" + strParameters + ")", "Export");
                return null;
            }
            finally
            {
                this.sqlCommand.Parameters.Clear();
            }
        }
        /// <summary>
        /// 获得数据行
        /// </summary>
        /// <param name="commandText">Sql语句</param>
        /// <returns>数据行</returns>
        public DataRow GetDataRow(string commandText)
        {
            if (!string.IsNullOrEmpty(commandText))
            {
                DataTable dt = this.GetDataTable(commandText);
                if (dt != null && dt.Rows.Count > 0)
                    return dt.Rows[0];
                else
                    return null;
            }
            else
                return null;
        }
        /// <summary>
        /// 获得数据行
        /// </summary>
        /// <param name="commandText">Sql语句</param>
        /// <param name="rowID">行号</param>
        /// <returns>数据行</returns>
        public DataRow GetDataRow(string commandText, int rowID)
        {
            if (!string.IsNullOrEmpty(commandText) && rowID >= 0)
            {
                DataTable dt = this.GetDataTable(commandText);
                if (dt != null && dt.Rows.Count > 0)
                    return dt.Rows[rowID];
                else
                    return null;
            }
            else
                return null;
        }
        /// <summary>
        /// 获得数据行
        /// </summary>
        /// <param name="commandText">Sql语句</param>
        /// <param name="rowID">行号</param>
        /// <returns>数据行</returns>
        public DataRow GetDataRow(string commandText, IList<SqlParameter> parameters)
        {
            if (!string.IsNullOrEmpty(commandText))
            {
                DataTable dt = this.GetDataTable(commandText, parameters);
                if (dt != null && dt.Rows.Count > 0)
                    return dt.Rows[0];
                else
                    return null;
            }
            else
                return null;
        }
        /// <summary>
        /// 获得数据行
        /// </summary>
        /// <param name="commandText">Sql语句</param>
        /// <param name="rowID">行号</param>
        /// <returns>数据行</returns>
        public DataRow GetDataRow(string commandText, IList<SqlParameter> parameters, int rowID)
        {
            if (!string.IsNullOrEmpty(commandText) && rowID >= 0)
            {
                DataTable dt = this.GetDataTable(commandText, parameters);
                if (dt != null && dt.Rows.Count > 0)
                    return dt.Rows[rowID];
                else
                    return null;
            }
            else
                return null;
        }
        /// <summary>
        /// 获得数据行
        /// </summary>
        /// <param name="commandText">Sql语句</param>
        /// <param name="tableName">表名</param>
        /// <param name="rowID">行号</param>
        /// <returns>数据行</returns>
        public DataRow GetDataRow(string commandText, string tableName, int rowID)
        {
            if (!string.IsNullOrEmpty(commandText) && !string.IsNullOrEmpty(tableName) && rowID >= 0)
            {
                DataSet ds = this.GetDataSet(commandText);
                if (ds != null && ds.Tables[tableName].Rows.Count > 0)
                    return ds.Tables[tableName].Rows[rowID];
                else
                    return null;
            }
            else
                return null;
        }
        /// <summary>
        /// 获得数据列
        /// </summary>
        /// <param name="commandText">Sql语句</param>
        /// <param name="columnID">列号</param>
        /// <returns>数据列</returns>
        public DataColumn GetDataColumn(string commandText, int columnID)
        {
            if (!string.IsNullOrEmpty(commandText) && columnID >= 0)
            {
                DataTable dt = this.GetDataTable(commandText);
                if (dt != null && dt.Rows.Count > 0)
                    return dt.Columns[columnID];
                else
                    return null;
            }
            else
                return null;
        }
        /// <summary>
        /// 获得数据列
        /// </summary>
        /// <param name="commandText">Sql语句</param>
        /// <param name="columnID">列号</param>
        /// <param name="parameters">参数列表</param>
        /// <returns>数据列</returns>
        public DataColumn GetDataColumn(string commandText, int columnID, IList<SqlParameter> parameters)
        {
            if (!string.IsNullOrEmpty(commandText) && columnID >= 0)
            {
                DataTable dt = this.GetDataTable(commandText, parameters);
                if (dt != null && dt.Rows.Count > 0)
                    return dt.Columns[columnID];
                else
                    return null;
            }
            else
                return null;
        }
        /// <summary>
        /// 获得数据列
        /// </summary>
        /// <param name="commandText">Sql语句</param>
        /// <param name="columnName">列名</param>
        /// <returns>数据列</returns>
        public DataColumn GetDataColumn(string commandText, string columnName)
        {
            if (!string.IsNullOrEmpty(commandText) && !string.IsNullOrEmpty(columnName))
            {
                DataTable dt = this.GetDataTable(commandText);
                if (dt != null && dt.Rows.Count > 0)
                    return dt.Columns[columnName];
                else
                    return null;
            }
            else
                return null;
        }
        /// <summary>
        /// 获得数据列
        /// </summary>
        /// <param name="commandText">Sql语句</param>
        /// <param name="columnName">列名</param>
        /// <param name="parameters">参数列表</param>
        /// <returns>数据列</returns>
        public DataColumn GetDataColumn(string commandText, string columnName, IList<SqlParameter> parameters)
        {
            if (!string.IsNullOrEmpty(commandText) && !string.IsNullOrEmpty(columnName))
            {
                DataTable dt = this.GetDataTable(commandText, parameters);
                if (dt != null && dt.Rows.Count > 0)
                    return dt.Columns[columnName];
                else
                    return null;
            }
            else
                return null;
        }
        /// <summary>
        /// 获得数据列
        /// </summary>
        /// <param name="commandText">Sql语句</param>
        /// <param name="tableName">表名</param>
        /// <param name="columnID">列号</param>
        /// <returns>数据列</returns>
        public DataColumn GetDataColumn(string commandText, string tableName, int columnID)
        {
            if (!string.IsNullOrEmpty(commandText) && !string.IsNullOrEmpty(tableName) && columnID >= 0)
            {
                DataSet ds = this.GetDataSet(commandText);
                if (ds != null && ds.Tables[tableName].Rows.Count > 0)
                    return ds.Tables[tableName].Columns[columnID];
                else
                    return null;
            }
            else
                return null;
        }
        /// <summary>
        /// 获得数据列
        /// </summary>
        /// <param name="commandText">Sql语句</param>
        /// <param name="tableName">表名</param>
        /// <param name="columnID">列号</param>
        /// <param name="parameters">参数列表</param>
        /// <returns>数据列</returns>
        public DataColumn GetDataColumn(string commandText, string tableName, int columnID, IList<SqlParameter> parameters)
        {
            if (!string.IsNullOrEmpty(commandText) && !string.IsNullOrEmpty(tableName) && columnID >= 0)
            {
                DataSet ds = this.GetDataSet(commandText, parameters);
                if (ds != null && ds.Tables[tableName].Rows.Count > 0)
                    return ds.Tables[tableName].Columns[columnID];
                else
                    return null;
            }
            else
                return null;
        }
        /// <summary>
        /// 获得数据列
        /// </summary>
        /// <param name="commandText">Sql语句</param>
        /// <param name="tableName">表名</param>
        /// <param name="columnName">列名</param>
        /// <returns>数据列</returns>
        public DataColumn GetDataColumn(string commandText, string tableName, string columnName)
        {
            if (!string.IsNullOrEmpty(commandText) && !string.IsNullOrEmpty(tableName) && !string.IsNullOrEmpty(columnName))
            {
                DataSet ds = this.GetDataSet(commandText);
                if (ds != null && ds.Tables[tableName].Columns.Count > 0)
                    return ds.Tables[tableName].Columns[columnName];
                else
                    return null;
            }
            else
                return null;
        }
        /// <summary>
        /// 获得数据列
        /// </summary>
        /// <param name="commandText">Sql语句</param>
        /// <param name="tableName">表名</param>
        /// <param name="columnName">列名</param>
        /// <param name="parameters">参数列表</param>
        /// <returns>数据列</returns>
        public DataColumn GetDataColumn(string commandText, string tableName, string columnName, IList<SqlParameter> parameters)
        {
            if (!string.IsNullOrEmpty(commandText) && !string.IsNullOrEmpty(tableName) && !string.IsNullOrEmpty(columnName))
            {
                DataSet ds = this.GetDataSet(commandText,parameters);
                if (ds != null && ds.Tables[tableName].Columns.Count > 0)
                    return ds.Tables[tableName].Columns[columnName];
                else
                    return null;
            }
            else
                return null;
        }
        /// <summary>
        /// 获得数据表
        /// </summary>
        /// <param name="commandText">Sql语句</param>
        /// <returns>数据表</returns>
        public DataTable GetDataTable(string commandText)
        {
            if (!string.IsNullOrEmpty(commandText))
            {
                try
                {
                    this.sqlDataAdapter = new SqlDataAdapter(commandText, this.sqlConnection);
                    DataTable table = new DataTable();
                    this.sqlDataAdapter.Fill(table);
                    return table;
                }
                catch (Exception ex)
                {
                    WriteLog(ex, "GetDataTable(commandText=" + commandText + ")", "Export");
                    return null;
                }
                finally
                {
                    //this.sqlDataAdapter.Dispose();
                }
            }
            else
                return null;
        }
        /// <summary>
        /// 获得数据表
        /// </summary>
        /// <param name="commandText">Sql语句</param>
        /// <param name="parameters">参数列表</param>
        /// <returns>数据表</returns>
        public DataTable GetDataTable(string commandText, IList<SqlParameter> parameters)
        {
            if (!string.IsNullOrEmpty(commandText))
            {
                try
                {
                    if (this.sqlCommand == null)
                        this.sqlCommand = new SqlCommand(commandText, this.sqlConnection);
                    else
                        this.sqlCommand.CommandText = commandText;
                    this.sqlCommand.CommandTimeout = timeOut;
                    this.sqlCommand.Parameters.Clear();
                    if (parameters != null && parameters.Count > 0)
                    {
                        foreach (SqlParameter param in parameters)
                            this.sqlCommand.Parameters.Add(param);
                    }
                    this.sqlDataAdapter = new SqlDataAdapter(this.sqlCommand);
                    DataTable table = new DataTable();
                    this.sqlDataAdapter.Fill(table);
                    return table;
                }
                catch (Exception ex)
                {
                    string strParameters = "";
                    if (parameters != null && parameters.Count > 0)
                    {
                        foreach (SqlParameter param in parameters)
                            strParameters += param.SqlValue.ToString() + ";";
                        strParameters = strParameters.Substring(0, strParameters.Length - 1);
                    }
                    WriteLog(ex, "GetDataTable(commandText=" + commandText + ",parameters=" + strParameters + ")", "Export");
                    return null;
                }
            }
            else
                return null;
        }
        /// <summary>
        /// 获得数据集
        /// </summary>
        /// <param name="commandText">Sql语句</param>
        /// <returns>数据集</returns>
        public DataSet GetDataSet(string commandText)
        {
            if (!string.IsNullOrEmpty(commandText))
            {
                try
                {
                    this.sqlCommand = new SqlCommand(commandText, this.sqlConnection);
                    this.sqlCommand.CommandTimeout = timeOut;
                    this.sqlDataAdapter = new SqlDataAdapter(this.sqlCommand);
                    dataSet = new DataSet();
                    this.sqlDataAdapter.Fill(dataSet);
                    return dataSet;
                }
                catch (Exception ex)
                {
                    WriteLog(ex, "GetDataSet(commandText=" + commandText + ")", "Export");
                    return null;
                }
            }
            else
                return null;
        }
        /// <summary>
        /// 获得数据集
        /// </summary>
        /// <param name="commandText">Sql语句</param>
        /// <param name="parameters">参数列表</param>
        /// <returns>数据集</returns>
        public DataSet GetDataSet(string commandText, IList<SqlParameter> parameters)
        {
            if (!string.IsNullOrEmpty(commandText))
            {
                try
                {
                    if (this.sqlCommand == null)
                        this.sqlCommand = new SqlCommand(commandText, this.sqlConnection);
                    else
                        this.sqlCommand.CommandText = commandText;
                    this.sqlCommand.CommandTimeout = timeOut;
                    this.sqlCommand.Parameters.Clear();
                    if (parameters != null && parameters.Count > 0)
                    {
                        foreach (SqlParameter param in parameters)
                            this.sqlCommand.Parameters.Add(param);
                    }
                    this.sqlDataAdapter = new SqlDataAdapter(this.sqlCommand);
                    dataSet = new DataSet();
                    this.sqlDataAdapter.Fill(dataSet);
                    return dataSet;
                }
                catch (Exception ex)
                {
                    string strParameters = "";
                    if (parameters != null && parameters.Count > 0)
                    {
                        foreach (SqlParameter param in parameters)
                            strParameters += param.SqlValue.ToString() + ";";
                        strParameters = strParameters.Substring(0, strParameters.Length - 1);
                    }
                    WriteLog(ex, "GetDataSet(commandText=" + commandText + ",parameters=" + strParameters + ")", "Export");
                    return null;
                }
            }
            else
                return null;
        }
        /// <summary>
        /// 获得第一行第一列值
        /// </summary>
        /// <param name="commandText">Sql语句</param>
        /// <returns>第一行第一列值</returns>
        public object GetScalar(string commandText)
        {
            if (!string.IsNullOrEmpty(commandText))
            {
                try
                {
                    if (this.sqlCommand == null)
                        this.sqlCommand = new SqlCommand(commandText, this.sqlConnection);
                    else
                        this.sqlCommand.CommandText = commandText;
                    this.sqlCommand.CommandTimeout = timeOut;
                    return this.sqlCommand.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    WriteLog(ex, "GetScalar(commandText=" + commandText + ")", "Export");
                    return null;
                }
            }
            else
                return null;
        }
        /// <summary>
        /// 获得第一行第一列值
        /// </summary>
        /// <param name="commandText">Sql语句</param>
        /// <param name="parameters">参数列表</param>
        /// <returns>第一行第一列值</returns>
        public object GetScalar(string commandText, IList<SqlParameter> parameters)
        {
            if (!string.IsNullOrEmpty(commandText))
            {
                try
                {
                    if (this.sqlCommand == null)
                        this.sqlCommand = new SqlCommand(commandText, this.sqlConnection);
                    else
                        this.sqlCommand.CommandText = commandText;
                    this.sqlCommand.CommandTimeout = timeOut;
                    this.sqlCommand.Parameters.Clear();
                    if (parameters != null && parameters.Count > 0)
                    {
                        foreach (SqlParameter param in parameters)
                            this.sqlCommand.Parameters.Add(param);
                    }
                    return this.sqlCommand.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    string strParameters = "";
                    if (parameters != null && parameters.Count > 0)
                    {
                        foreach (SqlParameter param in parameters)
                            strParameters += param.SqlValue.ToString() + ";";
                        strParameters = strParameters.Substring(0, strParameters.Length - 1);
                    }
                    WriteLog(ex, "GetScalar(commandText=" + commandText + ",parameters=" + strParameters + ")", "Export");
                    return null;
                }
            }
            else
                return null;
        }
        /// <summary>
        /// 获得所有列名
        /// </summary>
        /// <param name="commandText">Sql语句</param>
        /// <returns>所有列名</returns>
        public List<string> GetColumnNames(string commandText)
        {
            if (!string.IsNullOrEmpty(commandText))
            {
                List<string> columnNames = new List<string>();
                DataColumnCollection cols = this.GetDataTable(commandText).Columns;
                foreach (DataColumn col in cols)
                    columnNames.Add(col.ColumnName.Trim());
                return columnNames;
            }
            else
                return null;
        }
        /// <summary>
        /// 获得所有列名
        /// </summary>
        /// <param name="commandText">Sql语句</param>
        /// <param name="parameters">参数列表</param>
        /// <returns>所有列名</returns>
        public List<string> GetColumnNames(string commandText, IList<SqlParameter> parameters)
        {
            if (!string.IsNullOrEmpty(commandText))
            {
                List<string> columnNames = new List<string>();
                DataColumnCollection cols = this.GetDataTable(commandText, parameters).Columns;
                foreach (DataColumn col in cols)
                    columnNames.Add(col.ColumnName.Trim());
                return columnNames;
            }
            else
                return null;
        }
        /// <summary>
        /// 获得键值对
        /// </summary>
        /// <param name="commandText">Sql语句</param>
        /// <returns>键值对</returns>
        public Dictionary<object, object> GetKeyValues(string commandText)
        {
            if (!string.IsNullOrEmpty(commandText))
            {
                try
                {
                    if (this.sqlCommand == null)
                        this.sqlCommand = new SqlCommand(commandText, this.sqlConnection);
                    else
                        this.sqlCommand.CommandText = commandText;
                    this.sqlCommand.CommandTimeout = timeOut;
                    this.sqlDataReader = this.sqlCommand.ExecuteReader();
                    Dictionary<object, object> values = new Dictionary<object, object>();
                    this.sqlDataReader.Read();
                    if (this.sqlDataReader.HasRows)
                    {
                        for (int i = 0; i < this.sqlDataReader.FieldCount; i++)
                            values.Add(this.sqlDataReader.GetName(i), this.sqlDataReader.GetValue(i));
                    }
                    return values;
                }
                catch (Exception ex)
                {
                    WriteLog(ex, "GetKeyValues(commandText=" + commandText + ")", "Export");
                    return null;
                }
            }
            else
                return null;
        }
        /// <summary>
        /// 获得键值对
        /// </summary>
        /// <param name="commandText">Sql语句</param>
        /// <param name="parameters">参数列表</param>
        /// <returns>键值对</returns>
        public Dictionary<object, object> GetKeyValues(string commandText, IList<SqlParameter> parameters)
        {
            if (!string.IsNullOrEmpty(commandText))
            {
                try
                {
                    if (this.sqlCommand == null)
                        this.sqlCommand = new SqlCommand(commandText, this.sqlConnection);
                    else
                        this.sqlCommand.CommandText = commandText;
                    this.sqlCommand.CommandTimeout = timeOut;
                    this.sqlCommand.Parameters.Clear();
                    if (parameters != null && parameters.Count > 0)
                    {
                        foreach (SqlParameter param in parameters)
                            this.sqlCommand.Parameters.Add(param);
                    }
                    this.sqlDataReader = this.sqlCommand.ExecuteReader();
                    Dictionary<object, object> values = new Dictionary<object, object>();
                    this.sqlDataReader.Read();
                    if (this.sqlDataReader.HasRows)
                    {
                        for (int i = 0; i < this.sqlDataReader.FieldCount; i++)
                            values.Add(this.sqlDataReader.GetName(i), this.sqlDataReader.GetValue(i));
                    }
                    return values;
                }
                catch (Exception ex)
                {
                    string strParameters = "";
                    if (parameters != null && parameters.Count > 0)
                    {
                        foreach (SqlParameter param in parameters)
                            strParameters += param.SqlValue.ToString() + ";";
                        strParameters = strParameters.Substring(0, strParameters.Length - 1);
                    }
                    WriteLog(ex, "GetKeyValues(commandText=" + commandText + ",parameters=" + strParameters + ")", "Export");
                    return null;
                }
            }
            else
                return null;
        }
        /// <summary>
        /// 获得行数
        /// </summary>
        /// <param name="commandText">Sql语句</param>
        /// <returns>行数</returns>
        public int GetRowCount(string commandText)
        {
            DataTable dt = this.GetDataTable(commandText);
            if (dt != null && dt.Rows.Count > 0)
                return dt.Rows.Count;
            else
                return 0;
        }
        /// <summary>
        /// 获得行数
        /// </summary>
        /// <param name="commandText">Sql语句</param>
        /// <param name="parameters">参数列表</param>
        /// <returns>行数</returns>
        public int GetRowCount(string commandText, IList<SqlParameter> parameters)
        {
            DataTable dt = this.GetDataTable(commandText, parameters);
            if (dt != null && dt.Rows.Count > 0)
                return dt.Rows.Count;
            else
                return 0;
        }
        /// <summary>
        /// 获得列数
        /// </summary>
        /// <param name="commandText">Sql语句</param>
        /// <returns>列数</returns>
        public int GetColumnCount(string commandText)
        {
            DataTable dt = this.GetDataTable(commandText);
            if (dt != null && dt.Columns.Count > 0)
                return dt.Columns.Count;
            else
                return 0;
        }
        /// <summary>
        /// 获得列数
        /// </summary>
        /// <param name="commandText">Sql语句</param>
        /// <param name="parameters">参数列表</param>
        /// <returns>列数</returns>
        public int GetColumnCount(string commandText, IList<SqlParameter> parameters)
        {
            DataTable dt = this.GetDataTable(commandText, parameters);
            if (dt != null && dt.Columns.Count > 0)
                return dt.Columns.Count;
            else
                return 0;
        }
        /// <summary>
        /// 获得最大ID
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="columnName">列名</param>
        /// <returns>最大ID</returns>
        public long GetMaxID(string tableName, string columnName)
        {
            if (!string.IsNullOrEmpty(tableName) && !string.IsNullOrEmpty(columnName))
            {
                string commandText = "SELECT MAX(" + columnName + ") FROM " + tableName;
                return long.Parse(this.GetScalar(commandText).ToString());
            }
            else
                return 0;
        }
        /// <summary>
        /// 获得最大ID
        /// </summary>
        /// <param name="commandText">Sql语句</param>
        /// <param name="columnName">列名</param>
        /// <param name="parameters">参数列表</param>
        /// <returns>最大ID</returns>
        public long GetMaxID(string commandText, string columnName, IList<SqlParameter> parameters)
        {
            if (!string.IsNullOrEmpty(commandText) && !string.IsNullOrEmpty(columnName))
            {
                return long.Parse(this.GetScalar(commandText, parameters).ToString());
            }
            else
                return 0;
        }
        /// <summary>
        /// 获得新ID
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="columnName">列名</param>
        /// <returns>新ID</returns>
        public long GetNewID(string tableName, string columnName)
        {
            long maxID = this.GetMaxID(tableName, columnName);
            return maxID != 0 ? maxID + 1 : 1;
        }
        /// <summary>
        /// 获得新ID
        /// </summary>
        /// <param name="commandText">Sql语句</param>
        /// <param name="columnName">列名</param>
        /// <param name="parameters">参数列表</param>
        /// <returns>新ID</returns>
        public long GetNewID(string commandText, string columnName, IList<SqlParameter> parameters)
        {
            long maxID = this.GetMaxID(commandText, columnName, parameters);
            return maxID != 0 ? maxID + 1 : 1;
        }
        /// <summary>
        /// 获得连接状态
        /// </summary>
        /// <returns>连接状态</returns>
        public string GetConnnectionState()
        {
            return this.sqlConnection.State.ToString();
        }
        /// <summary>
        /// 批量增删改表
        /// </summary>
        /// <param name="dt">需要更新的表</param>
        /// <param name="sqlStr">查询SQL语句</param>
        /// <param name="m_PrimaryKeys">主键，以逗号分隔</param>
        /// <returns>返回结果 </returns>
        public bool BatchTable(DataTable dt, string sqlStr, string m_PrimaryKeys)
        {
            try
            {
                sqlCommand = new SqlCommand(sqlStr, this.sqlConnection);
                this.sqlDataAdapter = new SqlDataAdapter();
                if (!string.IsNullOrEmpty(m_PrimaryKeys))
                {
                    string[] m_PrimaryKey = m_PrimaryKeys.Split(',');
                    DataColumn[] keys = new DataColumn[m_PrimaryKey.Length];
                    for (int i = 0; i < m_PrimaryKey.Length; i++)
                        keys[i] = dt.Columns[m_PrimaryKey[i]];
                    dt.PrimaryKey = keys;
                }
                this.sqlDataAdapter.SelectCommand = sqlCommand;
                SqlCommandBuilder myCommandBuilder = new SqlCommandBuilder(this.sqlDataAdapter);
                this.sqlDataAdapter.InsertCommand = myCommandBuilder.GetInsertCommand();
                this.sqlDataAdapter.UpdateCommand = myCommandBuilder.GetUpdateCommand();  
                this.sqlDataAdapter.DeleteCommand = myCommandBuilder.GetDeleteCommand();
                sqlDataAdapter.Update(dt);
                return true;
            }
            catch (Exception ex)
            {
                WriteLog(ex, "BatchTable(DataTable dt, string sqlStr, string m_PrimaryKey) sqlStr=" + sqlStr + " m_PrimaryKeys=" + m_PrimaryKeys, "Export");
                return false;
            }
        }
        /// <summary>
        /// 把table批量插入数据到数据库
        /// </summary>
        /// <param name="table">数据表</param>
        /// <returns>是否成功</returns>
        public bool BatchInsertDataTable(DataTable table)
        {
            if (table is DataTable)
            {
                try
                {                    
                    if (table.Columns.Contains("InsertOrUpdate"))
                        table.Columns.Remove("InsertOrUpdate");
                    SqlBulkCopyColumnMapping[] mapping = new SqlBulkCopyColumnMapping[table.Columns.Count];
                    for (int i = 0; i < table.Columns.Count; i++)
                        mapping[i] = new SqlBulkCopyColumnMapping(table.Columns[i].ColumnName, table.Columns[i].ColumnName);
                    this.sqlBulkCopy.DestinationTableName = table.TableName;
                    this.sqlBulkCopy.BatchSize = table.Rows.Count;
                    this.sqlBulkCopy.ColumnMappings.Clear();
                    for (int i = 0; i < mapping.Length; i++)
                        this.sqlBulkCopy.ColumnMappings.Add(mapping[i]);
                    this.sqlBulkCopy.WriteToServer(table);
                    return true;
                }
                catch (Exception ex)
                {
                    WriteLog(ex, "BatchInsertDataTable(table)"+table.TableName, "Export");
                    return false;
                }
            }
            else
                return false;
        }
        /// <summary>
        /// 用DataTable批量插入和更新数据库，存在则更新，不存在则插入
        /// </summary>
        /// <param name="table">数据表</param>
        /// <param name="condition">查询条件，字段名之间以逗号分隔</param>
        /// <param name="updateFields">更新字段，以逗号分隔</param>
        /// <param name="incrementPrimaryKey">自增型主键，如果不是自增型，则为空</param>
        /// <returns></returns>
        public bool BatchInsertAndUpdateDataTable(DataTable table,string condition,string updateFields,string incrementPrimaryKey)
        {
            if (table is DataTable)
            {
                string tableName = "";
                try
                {
                    tableName = table.TableName + DateTime.Now.ToString("yyyyMMddHHmmss");
                    string sql = "IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[" + tableName + "]') AND type in (N'U'))";
                    sql += "Drop Table " +tableName + ";";
                    sql += "select * into " + tableName + " from " + table.TableName + " where 1=2;";
                    if (this.sqlCommand == null)
                        this.sqlCommand = new SqlCommand(sql, this.sqlConnection);
                    else
                        this.sqlCommand.CommandText = sql;
                    this.sqlCommand.CommandTimeout = timeOut;
                    this.sqlCommand.ExecuteNonQuery();

                    SqlBulkCopyColumnMapping[] mapping = new SqlBulkCopyColumnMapping[table.Columns.Count];
                    for (int i = 0; i < table.Columns.Count; i++)
                        mapping[i] = new SqlBulkCopyColumnMapping(table.Columns[i].ColumnName, table.Columns[i].ColumnName);
                    this.sqlBulkCopy.DestinationTableName = tableName;
                    this.sqlBulkCopy.BatchSize = table.Rows.Count;
                    this.sqlBulkCopy.ColumnMappings.Clear();
                    for (int i = 0; i < mapping.Length; i++)
                        this.sqlBulkCopy.ColumnMappings.Add(mapping[i]);
                    this.sqlBulkCopy.WriteToServer(table);
                    
                    sql = "Merge " + table.TableName + " as TableTarget ";
                    string[] arrCondition = condition.Split(',');
                    string strWhere = "1=1 And ";
                    for (int i = 0; i < arrCondition.Length; i++)
                        strWhere += "TableTarget." + arrCondition[i] + "=" + "TableDest." + arrCondition[i] + " And ";
                    strWhere = strWhere.Substring(0, strWhere.Length - 5);
                    sql += " using " + tableName + " as TableDest on (" + strWhere + ")";

                    if(!string.IsNullOrEmpty(updateFields.Trim()))
                    {
                        string[] arrUpdate = updateFields.Split(',');
                        string strUpdate = "";
                        for (int i = 0; i < arrUpdate.Length; i++)
                        {
                            if (arrUpdate[i].Trim() != "")
                                strUpdate += "TableTarget." + arrUpdate[i] + "=" + "TableDest." + arrUpdate[i] + ",";
                        }
                        strUpdate = strUpdate.Substring(0, strUpdate.Length - 1);
                        sql += " when matched then update set " + strUpdate;
                    }
                    string strInsertBefore="";
                    string strInsertAfter="";
                    for(int i=0;i<table.Columns.Count;i++)
                    {
                        if (incrementPrimaryKey.Trim().ToUpper() != table.Columns[i].ToString().Trim().ToUpper())
                        {
                            strInsertBefore += table.Columns[i] + ",";
                            strInsertAfter += "TableDest." + table.Columns[i] + ",";
                        }
                    }
                    strInsertBefore = strInsertBefore.Substring(0, strInsertBefore.Length - 1);
                    strInsertAfter = strInsertAfter.Substring(0, strInsertAfter.Length - 1);
                    sql += " when not matched then insert(" + strInsertBefore + ") values(" + strInsertAfter + ");";

                    //sql += "Drop table " + tableName + ";";
                    if (this.sqlCommand == null)
                        this.sqlCommand = new SqlCommand(sql, this.sqlConnection);
                    else
                        this.sqlCommand.CommandText = sql;
                    this.sqlCommand.CommandTimeout = timeOut;
                    this.sqlCommand.ExecuteNonQuery();
                    return true;
                }
                catch (Exception ex)
                {
                    WriteLog(ex, "public bool BatchInsertAndUpdateDataTable(DataTable table,string condition,string updateFields,string incrementPrimaryKey)" + table.TableName, "Export");
                    return false;
                }
                finally
                {
                    string sql = "IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[" + tableName + "]') AND type in (N'U'))";
                    sql += "Drop Table " + tableName + ";";
                    if (this.sqlCommand == null)
                        this.sqlCommand = new SqlCommand(sql, this.sqlConnection);
                    else
                        this.sqlCommand.CommandText = sql;
                    this.sqlCommand.CommandTimeout = timeOut;
                    this.sqlCommand.ExecuteNonQuery();
                    //this.sqlBulkCopy.Close();
                }
            }
            else
                return false;
        }
        /// <summary>
        /// 把dataRows批量插入数据到数据库
        /// </summary>
        /// <param name="dataRows">数据行</param>
        /// <param name="tableName">表名</param>
        /// <returns>是否成功</returns>
        public bool BatchInsertDataTable(DataRow[] dataRows, string tableName)
        {
            try
            {
                SqlBulkCopyColumnMapping[] mapping = new SqlBulkCopyColumnMapping[dataRows.Length];
                for (int i = 0; i < dataRows.Rank; i++)
                    mapping[i] = new SqlBulkCopyColumnMapping(dataRows[0].Table.Columns[i].ColumnName, dataRows[0].Table.Columns[i].ColumnName);
                this.sqlBulkCopy.DestinationTableName = tableName;
                this.sqlBulkCopy.BatchSize = dataRows.Length;
                for (int i = 0; i < mapping.Length; i++)
                    this.sqlBulkCopy.ColumnMappings.Add(mapping[i]);
                this.sqlBulkCopy.WriteToServer(dataRows);
                return true;
            }
            catch (Exception ex)
            {
                WriteLog(ex, "BatchInsertDataTable(dataRows," + tableName + ")", "Export");
                return false;
            }
            finally
            {
                this.sqlBulkCopy.Close();
            }
        }
        /// <summary>
        /// 获得服务器时间
        /// </summary>
        /// <returns>服务器时间</returns>
        public DateTime GetSysTime()
        {
            return Convert.ToDateTime(GetScalar("SELECT GetDate()"));
        }
        /// <summary>
        /// 获得该角色的所有邮件
        /// </summary>
        /// <param name="m_RoleName">RoleName</param>
        /// <param name="m_Prefix">Prefix</param>
        /// <returns>Email</returns>
        private string GetEmail(string m_RoleName,string m_Prefix)
        {
            using (DataAccess da = new DataAccess())
            {
                string strEmail = "";
                string[] arrEmployeeID = null;
                da.sqlParameters.Clear();
                da.sqlParameters.Add(da.CreateSqlParameter("@Type", "FISAccess"));
                da.sqlParameters.Add(da.CreateSqlParameter("@RoleName", m_RoleName));
                da.sqlParameters.Add(da.CreateSqlParameter("@Prefix", m_Prefix.Trim().ToUpper()));
                object dtEmployeeID = da.GetDataTable("Select Distinct CASE WHEN CHARINDEX('|',SUBSTRING(MappingValue2,LEN(@Prefix)+1,LEN(MappingValue2)-LEN(@Prefix)),0)=0 THEN SUBSTRING(MappingValue2,LEN(@Prefix)+1,LEN(MappingValue2)-LEN(@Prefix)) else SUBSTRING(SUBSTRING(MappingValue2,LEN(@Prefix)+1,LEN(MappingValue2)-LEN(@Prefix)),0,CHARINDEX('|',SUBSTRING(MappingValue2,LEN(@Prefix)+1,LEN(MappingValue2)-LEN(@Prefix)),0)) end As EmployeeID From Mapping Where Upper([Type])=@Type AND Upper(MappingValue1)=@RoleName And Upper(MappingValue2) LIKE @Prefix+'%' And IsDeleted=0", da.sqlParameters);
                if (dtEmployeeID != null)
                {
                    DataTable dt = dtEmployeeID as DataTable;
                    arrEmployeeID = new string[dt.Rows.Count];
                    for (int i = 0; i < dt.Rows.Count; i++)
                        arrEmployeeID[i] = dt.Rows[i]["EmployeeID"].ToString().Trim();
                    if (arrEmployeeID != null && arrEmployeeID.Length > 0)
                    {
                        for (int j = 0; j < arrEmployeeID.Length; j++)
                        {
                            da.sqlParameters.Clear();
                            da.sqlParameters.Add(da.CreateSqlParameter("@EmployeeID", arrEmployeeID[j].Trim().ToUpper()));
                            object email = da.GetScalar("Select Email From Employee Where Upper(EmployeeID)=@EmployeeID And IsPost=1 And IsDeleted=0", da.sqlParameters);
                            if (email != null)
                            {
                                if (!string.IsNullOrEmpty(email.ToString().Trim()))
                                    strEmail += email.ToString().Trim() + ";";
                            }
                        }
                    }
                }
                return strEmail.Substring(0,strEmail.Length-1);
            }
        }
        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="m_Log">Log</param>
        public static void WriteLog(Exception ex, string m_Description, string m_CreateUserID)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                da.sqlParameters.Add(da.CreateSqlParameter("@Source", ex.Source));
                da.sqlParameters.Add(da.CreateSqlParameter("@Message", ex.Message));
                da.sqlParameters.Add(da.CreateSqlParameter("@StackTrace", ex.StackTrace));
                da.sqlParameters.Add(da.CreateSqlParameter("@UserDescription", m_Description.Trim()));
                da.sqlParameters.Add(da.CreateSqlParameter("@CreateUserID", m_CreateUserID.Trim()));
                da.sqlParameters.Add(da.CreateSqlParameter("@ComputerName", Environment.MachineName));
                da.ExecuteCommand("Insert Into Log Values('DataAccess','Exception',@Source,@Message,@StackTrace,@UserDescription,GetDate(),@CreateUserID,@ComputerName,'')", da.sqlParameters);
            }
        }
    }
}