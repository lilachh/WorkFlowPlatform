using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkFlowEngine.BLL
{
    public class RequestorChoice
    {
        #region Fields
        private int systemID;
        private int documentID;
        private int mainSequenceID;
        private int conditionID;
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

        public int DocumentID
        {
            get
            {
                return documentID;
            }

            set
            {
                documentID = value;
            }
        }

        public int MainSequenceID
        {
            get
            {
                return mainSequenceID;
            }

            set
            {
                mainSequenceID = value;
            }
        }

        public int ConditionID
        {
            get
            {
                return conditionID;
            }

            set
            {
                conditionID = value;
            }
        }
        #endregion

        #region Constructors

        public RequestorChoice()
        { }

        public RequestorChoice(int systemID, int documentID, int mainSequenceID, int conditionID)
        {
            this.SystemID = systemID;
            this.DocumentID = documentID;
            this.mainSequenceID = mainSequenceID;
            this.conditionID = conditionID;
        }
        #endregion

    }
}
