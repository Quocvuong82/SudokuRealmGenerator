using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace RealmGenerator
{
    public sealed class SamuraiRelation : IEquatable<SamuraiRelation>, IEnumerable<int>
    {
        private readonly int[] data;
        public int this[int index]
        {
            get { return data[index]; }
        }
        public SamuraiRelation(params int[] data)
        {
            if (data == null) throw new ArgumentNullException("data");
            this.data = (int[])data.Clone();
        }
        private int? hash;
        public override int GetHashCode()
        {
            if (hash == null)
            {
                int result = 13;
                for (int i = 0; i < data.Length; i++)
                {
                    result = (result * 7) + data[i];
                }
                hash = result;
            }
            return hash.GetValueOrDefault();
        }
        public int Length { get { return data.Length; } }
        public IEnumerator<int> GetEnumerator()
        {
            for (int i = 0; i < data.Length; i++)
            {
                yield return data[i];
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        public override bool Equals(object obj)
        {
            return this == (obj as SamuraiRelation);
        }
        public bool Equals(SamuraiRelation obj)
        {
            return this == obj;
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder("[");
            if (data.Length > 0) sb.Append(data[0]);
            for (int i = 1; i < data.Length; i++)
            {
                sb.Append(',').Append(data[i]);
            }
            sb.Append(']');
            return sb.ToString();
        }
        public static bool operator ==(SamuraiRelation x, SamuraiRelation y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (ReferenceEquals(x, null) || ReferenceEquals(y, null)) return false;
            if (x.hash.HasValue && y.hash.HasValue && // exploit known different hash
                x.hash.GetValueOrDefault() != y.hash.GetValueOrDefault()) return false;
            int[] xdata = x.data, ydata = y.data;
            if (xdata.Length != ydata.Length) return false;
            for (int i = 0; i < xdata.Length; i++)
            {
                if (xdata[i] != ydata[i]) return false;
            }
            return true;
        }
        public static bool operator !=(SamuraiRelation x, SamuraiRelation y)
        {
            return !(x == y);
        }
        public static SamuraiRelation operator +(SamuraiRelation x, SamuraiRelation y)
        {
            if (x == null || y == null) throw new ArgumentNullException();
            int[] xdata = x.data, ydata = y.data;
            if (xdata.Length != ydata.Length) throw new InvalidOperationException("Length mismatch");
            int[] result = new int[xdata.Length];
            for (int i = 0; i < xdata.Length; i++)
            {
                result[i] = xdata[i] + ydata[i];
            }
            return new SamuraiRelation(result);
        }
    }
}
