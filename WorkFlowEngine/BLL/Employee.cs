using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using WorkFlowEngine.DBUtility;

namespace WorkFlowEngine.BLL
{
    public class Employee
    {
        #region Fields

        private int emplID;
        #endregion

        #region Properties

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

        #region Constructors

        public Employee(int emplid)
        { 
            //set other fields values
            this.EmplID=emplid;
        }

        public Employee()
        {

        }

        #endregion

        #region Methods

        public bool NTExist(string NT)
        {
            string sqlQuery = "select count(*) from dbo.EmployeeInfo where NTAccount='" + NT + "'";

            try
            {
                return DBUtility.SQLHelper.Exists(sqlQuery);
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

        public object GetEmplID(string NT)
        {
            string sqlQuery = "select emplid from dbo.EmployeeInfo where NTAccount='" + NT + "'";

            try
            {
                return DBUtility.SQLHelper.Exists(sqlQuery);
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


        public object GetName()
        {
            string sqlQuery = "select name from dbo.EmployeeInfo where emplid='"+this.emplID.ToString()+"'";

            try
            {
                return DBUtility.SQLHelper.GetSingle(sqlQuery);
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

        public static bool EmplIDExist(string emplid)
        {
            string sqlQuery = "select count(*) from dbo.EmployeeInfo where emplID='" + emplid + "'";

            try
            {
                return DBUtility.SQLHelper.Exists(sqlQuery);
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

        public bool VerifyPassword(string password)
        {
            string discryptPassword = EncodePassword(password);
            string sqlQuery = "select count(*) from dbo.EOffice_Password where badge='" + this.EmplID + "' and PIN='" + discryptPassword + "'";

            try
            {
                return DBUtility.SQLHelper.Exists(sqlQuery);
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

        private  string EncodePassword(string password)
        {
            char[] orginalChars = password.Trim().ToCharArray();
            string temp = string.Empty;

            for (int i = 0; i < orginalChars.Length; i++)
            {
                temp = temp + Convert.ToChar(Convert.ToInt32(orginalChars[i]) + 3);
            }

            return temp;
        }

        #endregion
    }
}
