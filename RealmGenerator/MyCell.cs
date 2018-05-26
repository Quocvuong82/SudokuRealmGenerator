using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RealmGenerator
{
    public class MyCell : ICloneable
    {
        public int index;
        public int value;
        public List<int> candidates;
        public bool isFixed;

        public MyCell(int _index, int _value, List<int> _candidates,bool _isFixed)
        {
            this.candidates = new List<int>();
            this.index = _index;
            this.value = _value;
            foreach (int i in _candidates)
            {
                this.candidates.Add(i);
            }
            this.isFixed = _isFixed;
        }

        public object Clone()
        {
            var clone = new MyCell(this.index,this.value,this.candidates,this.isFixed);
            return clone;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(this.value.ToString());
            sb.Append("[");
            if (!this.isFixed)
            {
                if (this.candidates.Contains(0))
                {
                    for (int j = 1; j <= 9; j++)
                    {
                        sb.Append('$');
                    }
                }
                else
                {
                    for (int j = 1; j <= 9; j++)
                    {
                        if (this.candidates.Contains(j))
                            sb.Append(j);
                        else
                            sb.Append('.');
                    }
                }
            }
            else
            {
                for (int j = 1; j <= 9; j++)
                {
                    sb.Append('x');
                }
            }
            sb.Append("]");
            return sb.ToString();
        }
        public bool isEqualInCandidates(MyCell cell)
        {
            return this.candidates.SequenceEqual(cell.candidates);
        }
    }
}
