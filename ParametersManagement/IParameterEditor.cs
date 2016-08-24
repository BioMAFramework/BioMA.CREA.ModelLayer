using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRA.ModelLayer.ParametersManagement
{
    /// <summary>
    /// Interface that a generic parameter editor must implement. The editor should be able to manage the <see cref="IValuesReader">IValuesReader</see> and <see cref="IValuesWriter">IValuesWriter</see> instances passed as parameters to read and write parameters from/to a persistence source (e.g. an XML file or a DB)
    /// </summary>
    public interface IParameterEditor
    {        
        /// <summary>
        /// Launch the parameter editor
        /// </summary>
        /// <param name="objArg">A generic object argument</param>
        /// <param name="reader">An instance of <see cref="IValuesReader">IValuesReader</see> to use in the parameter editor </param>
        /// <param name="writer">An instance of <see cref="IValuesWriter">IValuesWriter</see> to use in the parameter editor </param>
        /// <param name="parametersKey">The current value of the parameter key to be viewed/modified in the parameter editor</param>
        /// <returns>The parameter editor output</returns>
        object Launch(object objArg, IValuesReader reader, IValuesWriter writer, string parametersKey);
    }
}
