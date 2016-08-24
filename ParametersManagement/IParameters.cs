using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using CRA.ModelLayer;
using System.Collections.ObjectModel;
using CRA.ModelLayer.Core;

namespace CRA.ModelLayer.ParametersManagement
{

    /// <summary>
    /// Represents a parameter's class. A parameter class is a well-defined set of parameters plus the methods to read/write the parameters from/to a form of persitence (e.g. an XMl file or a DB).
    /// A parameter is a <see cref="CRA.ModelLayer.Core.VarInfo">VarInfo</see> object.
    /// The parameters belonging to the class can assume different values. Each set of values is called parameters set. The parameters set is identified by a specific value of the ParametersKey
    /// The code of a class realizing IParameters is typically generated via the code generator DCC (Domain Class Coder). The file name is automatically assigned by DCC according to 
    /// the following convention: FullClassName_parameters.xml 
    /// </summary>
    public interface IParameters : IDomainClass
    {
        /// <summary>
        /// The reader to read the values of the parameters from a form of persistence
        /// </summary>
        IValuesReader Reader { get; set; }

        /// <summary>
        /// The writer to write the values of the parameters to a form of persistence
        /// </summary>
        IValuesWriter Writer { get; set; }    

        /// <summary>
        /// Saves the parameters set identified by the specified parameters key to the form of persistence
        /// </summary>
        /// <param name="parametersKey"></param>
        /// <returns></returns>
        string SaveParameters(string parametersKey);

        /// <summary>
        /// Sets to the parameters properties the values taken from the specified list of <see cref="CRA.ModelLayer.Core.VarInfo">VarInfo</see> objects
        /// </summary>
        /// <param name="parametersToSet"></param>
        void SetParameters(IEnumerable<VarInfo> parametersToSet);

        /// <summary>
        /// Event triggered when the property of this parameters class is set
        /// </summary>
        event Action<string, object> ParameterClassPropertyValueSet;
    }


    /// <summary>
    /// Utility class containing extension method of the <see cref="IParameters">IParameters</see> interface.
    /// </summary>
    public static class ParametersExtension
    {
        private class SetPropertyVarInfo : VarInfo
        {
            private PropertyInfo _property;
            private object _target;

            internal SetPropertyVarInfo(object target, PropertyInfo property)
            {
                _property = property;
                _target = target;
            }

            public override event Action<VarInfo> CurrentValueSet;

            public override object CurrentValue
            {
                get
                {
                    return _property.GetGetMethod().Invoke(_target, new object[0]);
                }
                set
                {
                    _property.GetSetMethod().Invoke(_target, new object[1] { value });
                    if (CurrentValueSet != null) CurrentValueSet(this);
                }
            }
        }

        private static Dictionary<IParameters, KeyValuePair<IValuesReader, ReadOnlyCollection<VarInfo>>> _cachedVarInfoLists =
            new Dictionary<IParameters, KeyValuePair<IValuesReader, ReadOnlyCollection<VarInfo>>>();

        private static Dictionary<IParameters, string> _cachedParameterKeyValues = new Dictionary<IParameters, string>();


        /// <summary>
        /// Sets the value of parameters key of the parameters class.
        /// </summary>
        /// <param name="parameterClass"></param>
        /// <param name="parameterKey"></param>
        public static void SetParametersKey(this IParameters parameterClass, string parameterKey)
        {
            if (_cachedParameterKeyValues.ContainsKey(parameterClass)) _cachedParameterKeyValues.Remove(parameterClass);
            if (parameterKey == null) return;
            _cachedParameterKeyValues.Add(parameterClass, parameterKey);
        }

        /// <summary>
        /// Gets the value of parameters key of the parameters class.
        /// </summary>
        /// <param name="parameterClass"></param>
        /// <returns></returns>
        public static string GetParametersKey(this IParameters parameterClass)
        {
            if (_cachedParameterKeyValues.ContainsKey(parameterClass)) return _cachedParameterKeyValues[parameterClass];
            return null;
        }

        /// <summary>
        /// Loads the parameters from the form of persistence into the parameters class by using the currently selected <see cref="IValuesReader">IValuesReader</see> and the currently selected parameters key value.
        /// Reader and parameters key must be alredy set before invoking this method.
        /// </summary>
        /// <param name="parameterClass"></param>
        public static void LoadParameters(this IParameters parameterClass)
        {
            if (GetParametersKey(parameterClass) == null) throw new ArgumentNullException("Cannot load parameters if the Parameters Key is not selected.");
            string parametersKey = GetParametersKey(parameterClass);
            if (parameterClass.Reader == null) throw new ArgumentNullException("Cannot load parameters if the Reader is not configured.");
            parameterClass.Reader.LoadParameters(parametersKey, parameterClass);
        }


        /// <summary>
        /// Returns the list of VarInfo objects representing the parameters of the parameters class by using the  currently selected <see cref="IValuesReader">IValuesReader</see> .
        /// Reader must be alredy set before invoking this method.
        /// </summary>
        /// <param name="parameterClass"></param>
        /// <returns></returns>
        public static ReadOnlyCollection<VarInfo> GetParameters(this IParameters parameterClass)
        {
            KeyValuePair<IValuesReader, ReadOnlyCollection<VarInfo>> parametersKVP;
            bool found = _cachedVarInfoLists.TryGetValue(parameterClass, out parametersKVP);
            if (found && parameterClass.Reader != parametersKVP.Key)
            {
                _cachedVarInfoLists.Remove(parameterClass);
                found = false;
            }
            if (!found)
            {
                if (parameterClass.Reader == null) throw new ArgumentNullException("Cannot retrieve parameters if the Reader is not configured.");
                List<VarInfo> listVarInfo = new List<VarInfo>();

                IParametersSet parametersSet = parameterClass.Reader.ReadValues();
                foreach (VarInfo originalVarInfo in parametersSet.Values.First().Value.Select(p => p.Key))
                {
                    try
                    {
                        PropertyInfo property = parameterClass.PropertiesDescription[originalVarInfo.Name];
                        SetPropertyVarInfo wrappingVarInfo = new SetPropertyVarInfo(parameterClass, property)
                                                                 {
                                                                     DefaultValue = originalVarInfo.DefaultValue,
                                                                     Description = originalVarInfo.Description,
                                                                     Id = originalVarInfo.Id,
                                                                     MaxValue = originalVarInfo.MaxValue,
                                                                     MinValue = originalVarInfo.MinValue,
                                                                     Name = originalVarInfo.Name,
                                                                     Size = originalVarInfo.Size,
                                                                     Units = originalVarInfo.Units,
                                                                     URL = originalVarInfo.URL,
                                                                     ValueType = originalVarInfo.ValueType,
                                                                     VarType = originalVarInfo.VarType
                                                                 };
                        listVarInfo.Add(wrappingVarInfo);
                    }catch(KeyNotFoundException)
                    {
                        //suppress any error in case of looking for a parameter that is not defined in the parameters class
                        //TODO: must be managed better, for example launching a proper exception
                    }
                }

                parametersKVP = new KeyValuePair<IValuesReader, ReadOnlyCollection<VarInfo>>(parameterClass.Reader, new ReadOnlyCollection<VarInfo>(listVarInfo));
                _cachedVarInfoLists.Add(parameterClass, parametersKVP);
            }
            return parametersKVP.Value;
        }
    }
}
