using System;
using System.Text;
using System.Data;
using WorkFlowEngine.DBUtility;
using System.Data.SqlClient;

namespace WorkFlowEngine.BLL
{
    public class QueryList
    {
        #region Constructors

        public QueryList()
        {
            this.emplID = -1;
        }

        public QueryList(int emplID)
        {
            this.emplID = emplID;
        }

        #endregion

        #region Fields

        private int emplID;

        #endregion

        #region Methods

        /// <summary>
        /// Get query list of a specified system
        /// </summary>
        /// <param name="systemID">system id</param>
        /// <returns>query list</returns>
        public DataSet GetQueryList(int systemID)
        {
            if (this.emplID == -1)
            {
                return null;
            }

            string sqlQuery =
                " select ST.SystemID, ST.SystemName, DocumentID, (select [NAME] from EmployeeInfo where EMPLID = DH.RequestorID) Requestor,RequestorID,OwnerID, " +
                " (select [NAME] from EmployeeInfo where EMPLID = DH.OwnerID) Owner, convert(varchar(20), SubmitDate, 120) SubmitDate, DH.Status " +
                " from DocumentHeader DH join SystemType ST on DH.SystemID = ST.SystemID " +
                " where (RequestorID = " + this.emplID + " or OwnerID = " + this.emplID + ") and ST.SystemID = " + systemID + " order by DocumentID ";

            try
            {
                return SQLHelper.Query(sqlQuery);
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

        /// <summary>
        /// Get query list in all systems
        /// </summary>
        /// <returns>query list</returns>
        public DataSet GetQueryList()
        {
            if (this.emplID == -1)
            {
                return null;
            }

            string sqlQuery =
                 " select ST.SystemID, ST.SystemName, DocumentID, (select [NAME] from EmployeeInfo where EMPLID = DH.RequestorID) Requestor, RequestorID,OwnerID, " +
                " (select [NAME] from EmployeeInfo where EMPLID = DH.OwnerID) Owner, convert(varchar(20), SubmitDate, 120) SubmitDate, DH.Status " +
                " from DocumentHeader DH join SystemType ST on DH.SystemID = ST.SystemID " +
                " where RequestorID = " + this.emplID + " or OwnerID = " + this.emplID + " order by DocumentID ";

            try
            {
                return SQLHelper.Query(sqlQuery);
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


        /// <summary>
        /// Get approved by self list in all systems
        /// </summary>
        /// <returns>query list</returns>
        public DataSet GetApprovedList()
        {
            if (this.emplID == -1)
            {
                return null;
            }

            string sqlQuery =
                 " select ST.SystemID, ST.SystemName, DocumentID, (select [NAME] from EmployeeInfo where EMPLID = DH.RequestorID) Requestor, RequestorID," +
                " (select [NAME] from EmployeeInfo where EMPLID = DH.OwnerID) Owner,OwnerID, convert(varchar(20), SubmitDate, 120) SubmitDate, DH.Status " +
                " from DocumentHeader DH join SystemType ST on DH.SystemID = ST.SystemID " +
                " where DH.DocumentID in (select distinct DocumentID from workflowlog where ApproveByID=" + this.EmplID + " and systemID=DH.SystemID )";

            try
            {
                return SQLHelper.Query(sqlQuery);
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

        #region Properties

        public int EmplID
        {
            get
            {
                return this.emplID;
            }

            set
            {
                this.emplID = value;
            }
        }

        #endregion
    }
}
