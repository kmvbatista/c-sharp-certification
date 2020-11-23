using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace c__certifi
{
    class CustomCollection: IEquatable<CustomCollection>, IComparable
    {
        private Dictionary<string, int> list;
        public int this[string key]
        {
            get
            {
                return list[key];
            }
        }

        public int CompareTo(object obj)
        {
            throw new NotImplementedException();
        }

        public bool Equals([AllowNull] CustomCollection other)
        {
            throw new NotImplementedException();
        }
    }
}
