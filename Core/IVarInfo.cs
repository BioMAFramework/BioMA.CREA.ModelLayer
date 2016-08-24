/*! 
    \namespace CRA.ModelLayer.Core Core of the Bioma Model Layer
 * 
 * The CRA.ModelLayer.Core namespace contains the core of the Bioma Model Layer. In particular contains the definitions of domain classes (IDomainClass interface), variables (IVarInfo interface) and the logics to perform the pre/post conditions tests (Preconditions class).
    
*/
namespace CRA.ModelLayer.Core
{
    using System;

    /// <summary>
    /// The interface <c>IVarInfo</c> represents a variable (input/output or pararameter of a strategy).
    /// </summary>
    public interface IVarInfo
    {
        /// <summary>Default value</summary>
        double DefaultValue { get; set; }

        /// <summary>Variable description </summary>
        string Description { get; set; }

        /// <summary>Maximum value allowed (if applicable)</summary>
        double MaxValue { get; set; }

        /// <summary>Minimum value allowed (if applicable)</summary>
        double MinValue { get; set; }

        /// <summary>Variable name </summary>
        string Name { get; set; }

        /// <summary>Units of measure</summary>
        string Units { get; set; }

        /// <summary>Variable metadata URL</summary>
        string URL { get; set; }
    }
}

