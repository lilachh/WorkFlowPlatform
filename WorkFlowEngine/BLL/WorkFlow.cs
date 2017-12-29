/* <p>Description: Work flow configuration and generation</p>
 * <p>@version: 1.0.1</p>
 * <p>Modifition history (Date / Version / Author / Description)</p>
 * <p>--------------------------HISTORY----------------------------------------</p>
 * <p>2009-03-10 / 1.0.1 / Zhang Yunsong / Setting up class</p>
 * <p>2009-03-12 / 1.0.1 / Zhang Yunsong / implement functions of AddSequence(), DeleteSequence(), AddNextSequence(), AddSubSequence()and GenerateNextSequenceID</p>
 * <p>...</p>
 */

using System;
using System.Text;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using WorkFlowEngine.DBUtility;
using System.Data.SqlClient;

namespace WorkFlowEngine.BLL
{
    public class WorkFlow
    {

        #region Methods

        /// <summary>
        /// Check to see if the initial sequence of a system exists or not
        /// </summary>
        /// <param name="systemID">system id</param>
        /// <returns>return true if exists and false if not</returns>
        public static bool InitialSequenceExists(int systemID)
        {
            string sqlQuery = "select count(*) from MainWorkFlow where SequenceID = -1 and SystemID = " + systemID;
            
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
        /// Add new seqeunce to MainWorkFlow
        /// </summary>
        /// <param name="sequence">new sequence instance</param>
        /// <returns>return true if succeed, and false if not</returns>
        public static bool AddSequence(Sequence sequence)
        {
            if (sequence.SystemID == -1 || sequence.RoleID.Length == 0)
            {
                return false;
            }

            ArrayList sqlArray = new ArrayList(); ;

            for (int i = 0; i < sequence.RoleID.Length; i++)
            {
                // stringbuilderinstead
                string sqlCommand =
                " insert into MainWorkFlow (SystemID, SequenceID, SequenceDescription, RoleID, SequenceNeedAll, NextSequenceID, IsLastSequence, HasSubSequence, ValueFrom, IsActor)" +
                " values (" + sequence.SystemID + ", " + sequence.SequenceID + ", '" + sequence.SequenceDescription + "', " + sequence.RoleID[i] + ", '" +
                sequence.SequenceNeedAll + "', " + sequence.NextSequenceID + ", '" + sequence.IsLastSequence + "', '" + sequence.HasSubSequence + "', '" + sequence.ValueFrom + "', '" + sequence.IsActor + "')";

                sqlArray.Add(sqlCommand);
            }            

            try
            {
                return SQLHelper.ExecuteSqlByTransaction(sqlArray);
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

        /// <summary>
        /// Delete a sequence in a system
        /// </summary>
        /// <param name="systemID">system id</param>
        /// <param name="sequenceID">sequence id</param>
        /// <returns>return true if succeed, and false if not</returns>
        public static bool DeleteSequence(int systemID, int sequenceID)
        {
            if (CanDeleteSequence(systemID, sequenceID))
            {
                // delete
                string sqlCommand = "delete from MainWorkFlow where SystemID = " + systemID + " and SequenceID = " + sequenceID;
                
                try
                {
                    return SQLHelper.ExecuteSql(sqlCommand);
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
                    Logger.Log.Error(" --- Delete Sql:  " + sqlCommand);
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Add immediate next sequence.
        /// Only sequence that is not the last sequence and has no supbsequence can be added with next sequence
        /// </summary>
        /// <param name="systemID">system id</param>
        /// <param name="mainSequenceID">main sequence id</param>
        /// <param name="nextSequenceID">next sequence id</param>
        /// <returns>return true if succeeds and false if not</returns>
        public static bool AddNextSequence(int systemID, int mainSequenceID, int nextSequenceID)
        {
            Sequence sequence = new Sequence(systemID, mainSequenceID);

            if (sequence.SystemID == -1)
            {
                return false;
            }

            // Check to see if the mainsequence is the last sequence or has subsequence
            if (sequence.IsLastSequence == "1" || sequence.HasSubSequence == "1")
            {
                return false;
            }

            string sqlCommand =
                " update MainWorkFlow set NextSequenceID = " + nextSequenceID +
                " where SystemID = " + systemID + " and SequenceID = " + mainSequenceID;
            
            try
            {
                return SQLHelper.ExecuteSql(sqlCommand);
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

        /// <summary>
        /// Add subsequence.
        /// Only sequence that is not the last sequence and has no direct descendant subsequence can be added with  subsequence
        /// </summary>
        /// <param name="systemID">system id</param>
        /// <param name="mainSequenceID">main sequence id</param>
        /// <param name="nextSeauenceID">next sequence id</param>
        /// <param name="conditionID">condition id</param>
        /// <param name="conditionDescription">confition description</param>
        /// <returns>return true if succeeds and false if not</returns>
        public static bool AddSubSequence(int systemID, int mainSequenceID, int nextSeauenceID, string conditionDescription)
        {
            Sequence sequence = new Sequence(systemID, mainSequenceID);
            int conditionID = GenerateNextConditionID(systemID);
            if (sequence.SystemID == -1)
            {
                return false;
            }

            // Check to see if the mainsequence is the last sequence or has immediate subsequence
            if (sequence.IsLastSequence == "1" || sequence.NextSequenceID != -1)
            {
                return false;
            }

            string sqlCommand = "insert into SubWorkFlow (SystemID, MainSequenceID, ConditionID, ConditionDescription, NextSequenceID) " +
                " values ( " + systemID + ", " + mainSequenceID + ", " + conditionID + ", '" + conditionDescription + "', " + nextSeauenceID + ")";

            try
            {
                return SQLHelper.ExecuteSql(sqlCommand);
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
                Logger.Log.Error(" --- Insert Sql:  " + sqlCommand);
            }
        }

        /// <summary>
        /// Get sequence list
        /// </summary>
        /// <param name="sqlQuery">sql statement</param>
        /// <returns>sequence dataset</returns>
        public static DataSet GetSequences(string sqlQuery)
        {            
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
        /// Get system sequences using specified sql statement
        /// </summary>
        /// <param name="sqlQuery">sql statement</param>
        /// <returns>sequence dataset</returns>
        public static DataSet GetSystemSequences(string sqlQuery)
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

        /// <summary>
        /// Generate the next sequence id for a system, usually increment by 100
        /// </summary>
        /// <param name="systemID">system id</param>
        /// <returns>next max sequence id</returns>
        public static int GenerateNextSequenceID(int systemID)
        {
            string sqlQuery = "select max(isnull(SequenceID, 0)) from MainWorkFlow where SystemID = " + systemID;
            object maxSequenceID;

            try
            {
                maxSequenceID = SQLHelper.GetSingle(sqlQuery);
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

            if (maxSequenceID == null)
            {
                return 100;
            }
            else
            {
                int temp = (int)maxSequenceID;
                if (temp == -1)
                {
                    return 100;
                }
                else
                {
                    return 100 + temp;
                }
            }
        }

        /// <summary>
        /// Generate the next Condition id for SubWorkFlow, usually increment by 1
        /// </summary>
        /// <param name="systemID">system id</param>
        /// <returns>next max Condition id</returns>
        public static int GenerateNextConditionID(int systemID)
        {
            string sqlQuery = "select max(isnull(ConditionID, 0)) from SubWorkFlow where SystemID = " + systemID;
            object maxSequenceID;

            try
            {
                maxSequenceID = SQLHelper.GetSingle(sqlQuery);

                return (maxSequenceID == null) ? 1 : (int)maxSequenceID + 1;
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
        /// Check if a sequence can be deleted
        /// </summary>
        /// <param name="systemID">system id </param>
        /// <param name="sequenceID">sequence</param>
        /// <returns>return true if it can be deleted, or false if not</returns>
        private static bool CanDeleteSequence(int systemID, int sequenceID)
        {
            string sqlQuery =
                " select ((select count(*) from MainWorkFlow where SystemID = " + systemID + " and SequenceID = " + sequenceID + " and NextSequenceID != -1)) + " +
                " (select count(*) from SubWorkFlow where SystemID = " + systemID + " and MainSequenceID = " + sequenceID + ") + " +
                " (select count(*) from SubWorkFlow where SystemID = " + systemID + " and NextSequenceID = " + sequenceID + ") ";
            
            try
            {
                return !SQLHelper.Exists(sqlQuery);
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
        /// Show All Routing
        /// </summary>
        /// <param name="systemID">system id</param>
        /// <returns>Dataset</returns>
        public static DataSet ShowAllRouting(int systemID)
        {
            string sqlQuery = string.Empty;

            sqlQuery =
                " select MWF1.systemID,MWF1.SequenceID MainID, MWF1.SequenceDescription MainDescription, MWF2.SequenceID NextID, MWF2.SequenceDescription NextDescription, ' ' ConditionID,'NA' ConditionDescription " +
                " from MainWorkFlow MWF1 join MainWorkFlow MWF2 on MWF1.NextSequenceID = MWF2.SequenceID  and MWF1.NextSequenceID<>-1  " +
                " where MWF1.SystemID=" + systemID + " and " +
                " MWF2.SystemID=" + systemID + "";

            sqlQuery +=
                "union (select MWF1.systemID,MWF1.SequenceID MainID, MWF1.SequenceDescription MainDescription, MWF2.SequenceID NextID, MWF2.SequenceDescription NextDescription,cast(MWF1.ConditionID as varchar(20)) ConditionID,  MWF1.ConditionDescription " +
                " from (select MainWorkFlow.systemID,SequenceID, SequenceDescription, ConditionID, ConditionDescription, SubWorkFlow.NextSequenceID " +
                " from MainWorkFlow join SubWorkFlow on MainWorkFlow.SequenceID = SubWorkFlow.MainSequenceID and MainWorkFlow.SystemID= " + systemID + " and SubWorkFlow.SystemID = " + systemID + "" +
                " ) MWF1 join MainWorkFlow MWF2 on MWF1.NextSequenceID = MWF2.SequenceID " +
                "WHERE (MWF1.SystemID = " + systemID + ") AND (MWF2.SystemID = " + systemID + "))";
            
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
        /// Delete Routing
        /// </summary>
        /// <param name="systemID">systemID,mainID,nextID</param>
        /// <returns>Dataset</returns>
        public static bool DeleteRouting(int systemID, int mainID, int nextID)
        {
            Sequence sequence = new Sequence(systemID, mainID);
            string sqlCommand;
            if (sequence.HasSubSequence == "0")
            {
                sqlCommand = "update mainworkflow set NextSequenceID=-1 where systemID=" + systemID + " and sequenceID=" + mainID + "";
            }
            else
            {
                sqlCommand = "delete from SubWorkFlow where systemID=" + systemID + " and MainSequenceID=" + mainID + " and NextSequenceID=" + nextID + "";                
            }
            
            try
            {
                return SQLHelper.ExecuteSql(sqlCommand);
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
                Logger.Log.Error(" --- Sql:  " + sqlCommand);
            }

        }

        /// <summary>
        /// Generate work flow log
        /// </summary>
        /// <param name="systemID">system id</param>
        /// <param name="docID">document id</param>
        /// <param name="requestorID">requestor id</param>
        /// <param name="curSequenceID">current sequence id(-1 indicates initial sequence)</param>
        /// <param name="conditionID">condition id(-1 indicates no condition)</param>
        /// <returns>return true if succeeds and false if not</returns>
        public static bool GenerateWorkFlowLog(int systemID, int docID, int requestorID, int curSequenceID, int conditionID, bool firstTimeInvoked)
        {
            try
            {
                while (true)
                {
                    Sequence sequence = new Sequence(systemID, curSequenceID);

                    if (sequence.IsLastSequence == "1")
                    {
                        return true;
                    }

                    if (sequence.HasSubSequence == "0")
                    {
                        // no subsequence, get next sequence id from MainWorkFlow
                        curSequenceID = sequence.GetNextSequenceID();

                        if (!IsSeqauenceGenerated(systemID, docID, curSequenceID))
                        {
                            InsertLog(systemID, curSequenceID, docID, requestorID, firstTimeInvoked);
                            return GenerateWorkFlowLog(systemID, docID, requestorID, curSequenceID, -1, false);
                        }
                        else
                        {
                            return true;
                        }
                    }
                    else
                    {
                        // has subsequence
                        if (sequence.ValueFrom == "0")
                        {
                            // from requestor
                            conditionID = GetRequestorChoice(systemID, curSequenceID, docID);
                            curSequenceID = sequence.GetNextSequenceID(conditionID);
                            if (!IsSeqauenceGenerated(systemID, docID, curSequenceID))
                            {
                                InsertLog(systemID, curSequenceID, docID, requestorID, firstTimeInvoked);
                                return GenerateWorkFlowLog(systemID, docID, requestorID, curSequenceID, -1, false);
                            }
                            else
                            {
                                return true;
                            }
                        }
                        else
                        {
                            // from approver
                            if (conditionID != -1)
                            {
                                curSequenceID = sequence.GetNextSequenceID(conditionID);
                                InsertLog(systemID, curSequenceID, docID, requestorID, firstTimeInvoked);
                                return GenerateWorkFlowLog(systemID, docID, requestorID, curSequenceID, -1, false);
                            }
                            else
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // log here
                return false;
            }
        }

        /// <summary>
        /// Insert work flow log into WorkFlowLog using specified systemid, sequenceid
        /// </summary>
        /// <param name="systemID">system id</param>
        /// <param name="curSequenceID">current sequence id</param>
        /// <param name="docID">document id</param>
        /// <param name="requestorID">requestor employee id</param>
        private static void InsertLog(int systemID, int curSequenceID, int docID, int requestorID, bool firstTimeInvoked)
        {
            Sequence sequence = new Sequence(systemID, curSequenceID);
            InsertLog(sequence, docID, requestorID, firstTimeInvoked);
        }

        /// <summary>
        /// Insert work flow log into WorkFlowLog using specified sequence
        /// </summary>
        /// <param name="sequence"></param>
        /// <param name="docID"></param>
        /// <param name="requestorID"></param>
        private static void InsertLog(Sequence sequence, int docID, int requestorID, bool firstTimeInvoked)
        {
            if (sequence == null || sequence.SystemID == -1 || docID == -1)
                return;

            ApproverRole role;
            DataSet supervisors = null;
            ArrayList sqlCommandArray = new ArrayList();

            string status = (firstTimeInvoked ? "pending" : "-1");
            string pendingdate = (firstTimeInvoked ? "getdate()" : "null");

            for (int i = 0; i < sequence.RoleID.Length; i++)
            {
                int approveNeededID = -1;
                role = new ApproverRole(sequence.RoleID[i]);

                // roletype  --  0: report line, 1: responsibility, 2: Self
                string sqlCommand = string.Empty;

                if (role.RoleType == "0")
                {
                    string sqlQuery =
                        "select max(Grade) from WorkFlowLog where SystemID = " + sequence.SystemID +
                        " and DocumentID = " + docID + " and SequenceID < " + sequence.SequenceID;

                    object maxGrade = SQLHelper.GetSingle(sqlQuery);
                    if (maxGrade == null || (int)maxGrade < role.Grade || (sequence.HasSubSequence == "1" && sequence.ValueFrom == "1"))
                    {
                        if (supervisors == null)
                        {
                            supervisors = GetAllSupervisors(requestorID);
                        }

                        for (int k = 0; k < supervisors.Tables[0].Rows.Count; k++)
                        {
                            int curGrade = int.Parse(supervisors.Tables[0].Rows[k][1].ToString());
                            if (curGrade >= role.Grade)
                            {
                                approveNeededID = int.Parse(supervisors.Tables[0].Rows[k][0].ToString());
                                sqlCommand =
                                    " insert into WorkFlowLog (SystemID, DocumentId, SequenceID, RoleDescription, Roletype, ApproveNeededID, Grade, ApproveByID, Status, ApproveDate, PendingDate) " +
                                    " values (" + sequence.SystemID + ", " + docID + ", " + sequence.SequenceID + ", '" + role.RoleDescription + "', '" + role.RoleType +
                                    "', " + approveNeededID + ", " + curGrade + ", -1, '" + status + "', null, " + pendingdate + ")";

                                sqlCommandArray.Add(sqlCommand);

                                break;
                            }
                        }
                    }
                }
                else
                {
                    approveNeededID = ((role.RoleType == "1") ? role.RoleID : requestorID);
                    sqlCommand =
                        " insert into WorkFlowLog (SystemID, DocumentId, SequenceID, RoleDescription, Roletype, ApproveNeededID, Grade, ApproveByID, Status, ApproveDate, PendingDate) " +
                        " values (" + sequence.SystemID + ", " + docID + ", " + sequence.SequenceID + ", '" + role.RoleDescription + "', '" + role.RoleType + "', " + approveNeededID + ", " +
                        " -1, -1, '" + status + "', null, " + pendingdate + ")";

                    sqlCommandArray.Add(sqlCommand);
                }
            }
            
            try
            {
                SQLHelper.ExecuteSqlByTransaction(sqlCommandArray);
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
                for (int i = 0; i < sqlCommandArray.Count; i++)
                {
                    sqlStatements += sqlCommandArray[i].ToString() + "\n";
                }

                Logger.Log.Error(sqlStatements);
            }
        }

        public static void MailOnSubmitting(int sysID, int docID)    // inform that the request has submitted successful
        {
            string sqlCommand = " exec MailOnSubmitting " + sysID + ", " + docID;
            SQLHelper.ExecuteSql(sqlCommand);
        }

        public static void MailOnApproval(int sysID, int docID)     // inform that the request has been approved and the next approver
        {
            string sqlCommand = " exec MailOnApproval " + sysID + ", " + docID;
            SQLHelper.ExecuteSql(sqlCommand);
        }

        public static void MailOnFinalApproval(int sysID, int docID)     // inform that the request has been approved and the next approver
        {
            string sqlCommand = " exec MailOnFinalApproval " + sysID + ", " + docID;
            SQLHelper.ExecuteSql(sqlCommand);
        }

        public static void MailOnRejection(int sysID, int docID)    // inform that the request has been rejected
        {
            string sqlCommand = " exec MailOnRejection " + sysID + ", " + docID;
            SQLHelper.ExecuteSql(sqlCommand);
        }

        public static void MailForApproval(int sysID, int docID)     // inform that there's a request needing his approval
        {
            string sqlCommand = " exec MailForApproval " + sysID + ", " + docID;
            SQLHelper.ExecuteSql(sqlCommand);
        }

        /// <summary>
        /// Get requestor's choice using specified systemid, sequence and document id
        /// </summary>
        /// <param name="systemID">system id</param>
        /// <param name="sequenceID">sequence id</param>
        /// <param name="docID">document id</param>
        /// <returns>condition id</returns>
        public static int GetRequestorChoice(int systemID, int sequenceID, int docID)
        {
            string sqlQuery = "select ConditionID from RequestorChoice where SystemID = " + systemID + " and DocumentID = " + docID + " and MainSequenceID = " + sequenceID;
            object temp;

            try
            {
                temp = SQLHelper.GetSingle(sqlQuery);
                return (temp == null) ? -1 : (int)temp;
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
        /// 
        /// </summary>
        /// <param name="requestorID"></param>
        /// <returns></returns>
        private static DataSet GetAllSupervisors(int requestorID)
        {
            string sqlQuery = "exec P_GetAllSupervisors " + requestorID;
            
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

        public static bool IsSeqauenceGenerated(int systemID, int docID, int sequenceID)
        {
            string sqlQuery =
                " select count(*) from WorkFlowLog where SystemID = " + systemID + " and DocumentID = " + docID +
                " and SequenceID = " + sequenceID + " and Status = '-1'";
            
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
        /// Get Current Document Approval Flow
        /// </summary>
        /// <param name="systemID">system id</param>
        /// <param name="docID">document id</param>
        /// <returns>Approval Flow</returns>
        public static DataSet GetApprovalFlow(int systemID, int documentID)
        {
            string sqlQuery =
                " SELECT SequenceID, RoleDescription, CASE WHEN RoleType = '0' OR " +
                " RoleType = '2' THEN (SELECT name FROM  employeeInfo " +
                " WHERE emplid = ApproveNeededID) WHEN RoleType = '1' THEN '' END AS ApproveNeededID, CASE WHEN ApproveByID <> '' THEN " +
                " (SELECT name FROM  employeeInfo WHERE  emplid = ApproveByID) " +
                " ELSE '' END AS ApproveBy, case when Status='-1' then '' else Status end as Status, ApproveDate FROM  WorkFlowLog " +
                " where DocumentID=" + documentID + " and SystemID=" + systemID + " " +
                " order by SequenceID";            

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
        ///New Request use WorkFlow Engine
        /// </summary>
        /// <param name="systemID">system id</param>
        /// <param name="docID">document id</param>
        /// <param name="systemID">requestor id</param>
        /// <param name="docID">owner id</param>
        /// <param name="systemID">current sequence id</param>
        /// <param name="docID">condition id</param>
        /// <param name="docID">requestor choice list</param>
        /// <returns>bool</returns>
        public static bool NewRequest(int systemID, int docID, int requestorID,int owner, int curSequenceID, int conditionID, List<RequestorChoice> choiceList)
        {
            ArrayList sqlArray = new ArrayList();
            Document document = new Document(systemID, docID);
            document.InsertDocumentHeader(sqlArray, requestorID, owner);
            document.InsertRequestorChoice(sqlArray, choiceList);
            if (!SQLHelper.ExecuteSqlByTransaction(sqlArray))
            {
                return false;
            }

            //if (!GenerateWorkFlowLog(systemID, docID, requestorID, curSequenceID, conditionID, true))
            if (!GenerateWorkFlowLog(systemID, docID, owner, curSequenceID, conditionID, true))
            {
                return false;
            }

            //send mail to requestor 
            MailOnSubmitting(systemID, docID);

            // send to approver
            MailForApproval(systemID, docID);

            return true;
        }


        /// <summary>
        ///Get Approver Flow
        /// </summary>
        /// <param name="systemID">system id</param>
        /// <param name="docID">document id</param>
        /// <returns>DateSet</returns>
        public static DataSet GetApproverFlow(int systemID, int docID)
        {
            string sqlQuery = 
                " select SystemID, DocumentID, SequenceID, RoleDescription,"+
                " CASE WHEN ApproveByID !=-1 THEN"+
                          " (SELECT  name FROM employeeInfo"+
                           " WHERE emplid = ApproveByID) ELSE CASE WHEN RoleType = 1 THEN '' ELSE"+
                           " (SELECT  name FROM employeeInfo"+
                           " WHERE emplid = ApproveNeededID) END END AS Approver,case when Status='-1' then '' else Status end as Status, ApproveDate,remark" +
                " FROM WorkFlowLog where SystemID=" + systemID + " and DocumentID=" + docID + " order by SequenceID";
            
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

        public static bool NTExist(string NT)
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

        public static object GetEmplID(string NT)
        {
            string sqlQuery = "select emplid from dbo.EmployeeInfo where NTAccount='" + NT + "'";            

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

        #endregion
    }
    
}


