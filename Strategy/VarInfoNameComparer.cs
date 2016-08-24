namespace CRA.ModelLayer.Strategy
{
    using CRA.ModelLayer.Core;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Comparer to compare to instances of VarInfo. 
    /// </summary>
    internal class VarInfoNameComparer : IEqualityComparer<VarInfo>
    {
        /// <summary>
        /// returns true if the 2 VarInfo names are equal
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool Equals(VarInfo x, VarInfo y)
        {
            if ((x == null) && (y != null))
            {
                return false;
            }
            if ((x != null) && (y == null))
            {
                return false;
            }
            if ((x == null) && (y == null))
            {
                return true;
            }
            if ((x.Name != null) && (y.Name == null))
            {
                return false;
            }
            if ((x.Name == null) && (y.Name != null))
            {
                return false;
            }
            return (((x.Name == null) && (y.Name == null)) || x.Name.Equals(y.Name));
        }

        /// <summary>
        /// always returns 0
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int GetHashCode(VarInfo obj)
        {
            return 0;
        }
    }
}

