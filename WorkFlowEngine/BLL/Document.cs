using System;
using System.Data;
using System.Collections;
using WorkFlowEngine.DBUtility;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace WorkFlowEngine.BLL
{
    public class Document
    {
        #region Constructors

        public Document()
        {
            systemID = -1;
            documentID = -1;
        }

        public Document(int sysID, int docID)
        {
            this.systemID = sysID;
            this.documentID = docID;
        }

        #endregion

        #region Fields

        private int systemID;
        private int documentID;

        #endregion

        #region Methods

        public bool DoApproval(int sequenceID, int approveNeededID, int approverID, string remark)
        {
            ArrayList sqlArray = new ArrayList();

            try
            {
                if (this.systemID == -1 || this.documentID == -1 || sequenceID == -1 || approveNeededID == -1)
                {
                    return false;
                }

                DocumentLog docLog = new DocumentLog(this.systemID, this.documentID, sequenceID);
                // mark this role as approved
                sqlArray.Add(docLog.MarkAsApproved(approveNeededID, approverID, remark));

                if (docLog.RoleID.Length == 1 || docLog.SequenceNeedAll == "0" ||
                    !HasUnapprovedRole(this.systemID, this.documentID, sequenceID, approveNeededID))
                {
                    // only one role in this sequence or sequence doesn't need all or no more unapproved role
                    if (docLog.IsLastSequence == "0")
                    {
                        // no
                        bool temp = false;

                        int nextSequenceID = docLog.GetNextSequenceID();
                        if (WorkFlow.IsSeqauenceGenerated(this.systemID, this.documentID, nextSequenceID))
                        {
                            // exists already, update next sequence as pending and send mail
                            docLog = new DocumentLog(this.systemID, this.documentID, nextSequenceID);
                            sqlArray.Add(docLog.MarkAsPending());

                            temp = SQLHelper.ExecuteSqlByTransaction(sqlArray);
                        }
                        else
                        {
                            int requestorID = GetRequestorID(this.systemID, this.documentID);
                            temp = WorkFlow.GenerateWorkFlowLog(this.systemID, this.documentID, requestorID, sequenceID, -1, true);

                            string sqlQuery = "select count(*) from WorkFlowLog where SystemID = " + this.systemID + " and DocumentID = " + this.documentID + " and SequenceID > " + sequenceID;

                            sqlQuery =
                                    "select min(SequenceID) from WorkFlowLog where SystemID = " + this.systemID +
                                    " and DocumentID = " + this.documentID + " and SequenceID > " + sequenceID;

                            object objNextSequenceID = SQLHelper.GetSingle(sqlQuery);
                            //if (!SQLHelper.Exists(sqlQuery))
                            if (objNextSequenceID == null)
                            {
                                sqlArray.Add(UpdateDocHeaderAsApproved());
                                if (SQLHelper.ExecuteSqlByTransaction(sqlArray))
                                {
                                    WorkFlow.MailOnFinalApproval(this.systemID, this.documentID);
                                    return true;
                                }
                                else
                                {
                                    return false;
                                }
                            }
                            else
                            {
                                nextSequenceID = (int)objNextSequenceID;
                                docLog = new DocumentLog(this.systemID, this.documentID, nextSequenceID);
                                sqlArray.Add(docLog.MarkAsPending());

                                temp = SQLHelper.ExecuteSqlByTransaction(sqlArray);
                            }
                        }

                        if (temp)
                        {
                            //send mail to requestor 
                            WorkFlow.MailOnApproval(this.systemID, this.documentID);

                            // send to approver
                            WorkFlow.MailForApproval(this.systemID, this.documentID);

                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        // yes, update documentheader as approved
                        sqlArray.Add(UpdateDocHeaderAsApproved());

                        if (SQLHelper.ExecuteSqlByTransaction(sqlArray))
                        {
                            WorkFlow.MailOnFinalApproval(this.systemID, this.documentID);
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    // more than one roles in this sequence and sequence needs all
                    // no more role unapproved, simply return true
                    return SQLHelper.ExecuteSqlByTransaction(sqlArray);
                }
            }
            catch (SqlException ex)
            {
                Logger.Log.Error(ex.Message);
                throw ex;
            }
            finally
            {
                string sqlStatements = "";
                for (int i = 0; i < sqlArray.Count; i++)
                {
                    sqlStatements += sqlArray[i].ToString() + "\n";
                }

                Logger.Log.Error(sqlStatements);
            }
        }

        public bool DoApproval(int sequenceID, int approveNeededID, int conditionID,int approverID, string remark)
        {
            ArrayList sqlArray = new ArrayList();

            try
            {
                if (this.systemID == -1 || this.documentID == -1 || sequenceID == -1 || approveNeededID == -1 || conditionID == -1)
                {
                    return false;
                }                

                DocumentLog docLog = new DocumentLog(this.systemID, this.documentID, sequenceID);

                // mark this role as approved
                sqlArray.Add(docLog.MarkAsApproved(approveNeededID, approverID, remark));

                //Sequence sequence = new Sequence(this.systemID, sequenceID);

                if (docLog.RoleID.Length == 1 || docLog.SequenceNeedAll == "0" ||
                    !HasUnapprovedRole(this.systemID, this.documentID, sequenceID, approveNeededID))
                {
                    // only one role in this sequence or sequence doesn't need all or no more unapproved role
                    // that is , this sequence is completed

                    if (docLog.IsLastSequence == "0")
                    {
                        // no
                        bool temp = false;

                        int nextSequenceID = docLog.GetNextSequenceID(conditionID);
                        if (WorkFlow.IsSeqauenceGenerated(this.systemID, this.documentID, nextSequenceID))
                        {
                            // exists already, update next sequence as pending
                            docLog = new DocumentLog(this.systemID, this.documentID, nextSequenceID);
                            sqlArray.Add(docLog.MarkAsPending());
                            temp = SQLHelper.ExecuteSqlByTransaction(sqlArray);
                        }
                        else
                        {
                            // not exists, need to generate
                            int requestorID = GetRequestorID(this.systemID, this.documentID);
                            temp = WorkFlow.GenerateWorkFlowLog(this.systemID, this.documentID, requestorID, sequenceID, conditionID, true);

                            string sqlQuery = "select count(*) from WorkFlowLog where SystemID = " + this.systemID + " and DocumentID = " + this.documentID + " and SequenceID > " + sequenceID;

                            sqlQuery =
                                    "select min(SequenceID) from WorkFlowLog where SystemID = " + this.systemID +
                                    " and DocumentID = " + this.documentID + " and SequenceID > " + sequenceID;

                            object objNextSequenceID = SQLHelper.GetSingle(sqlQuery);
                            //if (!SQLHelper.Exists(sqlQuery))
                            if (objNextSequenceID == null)
                            {
                                sqlArray.Add(UpdateDocHeaderAsApproved());
                                if (SQLHelper.ExecuteSqlByTransaction(sqlArray))
                                {
                                    WorkFlow.MailOnFinalApproval(this.systemID, this.documentID);
                                    return true;
                                }
                                else
                                {
                                    return false;
                                }
                            }
                            else
                            {
                                nextSequenceID = (int)objNextSequenceID;
                                docLog = new DocumentLog(this.systemID, this.documentID, nextSequenceID);
                                sqlArray.Add(docLog.MarkAsPending());
                                temp = SQLHelper.ExecuteSqlByTransaction(sqlArray);
                            }
                        }

                        if (temp)
                        {
                            //send mail to requestor 
                            WorkFlow.MailOnApproval(this.systemID, this.documentID);

                            // send to approver
                            WorkFlow.MailForApproval(this.systemID, this.documentID);

                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        // yes, update documentheader as approved
                        sqlArray.Add(UpdateDocHeaderAsApproved());

                        if (SQLHelper.ExecuteSqlByTransaction(sqlArray))
                        {
                            WorkFlow.MailOnFinalApproval(this.systemID, this.documentID);
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    // more than one roles in this sequence and sequence needs all
                    // no more role unapproved, simply return true
                    return SQLHelper.ExecuteSqlByTransaction(sqlArray);
                }
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
                for (int i = 0; i < sqlArray.Count; i++)
                {
                    sqlStatements += sqlArray[i].ToString() + "\n";
                }

                Logger.Log.Error(sqlStatements);
            }
        }

        public bool DoReject(int sequenceID, int approveNeededID, int emplID,string remark)
        {
            ArrayList sqlArray = new ArrayList();

            try
            {
                if (this.systemID == -1 || this.documentID == -1 || sequenceID == -1 || approveNeededID == -1)
                {
                    return false;
                }

                DocumentLog docLog = new DocumentLog(this.systemID, this.documentID, sequenceID);

                sqlArray.Add(docLog.MarkAsReject(approveNeededID, emplID, remark));
                sqlArray.Add(UpdateDocHeaderAsRejected());

                //send mail to requestor
                if (SQLHelper.ExecuteSqlByTransaction(sqlArray))
                {
                    WorkFlow.MailOnRejection(this.systemID, this.documentID);
                    return true;
                }
                else
                {
                    return false;
                }
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
                for (int i = 0; i < sqlArray.Count; i++)
                {
                    sqlStatements += sqlArray[i].ToString() + "\n";
                }

                Logger.Log.Info(sqlStatements);
            }            
        }

        public void UpdateDocHeaderAsCanceled()
        {
            string sqlExecute= "update DocumentHeader set Status = 'canceled' where SystemID = " + this.systemID + " and DocumentID = " + this.documentID;
            try
            {
                DBUtility.SQLHelper.ExecuteSql(sqlExecute);
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message);
                throw ex;
            }
            finally
            {
                Logger.Log.Info(sqlExecute);
            }
            return;
        }

        private string UpdateDocHeaderAsApproved()
        {
            return "update DocumentHeader set Status = 'approved' where SystemID = " + this.systemID + " and DocumentID = " + this.documentID;
        }

        private string UpdateDocHeaderAsRejected()
        {
            return "update DocumentHeader set Status = 'rejected' where SystemID = " + this.systemID + " and DocumentID = " + this.documentID;
        }

        private int GetRequestorID(int sysID, int docID)
        {
            string sqlQuery = "select OwnerID from DocumentHeader where SystemID = " + sysID + " and DocumentID = " + docID;
            try
            {                
                object obj = SQLHelper.GetSingle(sqlQuery);

                return ((obj == null) ? -1 : (int)obj);
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

        private bool HasUnapprovedRole(int sysID, int docID, int seqID, int approveNeededID)
        {
            string sqlQuery =
                "select count(*) from WorkFlowLog where SystemID = " + sysID +
                " and DocumentID = " + docID + " and SequenceID = " + seqID + " and Status = 'pending' and ApproveNeededID != " + approveNeededID;

            try
            {                
                return SQLHelper.Exists(sqlQuery);
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
        /// Verify if emplID can query/withdraw this document
        /// </summary>
        /// <param name="systemID">system id</param>
        /// <param name="docID">document id</param>
        /// <param name="docID">employee id</param>
        /// <returns>bool</returns>
        public bool VerifyDocumentOwner(int emplID)
        {
            string sqlQuery = "SELECT * from DocumentHeader where RequestorID=" + emplID + " or OwnerID=" + emplID + "";
            try
            {
                return SQLHelper.Exists(sqlQuery);
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

        public bool VerifyDocumentApprover(int sequenceID, int approveNeededID, int emplID)
        {
            string sqlQuery =
                " select * from WorkFlowLog where SequenceID=" + sequenceID +
                " and SystemID=" + this.SystemID + " and DocumentID=" + this.DocumentID + " and ApproveNeededID=" + approveNeededID + "";

            try
            {
                DataSet ds = SQLHelper.Query(sqlQuery);
                if (ds.Tables[0].Rows.Count == 0)
                {
                    return false;
                }
                int RoleID = Convert.ToInt32(ds.Tables[0].Rows[0]["RoleType"].ToString());
                if (RoleID == 0 || RoleID == 1) //Report Line
                {
                    return (emplID == approveNeededID);
                }
                else
                {
                    sqlQuery = "select * from RoleMembers where EmplID=" + emplID + " and RoleID=" + RoleID + "";

                    return SQLHelper.Exists(sqlQuery);
                }
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

        public string GetDocHeaderStatus()
        {
            string sqlQuery = "select Status from DocumentHeader where SystemID = " + this.systemID + " and DocumentID = " + this.documentID;

            try
            {
                return SQLHelper.GetSingle(sqlQuery).ToString();
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

        public void InsertDocumentHeader(ArrayList sqlArray, int requestorID, int owner)
        {
            string sqlCommand =
                "insert into DocumentHeader values(" + this.SystemID + "," + this.DocumentID + "," + requestorID + "," + owner + ",'pending','" + DateTime.Now + "',null)";
            
            sqlArray.Add(sqlCommand);
        }

        public void InsertRequestorChoice(ArrayList sqlArray, List<RequestorChoice> choiceList)
        {
            if (choiceList == null || choiceList.Count == 0)
            {
                return;
            }

            string sqlCommand = string.Empty;
            foreach (RequestorChoice rChoice in choiceList)
            {
               sqlCommand =
               "insert into RequestorChoice values(" +rChoice.SystemID + "," + rChoice.DocumentID+ "," + rChoice.MainSequenceID + "," + rChoice.ConditionID + ")";
               sqlArray.Add(sqlCommand);
            }
           
        }

        public  bool IfWithDrawVisible(int emplID)
        {
            string sqlQuery = "select count(*) from documentHeader where requestorID=" + emplID + " and systemID="+this.SystemID+" and documentID="+this.DocumentID+"  ";

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

        #endregion

        #region Properties

        public int SystemID
        {
            get
            {
                return this.systemID;
            }

            set
            {
                this.systemID = value;
            }
        }

        public int DocumentID
        {
            get
            {
                return this.documentID;
            }

            set
            {
                this.documentID = value;
            }
        }

        #endregion
    }
}
