using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRA.ModelLayer.Cache
{
    internal class CompositeKey<TKey1, TKey2> :  IEquatable<CompositeKey<TKey1, TKey2>>
    {

        private TKey1 key1;
        private TKey2 key2;

        internal TKey1 Key1
        {
            get { return key1; }
        }

        internal TKey2 Key2
        {
            get { return key2; }
        }

        internal CompositeKey(TKey1 k1, TKey2 k2)
        {

            key1 = k1;
            key2 = k2;
        }


        public override bool Equals(object other)
        {

            return Equals(other as CompositeKey<TKey1, TKey2>);

        }

        public override int GetHashCode()
        {

            //return 0;
            if (key1 == null && key2 == null) return 0;
            if (key1 == null) return key2.GetHashCode();
            if (key2 == null) return key1.GetHashCode();
            return key1.GetHashCode()+key2.GetHashCode();
        }


        public bool Equals(CompositeKey<TKey1, TKey2> other)
        {
            if(other ==null) return false;
            if (key1 == null && key2 == null) return true;
            if (key1 == null && key2.Equals(other.key2)) return true;
            if (key2 == null && key1.Equals(other.key1)) return true;
            if (key1 != null && key2!=null && key1.Equals(other.key1) && key2.Equals(other.key2)) return true;
            return false;

        }

      

     
    }
}
