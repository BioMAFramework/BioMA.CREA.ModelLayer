namespace CRA.ModelLayer.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Xml.Linq;

    /// <summary>
    /// <a href="http://msmvps.com/blogs/jon_skeet/archive/2006/01/05/classenum.aspx">Typesafe Enum</a> having an instance for each
    /// parsed type that a <see cref="VarInfo">VarInfo</see> can take. It provides parsing functionalities for converting from string representations
    /// of <see cref="VarInfo.CurrentValue">VarInfo.CurrentValue</see> to parsed values and back.<br/>
    /// The constructor is private and the only available instances of this class can be obtained from public static properties (a variation of the
    /// <a href="http://en.wikipedia.org/wiki/Singleton_pattern">Singleton pattern</a>, where instead of an instance there is a set of instances).<br/>
    /// Each instance of <see cref="VarInfo">VarInfo</see> is configured with an object of this type by setting its   /// <see cref="VarInfo.ValueType">ValueType</see> property to an instance of this class.
    /// </summary>
    /// <remarks>
    /// <see cref="VarInfo">VarInfo</see>'s <see cref="VarInfo.CurrentValue">CurrentValue</see> types are implemented this way to have a single point of maintenance (this class) for parsing functionalities and to avoid cumbersome <c>switch</c> statements in applications dealing with <see cref="VarInfo">VarInfo</see>.
    /// </remarks>
    public class VarInfoValueTypes
    {
        // A T T E N T I O N !!!
        // if a new instance of this class is added as a public static property,
        // e.g. because a new VarInfo type needs to be managed, add it also to the returned values
        // of the property "Values".

        private IVarInfoConverter _converter;
        private static readonly Dictionary<string, VarInfoValueTypes> _instancesForType;
        private string _label;
        private string _name;
        private int _ordinal;
        private string _parsingPostfix;
        private string _parsingPrefix;
        private System.Type _type;
        private static readonly List<VarInfoValueTypes> _values = new List<VarInfoValueTypes>();
        private const string E_VALS_E_KV_E_KEY_SEPARATOR = "$";

        // A T T E N T I O N !!!
        // if a new instance of this class is added as a public static property,
        // e.g. because a new VarInfo type needs to be managed, add it also to the returned values
        // of the property "Values".

        /// <summary>
        /// Represents a <see cref="double">double</see> type.
        /// </summary>
        /// <remarks>
        /// The MPE's peculiar XML representation for this type is of the type (assume a <see cref="VarInfo.Name">
        /// VarInfo.Name</see> = "VarInfo Name"):
        /// <code>
        /// <Parameter name="VarInfo Name">
        ///      <Value>1.0</Value>
        /// </Parameter>
        /// </code>
        /// The MPE's peculiar List&lt;string&gt; representation for this type is a List containing one element,
        /// the string representation of the double.
        /// </remarks>
        internal static readonly VarInfoValueTypes Double = new VarInfoValueTypes("Double", 0, "double", "double", "", new VarInfoDoubleConverter(), typeof(double));

        /// <summary>
        /// Represents an array of <see cref="double">double</see> type.
        /// </summary>
        /// <remarks>
        /// The MPE's peculiar XML representation for this type is of the type (assume a <see cref="VarInfo.Name">
        /// VarInfo.Name</see> = "VarInfo Name"):
        /// <code>
        /// <Parameter name="VarInfo Name">
        ///      <Value>1.0</Value>
        ///      <Value>2.0</Value>
        ///      <Value>3.0</Value>
        /// </Parameter>
        /// </code>
        /// The MPE's peculiar List&lt;string&gt; representation for this type is a List containing several elements,
        /// each one being the string representation of a double.
        /// </remarks>
        internal static readonly VarInfoValueTypes ArrayDouble = new VarInfoValueTypes("ArrayDouble", 1, "double[]", "double[", "]", new VarInfoArrayDoubleConverter(), typeof(double[]));

        /// <summary>
        /// Represents a <see cref="List{T}">List&lt;double&gt;</see> type.
        /// </summary>
        /// <remarks>
        /// The MPE's peculiar XML representation for this type is of the type (assume a <see cref="VarInfo.Name">
        /// VarInfo.Name</see> = "VarInfo Name"):
        /// <code>
        /// <Parameter name="VarInfo Name">
        ///      <Value>1.0</Value>
        ///      <Value>2.0</Value>
        ///      <Value>3.0</Value>
        /// </Parameter>
        /// </code>
        /// The MPE's peculiar List&lt;string&gt; representation for this type is a List containing several elements,
        /// each one being the string representation of a double.
        /// </remarks>
        internal static readonly VarInfoValueTypes ListDouble = new VarInfoValueTypes("ListDouble", 2, "List<double>", "List<double>", "", new VarInfoListDoubleConverter(), typeof(List<double>));

        /// <summary>
        /// Represents an <see cref="int">int</see> type.
        /// </summary>
        /// <remarks>
        /// The MPE's peculiar XML representation for this type is of the type (assume a <see cref="VarInfo.Name">
        /// VarInfo.Name</see> = "VarInfo Name"):
        /// <code>
        /// <Parameter name="VarInfo Name">
        ///      <Value>1</Value>
        /// </Parameter>
        /// </code>
        /// The MPE's peculiar List&lt;string&gt; representation for this type is a List containing one element,
        /// the string representation of the int.
        /// </remarks>
        internal static readonly VarInfoValueTypes Integer = new VarInfoValueTypes("Integer", 3, "int", "int", "", new VarInfoIntConverter(), typeof(int));

        /// <summary>
        /// Represents an array of <see cref="int">int</see> type.
        /// </summary>
        /// <remarks>
        /// The MPE's peculiar XML representation for this type is of the type (assume a <see cref="VarInfo.Name">
        /// VarInfo.Name</see> = "VarInfo Name"):
        /// <code>
        /// <Parameter name="VarInfo Name">
        ///      <Value>1</Value>
        ///      <Value>2</Value>
        ///      <Value>3</Value>
        /// </Parameter>
        /// </code>
        /// The MPE's peculiar List&lt;string&gt; representation for this type is a List containing several elements,
        /// each one being the string representation of a int.
        /// </remarks>
        internal static readonly VarInfoValueTypes ArrayInteger = new VarInfoValueTypes("ArrayInteger", 4, "int[]", "int[", "]", new VarInfoArrayIntConverter(), typeof(int[]));

        /// <summary>
        /// Represents a <see cref="List{T}">List&lt;int&gt;</see> type.
        /// </summary>
        /// <remarks>
        /// The MPE's peculiar XML representation for this type is of the type (assume a <see cref="VarInfo.Name">
        /// VarInfo.Name</see> = "VarInfo Name"):
        /// <code>
        /// <Parameter name="VarInfo Name">
        ///      <Value>1</Value>
        ///      <Value>2</Value>
        ///      <Value>3</Value>
        /// </Parameter>
        /// </code>
        /// The MPE's peculiar List&lt;string&gt; representation for this type is a List containing several elements,
        /// each one being the string representation of a int.
        /// </remarks>
        internal static readonly VarInfoValueTypes ListInteger = new VarInfoValueTypes("ListInteger", 5, "List<int>", "List<int>", "", new VarInfoListIntConverter(), typeof(List<int>));

        /// <summary>
        /// Represents a <see cref="DateTime">DateTime</see> type.
        /// </summary>
        /// <remarks>
        /// The MPE's peculiar XML representation for this type is of the type (assume a <see cref="VarInfo.Name">
        /// VarInfo.Name</see> = "VarInfo Name"):
        /// <code>
        /// <Parameter name="VarInfo Name">
        ///      <Value>1/1/1970</Value>
        /// </Parameter>
        /// </code>
        /// The MPE's peculiar List&lt;string&gt; representation for this type is a List containing one element,
        /// the string representation of the DateTime.
        /// </remarks>
        internal static readonly VarInfoValueTypes Date = new VarInfoValueTypes("Date", 6, "Date", "Date", "", new VarInfoDateConverter(), typeof(DateTime));

        /// <summary>
        /// Represents an array of <see cref="DateTime">DateTime</see> type.
        /// </summary>
        /// <remarks>
        /// The MPE's peculiar XML representation for this type is of the type (assume a <see cref="VarInfo.Name">
        /// VarInfo.Name</see> = "VarInfo Name"):
        /// <code>
        /// <Parameter name="VarInfo Name">
        ///      <Value>1/1/1970</Value>
        ///      <Value>1/1/1971</Value>
        ///      <Value>1/1/1972</Value>
        /// </Parameter>
        /// </code>
        /// The MPE's peculiar List&lt;string&gt; representation for this type is a List containing several elements,
        /// each one being the string representation of a DateTime.
        /// </remarks>
        internal static readonly VarInfoValueTypes ArrayDate = new VarInfoValueTypes("ArrayDate", 7, "Date[]", "Date[", "]", new VarInfoArrayDateConverter(), typeof(DateTime[]));

        /// <summary>
        /// Represents a <see cref="List{T}">List&lt;DateTime&gt;</see> type.
        /// </summary>
        /// <remarks>
        /// The MPE's peculiar XML representation for this type is of the type (assume a <see cref="VarInfo.Name">
        /// VarInfo.Name</see> = "VarInfo Name"):
        /// <code>
        /// <Parameter name="VarInfo Name">
        ///      <Value>1/1/1970</Value>
        ///      <Value>1/1/1971</Value>
        ///      <Value>1/1/1972</Value>
        /// </Parameter>
        /// </code>
        /// The MPE's peculiar List&lt;string&gt; representation for this type is a List containing several elements,
        /// each one being the string representation of a DateTime.
        /// </remarks>
        internal static readonly VarInfoValueTypes ListDate = new VarInfoValueTypes("ListDate", 8, "List<Date>", "List<Date>", "", new VarInfoListDateConverter(), typeof(List<DateTime>));

        /// <summary>
        /// Represents a <see cref="string">string</see> type.
        /// </summary>
        /// <remarks>
        /// The MPE's peculiar XML representation for this type is of the type (assume a <see cref="VarInfo.Name">
        /// VarInfo.Name</see> = "VarInfo Name"):
        /// <code>
        /// <Parameter name="VarInfo Name">
        ///      <Value>String1</Value>
        /// </Parameter>
        /// </code>
        /// The MPE's peculiar List&lt;string&gt; representation for this type is a List containing one element,
        /// the string.
        /// </remarks>
        internal static readonly VarInfoValueTypes String = new VarInfoValueTypes("String", 9, "string", "string", "", new VarInfoStringConverter(), typeof(string));

        /// <summary>
        /// Represents an aray of <see cref="string">string</see> type.
        /// </summary>
        /// <remarks>
        /// The MPE's peculiar XML representation for this type is of the type (assume a <see cref="VarInfo.Name">
        /// VarInfo.Name</see> = "VarInfo Name"):
        /// <code>
        /// <Parameter name="VarInfo Name">
        ///      <Value>String1</Value>
        ///      <Value>String2</Value>
        ///      <Value>String3</Value>
        /// </Parameter>
        /// </code>
        /// The MPE's peculiar List&lt;string&gt; representation for this type is a List containing several elements,
        /// each one being one string.
        /// </remarks>
        internal static readonly VarInfoValueTypes ArrayString = new VarInfoValueTypes("ArrayString", 10, "string[]", "string[", "]", new VarInfoArrayStringConverter(), typeof(string[]));

        /// <summary>
        /// Represents a <see cref="List{T}">List&lt;string&gt;</see> type.
        /// </summary>
        /// <remarks>
        /// The MPE's peculiar XML representation for this type is of the type (assume a <see cref="VarInfo.Name">
        /// VarInfo.Name</see> = "VarInfo Name"):
        /// <code>
        /// <Parameter name="VarInfo Name">
        ///      <Value>String1</Value>
        ///      <Value>String2</Value>
        ///      <Value>String3</Value>
        /// </Parameter>
        /// </code>
        /// The MPE's peculiar List&lt;string&gt; representation for this type is a List containing several elements,
        /// each one being one string.
        /// </remarks>
        internal static readonly VarInfoValueTypes ListString = new VarInfoValueTypes("ListString", 11, "List<string>", "List<string>", "", new VarInfoListStringConverter(), typeof(List<string>));

        /// <summary>
        /// Represents a 2 dimensional array of <see cref="double">double</see> type, with 2 columns and a variable number of rows.
        /// </summary>
        /// <remarks>
        /// The MPE's peculiar XML representation for this type is of the type (assume a <see cref="VarInfo.Name">
        /// VarInfo.Name</see> = "VarInfo Name"):
        /// <code>
        /// <Parameter name="VarInfo Name">
        ///      <Value Key="1.0">10.0</Value>
        ///      <Value Key="2.0">20.0</Value>
        ///      <Value Key="3.0">30.0</Value>
        /// </Parameter>
        /// </code>
        /// Where the Key attribute goes in the first column of the array (array[0,i]) and the number in the "Value" tags
        /// goes in the second column of the array (array[1,i]).
        /// The MPE's peculiar List&lt;string&gt; representation for this type is a List containing several elements,
        /// each one being for each row of the array the concatenation of: array[1,i] + "$" + array[0,i].
        /// </remarks>
        internal static readonly VarInfoValueTypes Bidimensional = new VarInfoValueTypes("Bidimensional", 12, "double[,2]", "double[", ",2]", new VarInfoBidimensionalConverter(), typeof(double[,]));

        /// <summary>
        /// Represents a <see cref="Dictionary{K, T}">Dictionary&lt;string, string&gt;</see> type.
        /// </summary>
        /// <remarks>
        /// The MPE's peculiar XML representation for this type is of the type (assume a <see cref="VarInfo.Name">
        /// VarInfo.Name</see> = "VarInfo Name"):
        /// <code>
        /// <Parameter name="VarInfo Name">
        ///      <Value Key="Key1">Value1</Value>
        ///      <Value Key="Key2">Value2</Value>
        ///      <Value Key="Key3">Value3</Value>
        /// </Parameter>
        /// </code>
        /// Where the Key attribute becomes a key of the Dictionary parsed type and the string in the "Value" tags
        /// becomes the corresponding value.
        /// The MPE's peculiar List&lt;string&gt; representation for this type is a List containing several elements,
        /// each one being for each Dictionary entry the concatenation of: value + "$" + key.
        /// </remarks>
        internal static readonly VarInfoValueTypes DictionaryStringString = new VarInfoValueTypes("DictionaryStringString", 13, "dictionary<string,string>", "Dictionary<string,string>", "", new VarInfoDictionaryStringStringConverter(), typeof(Dictionary<string, string>));

        /// <summary>
        /// Represents a <see cref="Dictionary{K, T}">Dictionary&lt;string, double&gt;</see> type.
        /// </summary>
        /// <remarks>
        /// The MPE's peculiar XML representation for this type is of the type (assume a <see cref="VarInfo.Name">
        /// VarInfo.Name</see> = "VarInfo Name"):
        /// <code>
        /// <Parameter name="VarInfo Name">
        ///      <Value Key="Key1">10.0</Value>
        ///      <Value Key="Key2">20.0</Value>
        ///      <Value Key="Key3">30.0</Value>
        /// </Parameter>
        /// </code>
        ///Where the Key attribute becomes a key of the Dictionary parsed type and the string in the "Value" tags
        /// becomes the corresponding value.
        /// The MPE's peculiar List&lt;string&gt; representation for this type is a List containing several elements,
        /// each one being for each Dictionary entry the concatenation of: value + "$" + key.
        /// </remarks>
        internal static readonly VarInfoValueTypes DictionaryStringDouble = new VarInfoValueTypes("DictionaryStringDouble", 14, "dictionary<string,double>", "Dictionary<string,double>", "", new VarInfoDictionaryStringDoubleConverter(), typeof(Dictionary<string, double>));

        /// <summary>
        /// Represents a <see cref="Dictionary{K, T}">Dictionary&lt;double, double&gt;</see> type.
        /// </summary>
        /// <remarks>
        /// The MPE's peculiar XML representation for this type is of the type (assume a <see cref="VarInfo.Name">
        /// VarInfo.Name</see> = "VarInfo Name"):
        /// <code>
        /// <Parameter name="VarInfo Name">
        ///      <Value Key="1.0">10.0</Value>
        ///      <Value Key="2.0">20.0</Value>
        ///      <Value Key="3.0">30.0</Value>
        /// </Parameter>
        /// </code>
        ///Where the Key attribute becomes a key of the Dictionary parsed type and the string in the "Value" tags
        /// becomes the corresponding value.
        /// The MPE's peculiar List&lt;string&gt; representation for this type is a List containing several elements,
        /// each one being for each Dictionary entry the concatenation of: value + "$" + key.
        /// </remarks>
        internal static readonly VarInfoValueTypes DictionaryDoubleDouble = new VarInfoValueTypes("DictionaryDoubleDouble", 15, "dictionary<double,double>", "Dictionary<double,double>", "", new VarInfoDictionaryDoubleDoubleConverter(), typeof(Dictionary<double, double>));

        /// <summary>
        /// Represents a <see cref="Dictionary{K, T}">Dictionary&lt;int, double&gt;</see> type.
        /// </summary>
        /// <remarks>
        /// The MPE's peculiar XML representation for this type is of the type (assume a <see cref="VarInfo.Name">
        /// VarInfo.Name</see> = "VarInfo Name"):
        /// <code>
        /// <Parameter name="VarInfo Name">
        ///      <Value Key="1">10.0</Value>
        ///      <Value Key="2">20.0</Value>
        ///      <Value Key="3">30.0</Value>
        /// </Parameter>
        /// </code>
        ///Where the Key attribute becomes a key of the Dictionary parsed type and the string in the "Value" tags
        /// becomes the corresponding value.
        /// The MPE's peculiar List&lt;string&gt; representation for this type is a List containing several elements,
        /// each one being for each Dictionary entry the concatenation of: value + "$" + key.
        /// </remarks>
        internal static readonly VarInfoValueTypes DictionaryIntDouble = new VarInfoValueTypes("DictionaryIntDouble", 16, "dictionary<int,double>", "Dictionary<int,double>", "", new VarInfoDictionaryIntDoubleConverter(), typeof(Dictionary<int, double>));

        /// <summary>
        /// Represents a 2 dimensional array of double, with a variable number of rows and columns. The parsing functionalities are not implemented.
        /// </summary>
        internal static readonly VarInfoValueTypes DoubleMatrix = new VarInfoValueTypes("DoubleMatrix", 17, "double[,]", "", "", new UnimplementedVarInfoConverter(), typeof(double[,]));

        // A T T E N T I O N !!!
        // if a new instance of this class is added as a public static property,
        // e.g. because a new VarInfo type needs to be managed, add it also to the returned values
        // of the property "Values".



        static VarInfoValueTypes()
        {
            _values = new List<VarInfoValueTypes>();

            // A T T E N T I O N !!!
            // if a new instance of this class is added as a public static property,
            // e.g. because a new VarInfo type needs to be managed, add it in these returned values too.
            _values.Add(Double);
            _values.Add(ArrayDouble);
            _values.Add(ListDouble);
            _values.Add(Integer);
            _values.Add(ArrayInteger);
            _values.Add(ListInteger);
            _values.Add(Date);
            _values.Add(ArrayDate);
            _values.Add(ListDate);
            _values.Add(String);
            _values.Add(ArrayString);
            _values.Add(ListString);
            _values.Add(Bidimensional);
            _values.Add(DictionaryStringString);
            _values.Add(DictionaryStringDouble);
            _values.Add(DictionaryDoubleDouble);
            _values.Add(DictionaryIntDouble);
            _values.Add(DoubleMatrix);
            // A T T E N T I O N !!!
            // if a new instance of this class is added as a public static property,
            // e.g. because a new VarInfo type needs to be managed, add it in these returned values too.

            _instancesForType = new Dictionary<string, VarInfoValueTypes>();
            foreach (var val in _values)
            {
                _instancesForType.Add(val._name, val);
            }
        }

        private VarInfoValueTypes(string name, int ordinal, string label, string parsingPrefix, string parsingPostfix, IVarInfoConverter converter, System.Type type)
        {
            if (converter == null)
            {
                throw new ArgumentNullException("converter");
            }
            this._name = name;
            this._ordinal = ordinal;
            this._label = label;
            this._parsingPrefix = parsingPrefix;
            this._parsingPostfix = parsingPostfix;
            this._converter = converter;
            this._type = type;
        }

        /// <summary>
        /// Returns a string representation of dictionary-like entries, in the form of "value" + "$" + "key".
        /// </summary>
        /// <param name="key">The "key" part of the entry representation</param>
        /// <param name="value">The "value" part of the entry representation. If null, the empty string is used.</param>
        /// <returns>The concatenation of "value" + "$" + "key".</returns>
        /// <exception cref="ArgumentNullException">If the parameter 'key' is <c>null</c>.</exception>
        public static string ConcatenateKeyAndValue(string key, string value)
        {
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }
            if (value == null)
            {
                return ("$" + key);
            }
            return (value + "$" + key);
        }

        /// <summary>
        /// Returns an instance of VarInfoValueTypes from the specified name 
        /// </summary>
        /// <param name="instanceName"></param>
        /// <returns></returns>
        public static VarInfoValueTypes GetInstanceForName(string instanceName)
        {
            return _instancesForType[instanceName];
        }

        /// <summary>
        /// Returns the correct instance of this class representing the <see cref="VarInfo">VarInfo</see> type described by the string passed as the parameter 'source'.
        /// </summary>
        /// <param name="source">The string representation of the <see cref="VarInfo">VarInfo</see> type.</param>
        /// <param name="size">The size of the <see cref="VarInfo">VarInfo</see> type.
        /// If it is an array or a list, its length, if it is a Dictionary, the number of entries, if it is a primitive type, 1.</param>
        /// <returns>The correct instance of this class representing the <see cref="VarInfo">VarInfo</see>
        /// type described by the string passed as the parameter 'source'.</returns>
        public static VarInfoValueTypes GetVarInfoValueType(string source, out int size)
        {
            Func<VarInfoValueTypes, bool> func = null;
            VarInfoValueTypes types2;
            if (source == null)
            {
                throw new ArgumentNullException();
            }
            try
            {
                if (func == null)
                {
                    func = v => ((v.ParsingPostfix == null) || v.ParsingPostfix.Equals("")) ? source.Equals(v.ParsingPrefix) : (((source.StartsWith(v.ParsingPrefix) && source.EndsWith(v.ParsingPostfix)) && (source.Contains("[") == v.ParsingPrefix.Contains("["))) && (source.Contains(",2]") == v.ParsingPostfix.Contains(",2]")));
                }
                VarInfoValueTypes types = Enumerable.Where<VarInfoValueTypes>(Values, func).Single<VarInfoValueTypes>();
                if (!string.IsNullOrEmpty(source))
                {
                    source = source.Replace(types.ParsingPrefix, "");
                }
                if (!string.IsNullOrEmpty(source))
                {
                    source = source.Replace(types.ParsingPostfix, "");
                }
                if (source.Equals(""))
                {
                    size = 1;
                }
                else
                {
                    size = int.Parse(source);
                }
                types2 = types;
            }
            catch (InvalidOperationException)
            {
                throw new Exception("VarInfo type '" + source + "' not supported.");
            }
            return types2;
        }

        /// <summary>
        /// To be used when parsing dictionary-like entries, having a key and a value.
        /// Given one of these entries as the parameter 'complexValue', in the form of a unique string that is the concatenation
        /// of the value, the separating character "$" and the key, returns the value and populates the output parameter 'key'
        /// with the key.
        /// </summary>
        /// <param name="complexValue">The entry to be parsed in the form "value" + "$" + "key".</param>
        /// <param name="key">After the invocation, contains the "key".</param>
        /// <returns>The "value".</returns>
        public static string SplitKeyAndValue(string complexValue, out string key)
        {
            if (complexValue.Contains("$"))
            {
                string[] strArray = complexValue.Split(new string[] { "$" }, StringSplitOptions.RemoveEmptyEntries);
                key = strArray[1];
                return strArray[0];
            }
            key = null;
            return complexValue;
        }

        /// <summary>
        /// The converter to parse a string representation of <see cref="VarInfo.CurrentValue">VarInfo.CurrentValue</see> to its real type and back.
        /// </summary>
        public IVarInfoConverter Converter
        {
            get
            {
                return this._converter;
            }
        }

        /// <summary>
        /// Label identifying the type.
        /// </summary>
        public string Label
        {
            get
            {
                return this._label;
            }
        }

        /// <summary>
        /// The name of the type of the corresponding <see cref="VarInfo">VarInfo</see>'s parsed <see cref="VarInfo.CurrentValue">CurrentValue</see>.
        /// </summary>
        public string Name
        {
            get
            {
                return this._name;
            }
        }

        /// <summary>
        /// Ordinal number of each instance of this class.
        /// </summary>
        public int Ordinal
        {
            get
            {
                return this._ordinal;
            }
        }

        /// <summary>
        /// The postfix in the string representation of the type represented by this VarInfoValueType.<br/>
        /// A postfix is needed because some types, e.g double arrays, have a (variable) size in their name.
        /// So in this case, the type name is ParsingPrefix + size + ParsingPostfix.
        /// </summary>
        public string ParsingPostfix
        {
            get
            {
                return this._parsingPostfix;
            }
        }

        /// <summary>
        /// The prefix in the string representation of the type represented by this VarInfoValueType.<br/>
        /// A prefix is needed because some types, e.g double arrays, have a (variable) size in their name.
        /// So in this case, the type name is ParsingPrefix + size + ParsingPostfix.
        /// </summary>
        public string ParsingPrefix
        {
            get
            {
                return this._parsingPrefix;
            }
        }

        /// <summary>
        /// Returns true if the size is required by the definition of the VarInfo (according to its VarInfoValueType)
        /// </summary>
        public bool RequiresSizeInTypeDefinition
        {
            get
            {
                return ((this._parsingPostfix != null) && !this._parsingPostfix.Equals(""));
            }
        }

        /// <summary>
        /// Returns the <see cref="System.Type">Type</see> that <see cref="VarInfo.CurrentValue">VarInfo.CurrentValue</see> must have.
        /// </summary>
        public System.Type TypeForCurrentValue
        {
            get
            {
                return this._type;
            }
        }

        /// <summary>
        /// An enumeration of all the available instances of this class.
        /// </summary>
        public static IEnumerable<VarInfoValueTypes> Values
        {
            get
            {
                //// A T T E N T I O N !!!
                //// if a new instance of this class is added as a public static property,
                //// e.g. because a new VarInfo type needs to be managed, add it in these returned values too.
                //yield return Double;
                //yield return ArrayDouble;
                //yield return ListDouble;
                //yield return Integer;
                //yield return ArrayInteger;
                //yield return ListInteger;
                //yield return Date;
                //yield return ArrayDate;
                //yield return ListDate;
                //yield return String;
                //yield return ArrayString;
                //yield return ListString;
                //yield return Bidimensional;
                //yield return DictionaryStringString;
                //yield return DictionaryStringDouble;
                //yield return DictionaryDoubleDouble;
                //yield return DictionaryIntDouble;
                //yield return DoubleMatrix;
                //// A T T E N T I O N !!!
                //// if a new instance of this class is added as a public static property,
                //// e.g. because a new VarInfo type needs to be managed, add it in these returned values too.

                return _values.Select(a => a);
            }
        }

        private class UnimplementedVarInfoConverter : IVarInfoConverter
        {
            public object GetClonedCopy(object original)
            {
                throw new NotImplementedException();
            }

            public string GetConstructingString(int size)
            {
                return null;
            }

            public object GetParsedValue(XElement elem)
            {
                throw new NotImplementedException("Conversion not supported for this type");
            }

            public object GetParsedValueFromMPE(List<string> list)
            {
                throw new NotImplementedException("Conversion not supported for this type");
            }

            public string GetTypeNameRepresentation(int size)
            {
                throw new NotImplementedException("Conversion not supported for this type");
            }

            public bool HaveConsistentSize(List<string> list, object value)
            {
                throw new NotImplementedException("Conversion not supported for this type");
            }

            public bool HaveConsistentSize(XElement elem, object value)
            {
                throw new NotImplementedException("Conversion not supported for this type");
            }

            public XElement ParseValue(object value, string varInfoName)
            {
                throw new NotImplementedException("Conversion not supported for this type");
            }

            public List<string> ParseValueForMPE(object value)
            {
                throw new NotImplementedException("Conversion not supported for this type");
            }
        }

        private class VarInfoArrayDateConverter : IVarInfoConverter
        {
            public object GetClonedCopy(object original)
            {
                return CloneHelper.DeepClone<DateTime>((DateTime[]) original);
            }

            public string GetConstructingString(int size)
            {
                return ("new DateTime[" + size + "]");
            }

            public object GetParsedValue(XElement elem)
            {
                DateTime[] timeArray = new DateTime[elem.Elements("Value").Count<XElement>()];
                int num = 0;
                foreach (XElement element in elem.Elements("Value"))
                {
                    timeArray[num++] = Convert.ToDateTime(element.Value);
                }
                return timeArray;
            }

            public object GetParsedValueFromMPE(List<string> list)
            {
                if (list == null)
                {
                    throw new ArgumentNullException("list");
                }
                DateTime[] timeArray = new DateTime[list.Count];
                int num = 0;
                foreach (string str in list)
                {
                    timeArray[num++] = Convert.ToDateTime(str);
                }
                return timeArray;
            }

            public string GetTypeNameRepresentation(int size)
            {
                return (VarInfoValueTypes.ArrayDate.ParsingPrefix + size.ToString() + VarInfoValueTypes.ArrayDate.ParsingPostfix);
            }

            public bool HaveConsistentSize(List<string> list, object value)
            {
                if (!(value is DateTime[]))
                {
                    throw new ArgumentException("value must be a DateTime[]");
                }
                return (list.Count == ((DateTime[]) value).Length);
            }

            public bool HaveConsistentSize(XElement elem, object value)
            {
                if (!(value is DateTime[]))
                {
                    throw new ArgumentException("value must be a DateTime[]");
                }
                return (elem.Elements("Value").Count<XElement>() == ((DateTime[]) value).Length);
            }

            public XElement ParseValue(object value, string varInfoName)
            {
                if (!(value is DateTime[]))
                {
                    throw new ArgumentException("array of DateTime expected");
                }
                DateTime[] timeArray = (DateTime[]) value;
                XElement element = new XElement("Parameter");
                element.Add(new XAttribute("name", varInfoName));
                foreach (DateTime time in timeArray)
                {
                    element.Add(new XElement("Value", time.ToString()));
                }
                return element;
            }

            public List<string> ParseValueForMPE(object value)
            {
                if (!(value is DateTime[]))
                {
                    throw new ArgumentException("array of DateTime expected");
                }
                List<string> list = new List<string>();
                DateTime[] timeArray = (DateTime[]) value;
                foreach (DateTime time in timeArray)
                {
                    list.Add(time.ToString());
                }
                return list;
            }
        }

        private class VarInfoArrayDoubleConverter : IVarInfoConverter
        {
            public object GetClonedCopy(object original)
            {
                return CloneHelper.DeepClone<double>((double[]) original);
            }

            public string GetConstructingString(int size)
            {
                return ("new double[" + size + "]");
            }

            public object GetParsedValue(XElement elem)
            {
                double[] numArray = new double[elem.Elements("Value").Count<XElement>()];
                int num = 0;
                foreach (XElement element in elem.Elements("Value"))
                {
                    numArray[num++] = double.Parse(element.Value);
                }
                return numArray;
            }

            public object GetParsedValueFromMPE(List<string> list)
            {
                if (list == null)
                {
                    throw new ArgumentNullException("list");
                }
                double[] numArray = new double[list.Count];
                int num = 0;
                foreach (string str in list)
                {
                    numArray[num++] = double.Parse(str);
                }
                return numArray;
            }

            public string GetTypeNameRepresentation(int size)
            {
                return (VarInfoValueTypes.ArrayDouble.ParsingPrefix + size.ToString() + VarInfoValueTypes.ArrayDouble.ParsingPostfix);
            }

            public bool HaveConsistentSize(List<string> list, object value)
            {
                if (!(value is double[]))
                {
                    throw new ArgumentException("value must be a double[]");
                }
                return (list.Count == ((double[]) value).Length);
            }

            public bool HaveConsistentSize(XElement elem, object value)
            {
                if (!(value is double[]))
                {
                    throw new ArgumentException("value must be a double[]");
                }
                return (elem.Elements("Value").Count<XElement>() == ((double[]) value).Length);
            }

            public XElement ParseValue(object value, string varInfoName)
            {
                if (!(value is double[]))
                {
                    throw new ArgumentException("array of double expected");
                }
                double[] numArray = (double[]) value;
                XElement element = new XElement("Parameter");
                element.Add(new XAttribute("name", varInfoName));
                foreach (double num in numArray)
                {
                    element.Add(new XElement("Value", num.ToString()));
                }
                return element;
            }

            public List<string> ParseValueForMPE(object value)
            {
                if (!(value is double[]))
                {
                    throw new ArgumentException("array of double expected");
                }
                List<string> list = new List<string>();
                double[] numArray = (double[]) value;
                foreach (double num in numArray)
                {
                    list.Add(num.ToString());
                }
                return list;
            }
        }

        private class VarInfoArrayIntConverter : IVarInfoConverter
        {
            public object GetClonedCopy(object original)
            {
                return CloneHelper.DeepClone<int>((int[]) original);
            }

            public string GetConstructingString(int size)
            {
                return ("new int[" + size + "]");
            }

            public object GetParsedValue(XElement elem)
            {
                int[] numArray = new int[elem.Elements("Value").Count<XElement>()];
                int num = 0;
                foreach (XElement element in elem.Elements("Value"))
                {
                    numArray[num++] = int.Parse(element.Value);
                }
                return numArray;
            }

            public object GetParsedValueFromMPE(List<string> list)
            {
                if (list == null)
                {
                    throw new ArgumentNullException("list");
                }
                int[] numArray = new int[list.Count];
                int num = 0;
                foreach (string str in list)
                {
                    numArray[num++] = int.Parse(str);
                }
                return numArray;
            }

            public string GetTypeNameRepresentation(int size)
            {
                return (VarInfoValueTypes.ArrayInteger.ParsingPrefix + size.ToString() + VarInfoValueTypes.ArrayInteger.ParsingPostfix);
            }

            public bool HaveConsistentSize(List<string> list, object value)
            {
                if (!(value is int[]))
                {
                    throw new ArgumentException("value must be a int[]");
                }
                return (list.Count == ((int[]) value).Length);
            }

            public bool HaveConsistentSize(XElement elem, object value)
            {
                if (!(value is int[]))
                {
                    throw new ArgumentException("value must be a int[]");
                }
                return (elem.Elements("Value").Count<XElement>() == ((int[]) value).Length);
            }

            public XElement ParseValue(object value, string varInfoName)
            {
                if (!(value is int[]))
                {
                    throw new ArgumentException("array of int expected");
                }
                int[] numArray = (int[]) value;
                XElement element = new XElement("Parameter");
                element.Add(new XAttribute("name", varInfoName));
                foreach (int num in numArray)
                {
                    element.Add(new XElement("Value", num.ToString()));
                }
                return element;
            }

            public List<string> ParseValueForMPE(object value)
            {
                if (!(value is int[]))
                {
                    throw new ArgumentException("array of int expected");
                }
                List<string> list = new List<string>();
                int[] numArray = (int[]) value;
                foreach (int num in numArray)
                {
                    list.Add(num.ToString());
                }
                return list;
            }
        }

        private class VarInfoArrayStringConverter : IVarInfoConverter
        {
            public object GetClonedCopy(object original)
            {
                return CloneHelper.DeepClone<string>((string[]) original);
            }

            public string GetConstructingString(int size)
            {
                return ("new string[" + size + "]");
            }

            public object GetParsedValue(XElement elem)
            {
                string[] strArray = new string[elem.Elements("Value").Count<XElement>()];
                int num = 0;
                foreach (XElement element in elem.Elements("Value"))
                {
                    strArray[num++] = element.Value;
                }
                return strArray;
            }

            public object GetParsedValueFromMPE(List<string> list)
            {
                if (list == null)
                {
                    throw new ArgumentNullException("list");
                }
                return list.ToArray();
            }

            public string GetTypeNameRepresentation(int size)
            {
                return (VarInfoValueTypes.ArrayString.ParsingPrefix + size.ToString() + VarInfoValueTypes.ArrayString.ParsingPostfix);
            }

            public bool HaveConsistentSize(List<string> list, object value)
            {
                if (!(value is string[]))
                {
                    throw new ArgumentException("value must be a string[]");
                }
                return (list.Count == ((string[]) value).Length);
            }

            public bool HaveConsistentSize(XElement elem, object value)
            {
                if (!(value is string[]))
                {
                    throw new ArgumentException("value must be a string[]");
                }
                return (elem.Elements("Value").Count<XElement>() == ((string[]) value).Length);
            }

            public XElement ParseValue(object value, string varInfoName)
            {
                if (!(value is string[]))
                {
                    throw new ArgumentException("array of string expected");
                }
                string[] strArray = (string[]) value;
                XElement element = new XElement("Parameter");
                element.Add(new XAttribute("name", varInfoName));
                foreach (string str in strArray)
                {
                    element.Add(new XElement("Value", str));
                }
                return element;
            }

            public List<string> ParseValueForMPE(object value)
            {
                if (!(value is string[]))
                {
                    throw new ArgumentException("array of string expected");
                }
                return new List<string>((string[]) value);
            }
        }

        private class VarInfoBidimensionalConverter : IVarInfoConverter
        {
            public object GetClonedCopy(object original)
            {
                return ((double[,]) original).Clone();
            }

            public string GetConstructingString(int size)
            {
                return ("new double[" + size + ", 2]");
            }

            public object GetParsedValue(XElement elem)
            {
                double[,] numArray = new double[elem.Elements("Value").Count<XElement>(), 2];
                int num = 0;
                foreach (XElement element in elem.Elements("Value"))
                {
                    double num2 = double.Parse(element.Attribute("Key").Value);
                    numArray[0, num] = num2;
                    numArray[1, num++] = double.Parse(element.Value);
                }
                return numArray;
            }

            public object GetParsedValueFromMPE(List<string> list)
            {
                if (list == null)
                {
                    throw new ArgumentNullException("list");
                }
                double[,] numArray = new double[list.Count, 2];
                int num = 0;
                foreach (string str in list)
                {
                    string str2;
                    string s = VarInfoValueTypes.SplitKeyAndValue(str, out str2);
                    if (str2 == null)
                    {
                        str2 = "0";
                    }
                    numArray[num, 0] = double.Parse(str2);
                    numArray[num++, 1] = double.Parse(s);
                }
                return numArray;
            }

            public string GetTypeNameRepresentation(int size)
            {
                return (VarInfoValueTypes.Bidimensional.ParsingPrefix + size.ToString() + VarInfoValueTypes.Bidimensional.ParsingPostfix);
            }

            public bool HaveConsistentSize(List<string> list, object value)
            {
                if (!(value is double[,]))
                {
                    throw new ArgumentException("value must be a double[,]");
                }
                return (list.Count == ((double[,]) value).GetLength(0));
            }

            public bool HaveConsistentSize(XElement elem, object value)
            {
                if (!(value is double[,]))
                {
                    throw new ArgumentException("value must be a double[,]");
                }
                return (elem.Elements("Value").Count<XElement>() == ((double[,]) value).GetLength(0));
            }

            public XElement ParseValue(object value, string varInfoName)
            {
                if (!(value is double[,]))
                {
                    throw new ArgumentException("double[n,2] expected");
                }
                double[,] numArray = (double[,]) value;
                if (numArray.GetLength(1) != 2)
                {
                    throw new ArgumentException("double[n,2] expected");
                }
                XElement element = new XElement("Parameter");
                element.Add(new XAttribute("name", varInfoName));
                for (int i = 0; i < numArray.GetLength(0); i++)
                {
                    XElement content = new XElement("Value", numArray[i, 1].ToString());
                    content.SetAttributeValue("Key", numArray[i, 0].ToString());
                    element.Add(content);
                }
                return element;
            }

            public List<string> ParseValueForMPE(object value)
            {
                if (!(value is double[,]))
                {
                    throw new ArgumentException("double[n,2] expected");
                }
                double[,] numArray = (double[,]) value;
                if (numArray.GetLength(1) != 2)
                {
                    throw new ArgumentException("double[n,2] expected");
                }
                List<string> list = new List<string>();
                for (int i = 0; i < numArray.GetLength(0); i++)
                {
                    string item = VarInfoValueTypes.ConcatenateKeyAndValue(Convert.ToString(numArray[i, 0]), Convert.ToString(numArray[i, 1]));
                    list.Add(item);
                }
                return list;
            }
        }

        private class VarInfoDateConverter : IVarInfoConverter
        {
            public object GetClonedCopy(object original)
            {
                DateTime time = (DateTime) original;
                return new DateTime(time.Ticks);
            }

            public string GetConstructingString(int size)
            {
                return "new DateTime()";
            }

            public object GetParsedValue(XElement elem)
            {
                return Convert.ToDateTime(elem.Element("Value").Value);
            }

            public object GetParsedValueFromMPE(List<string> list)
            {
                if ((list == null) || (list.Count != 1))
                {
                    throw new ArgumentException("List of one double expected");
                }
                return Convert.ToDateTime(list[0]);
            }

            public string GetTypeNameRepresentation(int size)
            {
                return VarInfoValueTypes.Date.Label;
            }

            public bool HaveConsistentSize(List<string> list, object value)
            {
                return ((list.Count == 1) && (value is DateTime));
            }

            public bool HaveConsistentSize(XElement elem, object value)
            {
                return ((elem.Elements("Value").Count<XElement>() == 1) && (value is DateTime));
            }

            public XElement ParseValue(object value, string varInfoName)
            {
                if (!(value is DateTime))
                {
                    throw new ArgumentException("DateTime expected");
                }
                XElement element = new XElement("Parameter");
                element.Add(new XAttribute("name", varInfoName));
                element.Add(new XElement("Value", value.ToString()));
                return element;
            }

            public List<string> ParseValueForMPE(object value)
            {
                if (!(value is DateTime))
                {
                    throw new ArgumentException("DateTime expected");
                }
                return new List<string> { value.ToString() };
            }
        }

        private class VarInfoDictionaryDoubleDoubleConverter : IVarInfoConverter
        {
            public object GetClonedCopy(object original)
            {
                return CloneHelper.DeepClone<double, double>((Dictionary<double, double>) original);
            }

            public string GetConstructingString(int size)
            {
                return "new Dictionary<double,double>()";
            }

            public object GetParsedValue(XElement elem)
            {
                Dictionary<double, double> dictionary = new Dictionary<double, double>();
                foreach (XElement element in elem.Elements("Value"))
                {
                    string s = element.Attribute("Key").Value;
                    dictionary.Add(double.Parse(s), double.Parse(element.Value));
                }
                return dictionary;
            }

            public object GetParsedValueFromMPE(List<string> list)
            {
                if (list == null)
                {
                    throw new ArgumentNullException("list");
                }
                Dictionary<double, double> dictionary = new Dictionary<double, double>();
                foreach (string str in list)
                {
                    string str2;
                    string s = VarInfoValueTypes.SplitKeyAndValue(str, out str2);
                    dictionary.Add(double.Parse(str2), double.Parse(s));
                }
                return dictionary;
            }

            public string GetTypeNameRepresentation(int size)
            {
                return VarInfoValueTypes.DictionaryDoubleDouble.ParsingPrefix + VarInfoValueTypes.DictionaryDoubleDouble.ParsingPostfix;
            }

            public bool HaveConsistentSize(List<string> list, object value)
            {
                if (!(value is Dictionary<double, double>))
                {
                    throw new ArgumentException("value must be a Dictionary<double,double>");
                }
                return (list.Count == ((Dictionary<double, double>) value).Count);
            }

            public bool HaveConsistentSize(XElement elem, object value)
            {
                if (!(value is Dictionary<double, double>))
                {
                    throw new ArgumentException("value must be a Dictionary<double,double>");
                }
                return (elem.Elements("Value").Count<XElement>() == ((Dictionary<double, double>) value).Count);
            }

            public XElement ParseValue(object value, string varInfoName)
            {
                if (!(value is Dictionary<double, double>))
                {
                    throw new ArgumentException("Dictionary<double,double> expected");
                }
                Dictionary<double, double> dictionary = (Dictionary<double, double>) value;
                XElement element = new XElement("Parameter");
                element.Add(new XAttribute("name", varInfoName));
                foreach (KeyValuePair<double, double> pair in dictionary)
                {
                    XElement content = new XElement("Value", pair.Value.ToString());
                    content.SetAttributeValue("Key", pair.Key.ToString());
                    element.Add(content);
                }
                return element;
            }

            public List<string> ParseValueForMPE(object value)
            {
                if (!(value is Dictionary<double, double>))
                {
                    throw new ArgumentException("Dictionary<double,double> expected");
                }
                Dictionary<double, double> dictionary = (Dictionary<double, double>) value;
                List<string> list = new List<string>();
                foreach (KeyValuePair<double, double> pair in dictionary)
                {
                    string key = pair.Key.ToString();
                    string item = VarInfoValueTypes.ConcatenateKeyAndValue(key, pair.Value.ToString());
                    list.Add(item);
                }
                return list;
            }
        }

        private class VarInfoDictionaryIntDoubleConverter : IVarInfoConverter
        {
            public object GetClonedCopy(object original)
            {
                return CloneHelper.DeepClone<int, double>((Dictionary<int, double>) original);
            }

            public string GetConstructingString(int size)
            {
                return "new Dictionary<int,double>()";
            }

            public object GetParsedValue(XElement elem)
            {
                Dictionary<int, double> dictionary = new Dictionary<int, double>();
                foreach (XElement element in elem.Elements("Value"))
                {
                    string s = element.Attribute("Key").Value;
                    dictionary.Add(int.Parse(s), double.Parse(element.Value));
                }
                return dictionary;
            }

            public object GetParsedValueFromMPE(List<string> list)
            {
                if (list == null)
                {
                    throw new ArgumentNullException("list");
                }
                Dictionary<int, double> dictionary = new Dictionary<int, double>();
                foreach (string str in list)
                {
                    string str2;
                    string s = VarInfoValueTypes.SplitKeyAndValue(str, out str2);
                    dictionary.Add(int.Parse(str2), double.Parse(s));
                }
                return dictionary;
            }

            public string GetTypeNameRepresentation(int size)
            {
                return VarInfoValueTypes.DictionaryIntDouble.ParsingPrefix + VarInfoValueTypes.DictionaryIntDouble.ParsingPostfix;
            }

            public bool HaveConsistentSize(List<string> list, object value)
            {
                if (!(value is Dictionary<int, double>))
                {
                    throw new ArgumentException("value must be a Dictionary<int,double>");
                }
                return (list.Count == ((Dictionary<int, double>) value).Count);
            }

            public bool HaveConsistentSize(XElement elem, object value)
            {
                if (!(value is Dictionary<int, double>))
                {
                    throw new ArgumentException("value must be a Dictionary<int,double>");
                }
                return (elem.Elements("Value").Count<XElement>() == ((Dictionary<int, double>) value).Count);
            }

            public XElement ParseValue(object value, string varInfoName)
            {
                if (!(value is Dictionary<int, double>))
                {
                    throw new ArgumentException("Dictionary<int,double> expected");
                }
                Dictionary<int, double> dictionary = (Dictionary<int, double>) value;
                XElement element = new XElement("Parameter");
                element.Add(new XAttribute("name", varInfoName));
                foreach (KeyValuePair<int, double> pair in dictionary)
                {
                    XElement content = new XElement("Value", pair.Value.ToString());
                    content.SetAttributeValue("Key", pair.Key.ToString());
                    element.Add(content);
                }
                return element;
            }

            public List<string> ParseValueForMPE(object value)
            {
                if (!(value is Dictionary<int, double>))
                {
                    throw new ArgumentException("Dictionary<int,double> expected");
                }
                Dictionary<int, double> dictionary = (Dictionary<int, double>) value;
                List<string> list = new List<string>();
                foreach (KeyValuePair<int, double> pair in dictionary)
                {
                    string key = pair.Key.ToString();
                    string item = VarInfoValueTypes.ConcatenateKeyAndValue(key, pair.Value.ToString());
                    list.Add(item);
                }
                return list;
            }
        }

        private class VarInfoDictionaryStringDoubleConverter : IVarInfoConverter
        {
            public object GetClonedCopy(object original)
            {
                return CloneHelper.DeepClone<string, double>((Dictionary<string, double>) original);
            }

            public string GetConstructingString(int size)
            {
                return "new Dictionary<string, double>()";
            }

            public object GetParsedValue(XElement elem)
            {
                Dictionary<string, double> dictionary = new Dictionary<string, double>();
                foreach (XElement element in elem.Elements("Value"))
                {
                    string key = element.Attribute("Key").Value;
                    dictionary.Add(key, double.Parse(element.Value));
                }
                return dictionary;
            }

            public object GetParsedValueFromMPE(List<string> list)
            {
                if (list == null)
                {
                    throw new ArgumentNullException("list");
                }
                Dictionary<string, double> dictionary = new Dictionary<string, double>();
                foreach (string str in list)
                {
                    string str2;
                    string s = VarInfoValueTypes.SplitKeyAndValue(str, out str2);
                    dictionary.Add(str2, double.Parse(s));
                }
                return dictionary;
            }

            public string GetTypeNameRepresentation(int size)
            {
                return VarInfoValueTypes.DictionaryStringDouble.ParsingPrefix + VarInfoValueTypes.DictionaryStringDouble.ParsingPostfix;
            }

            public bool HaveConsistentSize(List<string> list, object value)
            {
                if (!(value is Dictionary<string, double>))
                {
                    throw new ArgumentException("value must be a Dictionary<string,double>");
                }
                return (list.Count == ((Dictionary<string, double>) value).Count);
            }

            public bool HaveConsistentSize(XElement elem, object value)
            {
                if (!(value is Dictionary<string, double>))
                {
                    throw new ArgumentException("value must be a Dictionary<string,double>");
                }
                return (elem.Elements("Value").Count<XElement>() == ((Dictionary<string, double>) value).Count);
            }

            public XElement ParseValue(object value, string varInfoName)
            {
                if (!(value is Dictionary<string, double>))
                {
                    throw new ArgumentException("Dictionary<string,double> expected");
                }
                Dictionary<string, double> dictionary = (Dictionary<string, double>) value;
                XElement element = new XElement("Parameter");
                element.Add(new XAttribute("name", varInfoName));
                foreach (KeyValuePair<string, double> pair in dictionary)
                {
                    XElement content = new XElement("Value", pair.Value.ToString());
                    content.SetAttributeValue("Key", pair.Key);
                    element.Add(content);
                }
                return element;
            }

            public List<string> ParseValueForMPE(object value)
            {
                if (!(value is Dictionary<string, double>))
                {
                    throw new ArgumentException("Dictionary<string,double> expected");
                }
                Dictionary<string, double> dictionary = (Dictionary<string, double>) value;
                List<string> list = new List<string>();
                foreach (KeyValuePair<string, double> pair in dictionary)
                {
                    string item = VarInfoValueTypes.ConcatenateKeyAndValue(pair.Key, pair.Value.ToString());
                    list.Add(item);
                }
                return list;
            }
        }

        private class VarInfoDictionaryStringStringConverter : IVarInfoConverter
        {
            public object GetClonedCopy(object original)
            {
                return CloneHelper.DeepClone<string, string>((Dictionary<string, string>) original);
            }

            public string GetConstructingString(int size)
            {
                return "new Dictionary<string, string>()";
            }

            public object GetParsedValue(XElement elem)
            {
                Dictionary<string, string> dictionary = new Dictionary<string, string>();
                foreach (XElement element in elem.Elements("Value"))
                {
                    string key = element.Attribute("Key").Value;
                    dictionary.Add(key, element.Value);
                }
                return dictionary;
            }

            public object GetParsedValueFromMPE(List<string> list)
            {
                if (list == null)
                {
                    throw new ArgumentNullException("list");
                }
                Dictionary<string, string> dictionary = new Dictionary<string, string>();
                foreach (string str in list)
                {
                    string str2;
                    string str3 = VarInfoValueTypes.SplitKeyAndValue(str, out str2);
                    dictionary.Add(str2, str3);
                }
                return dictionary;
            }

            public string GetTypeNameRepresentation(int size)
            {
                return VarInfoValueTypes.DictionaryStringString.ParsingPrefix + VarInfoValueTypes.DictionaryStringString.ParsingPostfix;
            }

            public bool HaveConsistentSize(List<string> list, object value)
            {
                if (!(value is Dictionary<string, string>))
                {
                    throw new ArgumentException("value must be a double[,]");
                }
                return (list.Count == ((Dictionary<string, string>) value).Count);
            }

            public bool HaveConsistentSize(XElement elem, object value)
            {
                if (!(value is Dictionary<string, string>))
                {
                    throw new ArgumentException("value must be a double[,]");
                }
                return (elem.Elements("Value").Count<XElement>() == ((Dictionary<string, string>) value).Count);
            }

            public XElement ParseValue(object value, string varInfoName)
            {
                if (!(value is Dictionary<string, string>))
                {
                    throw new ArgumentException("Dictionary<string,string> expected");
                }
                Dictionary<string, string> dictionary = (Dictionary<string, string>) value;
                XElement element = new XElement("Parameter");
                element.Add(new XAttribute("name", varInfoName));
                foreach (KeyValuePair<string, string> pair in dictionary)
                {
                    XElement content = new XElement("Value", pair.Value);
                    content.SetAttributeValue("Key", pair.Key);
                    element.Add(content);
                }
                return element;
            }

            public List<string> ParseValueForMPE(object value)
            {
                if (!(value is Dictionary<string, string>))
                {
                    throw new ArgumentException("double[n,2] expected");
                }
                Dictionary<string, string> dictionary = (Dictionary<string, string>) value;
                List<string> list = new List<string>();
                foreach (KeyValuePair<string, string> pair in dictionary)
                {
                    string item = VarInfoValueTypes.ConcatenateKeyAndValue(pair.Key, pair.Value);
                    list.Add(item);
                }
                return list;
            }
        }

        private class VarInfoDoubleConverter : IVarInfoConverter
        {
            public object GetClonedCopy(object original)
            {
                return (double) original;
            }

            public string GetConstructingString(int size)
            {
                return null;
            }

            public object GetParsedValue(XElement elem)
            {
                return double.Parse(elem.Element("Value").Value);
            }

            public object GetParsedValueFromMPE(List<string> list)
            {
                if ((list == null) || (list.Count != 1))
                {
                    throw new ArgumentException("List of one double expected");
                }
                return double.Parse(list[0]);
            }

            public string GetTypeNameRepresentation(int size)
            {
                return VarInfoValueTypes.Double.Label;
            }

            public bool HaveConsistentSize(List<string> list, object value)
            {
                return ((list.Count == 1) && (value is double));
            }

            public bool HaveConsistentSize(XElement elem, object value)
            {
                return ((elem.Elements("Value").Count<XElement>() == 1) && (value is double));
            }

            public XElement ParseValue(object value, string varInfoName)
            {
                if (!(value is double))
                {
                    throw new ArgumentException("double expected");
                }
                XElement element = new XElement("Parameter");
                element.Add(new XAttribute("name", varInfoName));
                element.Add(new XElement("Value", value.ToString()));
                return element;
            }

            public List<string> ParseValueForMPE(object value)
            {
                if (!(value is double))
                {
                    throw new ArgumentException("double expected");
                }
                return new List<string> { Convert.ToDouble(value).ToString() };
            }
        }

        private class VarInfoIntConverter : IVarInfoConverter
        {
            public object GetClonedCopy(object original)
            {
                return (int) original;
            }

            public string GetConstructingString(int size)
            {
                return null;
            }

            public object GetParsedValue(XElement elem)
            {
                return int.Parse(elem.Element("Value").Value);
            }

            public object GetParsedValueFromMPE(List<string> list)
            {
                if ((list == null) || (list.Count != 1))
                {
                    throw new ArgumentException("List of one int expected");
                }
                return int.Parse(list[0]);
            }

            public string GetTypeNameRepresentation(int size)
            {
                return VarInfoValueTypes.Integer.Label;
            }

            public bool HaveConsistentSize(List<string> list, object value)
            {
                return ((list.Count == 1) && (value is int));
            }

            public bool HaveConsistentSize(XElement elem, object value)
            {
                return ((elem.Elements("Value").Count<XElement>() == 1) && (value is int));
            }

            public XElement ParseValue(object value, string varInfoName)
            {
                if (!(value is int))
                {
                    throw new ArgumentException("int expected");
                }
                XElement element = new XElement("Parameter");
                element.Add(new XAttribute("name", varInfoName));
                element.Add(new XElement("Value", value.ToString()));
                return element;
            }

            public List<string> ParseValueForMPE(object value)
            {
                if (!(value is int))
                {
                    throw new ArgumentException("int expected");
                }
                return new List<string> { Convert.ToInt32(value).ToString() };
            }
        }

        private class VarInfoListDateConverter : IVarInfoConverter
        {
            public object GetClonedCopy(object original)
            {
                return CloneHelper.DeepClone<DateTime>((List<DateTime>) original);
            }

            public string GetConstructingString(int size)
            {
                return "new List<DateTime>()";
            }

            public object GetParsedValue(XElement elem)
            {
                List<DateTime> list = new List<DateTime>();
                foreach (XElement element in elem.Elements("Value"))
                {
                    list.Add(Convert.ToDateTime(element.Value));
                }
                return list;
            }

            public object GetParsedValueFromMPE(List<string> list)
            {
                if (list == null)
                {
                    throw new ArgumentNullException("list");
                }
                List<DateTime> list2 = new List<DateTime>();
                foreach (string str in list)
                {
                    list2.Add(Convert.ToDateTime(str));
                }
                return list2;
            }

            public string GetTypeNameRepresentation(int size)
            {
                return VarInfoValueTypes.Date.Label;
            }

            public bool HaveConsistentSize(List<string> list, object value)
            {
                if (!(value is List<DateTime>))
                {
                    throw new ArgumentException("value must be a List<DateTime>");
                }
                return (list.Count == ((List<DateTime>) value).Count);
            }

            public bool HaveConsistentSize(XElement elem, object value)
            {
                if (!(value is List<DateTime>))
                {
                    throw new ArgumentException("value must be a List<DateTime>");
                }
                return (elem.Elements("Value").Count<XElement>() == ((List<DateTime>) value).Count);
            }

            public XElement ParseValue(object value, string varInfoName)
            {
                if (!(value is List<DateTime>))
                {
                    throw new ArgumentException("List<DateTime> expected");
                }
                List<DateTime> list = (List<DateTime>) value;
                XElement element = new XElement("Parameter");
                element.Add(new XAttribute("name", varInfoName));
                foreach (DateTime time in list)
                {
                    element.Add(new XElement("Value", time.ToString()));
                }
                return element;
            }

            public List<string> ParseValueForMPE(object value)
            {
                if (!(value is List<DateTime>))
                {
                    throw new ArgumentException("List<DateTime> expected");
                }
                List<DateTime> list = (List<DateTime>) value;
                List<string> list2 = new List<string>();
                foreach (DateTime time in list)
                {
                    list2.Add(time.ToString());
                }
                return list2;
            }
        }

        private class VarInfoListDoubleConverter : IVarInfoConverter
        {
            public object GetClonedCopy(object original)
            {
                return CloneHelper.DeepClone<double>((List<double>) original);
            }

            public string GetConstructingString(int size)
            {
                return "new List<double>()";
            }

            public object GetParsedValue(XElement elem)
            {
                List<double> list = new List<double>();
                foreach (XElement element in elem.Elements("Value"))
                {
                    list.Add(double.Parse(element.Value));
                }
                return list;
            }

            public object GetParsedValueFromMPE(List<string> list)
            {
                if (list == null)
                {
                    throw new ArgumentNullException("list");
                }
                List<double> list2 = new List<double>();
                foreach (string str in list)
                {
                    list2.Add(double.Parse(str));
                }
                return list2;
            }

            public string GetTypeNameRepresentation(int size)
            {
                return VarInfoValueTypes.ListDouble.Label;
            }

            public bool HaveConsistentSize(List<string> list, object value)
            {
                if (!(value is List<double>))
                {
                    throw new ArgumentException("value must be a List<double>");
                }
                return (list.Count == ((List<double>) value).Count);
            }

            public bool HaveConsistentSize(XElement elem, object value)
            {
                if (!(value is List<double>))
                {
                    throw new ArgumentException("value must be a List<double>");
                }
                return (elem.Elements("Value").Count<XElement>() == ((List<double>) value).Count);
            }

            public XElement ParseValue(object value, string varInfoName)
            {
                if (!(value is List<double>))
                {
                    throw new ArgumentException("List<double> expected");
                }
                List<double> list = (List<double>) value;
                XElement element = new XElement("Parameter");
                element.Add(new XAttribute("name", varInfoName));
                foreach (double num in list)
                {
                    element.Add(new XElement("Value", num.ToString()));
                }
                return element;
            }

            public List<string> ParseValueForMPE(object value)
            {
                if (!(value is List<double>))
                {
                    throw new ArgumentException("List<double> expected");
                }
                List<double> list = (List<double>) value;
                List<string> list2 = new List<string>();
                foreach (double num in list)
                {
                    list2.Add(num.ToString());
                }
                return list2;
            }
        }

        private class VarInfoListIntConverter : IVarInfoConverter
        {
            public object GetClonedCopy(object original)
            {
                return CloneHelper.DeepClone<int>((List<int>) original);
            }

            public string GetConstructingString(int size)
            {
                return "new List<int>()";
            }

            public object GetParsedValue(XElement elem)
            {
                List<int> list = new List<int>();
                foreach (XElement element in elem.Elements("Value"))
                {
                    list.Add(int.Parse(element.Value));
                }
                return list;
            }

            public object GetParsedValueFromMPE(List<string> list)
            {
                if (list == null)
                {
                    throw new ArgumentNullException("list");
                }
                List<int> list2 = new List<int>();
                foreach (string str in list)
                {
                    list2.Add(int.Parse(str));
                }
                return list2;
            }

            public string GetTypeNameRepresentation(int size)
            {
                return VarInfoValueTypes.ListInteger.Label;
            }

            public bool HaveConsistentSize(List<string> list, object value)
            {
                if (!(value is List<int>))
                {
                    throw new ArgumentException("value must be a List<double>");
                }
                return (list.Count == ((List<int>) value).Count);
            }

            public bool HaveConsistentSize(XElement elem, object value)
            {
                if (!(value is List<int>))
                {
                    throw new ArgumentException("value must be a List<double>");
                }
                return (elem.Elements("Value").Count<XElement>() == ((List<int>) value).Count);
            }

            public XElement ParseValue(object value, string varInfoName)
            {
                if (!(value is List<int>))
                {
                    throw new ArgumentException("List<double> expected");
                }
                List<int> list = (List<int>) value;
                XElement element = new XElement("Parameter");
                element.Add(new XAttribute("name", varInfoName));
                foreach (int num in list)
                {
                    element.Add(new XElement("Value", num.ToString()));
                }
                return element;
            }

            public List<string> ParseValueForMPE(object value)
            {
                if (!(value is List<int>))
                {
                    throw new ArgumentException("List<double> expected");
                }
                List<int> list = (List<int>) value;
                List<string> list2 = new List<string>();
                foreach (int num in list)
                {
                    list2.Add(num.ToString());
                }
                return list2;
            }
        }

        private class VarInfoListStringConverter : IVarInfoConverter
        {
            public object GetClonedCopy(object original)
            {
                return CloneHelper.DeepClone<string>((List<string>) original);
            }

            public string GetConstructingString(int size)
            {
                return "new List<string>()";
            }

            public object GetParsedValue(XElement elem)
            {
                return (from e in elem.Elements("Value") select e.Value).ToList<string>();
            }

            public object GetParsedValueFromMPE(List<string> list)
            {
                if (list == null)
                {
                    throw new ArgumentNullException("list");
                }
                return list;
            }

            public string GetTypeNameRepresentation(int size)
            {
                return VarInfoValueTypes.ListString.Label;
            }

            public bool HaveConsistentSize(List<string> list, object value)
            {
                if (!(value is List<string>))
                {
                    throw new ArgumentException("value must be a List<string>");
                }
                return (list.Count == ((List<string>) value).Count);
            }

            public bool HaveConsistentSize(XElement elem, object value)
            {
                if (!(value is List<string>))
                {
                    throw new ArgumentException("value must be a List<string>");
                }
                return (elem.Elements("Value").Count<XElement>() == ((List<string>) value).Count);
            }

            public XElement ParseValue(object value, string varInfoName)
            {
                if (!(value is List<string>))
                {
                    throw new ArgumentException("List<string> expected");
                }
                List<string> list = (List<string>) value;
                XElement element = new XElement("Parameter");
                element.Add(new XAttribute("name", varInfoName));
                foreach (string str in list)
                {
                    element.Add(new XElement("Value", str));
                }
                return element;
            }

            public List<string> ParseValueForMPE(object value)
            {
                if (!(value is List<double>))
                {
                    throw new ArgumentException("List<string> expected");
                }
                return (List<string>) value;
            }
        }

        private class VarInfoStringConverter : IVarInfoConverter
        {
            public object GetClonedCopy(object original)
            {
                return ((string) original).Clone();
            }

            public string GetConstructingString(int size)
            {
                return null;
            }

            public object GetParsedValue(XElement elem)
            {
                return elem.Element("Value").Value;
            }

            public object GetParsedValueFromMPE(List<string> list)
            {
                if ((list == null) || (list.Count != 1))
                {
                    throw new ArgumentException("List of one double expected");
                }
                return list[0];
            }

            public string GetTypeNameRepresentation(int size)
            {
                return VarInfoValueTypes.String.Label;
            }

            public bool HaveConsistentSize(List<string> list, object value)
            {
                return ((list.Count == 1) && (value is string));
            }

            public bool HaveConsistentSize(XElement elem, object value)
            {
                return ((elem.Elements("Value").Count<XElement>() == 1) && (value is string));
            }

            public XElement ParseValue(object value, string varInfoName)
            {
                if (!(value is string))
                {
                    throw new ArgumentException("string expected");
                }
                XElement element = new XElement("Parameter");
                element.Add(new XAttribute("name", varInfoName));
                element.Add(new XElement("Value", (string) value));
                return element;
            }

            public List<string> ParseValueForMPE(object value)
            {
                if (!(value is string))
                {
                    throw new ArgumentException("string expected");
                }
                return new List<string> { ((string) value) };
            }
        }
    }
}

