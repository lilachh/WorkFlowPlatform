using System;
using System.Data;
using WorkFlowEngine.DBUtility;

namespace WorkFlowEngine.BLL
{
    public class ApproveList
    {
        #region Constructors

        public ApproveList()
        {
            this.emplID = -1;
        }

        public ApproveList(int emplID)
        {
            this.emplID = emplID;
        }

        #endregion

        #region Fields

        private int emplID;

        #endregion

        #region Methods

        /// <summary>
        /// Get approval list of a specified system
        /// </summary>
        /// <param name="systemID">system id</param>
        /// <returns>approval list</returns>
        public DataSet GetApproveList (int systemID)
        {
            if (this.emplID == -1)
            {
                return null;
            }

            // SystemID, SystemName, DocumentID, SequenceID, ApproveNeededID, EMPLID, [NAME], SubmitDate
            string sqlQuery =
                " select distinct st.SystemID, st.SystemName, wfl.DocumentID, SequenceID, RoleDescription, ApproveNeededID, EMPLID, [NAME], convert(varchar(20), SubmitDate, 120) SubmitDate " +
                " from WorkFlowLog wfl join SystemType st on wfl.SystemID = st.SystemID join DocumentHeader dh on wfl.SystemID = dh.SystemID and wfl.DocumentID = dh.DocumentID " +
                " join EmployeeInfo ei on dh.OwnerID = ei.EMPLID " +
                " where wfl.SystemID = " + systemID + " and wfl.RoleType != 1 and wfl.Status = 'pending' and dh.Status = 'pending' and (ApproveNeededID = " + this.emplID + " or exists " +
                " (select * from Delegation where DelegateFrom = ApproveNeededID and DelegateTo = " + this.emplID + " and DateFrom <= getdate() and DateTo >= getdate())) " +
                " union " +
                " select distinct st.SystemID, st.SystemName, wfl.DocumentID, SequenceID, RoleDescription, ApproveNeededID, ei.EMPLID, [NAME], convert(varchar(20), SubmitDate, 120) SubmitDate " +
                " from WorkFlowLog wfl join SystemType st on wfl.SystemID = st.SystemID join DocumentHeader dh on wfl.SystemID = dh.SystemID and wfl.DocumentID = dh.DocumentID " +
                " join EmployeeInfo ei on dh.OwnerID = ei.EMPLID join RoleMembers rm on wfl.ApproveNeededID = rm.RoleID " +
                " join (select " + this.emplID + " EmplID union select DelegateFrom EmplID from Delegation where DelegateTo = " + this.emplID + " and DateFrom <= getdate() and DateTo >= getdate()) temp on rm.EmplID = temp.EmplID " +
                " where wfl.SystemID = " + systemID + " and wfl.RoleType = 1 and wfl.Status = 'pending' and dh.Status = 'pending' order by SystemID, DocumentID, SequenceID";

            try
            {
                return SQLHelper.Query(sqlQuery);
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message + "--- Sql:  " + sqlQuery);
                throw ex;
            }            
        }

        /// <summary>
        /// Get query list of all systems
        /// </summary>
        /// <returns>approval list</returns>
        public DataSet GetApproveList()
        {
            if (this.emplID == -1)
            {
                return null;
            }

            // SystemID, SystemName, DocumentID, SequenceID, ApproveNeededID, EMPLID, [NAME], SubmitDate
            string sqlQuery =
                " select distinct st.SystemID, st.SystemName, wfl.DocumentID, SequenceID, RoleDescription, ApproveNeededID, EMPLID, [NAME], convert(varchar(20), SubmitDate, 120) SubmitDate " +
                " from WorkFlowLog wfl join SystemType st on wfl.SystemID = st.SystemID join DocumentHeader dh on wfl.SystemID = dh.SystemID and wfl.DocumentID = dh.DocumentID " +
                " join EmployeeInfo ei on dh.OwnerID = ei.EMPLID " +
                " where wfl.RoleType != 1 and wfl.Status = 'pending' and dh.Status = 'pending' and (ApproveNeededID = " + this.emplID + " or exists " +
                " (select * from Delegation where DelegateFrom = ApproveNeededID and DelegateTo = " + this.emplID + " and DateFrom <= getdate() and DateTo >= getdate())) " +
                " union " +
                " select distinct st.SystemID, st.SystemName, wfl.DocumentID, SequenceID, RoleDescription, ApproveNeededID, ei.EMPLID, [NAME], convert(varchar(20), SubmitDate, 120) SubmitDate " +
                " from WorkFlowLog wfl join SystemType st on wfl.SystemID = st.SystemID join DocumentHeader dh on wfl.SystemID = dh.SystemID and wfl.DocumentID = dh.DocumentID " +
                " join EmployeeInfo ei on dh.OwnerID = ei.EMPLID join RoleMembers rm on wfl.ApproveNeededID = rm.RoleID " +
                " join (select " + this.emplID + " EmplID union select DelegateFrom EmplID from Delegation where DelegateTo = " + this.emplID + " and DateFrom <= getdate() and DateTo >= getdate()) temp on rm.EmplID = temp.EmplID " +
                " where wfl.RoleType = 1 and wfl.Status = 'pending' and dh.Status = 'pending' ";

            try
            {                
                return SQLHelper.Query(sqlQuery);
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message + "--- Sql:  " + sqlQuery);
                
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
