using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.Common;
using System.Collections.Generic;
using System.Data.Sql;
using WorkFlowEngine.DBUtility;



namespace WorkFlowEngine.BLL
{
    public class Tree
    {
        #region Fields
        private List<Node1> listNode1 = new List<Node1>();
        #endregion

        #region Properties

        public List<Node1> ListNode1
        {
            get
            {
                return listNode1;
            }

            set
            {
                listNode1 = value;
            }
        }
        #endregion

        #region method

        /// <summary>
        /// Add Node1 into Tree
        /// </summary>
        /// <returns>void</returns>
        public void Add(Node1 Node1)
        {
            ListNode1.Add(Node1);
        }

        /// <summary>
        /// Get Next Index ID
        /// </summary>
        /// <returns>int</returns>
        public int GetNextIndexID()
        {
            int max=0;
            for (int i = 0; i < ListNode1.Count; i++)
            {
                if (max <= ListNode1[i].Index)
                {
                    max = ListNode1[i].Index;
                }
            }
            return max + 1;
        }

        /// <summary>
        /// GetnerateSon 
        /// </summary>
        /// <returns>(Node1)</returns>
        public DataSet GenerateSon(Node1 Node1)
        {
            int systemID = Node1.Sequ.SystemID;
            int sequenceID = Node1.Sequ.SequenceID;
            string sqlQuery;
            if (Node1.Sequ.HasSubSequence == "0")
            {
                // no subsequence
                sqlQuery =
                    " select distinct MWF1.systemID,MWF1.SequenceID MainID, MWF1.SequenceDescription MainDescription, MWF2.SequenceID NextID, MWF2.SequenceDescription NextDescription, 'NA' ConditionDescription " +
                    " from MainWorkFlow MWF1 join MainWorkFlow MWF2 on MWF1.NextSequenceID = MWF2.SequenceID  and MWF1.NextSequenceID<>-1  " +
                    " where MWF1.SequenceID = " + sequenceID + " and MWF1.SystemID="+systemID+" and "+
                    " MWF2.SystemID=" + systemID + "";
            }
            else
            {
                // has subsequence
                sqlQuery =
                    " select distinct MWF1.systemID,MWF1.SequenceID MainID, MWF1.SequenceDescription MainDescription, MWF2.SequenceID NextID, MWF2.SequenceDescription NextDescription,  MWF1.ConditionDescription " +
                    " from (select MainWorkFlow.systemID,SequenceID, SequenceDescription, ConditionID, ConditionDescription, SubWorkFlow.NextSequenceID " +
                    " from MainWorkFlow join SubWorkFlow on MainWorkFlow.SequenceID = SubWorkFlow.MainSequenceID" +
                    " where MainWorkFlow.SequenceID = " + sequenceID + " and SubWorkFlow.MainSequenceID = " + sequenceID + " and MainWorkFlow.SystemID = " + systemID + " and SubWorkFlow.SystemID = " + systemID + ") MWF1 join MainWorkFlow MWF2 on MWF1.NextSequenceID = MWF2.SequenceID " +
                    " WHERE (MWF1.SystemID = " + systemID + ") AND (MWF2.SystemID = " + systemID + ")";                  
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
        /// Add All Directed Son 
        /// </summary>
        /// <returns>Dateset</returns>
        public bool AddSon()
        {
            int index = FindIndex();
            if (index == -1)
            {
                return false;
            }
            DataSet ds = GenerateSon(ListNode1[index]) ;
            if (ds.Tables[0].Rows.Count == 0)
            {
                ListNode1[index].Spread = true;
                return false;
            }
            int width;
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                DataRow dr = ds.Tables[0].Rows[i];
                Node1 node = new Node1();
                node.RowID = ListNode1[index].RowID+1;
                node.Y = ListNode1[index].Y + 50;
                node.FatherID = ListNode1[index].Index;
                Sequence sequence = new Sequence(Convert.ToInt32(dr["systemID"].ToString()), Convert.ToInt32(dr["NextID"].ToString()));
                node.Sequ = sequence;
                node.Index = GetNextIndexID();
                width = 500 /( node.RowID-1);
                if (node.RowID > 2)
                {
                    width = width / 2;
                }
               // width = width / (node.RowID - 1);
                if (ds.Tables[0].Rows.Count == 1)
                {
                    node.X = ListNode1[index].X;
                }
                else
                {
                    node.X = ListNode1[index].X - width / 2 + width / (ds.Tables[0].Rows.Count - 1) * i;
                }
                ListNode1.Add(node);
            }
            ListNode1[index].Spread = true;
            return true;
        }

        /// <summary>
        /// Find Index
        /// </summary>
        /// <returns>(Node1)</returns>
        public int FindIndex()
        {
           // ListNode1.Sort();
            for (int i = 0; i < ListNode1.Count; i++)
            {
                if (ListNode1[i].Spread==false&&ListNode1[i].Sequ.IsLastSequence=="0")
                {
                    return i;
                }
            }
            return -1;
        }

        #endregion

    }
}
