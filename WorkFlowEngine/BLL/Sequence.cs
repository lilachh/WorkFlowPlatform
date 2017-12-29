using System;
using System.Data;
using System.Text;
using WorkFlowEngine.DBUtility;
using System.Data.SqlClient;

namespace WorkFlowEngine.BLL
{
    public class Sequence
    {

        #region Constructors

        public Sequence()
        {
            SetDefaultValues();
        }

        public Sequence(int systemID, int sequenceID)
        { 
            //set other fields values
            string sqlQuery =
                " select SequenceDescription, RoleID, SequenceNeedAll, NextSequenceID, IsLastSequence, HasSubSequence, ValueFrom, IsActor " +
                " from MainWorkFlow where SystemID = " + systemID + " and SequenceID = " + sequenceID;

            try
            {
                DataSet ds = SQLHelper.Query(sqlQuery);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    try
                    {
                        this.systemID = systemID;
                        this.sequenceID = sequenceID;
                        this.sequenceDescription = ds.Tables[0].Rows[0][0].ToString();
                        this.sequenceNeedAll = ds.Tables[0].Rows[0][2].ToString();
                        this.nextSequenceID = (int)ds.Tables[0].Rows[0][3];
                        this.isLastSequence = ds.Tables[0].Rows[0][4].ToString();
                        this.hasSubSequence = ds.Tables[0].Rows[0][5].ToString();
                        this.valueFrom = ds.Tables[0].Rows[0][6].ToString();
                        this.isActor = ds.Tables[0].Rows[0][7].ToString();

                        this.roleID = new int[ds.Tables[0].Rows.Count];
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            roleID[i] = (int)ds.Tables[0].Rows[i][1];
                        }
                    }
                    catch
                    {
                        SetDefaultValues();
                    }
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

        #endregion
        
        #region Fields

        private int systemID;
        private int sequenceID;
        private string sequenceDescription;
        private int[] roleID;
        private string sequenceNeedAll;        
        private int nextSequenceID;
        private string isLastSequence;
        private string hasSubSequence;
        private string valueFrom;
        private string isActor;

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

        /// <summary>
        /// initial sequence id is -1
        /// </summary>
        public int SequenceID
        {
            get
            {
                return sequenceID;
            }

            set
            {
                sequenceID = value;
            }
        }

        public string SequenceDescription
        {
            get
            {
                return sequenceDescription;
            }

            set
            {
                sequenceDescription = value;
            }
        }

        public int[] RoleID
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

        /// <summary>
        /// 0: no, 1: yes
        /// </summary>
        public string SequenceNeedAll
        {
            get
            {
                return sequenceNeedAll;
            }

            set
            {
                sequenceNeedAll = value;
            }
        }

        public int NextSequenceID
        {
            get
            {
                return nextSequenceID;
            }

            set
            {
                nextSequenceID = value;
            }
        }

        /// <summary>
        /// 0: no, 1: yes
        /// </summary>
        public string IsLastSequence
        {
            get
            {
                return isLastSequence;
            }

            set
            {
                isLastSequence = value;
            }
        }

        /// <summary>
        /// 0: no, 1: yes
        /// </summary>
        public string HasSubSequence
        {
            get
            {
                return hasSubSequence;
            }

            set
            {
                hasSubSequence = value;
            }
        }

        /// <summary>
        /// 0: from requestor, 1: from approver
        /// </summary>
        public string ValueFrom
        {
            get
            {
                return valueFrom;
            }

            set
            {
                valueFrom = value;
            }
        }

        /// <summary>
        /// 0: from requestor, 1: from approver
        /// </summary>
        public string IsActor
        {
            get
            {
                return isActor;
            }

            set
            {
                isActor = value;
            }
        }

        #endregion

        #region Methods

        private void SetDefaultValues()
        {
            systemID = -1;
            sequenceID = -1;
            sequenceDescription = string.Empty;
            roleID = new int[0];
            sequenceNeedAll = "0";
            nextSequenceID = -1;
            isLastSequence = "0";
            hasSubSequence = "0";
            valueFrom = "0";
            isActor = "0";
        }

        /// <summary>
        /// Get existing routing results
        /// </summary>
        /// <returns>(MainID, MainDescription, NextID, NextDescription, ConditionDescription)</returns>
        public DataSet GetRouting()
        {
            // return (MainID, MainDescription, NextID, NextDescription, ConditionDescription)

            if (this.systemID == -1)
            {
                return null;
            }

            string sqlQuery = string.Empty;

            if (this.hasSubSequence == "0")
            {
                // no subsequence
                sqlQuery =
                    " select distinct MWF1.systemID,MWF1.SequenceID MainID, MWF1.SequenceDescription MainDescription, MWF2.SequenceID NextID, MWF2.SequenceDescription NextDescription, 'NA' ConditionDescription , null ConditionID" +
                    " from MainWorkFlow MWF1 join MainWorkFlow MWF2 on MWF1.NextSequenceID = MWF2.SequenceID  and MWF1.NextSequenceID<>-1  " +
                    " where MWF1.SequenceID = " + this.sequenceID + " and MWF1.SystemID="+this.SystemID+" and "+
                    " MWF2.SystemID=" + this.SystemID + "";
            }
            else
            {
                // has subsequence
                sqlQuery =
                    " select distinct MWF1.systemID,MWF1.SequenceID MainID, MWF1.SequenceDescription MainDescription, MWF2.SequenceID NextID, MWF2.SequenceDescription NextDescription,  MWF1.ConditionDescription,MWF1.ConditionID " +
                    " from (select MainWorkFlow.systemID,SequenceID, SequenceDescription, ConditionID, ConditionDescription, SubWorkFlow.NextSequenceID " +
                    " from MainWorkFlow join SubWorkFlow on MainWorkFlow.SequenceID = SubWorkFlow.MainSequenceID" +
                    " where MainWorkFlow.SequenceID = " + this.sequenceID + " and SubWorkFlow.MainSequenceID = " + this.sequenceID + " and MainWorkFlow.SystemID = " + this.systemID + " and SubWorkFlow.SystemID = " + this.systemID + ") MWF1 join MainWorkFlow MWF2 on MWF1.NextSequenceID = MWF2.SequenceID " +
                    "WHERE (MWF1.SystemID = " + this.SystemID + ") AND (MWF2.SystemID = " + this.SystemID + ")";                  
            }

            
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
        /// If RoleIDExists
        /// </summary>
        /// <returns>(systemID,sequenceID)</returns>
        public static bool RoleIDExists(int systemID, int sequenceID)
        {
            string sqlQuery = "select count(*) from MainWorkFlow where SystemID = " + systemID + " and SequenceID = " + sequenceID;
            
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

        public static void UpdateConditionDescription(string sqlCommand)
        {
            try
            {
                SQLHelper.ExecuteSql(sqlCommand);
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

        public bool UpdateUrl(string url)
        {
            if (this.systemID == -1 || sequenceID == -1)
            {
                return false;
            }

            string sqlQuery = "select count(*) from SpecificUrl where SystemID = " + systemID + " and SequenceID = " + sequenceID;
            string sqlCommand = string.Empty;

            if (SQLHelper.Exists(sqlQuery))
            {
                if (url != "")
                {
                    sqlCommand = "update SpecificUrl set Url = '" + url.Replace("'", "''") + "' where SystemID = " + systemID + " and SequenceID = " + sequenceID;
                }
                else
                {
                    sqlCommand = "delete from SpecificUrl where SystemID = " + systemID + " and SequenceID = " + sequenceID;
                }
            }
            else
            {
                if (url != "")
                {
                    sqlCommand = "insert into SpecificUrl values(" + systemID + ", " + sequenceID + ", '" + url + "')";
                }                
            }

            try
            {
                if (sqlCommand == string.Empty)
                    return true;
                else
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
        /// Get next sequence id of a sequence with no subsequence
        /// </summary>
        /// <returns>next sequence id</returns>
        public int GetNextSequenceID ()
        {
            if (this.systemID == -1)
            {
                return -1;
            }

            if (this.hasSubSequence == "0")
            {
                // no subsequence
                string sqlQuery = "select top 1 NextSequenceID from MainWorkFlow where SystemID = " + this.systemID + " and SequenceID = " + this.sequenceID;                                

                try
                {
                    object temp = SQLHelper.GetSingle(sqlQuery);
                    return (int)temp;
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
            else
            {
                return -1;  // has subsequence
            }
        }

        /// <summary>
        /// Get next sequence id of a sequence with subsequence using specified conditionid
        /// </summary>
        /// <param name="conditionID">condition id</param>
        /// <returns>next sequence id</returns>
        public int GetNextSequenceID (int conditionID)
        {
            if (this.systemID == -1)
            {
                return -1;
            }

            if (this.hasSubSequence == "1")
            {
                // has subsequence
                string sqlQuery =
                    "select top 1 NextSequenceID from SubWorkFlow " +
                    "where SystemID = " + this.systemID + " and MainSequenceID = " + this.sequenceID + " and ConditionId = " + conditionID;                

                try
                {
                    object temp = SQLHelper.GetSingle(sqlQuery);
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
            else
            {
                return -1;  // no subsequence
            }
        }

        public string GetSpecificUrl()
        {
            if (this.systemID == -1)
            {
                return null;
            }

            string sqlQuery = "select Url from SpecificUrl where SystemID = " + this.systemID + " and SequenceID = " + this.sequenceID;

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

        public DataSet GetConditions()
        {
            if (this.systemID == -1 || this.hasSubSequence == "0")
            {
                return null;
            }

            string sqlQuery = "select ConditionID, ConditionDescription from SubWorkFlow where SystemID = " + this.systemID + " and MainSequenceID = " + this.sequenceID;
            
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

    }
}

