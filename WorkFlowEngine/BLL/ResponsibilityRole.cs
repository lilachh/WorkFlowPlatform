using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using WorkFlowEngine.DBUtility;
using System.Data.SqlClient;

namespace WorkFlowEngine.BLL
{
   public class ResponsibilityRole
   {
        #region Fields
       private int roleID;
        private int emplID;
       #endregion

        #region Properties
        public int RoleID
        {
            get
            {
                return roleID;
            }
            set
            {
                roleID = value;
            }
        }
        public int EmplID
        {
            get
            {
                return emplID;
            }
            set
            {
                emplID = value;
            }
        }
        #endregion

        # region Methods
        public static void AddMember(int roleID, int emplID)
        {
            string sqlQuery =
                    " insert into RoleMembers (RoleID, EmplID) " +
                    " values (" + roleID + ", " + emplID + ")";

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

        public static void DeleteMember(int roleID, int emplID)
        {
            string sqlQuery = "delete from  RoleMembers where roleid=" + roleID + " and emplid=" + emplID + "";

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
                Logger.Log.Error(" --- Delete Sql:  " + sqlQuery);
            }
        }

        public static DataSet GetMember(string sqlQuery)
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

        public static bool IfMemberExist(string sqlQuery)
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
       
        #endregion 

    }
}
