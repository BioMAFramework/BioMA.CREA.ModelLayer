namespace CRA.ModelLayer.Strategy
{
    using CRA.ModelLayer.Core;
    using System;

    /// <summary>
    /// Describes a property of a <see cref="CRA.ModelLayer.Core.IDomainClass">domain class object</see> in terms of domain class type, property type, property <see cref="CRA.ModelLayer.Core.VarInfo">VarInfo</see>, property name.
    /// </summary>
    public class PropertyDescription
    {
        /// <summary>
        /// Creates an empty PropertyDescription (for serialization purposes)
        /// </summary>
        public PropertyDescription()
        {
        }

        /// <summary>
        /// Creates a PropertyDescription from the specified values
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="propertyType"></param>
        /// <param name="domainClassType"></param>
        /// <param name="propertyVarInfo"></param>
        public PropertyDescription(string propertyName, Type propertyType, Type domainClassType, VarInfo propertyVarInfo)
        {
            PropertyName = propertyName;
            PropertyType = propertyType;
            DomainClassType = domainClassType;
            PropertyVarInfo = propertyVarInfo;
        }

        /// <summary>
        /// Property name in the domain class
        /// </summary>
        public string PropertyName;

        /// <summary>
        /// Property type
        /// </summary>
        public Type PropertyType;

        /// <summary>
        /// Domain class type
        /// </summary>
        public Type DomainClassType;

        /// <summary>
        /// VarInfo of the property
        /// </summary>
        public VarInfo PropertyVarInfo;
    }
}

