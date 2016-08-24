using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using CRA.ModelLayer;
using CRA.ModelLayer.Core;

namespace CRA.ModelLayer.ParametersManagement
{
    /* 11/6/2012 - DFa - Dependency Injection - begin */
    /* 
     * It would be better to abstract here from the presence of a filesystem in writing this interface.
     * This would allow to use parameters readers/writers instead of relying on the filesystem.
     * The DEFAULT injected dependency could very well be the one using the filesystem (using XmlRW.cs).
     * Also, it is arguable the need of having this type as an interface to abstract the behavior described,
     * because, apart from the form of persistence, there would be no different ways (implementations of this interface)
     * to persist/load the parameters.
     */
    /* old - begin */
    ///// <summary>
    ///// IParameters must be implemented by parameter classes which implement persistence
    ///// of parametrs via Xml files.
    ///// The code of a class realizing IParameters is typically generated via the code generator
    ///// DCC (Domain Class Coder). The file name is automatically assigned by DCC according to 
    ///// the following convention: FullClassName_parameters.xml 
    ///// Xml files containing parameters can be edited by the application MPE (Model Parameter Editor),
    ///// which also uses a DCC Xml file as definition.
    ///// Xml
    ///// </summary>
    ///// <author>
    ///// Marcello Donatelli, January 2011
    ///// </author>
    //public interface IParameters
    //{
    //    /// <summary>
    //    /// Load a recod of parameters from the Xml file using the key provided, and set value of the properties.
    //    /// All properties are set.
    //    /// A parameter class allows instantiating objects which have a collection of properties corresponding
    //    /// to parameters of one or more IStrategy classes. The definition of the parameters (their VarInfo)
    //    /// is in the IStrategy class.
    //    /// </summary>
    //    /// <param name="xmlPath">Path to Xml file parameters</param>
    //    /// <param name="parametersKey">ID to retrieve/save a parameter record</param>
    //    void LoadParameters(string xmlPath, string parametersKey);

    //    /// <summary>
    //    /// Set paramater values at run time. It might be a sub-set of parameters.
    //    /// </summary>
    //    /// <param name="parametersVarInfo">Collection of VarInfo instances in which at least 
    //    /// the property Name and CurrentValue have a value</param>
    //    void SetParameters(IEnumerable<VarInfo> parametersVarInfo);

    //    /// <summary>
    //    /// Save the current value of class properties to overwrite the record of parameters identified by the ID.
    //    /// In most cases, it will be used after calling the SetParameters method.
    //    /// </summary>
    //    /// <param name="xmlPath">Path to Xml file parameters</param>
    //    /// <param name="parametersKey">ID to retrieve/save a parameter record</param>
    //    void SaveParameters(string xmlPath, string parametersKey);
    //}
    /* old - end */
    
  
  /// <summary>
  /// Utility class to manage the parameters of a <see cref="CRA.ModelLayer.Core.IDomainClass">domain class</see>. The domain class is specified in the constructor.
  /// </summary>
    public class ParametersIO
    {
        #region Private Members

        private IValuesReader _reader;
        private IValuesWriter _writer;
        private IParametersSet _parametersSet;
        private Dictionary<string, PropertyInfo> _cachedProperties;
        private IDomainClass _containingObject;

        private Dictionary<string, PropertyInfo> GetProperties(Type[] excludedTypes)
        {
            var inheritedPropertiesNames = excludedTypes.Aggregate(new List<string>().AsEnumerable(), (tot, t) => tot.Union(GetPropertiesHierarchically(t))).ToList();
            return _containingObject.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => !inheritedPropertiesNames.Contains(p.Name))
                .ToDictionary(p => p.Name, p => p);
        }

        private List<string> GetPropertiesHierarchically(Type t)
        {
            return t.GetProperties()
                .Select(p => p.Name)
                .Concat(t.GetInterfaces()
                                .Aggregate(new List<string>().AsEnumerable(),
                                                    (tot, el) => tot.Concat(GetPropertiesHierarchically(el))))
                .ToList();
        }

        private object InvokeGetFor(PropertyInfo property, object obj)
        {
            return property.GetGetMethod().Invoke(obj, new object[0]);
        }

        private void InvokeSetFor(PropertyInfo property, object obj, object value)
        {
            property.GetSetMethod().Invoke(obj, new object[] { value });
        }

