using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using WorkFlowEngine.DBUtility;

namespace WorkFlowEngine.BLL
{
    public class ApproverRole
    {

        #region Constructors

        public ApproverRole()
        {
            SetDefaultValues();
        }

        public ApproverRole(int roleID)
        {
            //set other fields values

            string sqlQuery = " select RoleType, RoleID, RoleDescription, Grade from ApproverRole where RoleID = " + roleID;
            try
            {
                DataSet ds = SQLHelper.Query(sqlQuery);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {

                    this.roleID = roleID;
                    this.roleType = ds.Tables[0].Rows[0][0].ToString();
                    this.roleDescription = ds.Tables[0].Rows[0][2].ToString();

                    if (this.roleType == "0")
                    {
                        // report line
                        this.grade = (int)ds.Tables[0].Rows[0][3];
                    }
                }
            }
            catch (SqlException ex)
            {
                Logger.Log.Error(ex.Message + " --- Sql:  " + sqlQuery);
                SetDefaultValues();
                throw ex;
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message);
                throw ex;
            }
        }

        #endregion

        #region Fields

        private string roleType;
        private int roleID;
        private string roleDescription;
        private int grade;

        #endregion

        #region Properties

        public string RoleType
        {
            get 
            {
                return roleType;
            }
            set
            {
                roleType = value;
            }
        }
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
        public string RoleDescription
        {
            get
            {
                return roleDescription;
            }
            set
            {
                roleDescription = value;
            }
        }
        public int Grade
        {
            get
            {
                return grade;
            }
            set
            {
                grade = value;
            }
        }

        #endregion

        #region Methods

        private void SetDefaultValues()
        {
            try
            {
                roleType = string.Empty;
                roleID = -1;
                roleDescription = string.Empty;
                grade = -1;
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message);

                throw ex;
            }
        }

        /// <summary>
        /// Get all existing roles in ApproverRole
        /// </summary>
        /// <returns></returns>
        public static DataSet GetAllRole()
        {
            string sqlQuery = "select RoleID, RoleDescription from ApproverRole";
            try
            {                
                DataSet ds = DBUtility.SQLHelper.Query(sqlQuery);
                return ds;
            }
            catch (SqlException ex)
            {
                Logger.Log.Error(ex.Message + " --- Sql:  " + sqlQuery);
                throw ex;
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message);
                throw ex;
            }            
        }

        /// <summary>
        /// Get role of specified type. -1 indicate all roles
        /// </summary>
        /// <param name="roleType">role id</param>
        /// <returns>role dataset</returns>
        public static DataSet GetRoles(string sqlQuery)
        {
            try
            {
                return SQLHelper.Query(sqlQuery);
            }
            catch (SqlException ex)
            {
                Logger.Log.Error(ex.Message);
                throw ex;
            }            
        }

        public static bool RoleDescriptionExists(string roleDescription)
        {
            string sqlQuery = "select count(*) from ApproverRole where RoleDescription = '" + roleDescription + "'";

            try
            {
                return SQLHelper.Exists(sqlQuery);
            }
            catch (SqlException ex)
            {
                Logger.Log.Error(ex.Message + " --- Sql:  " + sqlQuery);
                throw ex;
            }
        }

        public static int GetNextMaxRoleID()
        {
            try
            {
                return SQLHelper.GetMaxID("RoleID", "ApproverRole");
            }
            catch (SqlException ex)
            {
                Logger.Log.Error(ex.Message);
                throw ex;
            }
        }

        public static bool AddReportLineRole(string roleType, int roleID, string roleDescription, int grade)
        {
            string sqlCommand =
                " insert into ApproverRole (RoleType, RoleID, RoleDescription, Grade) " +
                " values ('" + roleType + "', " + roleID + ", '" + roleDescription + "', " + grade + ")";

            try
            {
                return SQLHelper.ExecuteSql(sqlCommand);
            }
            catch (SqlException ex)
            {
                Logger.Log.Error(ex.Message + " --- Insert Sql:  " + sqlCommand);
                throw ex;
            }
        }

        public static bool AddResponsibility(string roleType, int roleID, string roleDescription)
        {
            string sqlCommand =
               " insert into ApproverRole (RoleType, RoleID, RoleDescription) " +
               " values ('" + roleType + "', " + roleID + ", '" + roleDescription + "')";

            try
            {
                return SQLHelper.ExecuteSql(sqlCommand);
            }
            catch (SqlException ex)
            {
                Logger.Log.Error(ex.Message + " --- Insert Sql:  " + sqlCommand);
                throw ex;
            }
        }

        public static bool UpdateRoleDescription(int roleID, string newRoleDescription)
        {
            string sqlCommand = " update ApproverRole set RoleDescription = '" + newRoleDescription + "' where RoleID = " + roleID;

            try
            {
                return SQLHelper.ExecuteSql(sqlCommand);
            }
            catch (SqlException ex)
            {
                Logger.Log.Error(ex.Message + " --- Update Sql:  " + sqlCommand);
                throw ex;
            }
        }

        #endregion
    }
}
