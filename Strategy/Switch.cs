using System;
using System.Collections.Generic;
using System.Linq;
using CRA.ModelLayer.Core;

namespace CRA.ModelLayer.Strategy
{
    /// <summary>
    /// The Switch class representsca choiche in the internal behavior of a model.
    /// A switch has a name and a value. The value must be selected among a set of acceptable values.
    /// A different value will cause a different model behavior and different <see cref="CRA.ModelLayer.Strategy.ModellingOptions">ModellingOptions</see> returned (e.g. a different set of inputs, outputs or parameters). 
    /// </summary>
    public class Switch
    {
        /// <summary>
        /// Value of the unique item of the trivial switch
        /// </summary>
        public const string NOOPTIONSWITCHVALUE = "The only option";

        private readonly Dictionary<string, ModellingOptions> _dict;

        internal Dictionary<string, ModellingOptions> ModelingOptions { get { return _dict; } }

        private string _switchValue;

        ///<summary>
        /// Switch constructor.
        /// By default if the dictionary is empty, it creates a dummy dictionary that returns no parameters.
        /// By default if there is one and only one switch acceptable value it is selected by default and its value is NOOPTIONSWITCHVALUE
        ///</summary>
        ///<param name="switchName">The switch name</param>
        ///<param name="switchDescription">The switch description</param>
        ///<param name="modelingOptions">The dictionary containing the acceptable switch values (keys) nad their corresponding MOdellingOptions objects (values)</param>
        public Switch(string switchName, string switchDescription, Dictionary<string, ModellingOptions> modelingOptions)
        {
            _switchName = switchName;
            _switchDescription = switchDescription;
            _dict = modelingOptions;



            //by default if the dictionary is empty, create a dummy dictionary that returns no parameters
            if (_dict == null || _dict.Keys.Count == 0)
            {
                _dict = new Dictionary<string, ModellingOptions> { { NOOPTIONSWITCHVALUE, new ModellingOptions() } };
            }

            //by default if there is one and only one switch acceptable value it is selected by default and its value is NOOPTIONSWITCHVALUE
            if (_dict.Keys.Count == 1)
            {
                ModellingOptions aux = _dict[_dict.Keys.First()];
                _dict.Remove(_dict.Keys.First());
                _dict.Add(NOOPTIONSWITCHVALUE, aux);
                _switchValue = _dict.Keys.First();
            }
        }

        /// <summary>
        /// Return true if the switch has one and only one acceptable value
        /// </summary>
        public bool IsNoOptionsSwitch { get { return _dict.Keys.Count == 1; } }

        private readonly string _switchName;

        ///<summary>
        /// Returns the switch name
        ///</summary>
        public string SwitchName { get { return _switchName; } }

        private readonly string _switchDescription;

        ///<summary>
        /// Returns the switch description
        ///</summary>
        public string SwitchDescription
        {
            get { return _switchDescription; }
        }

        ///<summary>
        /// Acceptable values for the SwitchValue property
        ///</summary>
        public IEnumerable<string> AcceptableSwitchValues
        {
            get { return _dict.Keys; }
        }

        /// <summary>
        /// Get/set the current value for the switch among the acceptable values returned from the GetAcceptableSwitchValues property
        /// </summary>
        public virtual string SwitchValue
        {
            get { return _switchValue; }
            set
            {
                if (value != null && AcceptableSwitchValues.Contains(value))
                {
                    _switchValue = value;
                }
                else
                {
                    throw new Exception("Wrong switch value. '" + value + "' is not a valid value for switch '" + this._switchName + "'");
                }
            }
        }

        /// <summary>
        /// Return the current set of parameters (depending on the switch value) of the strategy whose the Switch belongs to.
        /// </summary>
        internal virtual IEnumerable<VarInfo> Parameters
        {
            get
            {
                if (string.IsNullOrEmpty(SwitchValue))
                {
                    this.SwitchValue = this.AcceptableSwitchValues.First();
                }
                return _dict[SwitchValue].Parameters;
            }
        }


        /// <summary>
        /// return all the possible parameters (associated with all possible different values of the switch) with repetitions
        /// </summary>
        internal virtual IEnumerable<VarInfo> AllPossibleParameters
        {
            get
            {
                foreach (ModellingOptions c in _dict.Values)
                {
                    foreach (VarInfo v in c.Parameters)
                    {
                        yield return v;
                    }
                }
            }
        }


        /// <summary>
        /// Return the current set of inputs (depending on the switch value) of the strategy whose the Switch belongs to.
        /// </summary>
        internal virtual IEnumerable<PropertyDescription> Inputs
        {
            get
            {
                if (string.IsNullOrEmpty(SwitchValue)) throw new Exception("Switch '" + this._switchName + "' value not set");
                return _dict[SwitchValue].Inputs;
            }

        }

        /// <summary>
        /// Return the current set of outputs (depending on the switch value) of the strategy whose the Switch belongs to.
        /// </summary>
        internal virtual IEnumerable<PropertyDescription> Outputs
        {
            get
            {
                if (string.IsNullOrEmpty(SwitchValue)) throw new Exception("Switch '" + this._switchName + "' value not set");
                return _dict[SwitchValue].Outputs;
            }

        }


        /// <summary>
        /// Return the current set of associated strategies (depending on the switch value) of the strategy whose the Switch belongs to.
        /// </summary>
        internal virtual IEnumerable<string> AssociatedStrategies
        {
            get
            {
                if (string.IsNullOrEmpty(SwitchValue)) throw new Exception("Switch '" + this._switchName + "' value not set");
                return _dict[SwitchValue].AssociatedStrategies;
            }

        }

        /// <summary>
        /// Reset to null without checks. To be used only internallly!
        /// </summary>
        internal void ResetValueToNull()
        {
            this._switchValue = null;
        }
    }
}