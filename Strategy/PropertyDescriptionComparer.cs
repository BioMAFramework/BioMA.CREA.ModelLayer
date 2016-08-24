namespace CRA.ModelLayer.Strategy
{
    using System;
    using System.Collections.Generic;
    /// <summary>
    /// Comparer to compare to instances of PropertyDescription. 
    /// </summary>
    internal class PropertyDescriptionComparer : IEqualityComparer<PropertyDescription>
    {
        /// <summary>
        /// returns true if the 2 property names and the 2 domain class types are equal
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool Equals(PropertyDescription x, PropertyDescription y)
        {
            if ((x == null) && (y != null))
            {
                return false;
            }
            if ((x != null) && (y == null))
            {
                return false;
            }
            return (((x == null) && (y == null)) || (x.PropertyName.Equals(y.PropertyName) && x.DomainClassType.Equals(y.DomainClassType)));
        }

        /// <summary>
        /// always returns 0
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int GetHashCode(PropertyDescription obj)
        {
            return 0;
        }
    }
}

