namespace CRA.ModelLayer.Core
{
    using CRA.ModelLayer.MetadataTypes;
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    /// <summary>
    ///Interface implemented by domain classes. 
    ///From the software point of view, domain classes are the containers of the model's variables (input, outputs of the model's algorithms). Model's variables must be of type <see cref="VarInfo">VarInfo</see> and must be public properties of the domain classes.
    ///From the modelling point of view, domain classes represent the context in which the models are defined: being the context the ensemble of all the properties of the described environment (for example, on a model regarding soil, the context is made by all the soil properties used to describe the soil).
    ///The interface extends the <see cref="IAnnotatable">IAnnotatable</see> interface.
    ///Domain classes can be automatically generated using the DCC (Domain Class Coder) tool of the Bioma framework. See Bioma framework website for further info.
    ///Existing domain classes can be analyzed using the MCE (Model Component Explorer) tool of the Bioma framework. See Bioma framework website for further info.
    /// </summary>
    public interface IDomainClass : IAnnotatable
    {
        /// <summary>
        /// Clears the values of the properties of the domain class. Returns true if the method succeeds. This method is useful for re-initialize all the properties of a domain class altogether.
        /// This operation should be implemented by using the <see cref="CRA.ModelLayer.Core.IVarInfoConverter.GetConstructingString">CRA.ModelLayer.Core.IVarInfoConverter.GetConstructingString</see> method of the VarInfoValueType related to the type of the property (e.g. the string 'new double[3]' for a double array of size 3 or the string 'new DateTime()' for a Date).
        /// If the <see cref="CRA.ModelLayer.Core.IVarInfoConverter.GetConstructingString">CRA.ModelLayer.Core.IVarInfoConverter.GetConstructingString</see> method returns null, the default value for the type of the property should be used (e.g '0' for numbers, 'the empty string' for strings, etc.)
        /// </summary>       
        bool ClearValues();

        ///<summary>
        /// Returns the <see cref="PropertyInfo">reflection's PropertyInfo objects</see> of the public properties of the domain class. The keys of the dictionary are the properties names. 
        /// This method is useful to describe the content of a domain class, as a list of its properties.
        ///</summary>
        IDictionary<string, PropertyInfo> PropertiesDescription { get; }
    }
}