        private object CloneMember(object memberValue)
        {
            Type memberType = memberValue.GetType();
            VarInfoValueTypes valueType = VarInfoValueTypes.Values.Where(vt => vt.TypeForCurrentValue == memberType /*&& (vt != VarInfoValueTypes.DoubleMatrix)*/).SingleOrDefault();
            if (valueType != null)
            {
                try
                {
                    return valueType.Converter.GetClonedCopy(memberValue);
                }
                catch (NotImplementedException )
                {
                    // ignore it!
                }
            }
            object cloned = CloneHelper.DeepClone(memberValue);
            if (cloned != memberValue) return cloned;
            if (cloned is ICloneable) return ((ICloneable)cloned).Clone();
            return memberValue;
        }

        #endregion Private Members

        #region Constructor

        /// <summary>
        /// Creates an instance of this class from the instance of the associated domain class
        /// </summary>
        /// <param name="containingObject"></param>
        public ParametersIO(IDomainClass containingObject)
        {
            if (containingObject == null) throw new ArgumentNullException("containingObject");
            _containingObject = containingObject;
        }

        #endregion Constructor

        /// <summary>
        /// Returns the cached domain class properties
        /// </summary>
        /// <param name="excludedTypes"></param>
        /// <returns></returns>
        public IDictionary<string, PropertyInfo> GetCachedProperties(params Type[] excludedTypes)
        {
            if (_cachedProperties == null)
            {
                _cachedProperties = GetProperties(excludedTypes);
            }
            return new Dictionary<string, PropertyInfo>(_cachedProperties);
        }

        /// <summary>
        /// Gets/sets the domain class properties <see cref="IValuesReader">reader</see>. Used only for <see cref="CRA.ModelLayer.ParametersManagement.IParameters">parameter classes</see>
        /// </summary>
        public IValuesReader Reader
        {
            get { return _reader; }
            set { _reader = value; }
        }

        /// <summary>
        /// Gets/sets the domain class properties <see cref="IValuesWriter">writer</see>.Used only for <see cref="CRA.ModelLayer.ParametersManagement.IParameters">parameter classes</see>
        /// </summary>
        public IValuesWriter Writer
        {
            get { return _writer; }
            set { _writer = value; }
        }

        /// <summary>
        /// Load a record of parameters from the configured <see cref="IValuesReader">reader</see> using the key provided, and set value of the properties.
        /// All properties are set.        
        /// </summary>
        /// <param name="parametersKey">Parameters key of the record to load</param>
        public void LoadParameters(string parametersKey/*, params  KeyValuePair<string, string>[] varInfoNamesToIgnore*/)
        {
            if (_containingObject == null) throw new Exception("The ContainingObject is not configured");
            if (_reader == null) throw new Exception("The reader is not configured");
            _parametersSet = _reader.ReadValues();
            var varInfoValues = _parametersSet.Values.Where(kv => kv.Key.Name.Equals(parametersKey)).SingleOrDefault();
            if (varInfoValues.Key == null) throw new Exception("The parameters key '" + parametersKey + "' is not present.");
            if (varInfoValues.Value == null) throw new Exception("Values set for the parameters key '" + parametersKey + "' is null.");
            foreach (var keyAndValue in varInfoValues.Value)
            {
                var varInfo = keyAndValue.Key;

                //if (!varInfoNamesToIgnore.Contains(new KeyValuePair<string, string>(parametersKey, varInfo.Name)))
                //{

                    /*dfuma 2/oct/2012 - added check of the presence of the parameter in the properties description parameters list - new START */
                    PropertyInfo propertyInfo;
                    if (!_containingObject.PropertiesDescription.TryGetValue(varInfo.Name, out propertyInfo))
                    {
                        //suppress any error in case of looking for a parameter that is not defined in the parameters class                                             
                        continue;
                        //throw new Exception("Property '" + varInfo.Name + " not found in the properties list of class " + _containingObject.GetType());
                    }
                    /*dfuma 2/oct/2012 - new END*/

                    /*dfuma 2/oct/2012 - added check of the presence of the parameter in the properties description parameters list - old START
                    var propertyInfo = _containingObject.PropertiesDescription[varInfo.Name];
                     *dfuma 2/oct/2012 - old END*/

                    if (propertyInfo.PropertyType != varInfo.ValueType.TypeForCurrentValue)
                        throw new Exception("Type for property '" + propertyInfo.Name + "' (" + propertyInfo.PropertyType + ") is not coherent with the corresponding " +
                            "VarInfo type (" + varInfo.ValueType.TypeForCurrentValue + ")");
                    var setMethod = propertyInfo.GetSetMethod();
                    try
                    {
                        setMethod.Invoke(_containingObject,
                                         new object[]
                                             {varInfo.ValueType.Converter.GetParsedValueFromMPE(keyAndValue.Value)});
                    }catch(Exception e)
                    {
                        throw new ArgumentException("Error reading VarInfo '" + keyAndValue.Key.Name + "' (parameters key '" + parametersKey + "'):" + e.Message, e);
                    }
                //}
            }
        }

