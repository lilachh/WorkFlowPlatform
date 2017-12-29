using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using WorkFlowEngine.DBUtility;

namespace WorkFlowEngine.BLL
{
    public class SystemType
    {
        #region Fields
        private int systemID;
        private string systemName;
        #endregion

        #region Properties

        public int SystemID
        {
            get
            {
                return systemID;
            }
            set
            {
                systemID = value;
            }
        }

        public string SystemName
        {
            get
            {
                return systemName;
            }
            set
            {
                systemName = value;
            }
        }
        #endregion

        #region Methods

        /// <summary>
        /// Add New System
        /// </summary>
        /// <returns></returns>
        public static void AddSystem(string systemName, string bUrl, string sUrl, string applyUrl, string queryUrl, string navigateUrl)
        {
            int maxSystemID = WorkFlowEngine.DBUtility.SQLHelper.GetMaxID("SystemID", "SystemType");
            string sqlQuery =
                   " insert into SystemType (SystemID, SystemName, QueryBUrl, QuerySUrl, ApplyUrl, QueryUrl, NavigateUrl, status) " +
                   " values (" + maxSystemID + ", '" + systemName + "', '" + bUrl + "', '" + sUrl + "', '" + applyUrl + "', '" + queryUrl + "', '" + navigateUrl + "',  '0')";

            try
            {
                WorkFlowEngine.DBUtility.SQLHelper.ExecuteSql(sqlQuery);
            }
            catch (SqlException ex)
            {
                Logger.Log.Error(ex.Message);
                throw ex;
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message);
                throw ex;
            }
            finally
            {
                Logger.Log.Error(" --- Insert Sql:  " + sqlQuery);
            }
        }

        /// <summary>
        /// Add New System
        /// </summary>
        /// <returns></returns>
        public static void DeleteSystem(int systemID)
        {
            ArrayList sqlList = new ArrayList();

            string sqlQuery = "delete from SystemType where systemID=" + systemID + "";
            sqlList.Add(sqlQuery);

            sqlQuery = "delete from MainWorkFlow where systemID=" + systemID+ "";
            sqlList.Add(sqlQuery);

            sqlQuery = "delete from SubWorkFlow where systemID=" + systemID + "";
            sqlList.Add(sqlQuery);

            sqlQuery = "delete from SpecificUrl where systemID=" + systemID + "";
            sqlList.Add(sqlQuery);
            
            try
            {
                WorkFlowEngine.DBUtility.SQLHelper.ExecuteSqlByTransaction(sqlList);
            }
            catch (SqlException ex)
            {
                Logger.Log.Error(ex.Message);
                throw ex;
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message);
                throw ex;
            }
            finally
            {
                string sqlStatements = "";
                for (int i = 0; i < sqlList.Count; i++)
                {
                    sqlStatements += sqlList[i].ToString() + "\n";
                }

                Logger.Log.Error(" --- Delete Sqls:  " + sqlStatements);
            }
        }

        public static DataSet GetSystem(string sqlQuery)
        {            
            try
            {
                return WorkFlowEngine.DBUtility.SQLHelper.Query(sqlQuery);
            }
            catch (SqlException ex)
            {
                Logger.Log.Error(ex.Message + " --- Sql:  " + sqlQuery);
                throw ex;
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message + " --- Sql:  " + sqlQuery);
                throw ex;
            }
        }

        public static bool IfSystemExist(string sqlQuery)
        {
            try
            {
                return WorkFlowEngine.DBUtility.SQLHelper.Exists(sqlQuery);
            }
            catch (SqlException ex)
            {
                Logger.Log.Error(ex.Message + " --- Sql:  " + sqlQuery);
                throw ex;
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message + " --- Sql:  " + sqlQuery);
                throw ex;
            }
        }

        public static void UpdateSystem(string sqlCommand)
        {
            try
            {
                WorkFlowEngine.DBUtility.SQLHelper.ExecuteSql(sqlCommand);
            }
            catch (SqlException ex)
            {
                Logger.Log.Error(ex.Message);
                throw ex;
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message);
                throw ex;
            }
            finally
            {
                Logger.Log.Error(" --- Update Sql:  " + sqlCommand);
            }
        }

        public static object GetName(string sqlQuery)
        {            
            try
            {
                return WorkFlowEngine.DBUtility.SQLHelper.GetSingle(sqlQuery);
            }
            catch (SqlException ex)
            {
                Logger.Log.Error(ex.Message + " --- Sql:  " + sqlQuery);
                throw ex;
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message + " --- Sql:  " + sqlQuery);
                throw ex;
            }
        }

        public static object GetSUrl(string sqlQuery)
        {            
            try
            {
                return WorkFlowEngine.DBUtility.SQLHelper.GetSingle(sqlQuery);
            }
            catch (SqlException ex)
            {
                Logger.Log.Error(ex.Message + " --- Sql:  " + sqlQuery);
                throw ex;
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message + " --- Sql:  " + sqlQuery);
                throw ex;
            }
        }

        public static bool IfNeedBatch(int systemID)
        {
            string sqlQuery = "select QuerySUrl from  SystemType where systemID=" + systemID + "";
            
            try
            {
                string querySUrl = (string)WorkFlowEngine.DBUtility.SQLHelper.GetSingle(sqlQuery);

                return !(querySUrl == null || querySUrl == string.Empty || querySUrl.Length < 5);
            }
            catch (SqlException ex)
            {
                Logger.Log.Error(ex.Message + " --- Sql:  " + sqlQuery);
                throw ex;
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message + " --- Sql:  " + sqlQuery);
                throw ex;
            }

        }

        public static string GetQueryUrl(int sysID)
        {
            string sqlQuery = "select QueryUrl from SystemType where SystemID = " + sysID;

            try
            {
                object url = SQLHelper.GetSingle(sqlQuery);
                return (url == null ? null : url.ToString());
            }
            catch (SqlException ex)
            {
                Logger.Log.Error(ex.Message + " --- Sql:  " + sqlQuery);
                throw ex;
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message + " --- Sql:  " + sqlQuery);
                throw ex;
            }
        }


        public static string GetNavigateUrl(int sysID)
        {
            string sqlQuery = "select NavigateUrl from SystemType where SystemID = " + sysID;

            try
            {
                object url = SQLHelper.GetSingle(sqlQuery);
                return (url == null ? null : url.ToString());
            }
            catch (SqlException ex)
            {
                Logger.Log.Error(ex.Message + " --- Sql:  " + sqlQuery);
                throw ex;
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message + " --- Sql:  " + sqlQuery);
                throw ex;
            }
        }

        public static bool IfHaveNavigate(int systemID)
        {
            string sqlQuery = "select navigateurl  from  SystemType where systemID=" + systemID + "";

            try
            {
                string querySUrl = (string)WorkFlowEngine.DBUtility.SQLHelper.GetSingle(sqlQuery);

                return !(querySUrl == null || querySUrl == string.Empty || querySUrl.Length < 5);
            }
            catch (SqlException ex)
            {
                Logger.Log.Error(ex.Message + " --- Sql:  " + sqlQuery);
                throw ex;
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message + " --- Sql:  " + sqlQuery);
                throw ex;
            }

        }

        #endregion
    }
}
