using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using WorkFlowEngine.BLL;

namespace WorkFlowWebService
{
    /// <summary>
    /// Summary description for Service1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class WorkFlow2 : System.Web.Services.WebService
    {
        public WorkFlow2()
        { }

        [WebMethod]
        public bool NewRequest(int systemID, int docID, int requestorID, int owner, int curSequenceID, int conditionID, RequestorChoice[] choiceList)
        {
            List<RequestorChoice> choiceList2 = new List<RequestorChoice>();

            for (int i = 0; i < choiceList.Length; i++)
            {
                if (choiceList[i] != null)
                {
                    choiceList2.Add(choiceList[i]);
                }
            }

            return WorkFlowEngine.BLL.WorkFlow.NewRequest(systemID, docID, requestorID, owner, curSequenceID, conditionID, choiceList2);
        }

        [WebMethod]
        public RequestorChoice Construct(int systemID, int documentID, int mainSequenceID, int conditionID)
        {
            RequestorChoice rc = new RequestorChoice(systemID, documentID, mainSequenceID, conditionID);
            return rc;
        }

    }
}
