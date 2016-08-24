namespace CRA.ModelLayer.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    /// <summary>
    /// Utility methods to manage lists of <see cref="VarInfo">VarInfo</see> objects.
    /// </summary>
    public static class VarInfoEnumerableExtensionMethods
    {
        /// <summary>
        /// Returns a VarInfo from an IEnumerable of VarInfo objects given its name. If the IEnumerable&lt;VarInfo&gt; contains many parameters with the specified name, only the first one will be returned.
        /// If the IEnumerable&lt;VarInfo&gt; contains no parameter with the specified name, null will be returned
        /// </summary>
        /// <param name="parameters">The IEnumerable of VarInfo</param>
        /// <param name="varInfoName">The name of the VarInfo to return</param>
        /// <returns></returns>
        public static VarInfo GetParameterByName(this IEnumerable<VarInfo> parameters, string varInfoName)
        {
            if (string.IsNullOrEmpty(varInfoName))
            {
                throw new Exception("Requested parameter with an empty name.");
            }
            return (from a in parameters
                where varInfoName.Equals(a.Name)
                select a).FirstOrDefault<VarInfo>();
        }

        /// <summary>
        /// Returns a VarInfo current value given its name from an IEnumerable of VarInfo objects. If the IEnumerable&lt;VarInfo&gt; contains many parameters with the specified name, only the value of the first one will be returned.
        /// If the IEnumerable&lt;VarInfo&gt; contains no parameter with the specified name, an exception is thrown
        /// </summary>
        /// <param name="parameters">The IEnumerable of VarInfo</param>
        /// <param name="varInfoName">The name of the VarInfo to return</param>
        /// <returns></returns>
        public static object GetParameterCurrentValueByName(this IEnumerable<VarInfo> parameters, string varInfoName)
        {
            if (string.IsNullOrEmpty(varInfoName))
            {
                throw new Exception("Requested parameter with an empty name.");
            }
            VarInfo parameterByName = parameters.GetParameterByName(varInfoName);
            if (parameterByName == null)
            {
                throw new Exception("No parameter with name '" + varInfoName + "' found in the collection of parameters.");
            }
            return parameterByName.CurrentValue;
        }
    }
}

