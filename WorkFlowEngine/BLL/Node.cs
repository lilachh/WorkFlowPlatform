using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace WorkFlowEngine.BLL
{
    public class Node1
    {
        #region Fields

        private int index;
        private int rowID;
        private int columnID;
        private int fatherID;
        private int x;
        private int y;
        private Sequence sequ;
        private bool spread = false;
       

        #endregion

        #region Properties

        public int Index
        {
            get
            {
                return index;
            }

            set
            {
                index = value;
            }
        }


        public int X
        {
            get
            {
                return x;
            }

            set
            {
                x = value;
            }
        }

        public int Y
        {
            get
            {
                return y;
            }

            set
            {
                y = value;
            }
        }

        public int RowID
        {
            get
            {
                return rowID;
            }

            set
            {
                rowID = value;
            }
        }

        public int ColumnID
        {
            get
            {
                return columnID;
            }

            set
            {
                columnID = value;
            }
        }

        public int FatherID
        {
            get
            {
                return fatherID;
            }
            set
            {
                fatherID = value;
            }
        }

        public Sequence Sequ
        {
            get
            {
                return sequ;
            }
            set
            {
                sequ = value;
            }
        }

        public bool Spread
        {
            get
            {
                return spread;
            }

            set
            {
                spread = value;
            }
        }
        #endregion


        #region Constructors



        public Node1(int rowID, int index,int columnID,int fatherID,Sequence sequence)
        { 
            //set other fields values
            this.Index = index;
            this.RowID=rowID;
            this.ColumnID=columnID;
            this.fatherID=fatherID;
            this.Sequ=sequence;
        }

        public Node1()
        {
        }
        #endregion


        public class SortIndexAscendingHelper : IComparer
        {
            int IComparer.Compare(object a, object b)
            {
                Node1 c1 = (Node1)a;
                Node1 c2 = (Node1)b;
                if (c1.Index > c2.Index)
                    return 1;
                if (c1.Index < c2.Index)
                    return -1;
                else
                    return 0;
            }

        }


        // Method to return IComparer object for sort helper.
        public static IComparer SortIndexAscending()
        {
            return (IComparer)new SortIndexAscendingHelper();
        }

    }
}
