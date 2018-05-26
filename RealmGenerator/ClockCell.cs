using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RealmGenerator
{
    public class ClockCell : ICloneable
    {
        public int index;
        public int value;
        public string stringValue;
        public List<int> candidates;
        public bool isFixed;

        public ClockCell(int _index, int _value, List<int> _candidates, bool _isFixed)
        {
            this.candidates = new List<int>();
            this.index = _index;
            this.value = _value;
            this.stringValue = value <= 9 ? value.ToString() : value == 10 ? "A" : value == 11 ? "B" : "C";
            foreach (int i in _candidates)
            {
                this.candidates.Add(i);
            }
            this.isFixed = _isFixed;
        }

        public object Clone()
        {
            var clone = new ClockCell(this.index, this.value, this.candidates, this.isFixed);
            return clone;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(this.stringValue);
            sb.Append("[");
            if (!this.isFixed)
            {
                if (this.candidates.Contains(0))
                {
                    for (int j = 1; j <= 12; j++)
                    {
                        sb.Append('$');
                    }
                }
                else
                {
                    for (int j = 1; j <= 12; j++)
                    {
                        if (this.candidates.Contains(j))
                            sb.Append(j <= 9 ? j.ToString() : j == 10 ? "A" : j == 11 ? "B" : "C");
                        else
                            sb.Append('.');
                    }
                }
            }
            else
            {
                for (int j = 1; j <= 12; j++)
                {
                    sb.Append('x');
                }
            }
            sb.Append("]");
            return sb.ToString();
        }
        public bool isEqualInCandidates(ClockCell cell)
        {
            return this.candidates.SequenceEqual(cell.candidates);
        }
    }
}
