namespace CRA.ModelLayer.Core
{
    using System;

    /// <summary>
    /// Interface implemented by VarInfo classes. 
    /// For each domain class there should be a VarInfoClass: a matching class, which is conventionally called as the value class plus the postfix VarInfo, that contains the description of the variables of the domain class.
    /// The corresponding domain class is referenced here as the 'domain class of reference'
    /// </summary>
    public interface IVarInfoClass
    {
        /// <summary>
        /// It should return the same description of the domain class of reference
        /// </summary>
        string Description { get; }

        /// <summary>
        /// The name of the domain class of reference
        /// </summary>
        string DomainClassOfReference { get; }

        /// <summary>
        /// It should return the URL of the domain class of reference
        /// </summary>
        string URL { get; }
    }
}

