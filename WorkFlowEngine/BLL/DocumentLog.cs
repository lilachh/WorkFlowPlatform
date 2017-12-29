using System;
using System.Text;
using System.Data;
using WorkFlowEngine.DBUtility;

namespace WorkFlowEngine.BLL
{
    class DocumentLog
    {
        #region Constructors

        public DocumentLog()
        {
            systemID = -1;
            documentID = -1;
            sequenceID = -1;
            sequence = null;
        }

        public DocumentLog(int sysID, int docID, int seqID)
        {
            this.systemID = sysID;
            this.documentID = docID;
            this.sequenceID = seqID;

            this.sequence = new Sequence(sysID, seqID);
        }

        #endregion

        #region Fields

        private Sequence sequence;
        private int systemID;
        private int documentID;
        private int sequenceID;

        #endregion

        #region Methods.

        public string MarkAsApproved(int approveNeededID, int approverID,string remark)
        {
            return
                " update WorkFlowLog set Status = 'approved',ApproveByID=" + approverID + ",ApproveDate= '" + DateTime.Now + "',remark='" + remark + "' where SystemID = " + this.systemID +
                " and DocumentID = " + this.documentID + " and SequenceID = " + this.sequenceID + " and ApproveNeededID = " + approveNeededID;
        }

        public string MarkAsPending()
        {
            return
                "update WorkFlowLog set Status = 'pending', PendingDate = getdate() where SystemID = " + this.systemID +
                " and DocumentID = " + this.documentID + " and SequenceID = " + this.sequenceID;
        }

        public string MarkAsReject(int approveNeededID, int approverID, string remark)
        {
            return
                "update WorkFlowLog set Status = 'rejected',ApproveByID=" + approverID + ",ApproveDate='" + DateTime.Now + "',remark='" + remark + "' where SystemID = " + this.systemID +
                " and DocumentID = " + this.documentID + " and SequenceID = " + this.sequenceID + " and ApproveNeededID = " + approveNeededID;
        }

        /// <summary>
        /// Get next sequence id of a sequence with no subsequence
        /// </summary>
        /// <returns>next sequence id</returns>
        public int GetNextSequenceID()
        {
            return sequence.GetNextSequenceID();
        }

        /// <summary>
        /// Get next sequence id of a sequence with subsequence using specified conditionid
        /// </summary>
        /// <param name="conditionID">condition id</param>
        /// <returns>next sequence id</returns>
        public int GetNextSequenceID(int conditionID)
        {
            return sequence.GetNextSequenceID(conditionID);
        }

        #endregion

        #region Properties

        public int SystemID
        {
            get
            {
                return sequence.SystemID;
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
                return sequence.SequenceDescription;
            }
        }

        public int[] RoleID
        {
            get
            {
                return sequence.RoleID;
            }
        }

        /// <summary>
        /// 0: no, 1: yes
        /// </summary>
        public string SequenceNeedAll
        {
            get
            {
                return sequence.SequenceNeedAll;
            }
        }

        public int NextSequenceID
        {
            get
            {
                return sequence.NextSequenceID;
            }
        }

        /// <summary>
        /// 0: no, 1: yes
        /// </summary>
        public string IsLastSequence
        {
            get
            {
                return sequence.IsLastSequence;
            }
        }

        /// <summary>
        /// 0: no, 1: yes
        /// </summary>
        public string HasSubSequence
        {
            get
            {
                return sequence.HasSubSequence;
            }
        }

        /// <summary>
        /// 0: from requestor, 1: from approver
        /// </summary>
        public string ValueFrom
        {
            get
            {
                return sequence.ValueFrom;
            }
        }

        #endregion

    }
}