        /// <summary>
        /// Save the current value of class properties to overwrite the record of variables identified by the parameters key.
        /// It uses the configured <see cref="IValuesWriter">writer</see>.        
        /// </summary>
        /// <param name="parametersKey">Parameters key of the record to save</param>
        public string SaveParameters(string parametersKey)
        {
            string returnMessage = null;
            if (_containingObject == null) throw new Exception("The ContainingObject is not configured");
            //if (_parametersSet == null) throw new Exception("The parameters set is not defined: invoke 'LoadParameters()' first.");
            if (_writer == null) throw new Exception("The writer is not configured");
            if (_reader == null) throw new Exception("The reader is not configured");
            _parametersSet = _reader.ReadValues();
            IKeyValue keyValue = _parametersSet.Values.Keys.Where(k => k.Name.Equals(parametersKey)).SingleOrDefault();
            if (keyValue == null)
            {
                int maxId = 0;
                if (_parametersSet.Values.Count > 0)
                {
                    maxId = _parametersSet.Values.Keys.Select(k => k.Id).Max();
                }
                keyValue = DataTypeFactory.NewKeyValue(maxId + 1, parametersKey, parametersKey);
                _parametersSet.Values.Add(keyValue, new Dictionary<VarInfo, List<string>>());
                foreach (var varInfo in _parametersSet.Parameters)
                    _parametersSet.Values[keyValue].Add(varInfo, null);
            }
            else
            {
                returnMessage = "Overwriting key value '" + parametersKey + "'.";
            }
            foreach (var varInfo in _parametersSet.Parameters)
            {
                PropertyInfo propertyInfo = _containingObject.PropertiesDescription[varInfo.Name];
                if (propertyInfo.PropertyType != varInfo.ValueType.TypeForCurrentValue)
                    throw new Exception("Type for property '" + propertyInfo.Name + "' (" + propertyInfo.PropertyType + ") is not coherent with the corresponding " +
                        "VarInfo type (" + varInfo.ValueType.TypeForCurrentValue + ")");
                MethodInfo getMethod = propertyInfo.GetGetMethod();
                object propertyValue = getMethod.Invoke(_containingObject, new object[0]);
                List<string> serializedValue = varInfo.ValueType.Converter.ParseValueForMPE(propertyValue);
                _parametersSet.Values[keyValue][varInfo] = serializedValue;
            }
            _writer.WriteValues(_parametersSet);
            return returnMessage;
        }

        /// <summary>
        /// Set the variables values in the associated domain class. It might be a sub-set of parameters.
        /// </summary>
        /// <param name="parametersVarInfo">Collection of VarInfo instances in which at least  the property Name and CurrentValue have a non-<c>null</c> value.</param>
        public void SetParameters(IEnumerable<VarInfo> parametersVarInfo)
        {
            if (_containingObject == null) throw new Exception("The ContainingObject is not configured");
            if (_parametersSet == null) throw new Exception("The parameters set is not defined: invoke 'LoadParameters()' first.");
            foreach (var varInfo in parametersVarInfo)
            {
                if (varInfo.Name == null || varInfo.Name.Trim().Equals("")) throw new Exception("A VarInfo used to set values must have a Name.");
                if (varInfo.CurrentValue == null) throw new Exception("A VarInfo used to set values must have a CurrentValue.");
                var propertyInfo = _containingObject.PropertiesDescription[varInfo.Name];
                if (propertyInfo == null) throw new Exception("The Containing Object does not have a property named '" + varInfo.Name + "'.");
                var setMethod = propertyInfo.GetSetMethod();
                setMethod.Invoke(_containingObject, new object[] { varInfo.CurrentValue });
            }
        }

        /// <summary>
        /// Populate a cloned copy of the associated domain class
        /// </summary>
        /// <param name="clonedCopy"></param>
        public void PopulateClonedCopy(IDomainClass clonedCopy)
        {
            foreach (var propertyPair in
                this._containingObject.PropertiesDescription
                .Where(p => InvokeGetFor(p.Value, _containingObject) != null))
            {
                InvokeSetFor(clonedCopy.PropertiesDescription[propertyPair.Key],
                                      clonedCopy,
                                      CloneMember(InvokeGetFor(propertyPair.Value, _containingObject)));
            }
        }
    }
    /* 11/6/2012 - DFa - Dependency Injection - end */
}