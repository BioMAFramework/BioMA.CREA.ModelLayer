namespace CRA.ModelLayer.Core
{
    using System;
    using System.Threading;

    /// <summary>
    /// The class VarInfo represents a variable in the Bioma framework: for example a parameter of a <see cref="CRA.ModelLayer.Strategy.IStrategy">strategy</see>, or a property of a <see cref="CRA.ModelLayer.Core.IDomainClass">domain class</see>. 
    /// The value of the variable can be a simple value (for example a number) or a complex type (for example a Dictionary). The VarInfo object describes the type of its value using the <see cref="ValueType">ValueType</see> property. 
    /// The list of managed type is defined in the <see cref="CRA.ModelLayer.Core.VarInfoValueTypes">VarInfoValueTypes</see> enumeration.
    /// There should always be a correspondence between the VarInfo value (property CurrentValue) and the declared type, otherwise an Exception is thrown. This correspondence can be checked at any moment by calling the method IsTypeCorrect.
    /// VarInfo extends the <see cref="IVarInfo">IVarInfo interface</see> and the IEquatable&lt;VarInfo&gt; (this one to compare a VarInfo object vs another one).
    /// </summary>
    public class VarInfo : IVarInfo, IEquatable<VarInfo>
    {
        private object _currentValue;
        private double _defaultValue;
        private string _description;
        private double _maxValue;
        private double _minValue;
        private string _name;
        private string _units;
        private string _URL;
        private int idVar;
        private Type localVarType;
        private int sizeVar;
        private VarInfoValueTypes valueTypeVar;

        ///<summary>
        /// Event thrown when the <see cref="CurrentValue"> CurrentValue </see> is set
        ///</summary>
        public virtual event Action<VarInfo> CurrentValueSet;


        /// <summary>
        /// Returns <c>true</c> if 'other' is a VarInfo and it has the same <see cref="Name">Name</see> than this, <c>false</c> otherwise.
        /// </summary>
        /// <param name="other">The VarInfo to be tested for equality.</param>
        /// <returns><c>true</c> if 'other' is a VarInfo and it has the same <see cref="Name">Name</see> than this, <c>false</c> otherwise.</returns>
        public bool Equals(VarInfo other)
        {
            if (other == null)
            {
                return false;
            }
            return this.Name.Equals(other.Name);
        }

        /// <summary>
        /// Calls <see cref="Equals(VarInfo)">Equals(VarInfo)</see> and returns true if obj is a VarInfo and it has the same <see cref="Name">Name</see> than this
        /// </summary>
        /// <param name="obj">The object to be tested for equality</param>
        /// <returns><c>true</c> if obj is a VarInfo and it has the same <see cref="Name">Name</see> than this, <c>false</c> otherwise.</returns>
        public override bool Equals(object obj)
        {
            return this.Equals(obj as VarInfo);
        }

        /// <summary>
        /// Returns the hashcode of the <see cref="Name">Name</see> property.
        /// </summary>
        /// <returns>The hashcode of the <see cref="Name">Name</see>.</returns>
        public override int GetHashCode()
        {
            return this.Name.GetHashCode();
        }

        /// <summary>
        /// Returns true if the Type of the <see cref="CurrentValue"> CurrentValue </see> is consistent with the variable <see cref="ValueType">ValueType</see>, false otherwise.
        /// </summary>
        /// <returns></returns>
        public bool IsTypeCorrect()
        {
            return this.ValueType.TypeForCurrentValue.IsAssignableFrom(this.CurrentValue.GetType());
        }

        /// <summary>
        /// Parses the value type contained in the <c>source</c> argument and sets it on the <c>destination</c> VarInfo object.
        /// As a result of the execution of this method, the properties <see cref="ValueType">ValueType</see> and <see cref="Size">Size</see> are set.
        /// If source or destination are null, an <see cref="ArgumentNullException">ArgumentNullException</see> is thrown.
        /// </summary>
        /// <param name="source">source string</param>
        /// <param name="destination">destination VarInfo</param>
        public static void ParseValueType(string source, VarInfo destination)
        {
            int num;
            if ((source == null) || (destination == null))
            {
                throw new ArgumentNullException();
            }
            destination.ValueType = VarInfoValueTypes.GetVarInfoValueType(source, out num);
            destination.Size = num;
        }
        /// <summary>Gets/sets the value at run time </summary>
        public virtual object CurrentValue
        {
            get
            {
                return this._currentValue;
            }
            set
            {
                this._currentValue = value;
                if (this.CurrentValueSet != null)
                {
                    this.CurrentValueSet(this);
                }
            }
        }
        /// <summary>Gets/sets the default value (if applicable)</summary>
        public double DefaultValue
        {
            get
            {
                return this._defaultValue;
            }
            set
            {
                this._defaultValue = value;
            }
        }
        /// <summary>Gets/sets the variable description </summary>
        public string Description
        {
            get
            {
                return this._description;
            }
            set
            {
                this._description = value;
            }
        }

        /// <summary>
        /// Gets/sets the VarInfo numerical identifier.
        /// </summary>
        public int Id
        {
            get
            {
                return this.idVar;
            }
            set
            {
                this.idVar = value;
            }
        }
        /// <summary>Gets/sets the maximum value allowed (if applicable)</summary>
        public double MaxValue
        {
            get
            {
                return this._maxValue;
            }
            set
            {
                this._maxValue = value;
            }
        }

        /// <summary>Gets/sets the minimum value allowed (if applicable)</summary>
        public double MinValue
        {
            get
            {
                return this._minValue;
            }
            set
            {
                this._minValue = value;
            }
        }

        /// <summary>Gets/sets the variable name </summary>
        public string Name
        {
            get
            {
                return this._name;
            }
            set
            {
                this._name = value;
            }
        }


        /// <summary>
        /// Gets/sets the size of the array or the list if the <see cref="ValueType">ValueType</see> property requires a size (e.g. arrays). In the other cases it is 1.        
        /// </summary>
        public int Size
        {
            get
            {
                if ((this.ValueType == VarInfoValueTypes.Double) || (this.ValueType == VarInfoValueTypes.Integer))
                {
                    this.sizeVar = 1;
                }
                if ((this.ValueType == VarInfoValueTypes.ListDouble) || (this.ValueType == VarInfoValueTypes.ListInteger))
                {
                    this.sizeVar = -1;
                }
                return this.sizeVar;
            }
            set
            {
                this.sizeVar = value;
            }
        }

        /// <summary>Gets/sets the units of measure of the variable</summary>
        public string Units
        {
            get
            {
                return this._units;
            }
            set
            {
                this._units = value;
            }
        }

        /// <summary>Gets/sets the variable metadata URL</summary>
        public string URL
        {
            get
            {
                return this._URL;
            }
            set
            {
                this._URL = value;
            }
        }

        /// <summary>
        /// Gets/sets the variable value type (from typesafe enumeration <see cref="VarInfoValueTypes">VarInfoValueTypes</see>).
        /// The set can happen only once: once the ValueType is set, it cannot be changed. In case of a second set, an Exception is thrown.
        /// </summary>
        public VarInfoValueTypes ValueType
        {
            get
            {
                return this.valueTypeVar;
            }
            set
            {
                if ((this.valueTypeVar != null) && (this.valueTypeVar != value))
                {
                    throw new Exception("Cannot change a ValueType once set.");
                }
                if ((this.ValueType == VarInfoValueTypes.Double) || (this.ValueType == VarInfoValueTypes.Integer))
                {
                    this.sizeVar = 1;
                }
                this.valueTypeVar = value;
            }
        }

        /// <summary>Gets/sets the variable type (from the enumeration <see cref="CRA.ModelLayer.Core.VarInfo.Type">CRA.ModelLayer.Core.VarInfo.Type</see>)</summary>
        public Type VarType
        {
            get
            {
                return this.localVarType;
            }
            set
            {
                this.localVarType = value;
            }
        }

        /// <summary>
        /// Variable type enumeration.
        /// </summary>
        public enum Type
        {
            /// <summary>A state of the system being modelled</summary>
            STATE = 0,
            /// <summary>A rate of the system being modelled</summary>
            RATE = 1,
            /// <summary>A variable which changes during simulation only 
            /// due to events</summary>
            PARAMETER = 2,
            /// <summary>A variable which changes at each time step but  
            /// it is neither a state nor a rate</summary>
            AUXILIARY = 3,
            /// <summary>Another kind of variable, for example an exogenous variable</summary>
            UNDEFINED = 4
        }
    }
}

